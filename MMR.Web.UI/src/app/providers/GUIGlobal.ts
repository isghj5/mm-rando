import { Injectable, HostBinding, EventEmitter, Output, Directive, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ProgressWindowComponent } from '../pages/generator/progressWindow/progressWindow.component';

import * as post from 'post-robot';
import {GuiEvent} from './GuiEvent';

@Directive()
@Injectable()
export class GUIGlobal implements OnDestroy {

  //Globals for GUI HTML
  public generator_tabsVisibilityMap: Object = {};
  public generator_settingsVisibilityMap: Object = {};

  public generator_settingsMap: Object = {};
  public generator_customColorMap: Object = {};

  public generator_presets: Object = {};

  globalVars: Map<string, any>;
  electronEvents: Array<any> = [];

  @HostBinding('class.indigo-pink') materialStyleIndigo: boolean = true;

  @Output() globalEmitter: EventEmitter<GuiEvent> = new EventEmitter();

  constructor(private http: HttpClient) {
    this.globalVars = new Map<string, any>([
      ["appReady", false],
      ["appType", null],
      ["electronAvailable", false],
      ["webSourceVersion", ""],
      ["webIsMasterVersion", false],
      ["webSupportDirectoryPicker", false],
      ["generatorSettingsArray", []],
      ["generatorSettingsObj", {}],
      ["generatorCosmeticsArray", []],
      ["generatorCosmeticsObj", {}],
      ["generatorGoalDistros", []]
    ]);
  }

  globalInit(appType: string) {

    if (!appType)
      appType = "generator"; //Generator is the default

    console.log("APP Type:", appType);
    this.setGlobalVar("appType", appType);

    if ((<any>window).electronAvailable) { //Electron/Offline mode

      if ((<any>window).apiPythonSourceFound) {
        this.setGlobalVar("electronAvailable", true);
        console.log("Electron API available");
        console.log("Running on OS:", (<any>window).apiPlatform);

        this.createElectronEvents();
        this.electronInit();
      }
      else {
        console.error("Improper setup, exit GUI");
      }
    }
    else { //Online/Web mode
      console.log("Electron API not available, assume web mode");

      // In production, pythonSourceVersion is set by the embedding web page
      const webVersion = (<any>window).pythonSourceVersion || "dev_local";
      this.setGlobalVar("webSourceVersion", webVersion);

      if ((<any>window).pythonSourceIsMasterVersion)
        this.setGlobalVar("webIsMasterVersion", (<any>window).pythonSourceIsMasterVersion);

      console.log("Web version: " + webVersion + " ; master version:", this.getGlobalVar("webIsMasterVersion"));

      let dirPickerSupport = this.testDirectoryPickerFeatureWeb();
      this.setGlobalVar("webSupportDirectoryPicker", dirPickerSupport);

      console.log("webkitdirectory support:", dirPickerSupport);

      this.webInit();
    }
  }

  ngOnDestroy() {
    if ((<any>window).electronAvailable) {
      this.destroyElectronEvents();
    }
  }

  createElectronEvents() { //Electron only

    var self = this;

    let maximizedEvent = post.on('window-maximized', function (event) {
      let res = event.data;

      if (res == true)
        self.globalEmitter.emit({ name: "window_maximized" });
      else
        self.globalEmitter.emit({ name: "window_unmaximized" });
    });

    this.electronEvents.push(maximizedEvent);
  }

  createWebEvents() { //Web only

    var self = this;

    //Switch the GUI theme
    (<any>window).addEventListener('external_switch_theme', function (event) {

      let detail = event.detail;

      self.generator_settingsMap["theme"] = detail.theme; //Ensure theme is set
      self.globalEmitter.emit({ name: "theme_switch", message: detail.theme });

    }, false);

    //Allow external updates of any setting, then refresh GUI
    (<any>window).addEventListener('external_update_setting', function (event) {

      let detail = event.detail;

      if (detail && Array.isArray(detail)) { //Batch

        for (let setting of detail) {
          self.generator_settingsMap[setting.name] = setting.value;
        }
      }
      else if (detail && detail.name && "value" in detail) { //Single
        self.generator_settingsMap[detail.name] = detail.value;
      }

      self.globalEmitter.emit({ name: "refresh_gui" });

    }, false);

    //Legacy: Update settings entry for cached file and then refresh GUI
    (<any>window).addEventListener('emscripten_cache_file_found', function (event) {

      let detail = event.detail;

      if (detail) {

        if (detail.name == "ROM")
          self.generator_settingsMap["OutputSettings.InputROMFilename"] = "<using cached ROM>";
        else if (detail.name == "WAD")
          self.generator_settingsMap["Web.wad_file"] = "<using cached WAD>";
        else if (detail.name == "COMMONKEY")
          self.generator_settingsMap["Web.common_key_file"] = "<using cached common key>";

        self.globalEmitter.emit({ name: "refresh_gui" });
      }

    }, false);
  }

  destroyElectronEvents() {
    this.electronEvents.forEach(postRobotEvent => {
      postRobotEvent.cancel();
    });
  }

  electronInit() {
    this.electronLoadGeneratorGUISettings().then(() => {
      this.setGlobalVar("appReady", true);
      this.globalEmitter.emit({ name: "init_finished" });
    }).catch(err => {
      console.error("exit due error:", err);
    });
  }

  webInit() {
    this.webLoadGeneratorGUISettings().then(() => {
      this.setGlobalVar("appReady", true);
      this.globalEmitter.emit({ name: "init_finished" });
    }).catch(err => {
      console.error("exit due error:", err);
    });
  }

  setGlobalVar(key: string, value: any) {
    if (this.globalVars.has(key)) {
      this.globalVars.set(key, value);
    }
  }

  getGlobalVar(key: string) {

    if (this.globalVars.has(key))
      return this.globalVars.get(key);

    return "error";
  }

  copyToClipboard(content: string) {

    if (this.getGlobalVar('electronAvailable')) { //Electron
      post.send(window, 'copyToClipboard', { content: content }).then(event => {
        console.log('copied to clipboard');
      }).catch(err => {
        console.error(err);
      });
    }
    else { //Web
      let selBox = document.createElement('textarea');
      selBox.style.position = 'fixed';
      selBox.style.left = '0';
      selBox.style.top = '0';
      selBox.style.opacity = '0';
      selBox.value = content;

      document.body.appendChild(selBox);
      selBox.focus();
      selBox.select();
      document.execCommand('copy');

      document.body.removeChild(selBox);
    }
  }

  async browseForFile(fileTypes: any) { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      throw Error("electron_not_available");

    let event = await post.send(window, 'browseForFile', { fileTypes: fileTypes });
    let res = event.data;

    if (!res || res.length != 1 || !res[0] || res[0].length < 1)
      throw Error("dialog_cancelled");

    return res[0];
  }

  async browseForDirectory() { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      throw Error("electron_not_available");

    let event = await post.send(window, 'browseForDirectory');
    let res = event.data;

    if (!res || res.length != 1 || !res[0] || res[0].length < 1)
      throw Error("dialog_cancelled");

    return res[0];
  }

  async createAndOpenPath(path: string) { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      throw Error("electron_not_available");

    let event = null;

    try {
      event = await post.send(window, 'createAndOpenPath', path);
    } catch (ex) {
      console.error(ex);
    }

    if (event == null)
      throw Error("The specified output directory does not exist!");

    //If folder already existed, we get a quick sync exit, otherwise wait for async callback that the folder was created and opened
    let status = event.data;

    if (status)
      return true;

    let response = await new Promise(function (resolve, reject) {

      var listenerResult = post.once('createAndOpenPathResult', function (res) {

        listenerResult.cancel();

        let data = res.data;
        resolve(data);
      });
    });

    if (response === "")
      return true;
    else
      throw Error("path_not_opened");
  }

  minimizeWindow() { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      return;

    post.send(window, 'window-minimize').then(() => {
      console.log('window minimized');
    }).catch(err => {
      console.error(err);
    });
  }

  maximizeWindow() { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      return;

    post.send(window, 'window-maximize').then(() => {
      console.log('window maximize state updated');
    }).catch(err => {
      console.error(err);
    });
  }

  async isWindowMaximized() { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      throw Error("electron_not_available");

    try {
      let event = await post.send(window, 'window-is-maximized');
      let res = event.data;

      //console.log("window maximized state:", res);
      return res;
    }
    catch (err) {
      console.error(err);
      throw Error(err);
    }
  }

