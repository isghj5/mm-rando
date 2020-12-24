#!/usr/bin/env/python3

import argparse
import gzip
import struct
import subprocess

import alv
from table import Table

def get_parser():
    """ Get the argument parser. """
    def auto_int(x):
        return int(x, 0)
    parser = argparse.ArgumentParser()
    parser.add_argument('--table-offset', type=auto_int, help='Offset of file table (use with --virtual)')
    parser.add_argument('--virtual', action='store_true', help='Get virtual addresses instead of physical')
    parser.add_argument('BASE_FILE')
    parser.add_argument('COMPARISON_FILE')
    parser.add_argument('OUTPUT_FILE')
    return parser

def main():
    args = get_parser().parse_args()
    create_diff(args.base_file, args.comparison_file, args.output_file,
        offset=args.table_offset, virtual=args.virtual)

def create_diff(base_path, compare_path, output_path, compress=True, virtual=False, offset=0):
    data = bytes()
    with open(base_path, 'rb') as base_f, open(compare_path, 'rb') as comp_f:
        # Read file table if specified
        table = None
        if virtual:
            table = Table.read(comp_f, offset)
            comp_f.seek(0)
        # Perform ALV diff which returns builder.
        builder = alv.diff(base_f, comp_f)
        # Build and translate addresses to virtual.
        data = builder.build(addr_translate=table.resolve)
    if compress:
        # Write to compressed file.
        with gzip.GzipFile(filename=output_path, mode='wb', mtime=0) as out_f:
            out_f.write(data)
    else:
        # Write to non-compressed file.
        with open(output_path, 'wb') as out_f:
            out_f.write(data)

if __name__ == '__main__':
    main()
