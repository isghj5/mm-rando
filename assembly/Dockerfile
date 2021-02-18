FROM archlinux

RUN pacman -Syu --noconfirm

	# Update keyring
RUN pacman -Syu --noconfirm haveged \
#    && systemctl start haveged \
	&& systemctl enable haveged \
	&& rm -fr /etc/pacman.d/gnupg \
	&& pacman-key --init \
	&& pacman-key --populate archlinux

RUN pacman -Sy --noconfirm --noprogressbar archlinux-keyring

	# Install packages
RUN pacman -Syu --needed --noconfirm --noprogressbar \
	base-devel git lib32-gcc-libs python wget

ARG user=docker

RUN useradd -m $user -p $user
	
RUN echo "$user ALL=(ALL) NOPASSWD: ALL" >> /etc/sudoers

	# Setup abs directory
RUN mkdir /abs \
	&& chgrp "$user" /abs \
	&& chmod g+ws /abs

	# Install yay
RUN cd /abs \
	&& sudo -u "$user" wget 'https://aur.archlinux.org/cgit/aur.git/snapshot/yay.tar.gz' \
	&& sudo -u "$user" tar xzf 'yay.tar.gz' \
	&& cd yay \
	&& sudo -u "$user" makepkg -s --noconfirm --noprogressbar \
	&& pacman -U --needed --noconfirm --noprogressbar yay-*.pkg.tar.zst

	# Use yay to install armips via the AUR
RUN sudo -u "$user" yay -S --needed --noconfirm --noprogressbar armips-git

	# Clean pacman package cache to clear disk space
RUN pacman -Scc --noconfirm

	# Make directory for git repositories
RUN mkdir ~/git && cd ~/git

	# Versions to use for MIPS64 toolchain
RUN export BINUTILS_VERSION=binutils-2.33.1
RUN export GCC_VERSION=gcc-9.2.0
RUN export NEWLIB_VERSION=newlib-3.1.0
RUN export GDB_VERSION=gdb-8.3

	# Build and install glankk's n64 toolkit
RUN git clone https://github.com/glankk/n64 \
	&& cd n64 \
	&& mkdir build && cd build \
	&& ../install_deps \
	&& ../configure --prefix=/opt/n64 --enable-vc \
	&& make install-toolchain \
	&& make && make install \
	&& make install-sys

	# Append to PATH in .bashrc
RUN echo 'PATH="$PATH:/opt/n64/bin"' >> "/home/$user/.bashrc"

	# Change directory to /$user when logging in
RUN echo "cd /$user/assembly" >> "/home/$user/.bashrc"

USER $user