  closeWindow() { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      return;

    this.saveCurrentSettingsToFile();

    setTimeout(() => {

      post.send(window, 'window-close').then(() => {
        console.log('window closed');
      }).catch(err => {
        console.error(err);
      });
    });
  }

  async electronLoadGeneratorGUISettings() {

    var guiSettings = post.send(window, 'getGeneratorGUISettings');
    var lastUserSettings = post.send(window, 'getGeneratorGUILastUserSettings');

    try {
      var results = await Promise.all([guiSettings, lastUserSettings]);

      let res = JSON.parse(results[0].data);
      let userSettings = results[1].data;

      if (userSettings)
        userSettings = JSON.parse(userSettings);

      await this.parseGeneratorGUISettings(res, userSettings);

    } catch (err) {
      console.error(err);
      throw Error(err);
    }
  }

  async webLoadGeneratorGUISettings() {

    var res = null;
    var userSettings = null;

    //Get generator settings
    if ((<any>window).webGeneratorSettingsMap) { //Master versions on the web pre-define a settings map to use before Angular will load
      res = (<any>window).webGeneratorSettingsMap;
      console.log("Settings map is available from the DOM");
    }
    else { //Dev versions on the web request a pre-built settings map from the server at runtime

      let url = (<any>window).location.protocol + "//" + (<any>window).location.host + "/angular/dev/" + this.getGlobalVar("webSourceVersion").replace(/ /g, "_") + "/utils/settings_list.json";
      console.log("Settings map is not available. Request it:", url);

      res = await this.http.get(url, { responseType: "json" }).toPromise();
    }

    //Get last user settings from browser cache for the appropriate app
    try {
      userSettings = localStorage.getItem(this.getGlobalVar("appType") == "generator" ? "generatorSettings_" + this.getGlobalVar("webSourceVersion") : "patcherSettings_" + this.getGlobalVar("webSourceVersion"));

      if (userSettings && userSettings.length > 0) {
        userSettings = JSON.parse(userSettings);
      }
      else { //Try the opposite app type to see if a cached settings map is available there
        userSettings = localStorage.getItem(this.getGlobalVar("appType") == "generator" ? "patcherSettings_" + this.getGlobalVar("webSourceVersion") : "generatorSettings_" + this.getGlobalVar("webSourceVersion"));

        if (userSettings && userSettings.length > 0) {
          userSettings = JSON.parse(userSettings);
        }
      }
    } catch (err) {
      console.error("Local storage not available");
    }

    //Get user presets if appType = generator
    if (this.getGlobalVar("appType") == "generator") {

      // Initialize presets with system defaults if not provided (e.g., in ng-dev mode)
      if (!res.presets || Object.keys(res.presets).length === 0) {
        res.presets = {
          "[New Preset]": { isNewPreset: true },
          "Default / Beginner": { isDefaultPreset: true }
        };

        try {
          let presetsUrl = (<any>window).location.protocol + "//" + (<any>window).location.host + "/angular/dev/" + this.getGlobalVar("webSourceVersion").replace(/ /g, "_") + "/utils/presets_default.json";

          let builtInPresets: any = await this.http.get(presetsUrl, { responseType: "json" }).toPromise();

          if (builtInPresets && typeof builtInPresets === 'object') {
            let adjustedBuiltInPresets = {};

            Object.keys(builtInPresets).forEach(presetName => {
              if (!(presetName in res.presets)) {
                adjustedBuiltInPresets[presetName] = { isProtectedPreset: true, settings: builtInPresets[presetName] };
              }
            });

            Object.assign(res.presets, adjustedBuiltInPresets);
          }
        } catch (err) {
          console.warn("Could not load presets_default.json:", err);
        }
      }

      let userPresets = null;

      try {
        userPresets = localStorage.getItem("generatorPresets_" + this.getGlobalVar("webSourceVersion"));
      } catch { }

      if (userPresets && userPresets.length > 0) {

        userPresets = JSON.parse(userPresets);

        let adjustedUserPresets = {};

        //Tag user presets appropiately
        Object.keys(userPresets).forEach(presetName => {
          if (!(presetName in res.presets))
            adjustedUserPresets[presetName] = { settings: userPresets[presetName] };
        });

        Object.assign(res.presets, adjustedUserPresets);
      }
    }

    await this.parseGeneratorGUISettings(res, userSettings);

    //Legacy: Check for cached files and then create web events after
    if ((<any>window).emscriptenFoundCachedROMFile)
      this.generator_settingsMap["OutputSettings.InputROMFilename"] = "<using cached ROM>";

    if ((<any>window).emscriptenFoundCachedWADFile)
      this.generator_settingsMap["Web.wad_file"] = "<using cached WAD>";

    if ((<any>window).emscriptenFoundCachedCommonKeyFile)
      this.generator_settingsMap["Web.common_key_file"] = "<using cached common key>";

    //If we have an external override settings map, apply it now
    if ((<any>window).externalOverrideSettingsMap)
    {
      let settings = JSON.parse((<any>window).externalOverrideSettingsMap);

      if (settings && Array.isArray(settings)) { //Batch

        for (let setting of settings) {
          this.generator_settingsMap[setting.name] = setting.value;
        }
      }
      else if (settings && settings.name && "value" in settings) { //Single
        this.generator_settingsMap[settings.name] = settings.value;
      }
    }

    this.createWebEvents();
  }

