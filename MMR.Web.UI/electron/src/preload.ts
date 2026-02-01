import * as electron from 'electron';
import * as remote from '@electron/remote';
import * as os from "os";
import * as fs from 'fs';
import * as path from 'path';

import * as post from 'post-robot';

const commander = remote.getGlobal("commandLineArgs");

if ("criticalBootError" in commander)
{
   alert(commander.criticalBootError);
   remote.app.quit();
}

var testMode = commander.release || remote.app.isPackaged ? false : true;
console.log("Test Mode:", testMode);

var platform = os.platform();
console.log("Platform:", platform);

var appSourcePath = path.normalize(remote.app.getAppPath() + "/");


//Enable API in client window
electron.webFrame.executeJavaScript('window.electronAvailable = true;');
electron.webFrame.executeJavaScript('window.apiTestMode = ' + testMode + ';');
electron.webFrame.executeJavaScript('window.apiPlatform = "' + platform + '";');

//ELECTRON EVENTS
remote.getCurrentWindow().on('maximize', () => {
  post.send(window, 'window-maximized', true);
});
remote.getCurrentWindow().on('enter-full-screen', () => { //macOS exclusive
  post.send(window, 'window-maximized', true);
});
remote.getCurrentWindow().on('enter-html-full-screen', () => {
  post.send(window, 'window-maximized', true);
});

remote.getCurrentWindow().on('unmaximize', () => {
  post.send(window, 'window-maximized', false);
});
remote.getCurrentWindow().on('leave-full-screen', () => {
  post.send(window, 'window-maximized', false);
});
remote.getCurrentWindow().on('leave-html-full-screen', () => {
  post.send(window, 'window-maximized', false);
});


//FUNCTIONS
function dumpSettingsToFile(settingsObj) {
  settingsObj["check_version"] = true; //Not MMR specific

  if (!fs.existsSync(appSourcePath + "userData"))
    fs.mkdirSync(appSourcePath + "userData");

  fs.writeFileSync(appSourcePath + "userData/settings.sav", JSON.stringify(settingsObj, null, 4));
}

function dumpPresetsToFile(presetsString: string) {

  if (!fs.existsSync(appSourcePath + "userData"))
    fs.mkdirSync(appSourcePath + "userData");

  fs.writeFileSync(appSourcePath + "userData/presets.sav", presetsString);
}

function readSettingsFromFile() {

  let path = appSourcePath + "userData/settings.sav";

  if (fs.existsSync(path))
    return fs.readFileSync(path, 'utf8');
  else
    return false;
}

//POST ROBOT
post.on('getCurrentSourceVersion', function (event) {
  type VersionData = {
    baseVersion: string;
    supplementaryVersion: number;
    fullVersion: string;
    branchUrl: string;
  };

  let data: VersionData = { baseVersion : "1.0.0", supplementaryVersion : 0, fullVersion : "1.0.0-0", branchUrl : "" }
  return data; 
});

post.on('getGeneratorGUISettings', function (event) {

  return electron.ipcRenderer.sendSync('getGeneratorGUISettings');
});

post.on('getGeneratorGUILastUserSettings', function (event) {

  return readSettingsFromFile();
});

post.on('copyToClipboard', function (event) {

  let data = event.data;

  if (!data || typeof (data) != "object" || Object.keys(data).length != 1 || !data["content"] || typeof (data["content"]) != "string" || data["content"].length < 1)
    return false;

  remote.clipboard.writeText(data.content);

  return true;
});

post.on('browseForFile', function (event) {

  let data = event.data;

  if (!data || typeof (data) != "object" || Object.keys(data).length != 1 || !data["fileTypes"] || typeof (data["fileTypes"]) != "object")
    return false;

  return remote.dialog.showOpenDialogSync({ filters: data.fileTypes, properties: ["openFile", "treatPackageAsDirectory"]});
});


post.on('browseForDirectory', function (event) {
  return remote.dialog.showOpenDialogSync({ properties: ["openDirectory", "createDirectory", "treatPackageAsDirectory"] });
});

post.on('createAndOpenPath', function (event) {

  let data = event.data;

  //Use python dir if not specified otherwise
  if (!data || typeof (data) != "string" || data.length < 1) {
    data = appSourcePath;
  }
  else {
    if (!path.isAbsolute(data))
      data = appSourcePath + data;
  }

  if (fs.existsSync(data)) {
    remote.shell.openPath(data);
    return true;
  }
  else {
    fs.mkdirSync(data);

    remote.shell.openPath(data).then(res => {
      post.send(window, 'createAndOpenPathResult', res);
    });

    return false;
  }
});

post.on('window-minimize', function (event) {
  remote.getCurrentWindow().minimize();
});

post.on('window-maximize', function (event) {

  let currentWindow = remote.getCurrentWindow();

  if (currentWindow.isMaximized()) {
    currentWindow.unmaximize();
  }
  else {
    currentWindow.maximize();
  }
});

post.on('window-is-maximized', function (event) {
  return remote.getCurrentWindow().isMaximized();
});

post.on('window-close', function (event) {

  //Only close the window on macOS, on every other OS exit immediately
  if (os.platform() == "darwin") {
    remote.getCurrentWindow().close();
  }
  else {
    remote.app.quit();
  }
});

post.on('saveCurrentSettingsToFile', function (event) {

  let data = event.data;

  if (!data || typeof (data) != "object" || Object.keys(data).length < 1)
    return false;

  //Write settings obj to settings.sav
  dumpSettingsToFile(data);
});

//Not MMR specific
post.on('convertSettingsToString', function (event) {

  let data = event.data;

  if (!data || typeof (data) != "object" || Object.keys(data).length < 1)
    return false;

  //Write settings obj to settings.sav
  dumpSettingsToFile(data);

  post.send(window, 'convertSettingsToStringError', "not_supported");
  return false;
});

//Not MMR specific
post.on('updateDynamicSetting', function (event) {
  let data = event.data;

  if (!data || typeof (data) != "string" || data.length < 1)
    return false;

  //console.log("get settings from string", data);
  post.send(window, 'updateDynamicSettingError', "not_supported");
  return false;
})

//Not MMR specific
post.on('convertStringToSettings', function (event) {

  let data = event.data;

  if (!data || typeof (data) != "string" || data.length < 1)
    return false;

  //console.log("get settings from string", data);

  post.send(window, 'convertStringToSettingsError', "not_supported");
  return false;
});

post.on('saveCurrentPresetsToFile', function (event) {

  let data = event.data;

  if (!data || typeof (data) != "string" || data.length < 1)
    return false;

  //Write file contents to presets.sav
  dumpPresetsToFile(data);

  //console.log("presets saved to .sav");
});

//Not MMR specific
post.on('generateSeed', function (event) {

  let data = event.data;

  if (!data || typeof (data) != "object" || Object.keys(data).length != 2 || !("settingsFile" in data) || !("staticSeed" in data))
    return false;

  let settingsFile = data.settingsFile;

  if (!settingsFile || typeof (settingsFile) != "object" || Object.keys(settingsFile).length < 1)
    return false;

  //Write settings obj to settings.sav
  dumpSettingsToFile(settingsFile);

  //console.log("generate seed with settings:", data);

  post.send(window, 'generateSeedCancelled');
  return false;
});

//Not MMR specific
post.on('cancelGenerateSeed', function (event) {
  return true;
});

//STARTUP
//Test if we are in the proper path, else exit
electron.webFrame.executeJavaScript('window.apiPythonSourceFound = true;'); //Not MMR specific
