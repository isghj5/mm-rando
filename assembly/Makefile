BUILD_PY := scripts/build.py
PYTHON   := python

# Directories used for copying built Asm patch files to MMR Resources directory.
GEN_DIR  := build/generated
RES_DIR  := ../MMR.Randomizer/Resources/asm

# Build for Majora's Mask by default
all: mm

# Build for "mm" target (Majora's Mask)
mm: symbols
	$(PYTHON) $(BUILD_PY) -j -t mm --compile-c --virtual

copy: mm copy-only

copy-only:
	cp $(GEN_DIR)/rom_patch.bin $(RES_DIR)
	cp $(GEN_DIR)/symbols.json  $(RES_DIR)

symbols:
	$(PYTHON) scripts/symbols.py -o src/Symbols.asm symbols.csv
