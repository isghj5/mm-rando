#!/usr/bin/env bash

user='vagrant'

# Update keyring
pacman -Sy --noconfirm --noprogressbar archlinux-keyring

# Install packages
pacman -Syu --needed --noconfirm --noprogressbar \
base-devel git lib32-gcc-libs python wget

# Setup abs directory
mkdir /abs \
&& chgrp "$user" /abs \
&& chmod g+ws /abs

# Install yay
cd /abs \
&& sudo -u "$user" wget 'https://aur.archlinux.org/cgit/aur.git/snapshot/yay.tar.gz' \
&& sudo -u "$user" tar xzf 'yay.tar.gz' \
&& cd yay \
&& sudo -u "$user" makepkg -s --noconfirm --noprogressbar \
&& pacman -U --needed --noconfirm --noprogressbar yay-*.pkg.tar.zst

# Use yay to install armips via the AUR
sudo -u "$user" yay -S --needed --noconfirm --noprogressbar armips-git

# Clean pacman package cache to clear disk space
pacman -Scc --noconfirm

# Make directory for git repositories
mkdir ~/git && cd ~/git

# Versions to use for MIPS64 toolchain
export BINUTILS_VERSION=binutils-2.33.1
export GCC_VERSION=gcc-9.2.0
export NEWLIB_VERSION=newlib-3.1.0
export GDB_VERSION=gdb-8.3

# Build and install glankk's n64 toolkit
git clone https://github.com/glankk/n64 \
&& cd n64 \
&& mkdir build && cd build \
&& ../install_deps \
&& ../configure --prefix=/opt/n64 --enable-vc \
&& make install-toolchain \
&& make && make install \
&& make install-sys

# Append to PATH in .bashrc
echo 'PATH="$PATH:/opt/n64/bin"' >> "/home/$user/.bashrc"

# Change directory to /vagrant when logging in
echo 'cd /vagrant/assembly' >> "/home/$user/.bashrc"
