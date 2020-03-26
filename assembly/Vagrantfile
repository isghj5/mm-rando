Vagrant.configure("2") do |config|
  config.vm.box = 'archlinux/archlinux'
  config.vm.provision 'shell', path: 'vagrant/provision.sh'
  config.vm.synced_folder '..', '/vagrant'
end
