import io
import struct

# Default chunk size to use when comparing for diff.
CHUNK_SIZE = 4096

uint32 = struct.Struct('>I')

class AlvChunk(object):
    """ Address-Length-Value chunk. """
    def __init__(self, address, value):
        self._address = address
        self._value = value

    @property
    def address(self):
        """ Get address. """
        return self._address

    @property
    def value(self):
        """ Get value. """
        return self._value

    def __len__(self):
        """ Get length of value. """
        return len(self.value)

class AlvIoChunk(AlvChunk):
    """ Address-Length-Value chunk using IO type for values. """
    def __len__(self):
        """ Efficiently get length of written bytes. """
        return self.value.getbuffer().nbytes

class AlvBuilder(object):
    """ Builder for `AlvChunk` chunks. """
    def __init__(self, stype=uint32):
        self._chunks = []
        self._stype = stype

    def append(self, addr, value):
        """ Append value at specified address. """
        if len(self._chunks) > 0 and self.current_addr == addr:
            # Pack value into bytes and write to most recent chunk.
            packed = self._stype.pack(value)
            self._chunks[-1].value.write(packed)
        else:
            # Pack value into bytes and append as new chunk.
            chunk = AlvIoChunk(addr, io.BytesIO())
            packed = self._stype.pack(value)
            chunk.value.write(packed)
            self._chunks.append(chunk)

    def build(self, addr_translate=None):
        """ Build all chunks into single buffer. """
        builder = io.BytesIO()
        for chunk in self._chunks:
            valuebytes = chunk.value.getvalue()
            # Translate address if specified.
            address = chunk.address
            if addr_translate is not None:
                address = addr_translate(address)
            # Write 32-bit address, 32-bit length, and variable-size data.
            builder.write(uint32.pack(address))
            builder.write(uint32.pack(len(valuebytes)))
            builder.write(valuebytes)
        return builder.getvalue()

    @property
    def current_addr(self):
        """ Get current address, using address and length of most recent chunk. """
        return self._chunks[-1].address + self._chunks[-1].value.getbuffer().nbytes

def unequal_chunks(file1, file2, chunk_size=CHUNK_SIZE, stype=uint32):
    """ Returns a generator for non-matching chunks between two files. """
    addr = 0
    while True:
        chunk1 = file1.read(chunk_size)
        chunk2 = file2.read(chunk_size)
        if not chunk1:
            return
        if chunk1 != chunk2:
            words1 = [x[0] for x in stype.iter_unpack(chunk1)]
            words2 = [x[0] for x in stype.iter_unpack(chunk2)]
            yield (addr, words1, words2)
        addr += chunk_size

def diff(base, comp, chunk_size=CHUNK_SIZE, stype=uint32):
    """ Diff two files and return the `AlvBuilder`. """
    builder = AlvBuilder()
    for (addr, base_words, comp_words) in unequal_chunks(base, comp, chunk_size=chunk_size, stype=stype):
        for j in range(len(comp_words)):
            if comp_words[j] != base_words[j]:
                word_addr = addr + (stype.size * j)
                builder.append(word_addr, comp_words[j])
    return builder