  async parseGeneratorGUISettings(guiSettings, userSettings) {
    const isRGBHex = /[0-9A-Fa-f]{6}/;

    //Intialize settings maps
    for (let tabIndex = 0; tabIndex < guiSettings.settingsArray.length; tabIndex++) {
      let tab = guiSettings.settingsArray[tabIndex];

      //Skip tabs that don't belong to this app and delete them from the guiSettings
      if ("app_type" in tab && tab.app_type && tab.app_type.indexOf(this.getGlobalVar("appType")) == -1) {

        guiSettings.settingsArray.splice(tabIndex, 1);
        tabIndex--;

        let index = guiSettings.cosmeticsArray.findIndex(entry => entry.name == tab.name);
        if (index != -1)
          guiSettings.cosmeticsArray.splice(index, 1);

        delete guiSettings.settingsObj[tab.name];
        delete guiSettings.cosmeticsObj[tab.name];

        continue;
      }

      this.generator_tabsVisibilityMap[tab.name] = true;

      for (let sectionIndex = 0; sectionIndex < tab.sections.length; sectionIndex++) {

        let section = tab.sections[sectionIndex];

        //Skip sections that don't belong to this app and delete them from the guiSettings
        if ("app_type" in section && section.app_type && section.app_type.indexOf(this.getGlobalVar("appType")) == -1) {

          tab.sections.splice(sectionIndex, 1);
          sectionIndex--;

          let foundInCosmeticsTab = guiSettings.cosmeticsArray.find(entryTab => {

            let index = entryTab.sections.findIndex(entry => entry.name == section.name);

            if (index != -1) {
              entryTab.sections.splice(index, 1);
              return true;
            }

            return false;
          });

          delete guiSettings.settingsObj[tab.name].sections[section.name];

          if (foundInCosmeticsTab) {
            delete guiSettings.cosmeticsObj[foundInCosmeticsTab.name].sections[section.name];
          }

          continue;
        }
        for (let settingIndex = 0; settingIndex < section.settings.length; settingIndex++) {

          let setting = section.settings[settingIndex];

          this.generator_settingsVisibilityMap[setting.name] = true;

          if (setting.type == "SearchBox") { //Special parsing for SearchBox data

            let useDefault = true;

            //Dirty: Settings doctrine says that a default value must be applyable as is, so we need to fix it here by converting the default value (string array) to an options array
            let defaultOptionsToSearch = null;

            if (setting.linked_setting) {

              //Find correct options array based on linked setting
              let linkedSetting = this.findSettingByName(setting.linked_setting, guiSettings.settingsArray);

              if (linkedSetting) {
                //We assume here that the default value of the SearchBox will match the options based on the default value of the linked setting
                defaultOptionsToSearch = setting.options[linkedSetting.default];

                if (!defaultOptionsToSearch)
                  defaultOptionsToSearch = [];
              }
              else {
                defaultOptionsToSearch = []; //Invalid/missing linked setting, set empty array
              }
            }
            else {
              //Regular simple search
              defaultOptionsToSearch = setting.options;
            }

            let defaultValueArray = [];

            setting.default.forEach(entry => {

              let optionEntry = defaultOptionsToSearch.find(option => {
                if (option.name === entry)
                  return true;

                return false;
              });

              if (optionEntry)
                defaultValueArray.push(optionEntry);
            });

            setting.default = defaultValueArray;
            guiSettings.settingsObj[tab.name].sections[section.name].settings[setting.name].default = defaultValueArray;

            //Convert user options (string array) to options array if present
            if (userSettings && setting.name in userSettings) {

              let optionsToSearch = null;

              if (setting.linked_setting) {

                //Find correct options array based on linked setting
                if (setting.linked_setting in userSettings) {
                  optionsToSearch = setting.options[userSettings[setting.linked_setting]];

                  if (!optionsToSearch)
                    optionsToSearch = [];
                }
                else {
                  optionsToSearch = []; //Invalid/missing linked setting, set empty array
                }
              }
              else {
                //Regular simple search
                optionsToSearch = setting.options;
              }

              let valueArray = [];

              userSettings[setting.name].forEach(entry => {

                let optionEntry = optionsToSearch.find(option => {
                  if (option.name === entry)
                    return true;

                  return false;
                });

                if (optionEntry)
                  valueArray.push(optionEntry);
              });

              this.generator_settingsMap[setting.name] = valueArray;
              useDefault = false;
            }

            if (useDefault) {
              this.generator_settingsMap[setting.name] = typeof (setting.default) === "object" ? JSON.parse(JSON.stringify(setting.default)) : setting.default;
            }
          }
          else if (setting.type == "SearchBoxMMR") { //Special parsing for SearchBoxMMR data

            let useDefault = true;

            //Dirty: Settings doctrine says that a default value must be applyable as is, so we need to fix it here by converting the hex string to an options array
            let decodedDefaultHexStringRes = this.decodeSearchBoxHexString(setting.default, setting.options);

            if (decodedDefaultHexStringRes.success) {
              setting.default = decodedDefaultHexStringRes.decodedOptions;
              guiSettings.settingsObj[tab.name].sections[section.name].settings[setting.name].default = decodedDefaultHexStringRes.decodedOptions;
            }
            else {
              //In case of decode error just set empty options array
              setting.default = [];
              guiSettings.settingsObj[tab.name].sections[section.name].settings[setting.name].default = [];
            }

            if (userSettings && setting.name in userSettings) {

              //Convert user hex string value to options array
              let decodedUserHexStringRes = this.decodeSearchBoxHexString(userSettings[setting.name], setting.options);

              //In case of decode error just use default value
              if (decodedUserHexStringRes.success) {
                this.generator_settingsMap[setting.name] = decodedUserHexStringRes.decodedOptions;
                useDefault = false;
              }
            }

            if (useDefault) {
              this.generator_settingsMap[setting.name] = typeof (setting.default) === "object" ? JSON.parse(JSON.stringify(setting.default)) : setting.default;
            }
          }
          else if (setting.type == "MultipleSelect" && setting.string_value && userSettings && setting.name in userSettings) { //Parse MultipleSelect value back to array if needed
            this.generator_settingsMap[setting.name] = userSettings[setting.name].split(", ");
          }
          else if (setting.type == "Combobox" && userSettings && setting.name in userSettings) { //Ensure combobox option exists before applying it (in case of outdated settings being loaded)

            if (section.is_colors && typeof (userSettings[setting.name]) == "string" && isRGBHex.test(userSettings[setting.name])) { //Custom Color is treated as an exception
              this.generator_settingsMap[setting.name] = userSettings[setting.name];
            }
            else {
              let optionEntry = setting.options.find(option => {
                if (option.name === userSettings[setting.name])
                  return true;

                return false;
              });

              this.generator_settingsMap[setting.name] = optionEntry ? userSettings[setting.name] : setting.default;
            }
          }
          else if (userSettings && setting.name in userSettings && (setting.type == "Scale" || setting.type == "Numberinput")) { //Validate numberic values again before applying them
            this.verifyNumericSetting(userSettings, setting, false);
            this.generator_settingsMap[setting.name] = userSettings[setting.name];
          }
          else if (setting.type == "Dictionary" && userSettings && setting.name in userSettings && userSettings[setting.name]) { //Validate according to inner_type

            for (let key of Object.keys(userSettings[setting.name])) {

              switch (setting.inner_type) {

                case "Scale":
                case "Numberinput": {
                  this.verifyNumericSetting(userSettings, setting, false, key);
                  break;
                }
              }
            }

            this.generator_settingsMap[setting.name] = userSettings[setting.name];
          }
          else { //Everything else
            let valueToSet = userSettings && setting.name in userSettings ? userSettings[setting.name] : setting.default;
            this.generator_settingsMap[setting.name] = typeof (valueToSet) === "object" ? JSON.parse(JSON.stringify(valueToSet)) : valueToSet;
          }

          //Color section handling
          if (section.is_colors) {
            if (typeof (this.generator_settingsMap[setting.name]) == "string" && isRGBHex.test(this.generator_settingsMap[setting.name])) { //Resolve Custom Color
              this.generator_customColorMap[setting.name] = "#" + this.generator_settingsMap[setting.name];
              this.generator_settingsMap[setting.name] = "Custom Color";
            }
            else {
              this.generator_customColorMap[setting.name] = "";
            }
          }

          //Handle dynamic settings. The available options (and sometimes the tooltip) are determined at runtime
          if (setting.dynamic) {
            let dynamicSetting = await this.updateDynamicSetting(setting.name)

            if (dynamicSetting) {

              let parsedSetting = JSON.parse(dynamicSetting);

              let isCosmetic = (tab.name in guiSettings.cosmeticsObj);

              guiSettings.settingsObj[tab.name].sections[section.name].settings[setting.name] = parsedSetting.object;

              guiSettings.settingsArray[tabIndex].sections[sectionIndex].settings[settingIndex] = parsedSetting.array;

              if (isCosmetic) {
                guiSettings.cosmeticsObj[tab.name].sections[section.name].settings[setting.name] = parsedSetting.object;

                let cosmeticTabIndex = guiSettings.cosmeticsArray.findIndex(elem => elem.name == tab.name);

                if (cosmeticTabIndex != -1) {

                  //Note: This follows the assumption that sections and settings are structured identically between settings and cosmetics arrays.
                  guiSettings.cosmeticsArray[cosmeticTabIndex].sections[sectionIndex].settings[settingIndex] = parsedSetting.array;
                }
              }
            }
          }
        };
      }
    }

    //Add GUI only options
    this.generator_settingsMap["settings_string"] = userSettings && "settings_string" in userSettings ? userSettings["settings_string"] : "";
    this.generator_settingsMap["theme"] = userSettings && "theme" in userSettings ? userSettings["theme"] : "";
    this.generator_settingsVisibilityMap["settings_string"] = true;

    console.log("JSON Settings Data:", guiSettings);
    console.log("Last User Settings:", userSettings);
    console.log("Final Settings Map", this.generator_settingsMap);
    console.log("Goal Hint Distros", guiSettings.distroArray);

    //Save settings after parsing them
    this.setGlobalVar('generatorSettingsArray', guiSettings.settingsArray);
    this.setGlobalVar('generatorSettingsObj', guiSettings.settingsObj);
    this.setGlobalVar('generatorCosmeticsArray', guiSettings.cosmeticsArray);
    this.setGlobalVar('generatorCosmeticsObj', guiSettings.cosmeticsObj);
    this.setGlobalVar('generatorGoalDistros', guiSettings.distroArray);

    // Initialize presets with system defaults if not provided (e.g., in ng-dev mode)
    if (!guiSettings.presets || Object.keys(guiSettings.presets).length === 0) {
      guiSettings.presets = {
        "[New Preset]": { isNewPreset: true },
        "Default / Beginner": { isDefaultPreset: true }
      };
    }

    this.generator_presets = guiSettings.presets;
  }

