#!/usr/bin/env python

import argparse
import struct

def unpack_ia(value):
    """ Unpack 16-bit IA value to RGBA values. """
    intensity = value >> 8
    alpha = value & 0xFF
    return (intensity, intensity, intensity, alpha)

def pack32(values):
    """ Pack RGBA values to a 32-bit 8/8/8/8 integer."""
    r, g, b, a = values
    return ((r & 0xFF) << 24) | ((g & 0xFF) << 16) | ((b & 0xFF) << 8) | (a & 0xFF)

def get_parser():
    """ Get the argument parser. """
    parser = argparse.ArgumentParser(description='16-bit IA to 32-bit RGBA texture converter.')
    parser.add_argument('input', help='IA (16-bit) texture input file')
    parser.add_argument('output', help='RGBA (32-bit) texture output file')
    return parser

def main():
    """ Main function. """
    args = get_parser().parse_args()

    with open(args.input, 'rb') as infile:
        with open(args.output, 'wb') as outfile:
            while True:
                indata = infile.read(2)
                if len(indata) == 0:
                    break
                value = struct.unpack('>H', indata)[0]
                result = pack32(unpack_ia(value))
                outdata = struct.pack('>I', result)
                outfile.write(outdata)

if __name__ == '__main__':
    main()
