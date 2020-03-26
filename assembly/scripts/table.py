import ctypes

class TableEntry(ctypes.BigEndianStructure):
    """ Table entry structure. """
    _fields_ = (
        ('virt_start', ctypes.c_uint),
        ('virt_end', ctypes.c_uint),
        ('phys_start', ctypes.c_uint),
        ('phys_end', ctypes.c_uint),
    )

    def empty(self):
        """ Whether or not this entry is "empty." """
        return self.virt_start == self.virt_end == self.phys_start == self.phys_end == 0

    def does_not_exist(self):
        """ Whether or not this is a "DoesNotExist" file entry. """
        return self.phys_end == 0xFFFFFFFF

    def is_decompressed(self):
        """ Whether or not this data is decompressed. """
        return self.phys_end == 0

    def decompressed_size(self):
        """ Get size of decompressed data. """
        return self.virt_end - self.virt_start

    def read(self, file):
        """ Read a TableEntry from a file. """
        self.receiveSome(file.read(ctypes.sizeof(self)))
        return self

    def receiveSome(self, data):
        """ Parse from bytes. """
        fit = min(len(data), ctypes.sizeof(self))
        ctypes.memmove(ctypes.addressof(self), data, fit)

class Table(object):
    """ Parsed file table with entries. """
    def __init__(self, offset, entries):
        self._offset = offset
        self._entries = entries

    @staticmethod
    def read(file, offset):
        """ Read a Table from a file and offset. """
        entries = tuple(read_table(file, offset))
        return Table(offset, entries)

    def resolve(self, physical):
        """ Resolve a physical address to a virtual. """
        for entry in self._entries:
            if entry.empty() or entry.does_not_exist():
                continue
            if entry.is_decompressed():
                size = entry.decompressed_size()
                end = entry.phys_start + size
                if entry.phys_start <= physical < end:
                    return (physical - entry.phys_start) + entry.virt_start
            else:
                raise Exception('Must be a fully decompressed ROM! {:08X}'.format(entry.virt_start))

def read_table(file, offset):
    """ Generator for reading all table entries in a file. """
    table_start = offset
    table_entry = None
    while True:
        file.seek(offset)
        entry = TableEntry().read(file)

        # Check if found table entry
        if entry.phys_start == table_start:
            table_entry = entry

        # Check if at end of table
        if table_entry is not None:
            end = table_entry.phys_start + table_entry.decompressed_size()
            if offset >= end:
                break

        offset += ctypes.sizeof(entry)
        yield entry
