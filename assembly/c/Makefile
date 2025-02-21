CC = mips64-gcc
LD = mips64-ld
OBJDUMP = mips64-objdump

CFLAGS = -march=vr4300 -mtune=vr4300 -mabi=32 -mno-gpopt -mdivide-breaks \
	-O1 -fno-reorder-blocks -mexplicit-relocs

# Compile with F3DEX2 support.
CFLAGS += -DF3DEX_GBI_2 -I../include


SOURCES := $(sort $(wildcard *.c))
SOURCES := $(filter-out Kaleido_%.c Kaleido_%.s,$(SOURCES))
OBJECTS := $(patsubst %.c,%.o,$(SOURCES))

KALEIDOSRCS := $(sort $(wildcard Kaleido_*.c))
KALEIDOSRCS := $(filter Kaleido_%.c Kaleido_%.s,$(KALEIDOSRCS))
KALEIDOOBJS := $(patsubst %.c,%.o,$(KALEIDOSRCS))

OUTDIR = ../build

.PHONY: all clean bundle kaleido symbols

all: clean bundle kaleido symbols

$(OBJECTS): %.o: %.c
	$(CC) -c $< -o $@ $(CFLAGS)

$(KALEIDOOBJS): %.o: %.c
	$(CC) -c $< -o $@ $(CFLAGS)


$(OUTDIR)/bundle.o: $(OBJECTS)
	$(LD) -o $@ -i -L. $^

$(OUTDIR)/kaleido.o: $(KALEIDOOBJS)
	$(LD) -o $@ -i -L. $^


bundle: mkdir-outdir $(OUTDIR)/bundle.o

kaleido: mkdir-outdir $(OUTDIR)/kaleido.o

symbols: mkdir-outdir bundle
	$(OBJDUMP) -t $(OUTDIR)/bundle.o | tr -d '\015' > $(OUTDIR)/c_symbols.txt

mkdir-outdir:
	mkdir -p "$(OUTDIR)"

clean:
	rm -f *.o
