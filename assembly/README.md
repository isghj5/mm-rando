# MM Randomizer Assembly

This directory contains the Asm & C source code used to build a binary blob which is allocated in memory, and the
patches required to make use of the created binary blob.

## Building (Docker)

Currently [Docker] is used for building. It installs all required software onto a linux base image, and uses the
provided python scripts to build the following files:

- `rom_patch.bin`
- `symbols.json`

To build the binary blob, run the following command:

```sh
docker-compose up
```

This command will do the following:
- Provision the Docker image (this only needs to be done once and may take awhile).
- Build the binary blob files into `build/generated`.
- Copy these files into `MMR.Randomizer/Resources/asm`, where they are usable by the randomizer.

[Docker]:https://www.docker.com/
