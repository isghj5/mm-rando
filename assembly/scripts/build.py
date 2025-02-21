#!/usr/bin/env python3

import argparse
import collections
import json
import os
import re
from subprocess import check_call as call

from rom_diff import create_diff
from ntype import BigStream
from crc import calculate_crc

Offsets = collections.namedtuple('Offsets', ('table', 'file_start', 'ram_start', 'ram_end', 'kaleido_file_start', 'kaleidoram_start'))

# Hardcoded fields per target for added data:
# (Table Start, File Address, RAM Start, RAM End)
# OOT offsets for kaleido are placeholder
OOT_OFFSETS=Offsets(0x00007400, 0x03480000, 0x80400000, 0x80420000, 0x03480000, 0x80400000)
MM_OFFSETS=Offsets(0x0001A500, 0x03800000, 0x80720000, 0x80780000, 0x03880000, 0x80720000 - 0x0179C0)

def build_data_symbols(symbols, offsets):
    data_symbols = {}
    for (name, sym) in symbols.items():
        if sym['type'] == 'data':
            addr = int(sym['address'], 16)
            if offsets.kaleidoram_start <= addr < offsets.ram_start:
                addr = addr - offsets.kaleidoram_start + offsets.kaleido_file_start
            elif offsets.ram_start <= addr < offsets.ram_end:
                addr = addr - offsets.ram_start + offsets.file_start
            else:
                continue
            data_symbols[name] = '{0:08X}'.format(addr)
    return data_symbols

def dump_json_to_file(data, path):
    with open(path, 'w') as f:
        json.dump(data, f, indent=4, sort_keys=True)
        f.write('\n')

def fixup_asm_symbols(path):
    with open(path, 'rb') as f:
        asm_symbols_content = f.read()
    asm_symbols_content = asm_symbols_content.replace(b'\r\n', b'\n')
    asm_symbols_content = asm_symbols_content.replace(b'\x1A', b'')
    with open(path, 'wb') as f:
        f.write(asm_symbols_content)

def get_offsets_by_target(target):
    if target == 'mm':
        return MM_OFFSETS
    elif target == 'oot':
        return OOT_OFFSETS

def parse_asm_symbols(path, c_symbols):
    symbols = {}
    with open(path, 'r') as f:
        for line in f:
            parts = line.strip().split(' ')
            if len(parts) < 2:
                continue
            address, sym_name = parts
            if address[0] != '8':
                continue
            if sym_name[0] in ['.', '@']:
                continue
            sym_type = c_symbols.get(sym_name) or ('data' if sym_name.isupper() else 'code')
            symbols[sym_name] = {
                'type': sym_type,
                'address': address,
            }
    return symbols

def parse_c_symbols(path):
    c_sym_types = {}
    with open(path, 'r') as f:
        for line in f:
            m = re.match(r'''
                    ^
                    [0-9a-fA-F]+
                    .*
                    \.
                    ([^\s]+)
                    \s+
                    [0-9a-fA-F]+
                    \s+
                    ([^.$][^\s]+)
                    \s+$
                ''', line, re.VERBOSE)
            if m:
                sym_type = m.group(1)
                name = m.group(2)
                c_sym_types[name] = 'code' if sym_type == 'text' else 'data'
    return c_sym_types

def update_crc(path):
    with open(path, 'r+b') as stream:
        buffer = bytearray(stream.read(0x101000))
        crc = calculate_crc(BigStream(buffer))
        stream.seek(0x10)
        stream.write(bytearray(crc))

def write_pj64_symbols(f, symbols):
    key = lambda pair: pair[1]['address']
    for sym_name, sym in sorted(symbols.items(), key=key):
        f.write('{0},{1},{2}\n'.format(sym['address'], sym['type'], sym_name))

def get_parser():
    parser = argparse.ArgumentParser()
    parser.add_argument('-j', '--jobs', action='store', const=True, default=None, nargs='?', type=int, help='Number of jobs to run simultaneously when compiling C code')
    parser.add_argument('-t', '--target', default='mm', help='Target directory name')
    parser.add_argument('--pj64sym', help="Output path for PJ64 debugging symbols")
    parser.add_argument('--compile-c', action='store_true', help="Recompile C modules")
    parser.add_argument('--virtual', action='store_true', help='Use virtual addresses in diff')
    return parser

def get_jobs_arguments(value):
    if value is True:
        return ['-j']
    elif value is not None:
        return ['-j', str(value)]
    else:
        return []

def main():
    args = get_parser().parse_args()
    pj64_sym_path = args.pj64sym
    compile_c = args.compile_c

    script_dir = os.path.dirname(os.path.realpath(__file__))
    run_dir = os.path.join(script_dir, '..')
    os.chdir(run_dir)

    # Target relative path
    relpath = os.path.join('.')

    # Compile code

    os.environ['PATH'] = script_dir + os.pathsep + os.environ['PATH']

    if compile_c:
        os.chdir(os.path.join(run_dir, relpath, 'c'))
        if args.jobs is not None:
            # Call `make` with `-j` or `-j <number>` to run multiple jobs simultaneously.
            call(['make', '--always-make'] + get_jobs_arguments(args.jobs))
        else:
            call(['make'])

    os.chdir(os.path.join(run_dir, relpath, 'src'))
    call(['armips', '-sym2', '../build/asm_symbols.txt', 'Build.asm'])
    os.chdir(run_dir)

    fixup_asm_symbols(os.path.join(relpath, 'build/asm_symbols.txt'))

    # ...
    c_sym_types = parse_c_symbols(os.path.join(relpath, 'build/c_symbols.txt'))

    # ...
    symbols = parse_asm_symbols(os.path.join(relpath, 'build/asm_symbols.txt'), c_sym_types)

    # Output symbols

    os.chdir(run_dir)

    # Create subdirectory for generated files if it doesn't exist.
    generated_path = os.path.join(relpath, 'build/generated')
    os.makedirs(generated_path, exist_ok=True)

    # Dump symbols as JSON
    offsets = get_offsets_by_target(args.target)
    data_symbols = build_data_symbols(symbols, offsets)
    dump_json_to_file(data_symbols, os.path.join(generated_path, 'symbols.json'))

    if pj64_sym_path:
        pj64_sym_path = os.path.realpath(pj64_sym_path)
        with open(pj64_sym_path, 'w') as f:
            write_pj64_symbols(f, symbols)

    update_crc(os.path.join(relpath, 'roms/patched.z64'))

    # Diff ROMs
    create_diff(
        os.path.join(relpath, 'roms/base.z64'),
        os.path.join(relpath, 'roms/patched.z64'),
        os.path.join(generated_path, 'rom_patch.bin'),
        virtual=args.virtual,
        offset=offsets.table,
    )

if __name__ == '__main__':
    main()
