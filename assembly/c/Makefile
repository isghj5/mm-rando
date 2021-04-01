CC = mips64-gcc
LD = mips64-ld
OBJDUMP = mips64-objdump

CFLAGS = -march=vr4300 -mtune=vr4300 -mabi=32 -mno-gpopt -mdivide-breaks \
	-O1 -fno-reorder-blocks -mexplicit-relocs

# Compile with F3DEX2 support.
CFLAGS += -DF3DEX_GBI_2 -I../include

OBJECTS = $(patsubst %.c,%.o,$(sort $(wildcard *.c)))

OUTDIR = ../build

.PHONY: all clean bundle symbols

all: clean bundle symbols

%.o: %.c
	$(CC) -c $< $(CFLAGS)

bundle: mkdir-outdir $(OBJECTS)
	$(LD) -o $(OUTDIR)/bundle.o -i -L. $(patsubst %.o,-l:%.o,$(OBJECTS))

symbols: mkdir-outdir bundle
	$(OBJDUMP) -t $(OUTDIR)/bundle.o | tr -d '\015' > $(OUTDIR)/c_symbols.txt

mkdir-outdir:
	mkdir -p "$(OUTDIR)"

clean:
	rm -f *.o