  /**
   * Queries SettingsToJson.py to return a JSON object with the dynamically generated setting definition in object & array format
   * resolves with { object: settingInObjectFormat , array: settingInArrayFormat } (or null in web mode)
   * rejects with null
   * POTENTIAL TODO: add api calls or browser lookups for web if needed in the future
  */
  updateDynamicSetting(settingName: string) {
    let self = this;

    return new Promise<any>(function (resolve, reject) {
      if (self.getGlobalVar('electronAvailable')) { //app mode
        post.send(window, 'updateDynamicSetting', settingName).then(event => {
          var listenerSuccess = post.once('updateDynamicSettingSuccess', function (event) {

            listenerError.cancel();

            let data = event.data;
            resolve(data);
          });

          var listenerError = post.once('updateDynamicSettingError', function (event) {

            listenerSuccess.cancel();

            console.error("[updateDynamicSetting] Python Error:", event.data);
            reject(null);
          });

        }).catch(err => {
          console.error("[updateDynamicSettingy] Post-Robot Error:", err);
          reject(null);
        });
      } else { //web mode
          //TODO: Should dynamic settings ever be needed on web, add API call or browser lookups here.
          //for now, resolving with null is fine.
          resolve(null);
      }
    })
  }

  async versionCheck() { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      throw Error("electron_not_available");

    try {

      var event = await post.send(window, 'getCurrentSourceVersion');

      type VersionData = {
        baseVersion: string;
        supplementaryVersion: number;
        fullVersion: string;
        branchUrl: string;
      };

      var local: VersionData = event.data;
      var result = { hasUpdate: false, currentVersion: "", latestVersion: "" };

      if (local !== null) {

        console.log("Local:", local);
        result.currentVersion = local.fullVersion;

        this.globalEmitter.emit({ name: "local_version_checked", version: local.fullVersion, branchUrl: local.branchUrl });

        let remote: VersionData = { baseVersion : "", supplementaryVersion : 0, fullVersion : "", branchUrl : "" }
        let url = local.branchUrl.replace("https://github.com", "https://raw.githubusercontent.com").replace("tree/", "") + "/version.py";
        console.log("URL: ", url)
        var remoteFile = await this.http.get(url, { responseType: "text" }).toPromise();

        let baseMatch = remoteFile.match(/^[ \t]*__version__ = ['"](.+)['"]/m);
        let supplementaryMatch = remoteFile.match(/^[ \t]*supplementary_version = (\d+)$/m);
        let fullMatch = remoteFile.match(/^[ \t]*__version__ = f['"]*(.+)['"]/m);
        let urlMatch = remoteFile.match(/^[ \t]*branch_url = ['"](.+)['"]/m);

        remote.baseVersion = baseMatch != null && baseMatch[1] !== undefined ? baseMatch[1] : "";
        remote.supplementaryVersion = supplementaryMatch != null && supplementaryMatch[1] !== undefined ? parseInt(supplementaryMatch[1]) : 0;
        remote.fullVersion = fullMatch != null && fullMatch[1] !== undefined ? fullMatch[1] : remote.baseVersion;
        remote.fullVersion = remote.fullVersion
          .replace('{base_version}', remote.baseVersion)
          .replace('{supplementary_version}', remote.supplementaryVersion.toString())
        remote.branchUrl = urlMatch != null && urlMatch[1] !== undefined ? urlMatch[1] : "";

        console.log("Remote:", remote);
        result.latestVersion = remote.fullVersion;

        //Compare versions
        result.hasUpdate = this.isVersionNewer(remote.baseVersion, local.baseVersion, remote.supplementaryVersion, local.supplementaryVersion);

        return result;
      }
      else {
        return result;
      }
    }
    catch (err) {
      console.error(err);
      throw Error(err);
    }
  }

  isVersionNewer(newVersion: string, oldVersion: string, newSubVersion: number = 0, oldSubVersion: number = 0) {

    //Strip away dev strings
    if (oldVersion.startsWith("dev") && oldVersion.includes("_"))
      oldVersion = oldVersion.split("_")[1];

    if (newVersion.startsWith("dev") && newVersion.includes("_"))
      newVersion = newVersion.split("_")[1];

    let oldSplit = oldVersion.replace('v', '').replace(' ', '.').split('.');
    let newSplit = newVersion.replace('v', '').replace(' ', '.').split('.');

    //Version is not newer if the new version doesn't satisfy the format
    if (newSplit.length < 3)
      return false;
    else if (newSplit.length == 4)
      newSubVersion = newSubVersion == 0 ? Number(newSplit[3]) : 0;

    //Version is newer if the old version doesn't satisfy the format
    if (oldSplit.length < 3)
      return true;
    else if (oldSplit.length == 4)
      oldSubVersion = oldSubVersion == 0 ? Number(oldSplit[3]) : 0;

    //Compare major.minor.revision
    if (Number(newSplit[0]) > Number(oldSplit[0])) {
      return true;
    }
    else if (Number(newSplit[0]) == Number(oldSplit[0])) {
      if (Number(newSplit[1]) > Number(oldSplit[1])) {
        return true;
      }
      else if (Number(newSplit[1]) == Number(oldSplit[1])) {
        if (Number(newSplit[2]) > Number(oldSplit[2])) {
          return true;
        }
        else if (Number(newSplit[2]) == Number(oldSplit[2])) {
          if (newSubVersion > oldSubVersion) {
            return true;
          }
        }
      }
    }

    return false;
  }

  findSettingByName(settingName: string, settingsArrayOverride = null) {

    let settingsArrayToUse = settingsArrayOverride ? settingsArrayOverride : this.getGlobalVar('generatorSettingsArray');

    for (let tabIndex = 0; tabIndex < settingsArrayToUse.length; tabIndex++) {
      let tab = settingsArrayToUse[tabIndex];

      for (let sectionIndex = 0; sectionIndex < tab.sections.length; sectionIndex++) {
        let section = tab.sections[sectionIndex];

        for (let settingIndex = 0; settingIndex < section.settings.length; settingIndex++) {
          let setting = section.settings[settingIndex];

          if (setting.name == settingName)
            return setting;
        }
      }
    }

    return false;
  }

  verifyNumericSetting(settingsFile: any, setting: any, syncToGlobalMap: boolean = false, dictKey = null) {

    let settingValue: any = dictKey ? settingsFile[setting.name][dictKey] : settingsFile[setting.name];
    let error = false;
    let didCast = false;

    //Skip checks if null value with nullable allowed
    if (setting["nullable"] === true && (settingValue === null || settingValue === ""))
    {
      //Cast to null if needed
      if (settingValue === "") {

        settingValue = null;

        if (dictKey)
          settingsFile[setting.name][dictKey] = settingValue;
        else
          settingsFile[setting.name] = settingValue;

        if (syncToGlobalMap) { //Global too if needed and refresh GUI after to signal change

          if (dictKey)
            this.generator_settingsMap[setting.name] = JSON.parse(JSON.stringify(settingsFile[setting.name]));
          else
            this.generator_settingsMap[setting.name] = settingValue;

          this.globalEmitter.emit({ name: "refresh_gui" });
        }
      }

      return error;
    }

    if (typeof (settingValue) != "string" && typeof (settingValue) != "number") { //Can't recover, bad type
      error = true;
    }
    else if (typeof (settingValue) == "string") { //Try to cast it appropriately

      if (setting.is_decimal) {

        //Decimals
        if (Number(parseFloat(settingValue)) != Number(settingValue)) { //Cast failed, not numeric
          error = true;
        }
        else {
          settingValue = parseFloat(settingValue);
          didCast = true;
        }
      }
      else {
        //Non decimals
        if (Number(parseInt(settingValue)) != Number(settingValue)) { //Cast failed, not numeric
          error = true;
        }
        else {
          settingValue = parseInt(settingValue);
          didCast = true;
        }
      }
    }

    if (!error) {

      //Min/Max check
      let settingMin: number = setting["min"];
      let settingMax: number = setting["max"];

      if (("min" in setting) && settingValue < settingMin) {

        if (dictKey)
          settingsFile[setting.name][dictKey] = settingMin;
        else
          settingsFile[setting.name] = settingMin;

        if (syncToGlobalMap) { //Global too if needed and refresh GUI after to signal change

          if (dictKey)
            this.generator_settingsMap[setting.name] = JSON.parse(JSON.stringify(settingsFile[setting.name]));
          else
            this.generator_settingsMap[setting.name] = settingMin;

          this.globalEmitter.emit({ name: "refresh_gui" });
        }

        error = true;
      }
      else if (("max" in setting) && settingValue > settingMax) {

        if (dictKey)
          settingsFile[setting.name][dictKey] = settingMax;
        else
          settingsFile[setting.name] = settingMax;

        if (syncToGlobalMap) { //Global too if needed and refresh GUI after to signal change

          if (dictKey)
            this.generator_settingsMap[setting.name] = JSON.parse(JSON.stringify(settingsFile[setting.name]));
          else
            this.generator_settingsMap[setting.name] = settingMax;

          this.globalEmitter.emit({ name: "refresh_gui" });
        }

        error = true;
      }

      if (!error && didCast) { //No error, but setting had to be casted from string, fix it

        if (dictKey)
          settingsFile[setting.name][dictKey] = settingValue;
        else
          settingsFile[setting.name] = settingValue;

        if (syncToGlobalMap) { //Global too if needed and refresh GUI after to signal change

          if (dictKey)
            this.generator_settingsMap[setting.name] = JSON.parse(JSON.stringify(settingsFile[setting.name]));
          else
            this.generator_settingsMap[setting.name] = settingValue;

          this.globalEmitter.emit({ name: "refresh_gui" });
        }
      }
    }
    else { //Critical error, reset local value to default

      if (dictKey) {
        //Reset to default if one is available, else delete it
        if (dictKey in setting.default)
          settingsFile[setting.name][dictKey] = setting.default[dictKey];
        else
          delete settingsFile[setting.name][dictKey];
      }
      else {
        settingsFile[setting.name] = setting.default;
      }

      if (syncToGlobalMap) { //Global too if needed and refresh GUI after to signal change

        if (dictKey)
          this.generator_settingsMap[setting.name] = JSON.parse(JSON.stringify(settingsFile[setting.name]));
        else
          this.generator_settingsMap[setting.name] = setting.default;

        this.globalEmitter.emit({ name: "refresh_gui" });
      }
    }

    return error;
  }

  decodeSearchBoxHexString(hexString: string, optionList: any) {

    let result = { success: false, decodedOptions: null };

    //Test hex string first
    let hexGrep = /[0-9A-Fa-f\-]/g;
    let hexTest = hexString.match(hexGrep);

    if ((hexString.length > 0 && (!hexTest || hexTest.length == 0)) || (hexTest && hexTest.length > 0 && hexTest.join("") != hexString)) {
      console.log("Invalid character in hex string");
      return result;
    }

    let optionCount = optionList.length;
    let byteCount = Math.ceil(optionCount / 8);
    let groupCount = Math.ceil(byteCount / 4);

    //Test if enough group separators in string
    let groupGrep = /[\-]/g;
    let groupTest = hexString.match(groupGrep);

    if ((groupCount - 1 == 0 && (groupTest && groupTest.length != 0)) || (groupCount - 1 > 0 && (!groupTest || groupTest.length != groupCount - 1))) {

      //If empty string, allow to proceed
      if (hexString.length != 0) {
        console.log("Invalid group count in hex string");
        return result;
      }
    }

    //Split string
    let groupsText = hexString.split("-");
    groupsText.reverse();

    //console.log("Groups:", groupsText);

    //Loop groups and get flag values
    let decodedOptionList = [];

    if (hexString.length > 0) {

      for (let i = 0; i < groupCount; i++) {

        let group = groupsText[i];
        let charsRemaining = group.length;

        if (charsRemaining > 8) {
          console.log("Invalid group length in hex string");
          return result;
        }

        let charPosition = charsRemaining > 1 ? group.length - 2 : group.length - 1;
        let bytePos = 0;

        while (charsRemaining > 0) {

          let hexByte = group.substr(charPosition, Math.min(charsRemaining, 2));
          let byte = Number.parseInt(hexByte, 16);

          for (let n = 0; n < 8; n++) {
            let flagValueTest = Math.pow(2, n);

            if ((byte & flagValueTest) == flagValueTest) {

              let targetIndex = (((i * 4) + bytePos) * 8) + n;

              if (targetIndex >= optionCount) {
                console.log("invalid option index, out of range");
                return result;
              }

              //Find target option and add it to new list
              let targetOption = optionList.find(item => item.index === targetIndex);

              if (!targetOption) {
                console.log("Invalid option index for hex string, option not found");
                return result;
              }

              decodedOptionList.push(targetOption);
            }
          }

          charsRemaining -= hexByte.length;
          charPosition -= charsRemaining > 1 ? 2 : 1;
          bytePos++;
        }
      }
    }

    //console.log("New list:", decodedOptionList);
    result.success = true;
    result.decodedOptions = decodedOptionList;

    return result;
  }

  encodeSearchBoxSelectionsAsHexString(currentSelections: any, optionList: any) {

    //Compute hex string
    let optionCount = optionList.length;
    let byteCount = Math.ceil(optionCount / 8); //7
    let groupCount = Math.ceil(byteCount / 4); //2

    //Init groups
    let groups = Array(groupCount);

    for (let i = 0; i < groupCount; i++) {
      let byteStartIndex = i * 4;
      let bytesInGroup = (byteCount - byteStartIndex) < 4 ? byteCount - byteStartIndex : 4;

      groups[i] = Array(bytesInGroup);
      groups[i].fill(0, 0, bytesInGroup);
    }

    //Loop currently selected options and add flag values
    for (let option of currentSelections) {
      let optionIndex = option.index;

      let bytePos = Math.floor(optionIndex / 8);
      let groupPosAbs = Math.floor(bytePos / 4);
      let groupPosRel = bytePos % 4;

      let flagPos = optionIndex % 8;
      let flagValue = Math.pow(2, flagPos);

      //console.log("byte pos:", bytePos, "group pos abs:", groupPosAbs, "group pos rel:", groupPosRel, "flagPos:", flagPos, "flagValue:", flagValue);

      groups[groupPosAbs][groupPosRel] += flagValue;
    }

    //Convert groups to hex strings
    let groupsText = [];

    for (let i = 0; i < groupCount; i++) {

      let group = groups[i];
      group.reverse();

      let groupText = "";
      let skipLeadingZeroes = true;
      let skipFirstPad = true;

      for (let n = 0; n < group.length; n++) {

        if (skipLeadingZeroes) {

          if (group[n] == 0)
            continue;
          else
            skipLeadingZeroes = false;
        }

        if (skipFirstPad) {
          groupText += group[n].toString(16);
          skipFirstPad = false;
        }
        else {
          groupText += group[n].toString(16).padStart(2, "0");
        }
      }

      groupsText.push(groupText);
    }

    //Build string
    groupsText.reverse();
    return groupsText.join("-");
  }

  applySettingsObject(settingsObj) {

    if (!settingsObj)
      return;

    const isRGBHex = /[0-9A-Fa-f]{6}/;

    this.getGlobalVar('generatorSettingsArray').forEach(tab => {
      tab.sections.forEach(section => {
        section.settings.forEach(setting => {

          if (setting.name in settingsObj) {

            if (setting.type == "SearchBox") { //Special parsing for SearchBox data

              //Convert setting options (string array) to options array
              let optionsToSearch = null;

              if (setting.linked_setting) {

                //Find correct options array based on linked setting
                if (setting.linked_setting in settingsObj) {
                  optionsToSearch = setting.options[settingsObj[setting.linked_setting]];

                  if (!optionsToSearch)
                    optionsToSearch = [];
                }
                else {
                  optionsToSearch = []; //Invalid/missing linked setting, set empty array
                }
              }
              else {
                //Regular simple search
                optionsToSearch = setting.options;
              }

              let valueArray = [];

              settingsObj[setting.name].forEach(entry => {

                let optionEntry = optionsToSearch.find(option => {
                  if (option.name === entry)
                    return true;

                  return false;
                });

                if (optionEntry)
                  valueArray.push(optionEntry);
              });

              this.generator_settingsMap[setting.name] = valueArray;
            }
            else if (setting.type == "SearchBoxMMR") { //Special parsing for SearchBoxMMR data

              //Convert settings object string value to options array
              let decodedSettingHexStringRes = this.decodeSearchBoxHexString(settingsObj[setting.name], setting.options);

              if (decodedSettingHexStringRes.success) {
                this.generator_settingsMap[setting.name] = decodedSettingHexStringRes.decodedOptions;
              }
              else {
                //In case of decode error set empty
                this.generator_settingsMap[setting.name] = [];
              }
            }
            else if (setting.type == "Combobox") { //Ensure combobox option exists before applying it (in case of outdated settings being loaded)

              let optionEntry = setting.options.find(option => {
                if (option.name === settingsObj[setting.name])
                  return true;

                return false;
              });

              if (optionEntry)
                this.generator_settingsMap[setting.name] = settingsObj[setting.name];
            }
            else if (setting.type == "Scale" || setting.type == "Numberinput") { //Validate numberic values again before applying them
              this.verifyNumericSetting(settingsObj, setting, false);
              this.generator_settingsMap[setting.name] = settingsObj[setting.name];
            }
            else if (setting.type == "MultipleSelect") { //Validate list types before applying them and convert string value back to array if needed

              if (setting.string_value) {
                this.generator_settingsMap[setting.name] = settingsObj[setting.name].split(", ");
              }
              else {
                if (Array.isArray(settingsObj[setting.name]))
                  this.generator_settingsMap[setting.name] = JSON.parse(JSON.stringify(settingsObj[setting.name]));
              }
            }
            else { //Everything else
              this.generator_settingsMap[setting.name] = typeof (settingsObj[setting.name]) === "object" ? JSON.parse(JSON.stringify(settingsObj[setting.name])) : settingsObj[setting.name];
            }

            //Color section handling
            if (section.is_colors) {

              if (typeof (settingsObj[setting.name]) == "string" && isRGBHex.test(settingsObj[setting.name])) { //Resolve Custom Color
                this.generator_customColorMap[setting.name] = "#" + settingsObj[setting.name];
                this.generator_settingsMap[setting.name] = "Custom Color";
              }
              else {
                this.generator_customColorMap[setting.name] = "";
              }
            }
          }
        });
      });
    });
  }

  applyDefaultSettings() {

    //cleanSettings is only used to identify settings that should be reset, don't use the values as is!
    let cleanSettings = this.createSettingsFileObject(false, true, !this.getGlobalVar('electronAvailable'));

    this.getGlobalVar('generatorSettingsArray').forEach(tab => {
      tab.sections.forEach(section => {
        section.settings.forEach(setting => {

          if (setting.name in cleanSettings) {
            this.generator_settingsMap[setting.name] = typeof (setting.default) === "object" ? JSON.parse(JSON.stringify(setting.default)) : setting.default;
          }
        });
      });
    });
  }

  deleteSettingsFromMapWithCondition(settingsMap: any, keyName: string, keyValue: any) {

    this.getGlobalVar("generatorSettingsArray").forEach(tab => {

      tab.sections.forEach(section => {
        section.settings.forEach(setting => {

          if (keyName in setting && setting[keyName] == keyValue) {
            delete settingsMap[setting.name];
          }
        });
      });
    });
  }

  createSettingsFileObject(includeFromPatchFileSettings: boolean = true, includeSeedSettingsOnly: boolean = false, sanitizeForBrowserCache: boolean = false, cancelWhenError: boolean = false, sanitizeForServer: boolean = false) {

    let settingsFile: any = {};

    Object.assign(settingsFile, this.generator_settingsMap);

    //Add in custom colors
    Object.keys(this.generator_customColorMap).forEach(colorSettingName => {
      if (this.generator_customColorMap[colorSettingName].length > 0 && settingsFile[colorSettingName] === "Custom Color") {
        settingsFile[colorSettingName] = this.generator_customColorMap[colorSettingName].substr(1);
      }
    });

    let invalidSettingsList = [];

    //Resolve special settings
    this.getGlobalVar("generatorSettingsArray").forEach(tab => {

      tab.sections.forEach(section => {
        section.settings.forEach(setting => {

          if (setting.type == "SearchBox") {

            //Resolve search box entries (name only)
            let valueArray = [];

            settingsFile[setting.name].forEach(entry => {
              valueArray.push(entry.name);
            });

            settingsFile[setting.name] = valueArray;
          }
          else if (setting.type == "SearchBoxMMR") {

            //Encode MMR search box entries to a hex string for export
            settingsFile[setting.name] = this.encodeSearchBoxSelectionsAsHexString(settingsFile[setting.name], setting.options);
          }
          else if (setting.type == "MultipleSelect") {

            //Revert to null value if empty value if a special null value exists
            if (setting.null_value && (!settingsFile[setting.name] || settingsFile[setting.name].length < 1)) {
              settingsFile[setting.name] = setting.null_value;
            }

            //Encode MultipleSelect entries to a plain string if needed
            if (setting.string_value)
              settingsFile[setting.name] = settingsFile[setting.name].join(", ");
          }
          else if (setting.type == "Scale" || setting.type == "Numberinput") { //Validate numberic values again before saving them

            let error = this.verifyNumericSetting(settingsFile, setting, true);

            if (error) { //Input could not be recovered, abort
              console.log("check setting numeric failed:", settingsFile[setting.name], "setting:", setting);
              invalidSettingsList.push(setting.text);
            }
          }
          else if (setting.type == "Dictionary" && settingsFile[setting.name]) { //Validate according to inner_type

            //Deep clone dicts beforehand to avoid reference related bugs
            settingsFile[setting.name] = JSON.parse(JSON.stringify(settingsFile[setting.name]));

            for (let key of Object.keys(settingsFile[setting.name])) {

              let error = false;

              switch (setting.inner_type) {

                case "Scale":
                case "Numberinput": {

                  error = this.verifyNumericSetting(settingsFile, setting, true, key);

                  if (error)
                    console.log("Dictionary inner check setting numeric failed:", key, settingsFile[setting.name], "setting:", setting);

                  break;
                }
              }

              if (error) { //Value could not be handled, abort
                invalidSettingsList.push(setting.text);
                break;
              }
            }
          }
          else if (typeof (settingsFile[setting.name]) === "object" && settingsFile[setting.name] && setting.type != "Fileinput" && setting.type != "Directoryinput") {
            //Deep clone other dicts beforehand to avoid reference related bugs
            settingsFile[setting.name] = JSON.parse(JSON.stringify(settingsFile[setting.name]));
          }
        });
      });
    });

    //Abort if any invalid settings were found
    if (invalidSettingsList && invalidSettingsList.length > 0) {

      this.globalEmitter.emit({ name: "dialog_error", message: "Some invalid settings were detected and had to be reset! This can happen if you type too fast into an input box. Please try again. The following settings were affected: " + invalidSettingsList.join(", ") });

      if (cancelWhenError)
        return null;
    }

    //Delete keys the python source doesn't need
    delete settingsFile["Web.presets"];
    delete settingsFile["open_output_dir"]; //Unneeded
    delete settingsFile["open_python_dir"]; //Unneeded
    delete settingsFile["Web.generate_from_file"];

    //Delete fromPatchFile keys if mode is fromSeed
    if (!includeFromPatchFileSettings) {
      delete settingsFile["Web.patch_file"];
      delete settingsFile["Web.repatch_cosmetics"];

      //Web only keys
      if (!this.getGlobalVar('electronAvailable')) {
        delete settingsFile["Web.persist_in_cache"];
      }
    }

    //Delete keys not included in the seed or should be skipped for server processing
    if (includeSeedSettingsOnly || sanitizeForServer) {

      //Not mapped settings need to be deleted manually
      delete settingsFile["settings_string"];
      delete settingsFile["theme"]

      //Delete all shared = false keys from map since they aren't included in the seed
      if (includeSeedSettingsOnly)
        this.deleteSettingsFromMapWithCondition(settingsFile, "shared", false);

      //Delete all DictionaryLinked as they are only intended for GUI design
      this.deleteSettingsFromMapWithCondition(settingsFile, "type", "DictionaryLinked");
    }

    //Delete keys the browser can't save (web only)
    if (sanitizeForBrowserCache) {

      //Delete all settings of type Fileinput/Directoryinput. File objects can not be saved due browser sandbox
      this.deleteSettingsFromMapWithCondition(settingsFile, "type", "Fileinput");
      this.deleteSettingsFromMapWithCondition(settingsFile, "type", "Directoryinput");
    }

    return settingsFile;
  }

  saveCurrentSettingsToFile() {

    if (this.getGlobalVar('electronAvailable')) { //Electron
      post.send(window, 'saveCurrentSettingsToFile', this.createSettingsFileObject()).then(event => {
        console.log("settings saved to file");
      }).catch(err => {
        console.error(err);
      });
    }
    else { //Web
      try {
        localStorage.setItem(this.getGlobalVar("appType") == "generator" ? "generatorSettings_" + this.getGlobalVar("webSourceVersion") : "patcherSettings_" + this.getGlobalVar("webSourceVersion"), JSON.stringify(this.createSettingsFileObject(true, false, true)));
      } catch { }
    }
  }

  convertSettingsToString() {
    var self = this;

    return new Promise<string>(function (resolve, reject) {

      if (self.getGlobalVar('electronAvailable')) { //Electron

        post.send(window, 'convertSettingsToString', self.createSettingsFileObject(false, true)).then(event => {

          var listenerSuccess = post.once('convertSettingsToStringSuccess', function (event) {

            listenerError.cancel();

            let data = event.data;
            resolve(data);
          });

          var listenerError = post.once('convertSettingsToStringError', function (event) {

            listenerSuccess.cancel();

            let data = event.data;

            console.error("[convertSettingsToString] Python Error:", data);
            reject(data);
          });

        }).catch(err => {
          console.error("[convertSettingsToString] Post-Robot Error:", err);
          reject(err);
        });
      }
      else { //Web
        let url = (<any>window).location.protocol + "//" + (<any>window).location.host + "/settings/parse?version=" + self.getGlobalVar("webSourceVersion");
        console.log("Request string from:", url);

        self.http.post(url, JSON.stringify(self.createSettingsFileObject(false, true, true)), { responseType: "text", headers: { "Content-Type": "application/json" } }).toPromise().then(res => {
          resolve(res);
        }).catch(err => {
          console.error("[convertSettingsToString] Web Error:", err);
          reject(err);
        });
      }
    });
  }

  convertStringToSettings(settingsString: string) {
    var self = this;

    return new Promise(function (resolve, reject) {

      if (self.getGlobalVar('electronAvailable')) { //Electron

        post.send(window, 'convertStringToSettings', settingsString).then(event => {

          var listenerSuccess = post.once('convertStringToSettingsSuccess', function (event) {

            listenerError.cancel();

            let data = JSON.parse(event.data);
            resolve(data);
          });

          var listenerError = post.once('convertStringToSettingsError', function (event) {

            listenerSuccess.cancel();

            let data = event.data;

            console.error("[convertStringToSettings] Python Error:", data);
            reject(data);
          });

        }).catch(err => {
          console.error("[convertStringToSettings] Post-Robot Error:", err);
          reject(err);
        });
      }
      else { //Web
        let url = (<any>window).location.protocol + "//" + (<any>window).location.host + "/settings/get?version=" + self.getGlobalVar("webSourceVersion") + "&settingsString=" + settingsString;
        console.log("Request settings from:", url);

        self.http.get(url, { responseType: "json" }).toPromise().then(res => {
          resolve(res);
        }).catch(err => {
          console.error("[convertStringToSettings] Web Error:", err);
          reject(err);
        });
      }
    });
  }

  createPresetFileObject() {

    let presetsFile: any = {};

    //Create presets object
    Object.keys(this.generator_presets).forEach(presetKey => {
      let preset = this.generator_presets[presetKey];

      if (!("isNewPreset" in preset) && !("isDefaultPreset" in preset) && !("isProtectedPreset" in preset) && ("settings" in preset) && typeof (preset.settings) == "object" && Object.keys(preset.settings).length > 0) {
        //console.log("store " + presetKey, preset.settings);
        presetsFile[presetKey] = preset.settings;
      }
    });

    return presetsFile;
  }

  saveCurrentPresetsToFile() {

    if (this.getGlobalVar('electronAvailable')) { //Electron
      post.send(window, 'saveCurrentPresetsToFile', JSON.stringify(this.createPresetFileObject(), null, 4)).then(event => {
        console.log("presets saved to file");
      }).catch(err => {
        console.error(err);
      });
    }
    else { //Web
      if (this.getGlobalVar("appType") == "generator") { //Generator only
        try {
          localStorage.setItem("generatorPresets_" + this.getGlobalVar("webSourceVersion"), JSON.stringify(this.createPresetFileObject(), null, 4));
        } catch { }
      }
    }
  }

  generateSeedElectron(progressWindowRef: ProgressWindowComponent, fromPatchFile: boolean = false, useStaticSeed: string = "") { //Electron only
    var self = this;

    return new Promise<void>(function (resolve, reject) {

      let settingsMap = self.createSettingsFileObject(fromPatchFile, false, false, true);

      if (!settingsMap) {
        reject({ short: "Generation aborted.", long: "Generation aborted." });
        return;
      }

      //Hack: fromPatchFile forces generation count to 1 to avoid wrong percentage calculation
      if (fromPatchFile)
        settingsMap["count"] = 1;

      post.send(window, 'generateSeed', { settingsFile: settingsMap, staticSeed: useStaticSeed }).then(event => {

        var listenerProgress = post.on('generateSeedProgress', function (event) {

          let data = event.data;

          //console.log("progress report", data);

          if (progressWindowRef) {
            progressWindowRef.currentGenerationIndex = data.generationIndex;
            progressWindowRef.progressPercentageCurrent = data.progressCurrent;
            progressWindowRef.progressPercentageTotal = data.progressTotal;
            progressWindowRef.progressMessage = data.message;
            progressWindowRef.refreshLayout();
          }
        });

        var listenerSuccess = post.once('generateSeedSuccess', function (event) {

          listenerProgress.cancel();
          listenerCancel.cancel();
          listenerError.cancel();

          let data = event.data;
          resolve();
        });

        var listenerError = post.once('generateSeedError', function (event) {

          listenerProgress.cancel();
          listenerCancel.cancel();
          listenerSuccess.cancel();

          let data = event.data;

          console.error("[generateSeedElectron] Python Error:", data);
          reject(data);
        });

        var listenerCancel = post.once('generateSeedCancelled', function (event) {

          listenerProgress.cancel();
          listenerSuccess.cancel();
          listenerError.cancel();

          reject({ short: "Generation cancelled.", long: "Generation cancelled." });
        });
      }).catch(err => {
        console.error("[generateSeedElectron] Post-Robot Error:", err);
        reject({ short: err, long: err });
      });
    });
  }

  async cancelGenerateSeedElectron() { //Electron only

    if (!this.getGlobalVar('electronAvailable'))
      throw Error("electron_not_available");

    try {

      let event = await post.send(window, 'cancelGenerateSeed');
      let data = event.data;

      if (data == true)
        return;
      else
        throw Error("cancel_failed");
    }
    catch (err) {
      console.error("[cancelGenerateSeedElectron] Couldn't cancel due Post-Robot Error:", err);
      throw Error(err);
    }
  }

  hasRomExtension(fileName: string) {
    fileName = fileName.toLowerCase();
    return fileName.endsWith(".z64") || fileName.endsWith(".n64") || fileName.endsWith(".v64");
  }

  testDirectoryPickerFeatureWeb() { //Web only

    var input = (<any>document).createElement('input');
    input.type = 'file';

    if (!("webkitdirectory" in input))
      return false;

    //Hack: Mobile Chrome claims support, but actually does nothing, so we need to block it separately
    let isMobile = /Android|iPhone|iPad|iPod|BlackBerry|IEMobile/i.test((<any>navigator).userAgent);
    let isChrome = /Chrome/i.test((<any>navigator).userAgent);

    if (isMobile && isChrome)
      return false;

    return true;
  }

  isValidFileObjectWeb(file: any) { //Web only
    return file && typeof (file) == "object" && file.name && file.name.length > 0;
  }

  readFileIntoMemoryWeb(fileObject: any, useArrayBuffer: boolean) { //Web only

    return new Promise<any>(function (resolve, reject) {

      let fileReader = new FileReader();
      fileReader.onabort = function (event) {
        console.error("Loading of the file was aborted unexpectedly!");
        reject();
      };
      fileReader.onerror = function (event) {
        console.error("An error occurred during the loading of the file!");
        reject();
      };
      fileReader.onload = function (event) {

        console.log("Read in file successfully");
        resolve(event.target["result"]);
      };

      if (useArrayBuffer)
        fileReader.readAsArrayBuffer(fileObject);
      else
        fileReader.readAsText(fileObject);
    });
  }

  async readJsonFileIntoMemoryWeb(file: any, sizeLimit: number) { //Web only

    let fileJSON;

    if (this.hasRomExtension(file.name)) { //Not a ROM check...
      throw { error: "file_extension_was_rom" };
    }

    try {
      fileJSON = await this.readFileIntoMemoryWeb(file, false);
    }
    catch (ex) {
      throw { error: "file_read_error" };
    }

    if (!fileJSON || fileJSON.length < 1) {
      throw { error: "file_not_valid" };
    }

    if (fileJSON.length > sizeLimit) { //Impose size limit to avoid server overload
      throw { error: "file_too_big" };
    }

    //Test JSON parse it
    try {
      let jsonFileParsed = JSON.parse(fileJSON);

      if (!jsonFileParsed || Object.keys(jsonFileParsed).length < 1) {
        throw { error: "file_not_valid_json_empty" };
      }
    }
    catch (err) {
      console.error(err);
      throw { error: "file_not_valid_json_syntax", message: err.message };
    }

    return fileJSON;
  }

  async readPlandoFileIntoMemoryWeb(settingName: string, settingText: string) { //Web only

    let plandoFile = this.generator_settingsMap[settingName];

    if (!this.isValidFileObjectWeb(plandoFile))
      return null;

    //Try to resolve the plando file by reading it into memory
    console.log("Read Plando JSON file: " + plandoFile.name);

    try {
      let plandoFileText = await this.readJsonFileIntoMemoryWeb(plandoFile, 2 * 1000 * 1000); //Will return the JSON file contents as text
      return plandoFileText;
    }
    catch (ex) {
      switch (ex.error) {
        case "file_read_error": {
          throw { error: `An error occurred during the loading of the ${settingText}! Please try to enter it again.` };
        }
        case "file_not_valid": {
          throw { error: `The ${settingText} specified is not valid!` };
        }
        case "file_too_big": {
          throw { error: `The ${settingText} specified is too big! The maximum file size allowed is 2 MB.` };
        }
        case "file_not_valid_json_empty": {
          throw { error: `The ${settingText} specified is not valid JSON! Please verify the syntax.` };
        }
        case "file_not_valid_json_syntax": {
          throw { error: `The ${settingText} specified is not valid JSON! Please verify the syntax. Detail: ${ex.message}` };
        }
        default: {
          //Bubble error upwards
          throw ex;
        }
      }
    }
  }

  async generateSeedWeb(raceSeed: boolean = false, useStaticSeed: string = "") { //Web only

    //Plando Files are read to a string before sending them to the server
    let customUserFiles: any[] = [
      /*
      //Standard
      {
        enablerSetting: "enable_distribution_file",
        setting: "distribution_file",
        raceSeedAllowed: false,
        raceSeedWarning: "Plandomizer for seed creation is currently not supported for race seeds due security concerns. Please use a normal seed instead!",
        errorRomEntered: "Your ROM doesn't belong in a plandomizer setting. This entirely optional setting is used to plan out seeds before generation by manipulating spoiler log files. If you want to generate a normal seed instead, please click YES!"
      },
      {
        enablerSetting: "enable_cosmetic_file",
        setting: "cosmetic_file",
        raceSeedAllowed: true,
        errorRomEntered: "Your ROM doesn't belong in a plandomizer setting. This entirely optional setting is used to give you more control over your cosmetic and sound settings. If you want to generate a normal seed with regular cosmetics instead, please click YES!"
      },
      */
      //MMR
      {
        enablerSetting: null,
        setting: "GameplaySettings.UserLogicFileName",
        raceSeedAllowed: true,
        errorRomEntered: "Your Majora's Mask ROM doesn't belong in the User Logic setting. This entirely optional setting is used to give you more control over your seed logic. If you want to generate a normal seed instead, please click YES!"
      }
    ];

    for (let userFile of customUserFiles) {

      if (userFile.enablerSetting && !this.generator_settingsMap[userFile.enablerSetting])
        continue;

      if (!userFile.raceSeedAllowed && raceSeed) { //No support for race seeds
        throw { error: userFile.raceSeedWarning };
      }

      let setting = this.findSettingByName(userFile.setting);

      try {
        userFile._fileData = await this.readPlandoFileIntoMemoryWeb(userFile.setting, setting.text);
      }
      catch (ex) {
        switch (ex.error) {
          case "file_extension_was_rom": {
            throw { error_rom_in_plando: userFile.errorRomEntered, type: userFile };
          }
          default: {
            //Bubble error upwards
            throw ex;
          }
        }
      }
    }

    let settingsFile = this.createSettingsFileObject(false, false, true, true, true);

    if (!settingsFile) {
      throw { error: "The generation was aborted due to previous errors!" };
    }

    //Add user files back into map as string if available, else clear them
    for (let userFile of customUserFiles) {

      if (!userFile._fileData) {

        if (userFile.enablerSetting)
          settingsFile[userFile.enablerSetting] = false;

        settingsFile[userFile.setting] = "";
      }
      else {
        settingsFile[userFile.setting] = userFile._fileData;
      }
    }

    if (raceSeed) {
      useStaticSeed = ""; //Static seeds aren't allowed in race mode
      settingsFile["create_spoiler"] = true; //Force spoiler mode to on
      settingsFile["encrypt"] = true;
      settingsFile["web_lock"] = true;
    }
    else {
      delete settingsFile["encrypt"];
    }

    if (useStaticSeed) {
      console.log("Use Static Seed:", useStaticSeed);
      settingsFile["seed"] = useStaticSeed;
    }
    else {
      delete settingsFile["seed"];
    }

    //If spoiler log creation was intentionally disabled, warn user one time about the consequences
    try {
      let spoilerLogWarningSeen = localStorage.getItem("spoilerLogWarningSeen");

      if ((!spoilerLogWarningSeen || spoilerLogWarningSeen == "false") && settingsFile["create_spoiler"] == false) {
        localStorage.setItem("spoilerLogWarningSeen", JSON.stringify(true));
        throw { error_spoiler_log_disabled: "Generating a seed without a spoiler log means you won't be able to receive any help in case you get stuck! Would you rather generate a seed WITH a spoiler log?" };
      }
    }
    catch (err) { //Bubble through in case the warning should be displayed, ignore if local storage is not available
      if (err.hasOwnProperty('error_spoiler_log_disabled'))
        throw err;
    }

    console.log(settingsFile);
    console.log("Race Seed:", raceSeed);

    //Request Seed Generation
    let url = (<any>window).location.protocol + "//" + (<any>window).location.host + "/seed/create?version=" + this.getGlobalVar("webSourceVersion");
    console.log("Request seed id from:", url);

    try {
      let res = await this.http.post(url, JSON.stringify(settingsFile), { responseType: "text", headers: { "Content-Type": "application/json" } }).toPromise();
      return res;
    }
    catch (err) {
      console.error("[generateSeedWeb] Web Error:", err);
      throw err;
    }
  }

  async patchROMWeb() { //Web only

    //Plando Cosmetics Logic
    let plandoFileCosmetics = null;
    if (this.generator_settingsMap["enable_cosmetic_file"]) {

      try {
        plandoFileCosmetics = await this.readPlandoFileIntoMemoryWeb("cosmetic_file", "Cosmetic Plandomizer File");
      }
      catch (ex) {
        switch (ex.error) {
          case "file_extension_was_rom": {
            throw { error_rom_in_plando: "Your Majora's Mask ROM doesn't belong in a plandomizer setting. This entirely optional setting is used to give you more control over your cosmetic and sound settings. If you want to patch your ROM with regular cosmetics instead, please click YES!", type: "cosmetic_file" };
          }
          default: {
            //Bubble error upwards
            throw ex;
          }
        }
      }
    }

    let settingsFile = this.createSettingsFileObject(true, false, false, true);

    if (!settingsFile) {
      throw { error: "The patching was aborted due to previous errors!" };
    }

    //Add cosmetics plando file back into map as string if available, else clear it
    if (plandoFileCosmetics) {
      settingsFile["cosmetic_file"] = plandoFileCosmetics;
    }
    else {
      settingsFile["enable_cosmetic_file"] = false;
      settingsFile["cosmetic_file"] = "";
    }

    if (typeof (<any>window).patchROM === "function") { //Try to call patchROM function on the DOM
      //Delay this so the async function can return early with "success"
      setTimeout(() => {
        (<any>window).patchROM(5, settingsFile); //Patch Version 5
      }, 0);
    }
    else {
      console.error("[patchROMWeb] Patcher not available!");
      throw { error: "Patcher not available!" };
    }
  }
}
