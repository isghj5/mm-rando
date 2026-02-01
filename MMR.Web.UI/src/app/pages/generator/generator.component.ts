import { Component, OnInit, OnDestroy, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild, ElementRef, HostListener } from '@angular/core';

import { OverlayContainer } from '@angular/cdk/overlay';

import { GUIGlobal } from '../../providers/GUIGlobal';
import { MatGridList, MatGridTile } from '@angular/material/grid-list';
import {NbDialogService, NbTabsetComponent} from '@nebular/theme';

import { GUITooltipComponent } from './guiTooltip/guiTooltip.component';
import { ProgressWindowComponent } from './progressWindow/progressWindow.component';
import { DialogWindowComponent } from './dialogWindow/dialogWindow.component';
import { ConfirmationWindowComponent } from './confirmationWindow/confirmationWindow.component';

@Component({
  selector: 'mmr-generator',
  templateUrl: './generator.component.html',
  styleUrls: ['./generator.component.scss'],
  standalone: false
})
export class GeneratorComponent implements OnInit, OnDestroy {

  tooltipComponent = GUITooltipComponent;

  @ViewChild('refTabSet') tabSet: NbTabsetComponent;
  @ViewChild('refTabSet', { read: ElementRef }) tabSetNative: ElementRef<HTMLElement>;
  @ViewChild('refTabFooter') tabSetFooter: NbTabsetComponent;
  activeTab: string = "";
  activeFooterTab: string = "";
  settingsLocked: boolean = false;
  
  // Mobile scroll indicator properties
  showMobileScrollIndicator: boolean = false;
  private isScrolling: boolean = false;
  private scrollTimeout: any;

  private scrollIndicatorElement: HTMLElement | null = null;


  //Busy Spinners
  generatorBusy: boolean = true;
  settingsBusy: boolean = false;
  settingsBusySaveOnly: boolean = true;

  //Local (non persistent) Variables
  seedString: string = "";
  generateSeedButtonEnabled: boolean = true;
  inputOldValue: any = null; //Used to manage input field backup/restore

  //Static settings
  generateFromSeedTabTitle: string = "Generate New Seed";
  generateFromFileTabTitle: string = "Generate From Patch File";

  //repatchCosmeticsCheckboxText: string = "Override Original Cosmetics";
  //repatchCosmeticsCheckboxTooltipPatch: string = "Replaces the cosmetic and sound settings generated in the patch file<br>with those selected on this page.";
  //repatchCosmeticsCheckboxTooltipSeedPageWeb: string = "Replaces the cosmetic and sound settings generated in the seed<br>with those selected on this page.";

  // Cache for no_line_break decisions to avoid unnecessary recalculations
  private noLineBreakCache = new Map<string, boolean>();

  constructor(private overlayContainer: OverlayContainer, private cd: ChangeDetectorRef, public global: GUIGlobal, private dialogService: NbDialogService) {
  }

  ngOnInit() {

    if ((<any>window).apiTestMode) {
      console.log("Test mode is active!");
    }

    //Refresh/render GUI on startup if ready or wait until ready event is fired
    if (this.global.getGlobalVar("appReady")) {
      this.generatorReady();
    }
    else {

      let eventSub = this.global.globalEmitter.subscribe(eventObj => {

        if (eventObj.name == "init_finished") {
          this.generatorReady();

          eventSub.unsubscribe();
        }
      });
    }
    
    // Add window resize listener for no line break calculations
    window.addEventListener('resize', () => {
      this.onWindowResize();
    });
  }

  ngOnDestroy() {
    window.removeEventListener('resize', () => {
      this.onWindowResize();
    });
    
    if (this.scrollTimeout) {
      clearTimeout(this.scrollTimeout);
    }
  }

  generatorReady() {
    this.generatorBusy = false;

    //Set active tab on boot
    this.activeTab = this.global.getGlobalVar('generatorSettingsArray')[0].text;

    //Set active footer tab on boot
    if (this.global.getGlobalVar('appType') == 'generator') {
      this.activeFooterTab = this.generateFromSeedTabTitle;
      this.global.generator_settingsMap["Web.generate_from_file"] = false;
    }
    else {
      this.activeFooterTab = this.generateFromFileTabTitle;
      this.global.generator_settingsMap["Web.generate_from_file"] = true;
    }

    let generateFromFileSetting = this.global.findSettingByName("Web.generate_from_file");
    if (generateFromFileSetting) {
      let currentValue = this.global.generator_settingsMap["Web.generate_from_file"];
      let currentOption = this.findOption(generateFromFileSetting.options, currentValue);
      
      if (currentOption && "controls_visibility_tab" in currentOption) {
        currentOption["controls_visibility_tab"].split(",").forEach(tab => {
          if (tab in this.global.generator_tabsVisibilityMap) {
            this.global.generator_tabsVisibilityMap[tab] = currentValue;
          }
        });
      }
      
      this.checkVisibility(currentValue, generateFromFileSetting, currentOption);
    }

    this.recheckAllSettings(); //All settings are default set to enabled, so at init time it only needs to potentially disable settings
    this.cd.markForCheck();
    this.cd.detectChanges();

    this.runEventListeners();

    //Electron only: Ensure settings string is up-to-date on app launch
    if (this.global.getGlobalVar('electronAvailable'))
      this.getSettingsString();
    else //Web only: Check if we should auto import settings/presets from a prior version
      this.checkAutoImportSettings();
  }

  runEventListeners() {

    //Subscribe to event listeners after initial rendering has concluded
    setTimeout(() => {
      
      // Initialize mobile scroll indicator logic after ensuring ViewChild is available
      this.initializeMobileScrollIndicator();
      
      // Note: We don't force recalculation on initial load anymore
      // The default behavior is now to assume unwrapped and only wrap when necessary

      // Tab change subscriptions
      this.tabSet.changeTab.subscribe(eventObj => {
        this.activeTab = eventObj.tabTitle;
      });

      this.tabSetFooter.changeTab.subscribe(eventObj => {
        this.activeFooterTab = eventObj.tabTitle;
      });

      // Global event subscriptions
      this.global.globalEmitter.subscribe(eventObj => {

        if (eventObj.name == "refresh_gui") {
          this.cd.markForCheck();
          this.cd.detectChanges();
        }
        else if (eventObj.name == "dialog_error") {

          this.dialogService.open(DialogWindowComponent, {
            autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: eventObj.message }
          });
        }
      });
      
    }, 100);
  }

  getTabList(footer: boolean) {
    let filteredTabList = [];

    this.global.getGlobalVar('generatorSettingsArray').forEach(tab => {

      // Always include all tabs for animation to work - let [disabled] handle visibility
      if (!footer) {
        if (!("footer" in tab) || !tab.footer)
          filteredTabList.push(tab);
      }
      else {
        if ("footer" in tab && tab.footer)
          filteredTabList.push(tab);
      }
    });

    return filteredTabList;
  }

  generateSeed(fromPatchFile: boolean = false, webRaceSeed: boolean = false, goalHintsConfirmed: boolean = false) {

    this.generateSeedButtonEnabled = false;
    this.seedString = this.seedString.trim().replace(/[^a-zA-Z0-9_-]/g, '');

    //console.log("fromPatchFile:", fromPatchFile);
    //console.log(this.global.generator_settingsMap);
    //console.log(this.global.generator_customColorMap);

    //Delay the generation if settings are currently locked to avoid race conditions.
    //Do this here so the goal hint confirmation dialog can be defined once for
    //Electron and Web
    if (this.global.getGlobalVar('electronAvailable')) {
      if (this.settingsLocked) {
        setTimeout(() => {
          this.generateSeed(fromPatchFile, webRaceSeed);
        }, 50);

        return;
      }
    }

    let goalErrorText = "The selected hint distribution includes the Goal hint type. This can drastically increase generation time for large multiworld seeds. Continue?";
    let noLogicErrorText = "You have selected No Logic. This can produce unbeatable seeds. Continue?";
    let goalDistros = this.global.getGlobalVar('generatorGoalDistros');

    if (!goalHintsConfirmed && goalDistros.indexOf(this.global.generator_settingsMap["hint_dist"]) > -1 && this.global.generator_settingsMap["world_count"] > 5) {
      this.dialogService.open(ConfirmationWindowComponent, {
        autoFocus: true, closeOnBackdropClick: false, closeOnEsc: false, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Goal Hint Warning", dialogMessage: goalErrorText }
      }).onClose.subscribe(confirmed => {
        //User acknowledged increased generation time for multiworld seeds with goal hints
        if (confirmed) {
          this.generateSeed(fromPatchFile, webRaceSeed, true);
        }
      });

      this.generateSeedButtonEnabled = true;
      this.cd.markForCheck();
      this.cd.detectChanges();

      return;
    }

    try {
      let noLogicConfirmed = localStorage.getItem("noLogicConfirmed");
      if ((!noLogicConfirmed || noLogicConfirmed == "false") && this.global.generator_settingsMap["logic_rules"] === "none") {
        this.dialogService.open(ConfirmationWindowComponent, {
          autoFocus: true, closeOnBackdropClick: false, closeOnEsc: false, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "No Logic Warning", dialogMessage: noLogicErrorText }
        }).onClose.subscribe(confirmed => {
          //User acknowledged possible unbeatability of no logic seeds
          if (confirmed) {
            localStorage.setItem("noLogicConfirmed", JSON.stringify(true));
            this.generateSeed(fromPatchFile, webRaceSeed, goalHintsConfirmed);
          }
        });

        this.generateSeedButtonEnabled = true;
        this.cd.markForCheck();
        this.cd.detectChanges();

        return;
      }
    } catch (e) {
      //Browser doesn't allow localStorage access
    }

    if (this.global.getGlobalVar('electronAvailable')) { //Electron

      //Hack: Fix Generation Count being None occasionally
      if (!this.global.generator_settingsMap["count"] || this.global.generator_settingsMap["count"] < 1)
        this.global.generator_settingsMap["count"] = 1;

      //Error if no patch file was entered in fromPatchFile mode
      if (fromPatchFile && !this.global.generator_settingsMap['Web.patch_file']) {
        this.dialogService.open(DialogWindowComponent, {
          autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: "You didn't enter a patch file!" }
        });

        this.generateSeedButtonEnabled = true;
        this.cd.markForCheck();
        this.cd.detectChanges();

        return;
      }

      //Hack: fromPatchFile forces generation count to 1 to avoid wrong count on progress window
      let generationCount = fromPatchFile ? 1 : this.global.generator_settingsMap["count"];

      let dialogRef = this.dialogService.open(ProgressWindowComponent, {
        autoFocus: true, closeOnBackdropClick: false, closeOnEsc: false, hasBackdrop: true, hasScroll: false, context: { dashboardRef: this, totalGenerationCount: generationCount }
      });

      this.global.generateSeedElectron(dialogRef && dialogRef.componentRef && dialogRef.componentRef.instance ? dialogRef.componentRef.instance : null, fromPatchFile, fromPatchFile == false && this.seedString.length > 0 ? this.seedString : "").then(res => {
        console.log('[Electron] Gen Success');

        this.generateSeedButtonEnabled = true;
        this.cd.markForCheck();
        this.cd.detectChanges();

        if (dialogRef && dialogRef.componentRef && dialogRef.componentRef.instance) {
          dialogRef.componentRef.instance.progressStatus = 1;
          dialogRef.componentRef.instance.progressPercentageCurrent = 100;
          dialogRef.componentRef.instance.progressPercentageTotal = 100;
          dialogRef.componentRef.instance.progressMessage = "Done. Enjoy.";
          dialogRef.componentRef.instance.progressErrorDetails = "";
          dialogRef.componentRef.instance.refreshLayout();
        }
      }).catch((err) => {
        console.log('[Electron] Gen Error');

        this.generateSeedButtonEnabled = true;
        this.cd.markForCheck();
        this.cd.detectChanges();

        if (dialogRef && dialogRef.componentRef && dialogRef.componentRef.instance) {
          dialogRef.componentRef.instance.progressStatus = -1;
          dialogRef.componentRef.instance.progressPercentageCurrent = 100;
          dialogRef.componentRef.instance.progressPercentageTotal = 100;
          dialogRef.componentRef.instance.progressMessage = err.short;
          dialogRef.componentRef.instance.progressErrorDetails = err.short === err.long ? "" : err.long;
          dialogRef.componentRef.instance.refreshLayout();
        }
      });
    }
    else { //Web

      this.global.generateSeedWeb(webRaceSeed, this.seedString.length > 0 ? this.seedString : "").then(seedID => {

        try {
          //Save last seed id in browser cache
          localStorage.setItem("lastSeed", seedID);

          //Save up to 10 seed ids in a sliding array in browser cache
          let seedHistory = localStorage.getItem("seedHistory");

          if (seedHistory == null || seedHistory.length < 1) { //First entry
            localStorage.setItem("seedHistory", JSON.stringify([seedID]));
          }
          else { //Update array (10 entries max)
            let seedHistoryArray = JSON.parse(seedHistory);

            if (seedHistoryArray && typeof (seedHistoryArray) == "object" && Array.isArray(seedHistoryArray)) {

              if (seedHistoryArray.length > 9) {
                seedHistoryArray.shift();
              }

              seedHistoryArray.push(seedID);
              localStorage.setItem("seedHistory", JSON.stringify(seedHistoryArray));
            }
          }
        } catch (e) {
          //Browser doesn't allow localStorage access
        }

        //Re-direct to seed (waiting) page
        let seedURL = (<any>window).location.protocol + "//" + (<any>window).location.host + "/seed/get?id=" + seedID;

        console.log('[Web] Success, will re-direct to:', seedURL);

        setTimeout(() => {
          (<any>window).location.href = seedURL;
        }, 250);

      }).catch((err) => {
        console.log('[Web] Gen Error:', err);

        if (err.status == 403) { //Rate Limited
          this.dialogService.open(DialogWindowComponent, {
            autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: "You may only generate one seed per minute to prevent spam!" }
          });
        }
        else if (err.hasOwnProperty('error_rom_in_plando')) {

          this.dialogService.open(ConfirmationWindowComponent, {
            autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Your ROM doesn't belong here!", dialogMessage: err.error_rom_in_plando }
          }).onClose.subscribe(confirmed => {
            if (confirmed) {

              let userFileConfig = err.type;

              if (userFileConfig.enablerSetting)
                this.global.generator_settingsMap[userFileConfig.enablerSetting] = false;

              this.global.generator_settingsMap[userFileConfig.setting] = "";

              if (userFileConfig.enablerSetting) {
                let setting = this.global.findSettingByName(userFileConfig.enablerSetting);
                this.checkVisibility(false, setting, this.findOption(setting.options, false));
              }

              this.generateSeed(fromPatchFile, webRaceSeed);
            }
          });
        }
        else if (err.hasOwnProperty('error_spoiler_log_disabled')) {

          this.dialogService.open(ConfirmationWindowComponent, {
            autoFocus: true, closeOnBackdropClick: false, closeOnEsc: false, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Spoiler Log Warning", dialogMessage: err.error_spoiler_log_disabled }
          }).onClose.subscribe(confirmed => {

            //Enable spoiler log if user accepts and save changed setting
            if (confirmed) {
              this.global.generator_settingsMap["create_spoiler"] = true;
              this.afterSettingChange();
            }

            this.generateSeed(fromPatchFile, webRaceSeed);
          });
        }
        else {
          this.dialogService.open(DialogWindowComponent, {
            autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: err.error && typeof (err.error) == "string" ? err.error : err.message }
          });
        }

        this.generateSeedButtonEnabled = true;
        this.cd.markForCheck();
        this.cd.detectChanges();
      });
    }
  }

  async cancelGeneration() { //Electron only

    return await this.global.cancelGenerateSeedElectron();
  }

  patchROM() { //Web only

    this.generateSeedButtonEnabled = false;

    console.log("Patch ROM");

    this.global.patchROMWeb().then(() => {

      //No actual callback, just deactivate button for 1 second
      setTimeout(() => {
        this.generateSeedButtonEnabled = true;
        this.cd.markForCheck();
        this.cd.detectChanges();
      }, 1000);

    }).catch ((err) => {
      console.log('[Web] Patching Error:', err);

      if (err.hasOwnProperty('error_rom_in_plando')) {

        this.dialogService.open(ConfirmationWindowComponent, {
          autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Your ROM doesn't belong here!", dialogMessage: err.error_rom_in_plando }
        }).onClose.subscribe(confirmed => {
          if (confirmed) {

            if (err.type == "cosmetic_file") {
              this.global.generator_settingsMap["enable_cosmetic_file"] = false;
              this.global.generator_settingsMap["cosmetic_file"] = "";

              let setting = this.global.findSettingByName("enable_cosmetic_file");
              this.checkVisibility(false, setting, this.findOption(setting.options, false));
            }

            this.patchROM();
          }
        });
      }
      else {
        this.dialogService.open(DialogWindowComponent, {
          autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: err.error && typeof (err.error) == "string" ? err.error : err.message }
        });
      }

      this.generateSeedButtonEnabled = true;
      this.cd.markForCheck();
      this.cd.detectChanges();
    });
  }

  copySettingsString() {

    this.global.copyToClipboard(this.global.generator_settingsMap["settings_string"]);
  }

  getSettingsString() {

    this.global.saveCurrentSettingsToFile();

    if (this.settingsLocked == true)
      this.settingsLocked = false;

    if (this.settingsBusy) { //Execute delayed task
      this.settingsBusy = false;
      this.afterSettingChange(this.settingsBusySaveOnly);
      this.settingsBusySaveOnly = true;
    }

    this.cd.markForCheck();
    this.cd.detectChanges();
  }

  //Not relevant for MMR
  importSettingsString() {

    this.generatorBusy = true;

    this.global.convertStringToSettings(this.global.generator_settingsMap["settings_string"]).then(res => {

      //console.log(res);

      this.global.applySettingsObject(res);
      this.global.saveCurrentSettingsToFile();

      this.recheckAllSettings("", false, true);

      this.generatorBusy = false;

      this.cd.markForCheck();
      this.cd.detectChanges();

    }).catch((err) => {

      this.generatorBusy = false;
      this.cd.markForCheck();
      this.cd.detectChanges();

      this.dialogService.open(DialogWindowComponent, {
        autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: "The entered settings string seems to be invalid!" }
      });
    });
  }

  browseForFile(setting: any) { //Electron only

    this.global.browseForFile(setting.file_types).then(res => {
      this.global.generator_settingsMap[setting.name] = res;
      this.cd.markForCheck();
      this.afterSettingChange();
    }).catch(err => {
      console.log(err);
    });
  }

  browseForDirectory(setting: any) { //Electron only

    this.global.browseForDirectory().then(res => {
      this.global.generator_settingsMap[setting.name] = res;
      this.cd.markForCheck();
      this.afterSettingChange();
    }).catch(err => {
      console.log(err);
    });
  }

  getFileFromFileSystemEntry(entry) { //Web only
    return new Promise<any>(function (resolve, reject) {
      entry.file((file) => resolve(file), (error) => reject(error));
    });
  }

  readDirectoryWeb(directory, fileList: any[]) { //Web only

    var self = this;

    //Recursively read directories
    return new Promise<any>(function (resolve, reject) {

      let directoryReader = directory.createReader();

      directoryReader.readEntries(async function (entries) {

        for (let entry of entries) {

          if (entry.isDirectory) {

            try {
              await self.readDirectoryWeb(entry, fileList);
            }
            catch (error) {
              reject(error);
              return;
            }
          }
          else if (entry.isFile) {

            try {
              fileList.push(await self.getFileFromFileSystemEntry(entry));
            }
            catch (error) {
              reject(error);
              return;
            }
          }
        }

        resolve(null);

      }, (error) => reject(error));
    });
  }

  onDirectoryDragOverWeb(event, setting: any) { //Web only

    event.preventDefault();

    if (!event.dataTransfer)
      return;

    //Check setting is enabled first
    if (!this.global.generator_settingsVisibilityMap[setting.name])
      return;

    event.dataTransfer.dropEffect = 'link'; //Change cursor to link icon when in input area
  }

  async onDirectoryDropWeb(event, setting: any) { //Web only

    event.preventDefault();

    if (!event.dataTransfer)
      return;

    //Only proceed if we have at least one file in the drop
    if (!event.dataTransfer.files || event.dataTransfer.files.length < 1)
      return;

    //Check setting is enabled first
    if (!this.global.generator_settingsVisibilityMap[setting.name])
      return;

    let items = event.dataTransfer.items;
    let entries = [];

    //Collect entries
    for (let item of items) {
      let entry = item.webkitGetAsEntry();

      if (entry)
        entries.push(entry);
    }

    //Build file list
    let fileList = [];
    let mainFolders = [];
    let mainFiles = [];

    try {
      for (let entry of entries) {

        if (entry.isDirectory) {
          mainFolders.push(entry.name);
          await this.readDirectoryWeb(entry, fileList);
        }
        else if (entry.isFile) {
          mainFiles.push(entry.name);
          fileList.push(await this.getFileFromFileSystemEntry(entry));
        }
      }
    }
    catch (error) {

      this.dialogService.open(DialogWindowComponent, {
        autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: error }
      });

      throw new Error(error);
    }

    //Create a sensible display name for any directory/file combination
    let nameParts = [];

    if (mainFolders.length > 0) {
      nameParts.push(mainFolders[0]);

      if (mainFolders.length > 1) {
        nameParts.push(`${mainFolders.length - 1} other folder${mainFolders.length > 2 ? "s" : ""}`);
      }
    }

    if (mainFiles.length > 0) {
      nameParts.push(mainFiles[0]);

      if (mainFiles.length > 1) {
        nameParts.push(`${mainFiles.length - 1} other file${mainFiles.length > 2 ? "s" : ""}`);
      }
    }

    let lastNamePart = nameParts.pop();
    let displayName: string;

    if (nameParts.length > 0)
      displayName = `${nameParts.join(", ")} and ${lastNamePart}`;
    else
      displayName = `${lastNamePart}`;

    //Set setting with display name and file list array
    this.global.generator_settingsMap[setting.name] = { name: displayName, fileList: fileList };
    this.cd.markForCheck();
    this.afterSettingChange(true);
  }

  onDirectorySelectedWeb(event, setting: any) { //Web only

    let dirPickerMode: boolean = this.global.getGlobalVar("webSupportDirectoryPicker");

    let fileList = event.currentTarget.files;

    if (!fileList || fileList.length < 1)
      return;

    let displayName: string;

    if (dirPickerMode) {
      displayName = fileList[0].webkitRelativePath.substr(0, fileList[0].webkitRelativePath.indexOf("/")); //Grab dropped folder name from first file path
    }
    else {
      //Create a sensible display name for any file combination
      let nameParts = [];

      nameParts.push(fileList[0].name);

      if (fileList.length > 1) {
        nameParts.push(`${fileList.length - 1} other file${fileList.length > 2 ? "s" : ""}`);
      }

      let lastNamePart = nameParts.pop();

      if (nameParts.length > 0)
        displayName = `${nameParts.join(", ")} and ${lastNamePart}`;
      else
        displayName = `${lastNamePart}`;
    }

    //Set setting
    this.global.generator_settingsMap[setting.name] = { name: displayName, fileList: fileList };
    this.cd.markForCheck();
    this.afterSettingChange(true);
  }

  browseForPatchFile() { //Electron only

    this.global.browseForFile([{ name: 'Patch File Archive', 'extensions': ['zpfz', 'zpf', 'patch'] }, { 'name': 'All Files', 'extensions': ['*'] }]).then(res => {
      this.global.generator_settingsMap['Web.patch_file'] = res;
      this.cd.markForCheck();
      this.afterSettingChange(true);
    }).catch(err => {
      console.log(err);
    });
  }

  changeFooterTabSelection(event) {

    let title = "";
    let value = false;

    if (event) {
      title = event.tabTitle;
      this.activeFooterTab = title;
    }
    else {
      title = this.activeFooterTab;
    }

    if (title === this.generateFromFileTabTitle) {
      value = true;
    }

    //Cache old visibility for later use to compute a change map
    let oldTabVisibility = { ...this.global.generator_tabsVisibilityMap };

    //Set new tab visibility
    this.global.generator_settingsMap['Web.generate_from_file'] = value;
    let setting = this.global.findSettingByName("Web.generate_from_file");
    this.checkVisibility(value, setting, this.findOption(setting.options, value));

    //Handle added/removed tabs gracefully
    let newTabVisibility = { ...this.global.generator_tabsVisibilityMap };
    let tabNameList = this.global.getGlobalVar('generatorSettingsArray').filter(tab => !tab.footer).map(tab => tab.name);
    this.manageTabVisibilityAnimation(tabNameList, this.computeTabVisibilityChangeData(oldTabVisibility, newTabVisibility));
    
    // Update scroll indicator visibility after tab change
    setTimeout(() => {
      this.checkScrollIndicatorVisibility();
    }, 100);
  }

  computeTabVisibilityChangeData(oldList, newList) {

    let changeList = {};
    let fullList = {};
    let settingsObj = this.global.getGlobalVar("generatorSettingsObj");

    for (let tab of Object.keys(oldList)) {

      if (settingsObj[tab].hide_when_disabled) {

        if (oldList[tab] && !newList[tab])
          changeList[tab] = false;
        else if (!oldList[tab] && newList[tab])
          changeList[tab] = true;
      }

      if (!settingsObj[tab].footer)
        fullList[tab] = newList[tab];
    }

    return { currentVisibility: fullList, visibilityChangeList: changeList };
  }

  manageTabVisibilityAnimation(tabNameList, visibilityChangeData) {

    const { currentVisibility, visibilityChangeList } = visibilityChangeData;

    let addedInOrder = this.getVisibilityChangedTabsCondition("linear", 1, true, tabNameList, currentVisibility, visibilityChangeList);
    let removedInOrder = this.getVisibilityChangedTabsCondition("linear", 1, false, tabNameList, currentVisibility, visibilityChangeList);
    let tabHeaderBar = this.tabSetNative.nativeElement.querySelector(".tabset");
    let rawTabs = tabHeaderBar.querySelectorAll("li");

    if (typeof addedInOrder == "object" && "tabs" in addedInOrder) {

      for (var i=0; i< addedInOrder.tabs.length; i++) {
        let tabNameForAdd = addedInOrder.tabs[i];
        let tabIndex = tabNameList.findIndex(elem => elem == tabNameForAdd);
        if (tabIndex != -1 && rawTabs[tabIndex] && rawTabs[tabIndex].classList.contains("collapsed")) {
          rawTabs[tabIndex].classList.add("decollapsing");
          rawTabs[tabIndex].classList.remove("collapsed");

          setTimeout(() => {
            rawTabs[tabIndex].classList.remove("decollapsing");
          }, 900);
        }
      }
    }

    if (typeof removedInOrder == "object" && "tabs" in removedInOrder) {

      for (var i=0; i< removedInOrder.tabs.length; i++) {
        let tabNameForRemoval = removedInOrder.tabs[i];
        let tabIndex = tabNameList.findIndex(elem => elem == tabNameForRemoval);
        if (tabIndex != -1 && rawTabs[tabIndex]) {
          rawTabs[tabIndex].classList.add("collapsed");
        }
      }
    }
  }

  getVisibilityChangedTabsCondition(order, startIndex, becameVisible, tabNameList, currentVisibility, visibilityChangeList) {

    let data = { tabs: [], counter: 0 };

    switch (order) {

      case "linear": {

        for (let i = startIndex; startIndex < tabNameList.length; i++) {

          let tab = tabNameList[i];

          if (tab in visibilityChangeList && visibilityChangeList[tab] == becameVisible) {

            data.tabs.push(tab);
            data.counter++;
          }
          else if (currentVisibility[tab] == !becameVisible) {
            continue;
          }
          else {
            return data;
          }
        }

        return data;
      }
      default: {
        return 0;
      }
    }
  }

  calculateRowHeight(listRef: MatGridList, tab: any) {

    let columnCount = this.verifyColumnCount(listRef.cols);
    let absoluteRowCount = 0;
    let absoluteRowIndex = 0;
    let gutterPercentage = 0;

    for (let i = 0; i < tab.sections.length; i++) {

      let section = tab.sections[i];

      absoluteRowIndex += this.getColumnWidth(null, tab.sections, i, tab.sections.length, section['col_span'] ? section['col_span'] : 0, columnCount);

      if (absoluteRowIndex >= columnCount) {
        absoluteRowIndex = 0;

        let columnRowCount = this.getColumnHeight(null, section, columnCount);
        absoluteRowCount += columnRowCount;
        gutterPercentage += (columnRowCount * 0.5);
      }
    }

    let heightPerRow = (100 - gutterPercentage) / absoluteRowCount;
    return heightPerRow + "%";
  }

  getColumnCount(tileRef: MatGridTile) {
    const columnCount = this.verifyColumnCount(tileRef._gridList.cols);
    return columnCount;
  }

  verifyColumnCount(count: number) {

    if (count != 1 && count != 2 && count != 12) {
      return 1;
    }

    return count;
  }

  getColumnWidth(tileRef: MatGridTile, sections: any, index: number, length: number, colSpan: number = 0, columnCount: number = -1) {

    if (columnCount == -1) {
      columnCount = this.getColumnCount(tileRef);
    }

    //col_span override
    if (colSpan > 0)
      if (columnCount == 12) //Treat 12x1 column setup as 4x1 internally
        return 4 >= colSpan ? colSpan * 3 : 12;
      else
        return columnCount >= colSpan ? colSpan : columnCount;

    //Account for col_span override sections in a special way
    let searchIndex = 0;
    let sectionIndex = index;

    sections.forEach(section => {

      if (section.col_span > 0) {

        let sectionsToAdd;

        if (columnCount == 12) //Treat 12x1 column setup as 4x1 internally
          sectionsToAdd = (4 >= section.col_span ? section.col_span : 4) - 1;
        else
          sectionsToAdd = (columnCount >= section.col_span ? section.col_span : columnCount) - 1;

        length += sectionsToAdd;

        if (searchIndex < sectionIndex)
          index += sectionsToAdd;
      }

      searchIndex++;
    });

    if (columnCount == 2 && length % 2 != 0 && index == length - 1) { //If an odd number of cols exist with 2 cols length, make last col take the entire row size
      return 2;
    }
    else if (columnCount == 3 && length % 3 != 0 && index == length - 1) { //Make final column size 3 if it is on its row alone. If there are 2 columns, make the last column size 2
      if (index % 3 == 0)
        return 3;
      else
        return 2;
    }
    else if (columnCount == 4 && length % 4 != 0) { //Make final column size 4 if it is on its row alone. If there are 2 columns, make both size 2. Else just make the final one size 2

      if (index == length - 1) {
        if (index % 4 == 0)
          return 4;
        else if (index % 4 == 1)
          return 2;
        else
          return 2;
      }
      else if (index + 1 == length - 1) {
        if (index % 4 == 0)
          return 2;
      }
    }
    else if (columnCount == 12) { //We limit max sections per line at 4 in a 12x1 column setup

      if (index == length - 1) {
        if (index % 4 == 0)
          return 12;
        else if (index % 4 == 1)
          return 6;
        else if (index % 4 == 2)
          return 4;
      }
      else if (index + 1 == length - 1) {
        if (index % 4 == 0)
          return 6;
        else if (index % 4 == 1)
          return 4;
      }
      else if (index + 2 == length - 1) {
        if (index % 4 == 0)
          return 4;
      }

      return 3;
    }

    return 1;
  }

  getColumnHeight(tileRef: MatGridTile, section: any, columnCount: number = -1) {
    if (columnCount == -1) {
      columnCount = this.getColumnCount(tileRef);
    }

    let spanIndex = 0;

    switch (columnCount) {
      case 2:
        spanIndex = 1;
        break;
      case 12:
        spanIndex = 2;
        break;
    }

    // Get the base row span from the section configuration
    let baseRowSpan = section.row_span[spanIndex];
      
    return baseRowSpan;
  }

  findOption(options: any, optionName: any) {
    return options.find(option => { return option.name == optionName });
  }

  getVariableType(variable: any) {
    return typeof (variable);
  }

  getNextVisibleSetting(settings: any, startingIndex: number) {

    if (settings.length > startingIndex) {
      for (let i = startingIndex; i < settings.length; i++) {
        let setting = settings[i];

        if (this.global.generator_settingsVisibilityMap[setting.name] || !setting.hide_when_disabled)
          return setting;
      }
    }

    return null;
  }

  /**
   * Check if a section is wide enough to accommodate two settings side by side
   * If not, we should ignore the no_line_break setting to prevent uncontrolled wrapping
   */
  shouldRespectNoLineBreak(refEl: any, setting: any, sectionSettings: any[] = null, itemIndex: number = null): boolean {
    if (!refEl || !setting || !sectionSettings || itemIndex === null) {
      return true;
    }

    // Handle no_inner_line_break settings differently
    if (setting.no_inner_line_break) {
      return this.shouldRespectNoInnerLineBreak(refEl, setting, sectionSettings, itemIndex);
    }

    // Handle regular no_line_break settings
    if (!setting.no_line_break) {
      return true;
    }

    const cacheKey = `${setting.name}_${this.getSectionWidth(refEl)}_${this.getColumnCount(refEl)}`;
    
    if (this.noLineBreakCache.has(cacheKey)) {
      return this.noLineBreakCache.get(cacheKey)!;
    }

    const sectionWidth = this.getSectionWidth(refEl);
    const columnCount = this.getColumnCount(refEl);
    const shouldRespect = this.calculateNoLineBreakDecision(refEl, setting, sectionSettings, itemIndex, sectionWidth, columnCount);
    
    this.noLineBreakCache.set(cacheKey, shouldRespect);
    return shouldRespect;
  }

  private getInnerSettingsForDictionary(parentSetting: any, sectionSettings: any[], itemIndex: number): any[] {
    const innerSettings: any[] = [];
    
    // For dictionary settings with no_inner_line_break, the inner settings are the setting.keys array
    if (parentSetting.keys && Array.isArray(parentSetting.keys)) {
      // Convert the keys to a format that can be processed by the width calculation
      for (const keySetting of parentSetting.keys) {
        const mockInnerSetting = {
          name: keySetting.name,
          text: keySetting.text,
          type: parentSetting.inner_type || 'Numberinput',
          tooltip: keySetting.tooltip,
          no_line_break: true
        };
        innerSettings.push(mockInnerSetting);
      }
    } else {
      // Fallback: Find all inner settings that belong to this dictionary by name
      for (let i = 0; i < sectionSettings.length; i++) {
        if (sectionSettings[i] && sectionSettings[i].name && sectionSettings[i].name.startsWith(parentSetting.name + '.')) {
          innerSettings.push(sectionSettings[i]);
        }
      }
    }
    
    return innerSettings;
  }
  
  private calculateInnerSettingsWidthNeeded(innerSettings: any[], refEl: any): number {
    let totalWidth = 0;
    
    for (const innerSetting of innerSettings) {
      const width = this.estimateSettingWidth(innerSetting, refEl);
      totalWidth += width + 20;
    }
  
    totalWidth += 40;
    
    return totalWidth;
  }

  shouldRespectNoInnerLineBreak(refEl: any, setting: any, sectionSettings: any[] = null, itemIndex: number = null): boolean {
    if (!refEl || !setting || !sectionSettings || itemIndex === null) {
      return true;
    }

    // Check if the current setting is a Dictionary with no_inner_line_break
    if (setting.type === 'Dictionary' && setting.no_inner_line_break) {
      
      const sectionWidth = this.getSectionWidth(refEl);
      const columnCount = this.getColumnCount(refEl);
      
      
      // If width is 0, don't cache and assume unwrapped (this happens on initial load)
      if (sectionWidth === 0) {
        return true; // Assume unwrapped when we don't have width data yet
      }
      
      // Use the same caching approach as no_line_break, but with valid width data
      const cacheKey = `inner_${setting.name}_${sectionWidth}_${columnCount}`;
      
      if (this.noLineBreakCache.has(cacheKey)) {
        const cachedResult = this.noLineBreakCache.get(cacheKey)!;
        return cachedResult;
      }
      
      // Use the existing calculateNoLineBreakDecision logic for consistency
      const shouldRespect = this.calculateNoLineBreakDecision(refEl, setting, sectionSettings, itemIndex, sectionWidth, columnCount);
      
      
      // Only cache results when we have valid width data
      this.noLineBreakCache.set(cacheKey, shouldRespect);
      
      return shouldRespect;
    }
    return true;
  }

  private getSectionWidth(refEl: any): number {
    
    let gridTileElement: HTMLElement | null = null;
    
    if (refEl._element && refEl._element.nativeElement) {
      gridTileElement = refEl._element.nativeElement;
    } else if (refEl.nativeElement) {
      gridTileElement = refEl.nativeElement;
    } else if (refEl.closest) {
      gridTileElement = refEl;
    }
    
    if (!gridTileElement) {
      return 0;
    }
    
    // The issue is that we're getting a child element, not the actual grid tile container
    // We need to find the mat-grid-tile element that contains this
    let actualGridTile: HTMLElement | null = gridTileElement;
    
    // Walk up the DOM tree to find the actual mat-grid-tile element
    while (actualGridTile && !actualGridTile.classList.contains('mat-grid-tile')) {
      actualGridTile = actualGridTile.parentElement;
    }
    
    if (actualGridTile) {
      gridTileElement = actualGridTile;
    }
    
    // Also try to find the parent mat-grid-list to get the total width
    let gridListElement: HTMLElement | null = gridTileElement;
    while (gridListElement && !gridListElement.classList.contains('mat-grid-list')) {
      gridListElement = gridListElement.parentElement;
    }
    
    if (gridListElement) {
      const gridListRect = gridListElement.getBoundingClientRect();
    }
    
    // Try to get the grid list width from the refEl if it has _gridList
    if (refEl._gridList && refEl._gridList._element && refEl._gridList._element.nativeElement) {
      const gridListNative = refEl._gridList._element.nativeElement;
      const gridListRect = gridListNative.getBoundingClientRect();
      
      if (gridListRect.width > 0) {
        // Calculate width based on colspan (6 out of 12 columns)
        const columnWidth = gridListRect.width / 12;
        const tileWidth = columnWidth * 6; // colspan = 6
        return tileWidth;
      }
    }
    
    // Try getBoundingClientRect first
    let tileRect = gridTileElement.getBoundingClientRect();
    
    // If width is 0, try alternative methods
    if (tileRect.width === 0) {
      
      // Try offsetWidth
      if (gridTileElement.offsetWidth > 0) {
        return gridTileElement.offsetWidth;
      }
      
      // Try clientWidth
      if (gridTileElement.clientWidth > 0) {
        return gridTileElement.clientWidth;
      }
      
      // Try computed style - this might give us the percentage
      const computedStyle = window.getComputedStyle(gridTileElement);
      const width = computedStyle.width;
      
      if (width && width !== 'auto' && width !== '0px') {
        // Check if it's a percentage
        if (width.includes('%')) {
          const percentage = parseFloat(width);
          
          // Try to get the parent grid list width to calculate the actual width
          if (gridListElement) {
            const gridListRect = gridListElement.getBoundingClientRect();
            const actualWidth = (percentage / 100) * gridListRect.width;
            if (actualWidth > 0) {
              return actualWidth;
            }
          }
          
          // If grid list width is also 0, try to estimate based on viewport width
          // This is a fallback for when the grid hasn't fully rendered yet
          const viewportWidth = window.innerWidth;
          if (viewportWidth > 0) {
            // Estimate: if we're in a 2-column layout, each section gets roughly 50% of viewport
            // But account for margins, padding, and the fact that we're in a tab container
            const estimatedWidth = (percentage / 100) * (viewportWidth * 0.8); // 80% of viewport as estimate
            return estimatedWidth;
          }
        } else {
          const numericWidth = parseFloat(width);
          return numericWidth;
        }
      }
      
      // Try parent element width as fallback
      const parentElement = gridTileElement.parentElement;
      if (parentElement) {
        const parentRect = parentElement.getBoundingClientRect();
        return parentRect.width;
      }
    }
    
    return tileRect.width;
  }

  private calculateNoLineBreakDecision(refEl: any, setting: any, sectionSettings: any[], itemIndex: number, sectionWidth: number, columnCount: number): boolean {
    if (sectionWidth < 500) {
      return false;
    }

    if (columnCount <= 2) {
      return false;
    }

    // Find all settings in the same row that have no_line_break
    const rowSettings = this.getRowSettings(sectionSettings, itemIndex);
    
    if (rowSettings.length <= 1) {
      return true; // Single setting or no no_line_break settings
    }

    // Calculate total width needed for the row
    const totalWidthNeeded = this.calculateRowWidthNeeded(rowSettings, refEl);
    
    // Add some buffer for spacing and margins
    const bufferWidth = 20;
    const totalNeeded = totalWidthNeeded + bufferWidth;

    // Check if there's enough space
    const hasEnoughSpace = sectionWidth >= totalNeeded;
    
    return hasEnoughSpace;
  }

  private getRowSettings(sectionSettings: any[], currentIndex: number): any[] {
    const rowSettings: any[] = [];
    
    // Find the start of the current row
    let rowStartIndex = currentIndex;
    while (rowStartIndex > 0 && sectionSettings[rowStartIndex - 1].no_line_break) {
      rowStartIndex--;
    }
    
    // Collect all settings in the same row
    let i = rowStartIndex;
    while (i < sectionSettings.length && sectionSettings[i].no_line_break) {
      rowSettings.push(sectionSettings[i]);
      i++;
    }
    
    // Also include the current setting if it's not already in the row
    if (!rowSettings.includes(sectionSettings[currentIndex])) {
      rowSettings.push(sectionSettings[currentIndex]);
    }
    
    // If we still don't have any settings, try a broader approach
    if (rowSettings.length === 0) {
      // Look for any nearby settings that might be part of the same logical row
      const nearbyRange = 2; // Check 2 settings before and after
      for (let j = Math.max(0, currentIndex - nearbyRange); j <= Math.min(sectionSettings.length - 1, currentIndex + nearbyRange); j++) {
        if (sectionSettings[j].no_line_break && !rowSettings.includes(sectionSettings[j])) {
          rowSettings.push(sectionSettings[j]);
        }
      }
    }
    
    return rowSettings;
  }

  private calculateRowWidthNeeded(rowSettings: any[], refEl: any): number {
    let totalWidth = 0;
    
    for (const rowSetting of rowSettings) {
      const settingWidth = this.estimateSettingWidth(rowSetting, refEl);
      totalWidth += settingWidth;
      
      // Add spacing between settings (10px as requested)
      if (rowSetting !== rowSettings[rowSettings.length - 1]) {
        totalWidth += 10;
      }
    }
    
    return totalWidth;
  }

  private estimateSettingWidth(setting: any, refEl: any): number {
    let totalWidth = 0;
    
    try {
      const settingElements = this.findSettingElements(refEl, setting);
      
      if (settingElements.label && settingElements.input) {
        const labelRect = settingElements.label.getBoundingClientRect();
        const inputRect = settingElements.input.getBoundingClientRect();
        
        totalWidth = labelRect.width + 10 + inputRect.width;
        return totalWidth;
      }
    } catch (error) {
      // Fallback to estimates if measurement fails
    }
    
    // Fallback estimates
    const labelWidth = this.estimateLabelWidth(setting);
    const inputWidth = this.estimateInputWidth(setting);
    
    totalWidth = labelWidth + 10 + inputWidth;
    return totalWidth;
  }

  private getGridTileElement(refEl: any): HTMLElement | null {
    if (refEl._element && refEl._element.nativeElement) {
      return refEl._element.nativeElement;
    } else if (refEl.nativeElement) {
      return refEl.nativeElement;
    } else if (refEl.closest) {
      return refEl;
    }
    return null;
  }

  private findSettingElements(gridTileElement: HTMLElement, setting: any): { label: HTMLElement | null, input: HTMLElement | null } {
    try {
      let labelElement: HTMLElement | null = null;
      let inputElement: HTMLElement | null = null;
      
      // Try different selectors for the label
      const labelSelectors = [
        '.comboBoxLabel',
        '.numberTextInputLabel',
        '.ioInputLabel',
        '.settingButton'
      ];
      
      for (const selector of labelSelectors) {
        const element = gridTileElement.querySelector(selector) as HTMLElement;
        if (element) {
          labelElement = element;
          break;
        }
      }
      
      // Try different selectors for the input
      const inputSelectors = [
        'input',
        'nb-select',
        'button',
        '.settingButton'
      ];
      
      for (const selector of inputSelectors) {
        const element = gridTileElement.querySelector(selector) as HTMLElement;
        if (element) {
          inputElement = element;
          break;
        }
      }
      
      return { label: labelElement, input: inputElement };
    } catch (error) {
      return { label: null, input: null };
    }
  }

  private estimateLabelWidth(setting: any): number {
    // Conservative estimates for different setting types
    const baseWidth = 120; // Base width for most labels
    
    // Adjust based on setting name length (rough estimate)
    const nameLength = setting.name?.length || 0;
    const charWidth = 8; // Rough estimate per character
    
    return Math.max(baseWidth, nameLength * charWidth);
  }

  private estimateInputWidth(setting: any): number {
    // Conservative estimates for different input types
    switch (setting.type) {
      case 'Combobox':
      case 'Indexed Combobox':
        return 150;
      case 'Text':
        return 120;
      case 'Number':
        return 80;
      case 'Checkbutton':
        return 20;
      case 'Radiobutton':
        return 20;
      case 'Color':
        return 60;
      case 'Button':
        return 100;
      case 'Numberinput':
        return 80;
      case 'Dictionary':
        // For dictionary settings, estimate based on inner type
        if (setting.inner_type) {
          switch (setting.inner_type) {
            case 'Numberinput':
              return 80;
            case 'Combobox':
              return 150;
            case 'Text':
              return 120;
            default:
              return 120;
          }
        }
        return 120;
      default:
        return 120;
    }
  }

  // Method to invalidate cache when dialog size changes
  invalidateNoLineBreakCache(): void {
    this.noLineBreakCache.clear();
  }

  // Method to calculate optimal height for sections with no_inner_line_break settings
  calculateOptimalHeightForNoInnerLineBreak(setting: any, refEl: any): number {
    if (!setting.no_inner_line_break || !setting.keys || !Array.isArray(setting.keys)) {
      return 0; // No height adjustment needed
    }

    const sectionWidth = this.getSectionWidth(refEl);
    const columnCount = this.getColumnCount(refEl);
    
    // If we should respect no_inner_line_break, no height adjustment needed
    if (this.shouldRespectNoLineBreak(refEl, setting, null, 0)) {
      return 0;
    }

    // Calculate how many rows the inner settings will need when wrapped
    const innerSettings = this.getInnerSettingsForDictionary(setting, [], 0);
    if (innerSettings.length === 0) {
      return 0;
    }

    // Estimate the width needed for all inner settings
    const totalWidthNeeded = this.calculateInnerSettingsWidthNeeded(innerSettings, refEl);
    
    // If there's enough space, no height adjustment needed
    if (totalWidthNeeded <= sectionWidth) {
      return 0;
    }

    // Calculate how many rows will be needed
    const settingsPerRow = Math.max(1, Math.floor(sectionWidth / (totalWidthNeeded / innerSettings.length)));
    const rowsNeeded = Math.ceil(innerSettings.length / settingsPerRow);
    
    // Calculate height adjustment (each row needs about 36px + 8px spacing)
    const baseRowHeight = 44; // 36px for the setting + 8px spacing
    const heightAdjustment = (rowsNeeded - 1) * baseRowHeight;
    
    return heightAdjustment;
  }


  // Method to handle window resize and recalculate no line break decisions
  onWindowResize(): void {
    // Invalidate the cache when window is resized
    this.invalidateNoLineBreakCache();
        
    // Trigger change detection to update the UI
    this.cd.detectChanges();
  }

  // Method to handle dialog resize (for Electron apps)
  onDialogResize(): void {
    // Invalidate the cache when dialog is resized
    this.invalidateNoLineBreakCache();
        
    // Trigger change detection to update the UI
    this.cd.detectChanges();
  }

  
  checkVisibility(newValue: any, setting: any, option: any = null, refColorPicker: HTMLInputElement = null, disableOnly: boolean = false, noValueChange: boolean = false) {

    if (!disableOnly && !noValueChange)
      this.afterSettingChange();

    //Array of settings that should have its visibility altered
    var targetSettings = [];

    if (setting["type"] === "Checkbutton" || setting["type"] === "Radiobutton" || setting["type"] === "Combobox" || setting["type"] === "SearchBox") {

      //Due to dictionaries having an inner type that differs from the actual type (aka Dictionary), we ensure the checking setting is actually of this main type before proceeding
      //Dictionaries are currently not supported in this function and so their inner types should not trigger visibility checks
      let actualSetting = this.global.findSettingByName(setting.name);

      if (actualSetting && actualSetting.type == setting.type) {

        let value = (typeof (newValue) == "object") && ("value" in newValue) ? newValue.value : newValue;

        //Open color picker if custom color is selected
        if (refColorPicker && value == "Custom Color") {

          if (this.global.generator_customColorMap[setting.name].length < 1)
            this.global.generator_customColorMap[setting.name] = "#ffffff";

          refColorPicker.click();
        }

        if (setting["type"] === "SearchBox") { //Special handling for type "SearchBox"

          let optionsSelected = value && typeof (value) == "object" && Array.isArray(value) && value.length > 0;

          //Identify the relevant options array
          let optionsToSearch = null;

          if (setting.linked_setting) {

            //Find correct options array based on linked setting value
            if (setting.linked_setting in this.global.generator_settingsMap) {
              optionsToSearch = setting.options[this.global.generator_settingsMap[setting.linked_setting]];

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

          //First build a complete list consisting of every option that hasn't been selected yet with a true value
          optionsToSearch.forEach(optionToAdd => {

            //Ensure option isn't selected before adding it
            if (optionsSelected) {
              let alreadySelected = value.find(selectedItem => selectedItem.name == optionToAdd.name);

              if (alreadySelected)
                return;
            }

            targetSettings.push({ target: optionToAdd, value: true });
          });

          //Push every selected option last with a false value
          if (optionsSelected) {
            value.forEach(selectedItem => {
              targetSettings.push({ target: selectedItem, value: false });
            });
          }
        }
        else { //Every other settings type

          //Build list of options
          setting.options.forEach(optionToAdd => {

            if (optionToAdd.name === option.name) //Add currently selected item last for priority
              return;

            targetSettings.push({ target: optionToAdd, value: optionToAdd.name != value });
          });

          targetSettings.push({ target: option, value: false }); //Selected setting uses false as it can disable settings now
        }
      }
    }

    //Handle activations/deactivations
    this.toggleVisibility(targetSettings, disableOnly, setting.name);
  }

  toggleVisibility(targetSettings: any, disableOnly: boolean, skipSetting: string = "") {

    var triggeredChange = false;

    targetSettings.forEach(setting => {

      let targetSetting = setting.target;
      let targetValue = setting.value;

      if (disableOnly && targetValue == true)
        return;

      if (this.executeVisibilityForSetting(targetSetting, targetValue))
        triggeredChange = true;
    });

    //Re-run function with every single setting to ensure integrity (nothing gets re-activated when it shouldn't)
    if (triggeredChange) {
      this.recheckAllSettings(skipSetting);
    }
  }

  executeVisibilityForSetting(targetSetting: any, targetValue: boolean) {

    let triggeredChange = false;

    if ("controls_visibility_tab" in targetSetting) {
      this.triggerTabVisibility(targetSetting, targetValue);
    }

    if ("controls_visibility_setting" in targetSetting) {
      triggeredChange = this.triggerSettingVisibility(targetSetting, targetValue, triggeredChange);
    }

    if ("controls_visibility_section" in targetSetting) {
      triggeredChange = this.triggerSectionVisibility(targetSetting, targetValue, triggeredChange);
    }

    return triggeredChange;
  }

  private triggerTabVisibility(targetSetting: any, targetValue: boolean) {
      //console.log(targetSetting, setting);

      targetSetting["controls_visibility_tab"].split(",").forEach(tab => {

        //Ignore tabs that don't exist in this specific app
        if (!(tab in this.global.generator_tabsVisibilityMap))
          return;

        this.global.generator_tabsVisibilityMap[tab] = targetValue;

        // Apply/remove collapsed class for animation
        let tabNameList = this.global.getGlobalVar('generatorSettingsArray').filter(tab => !tab.footer).map(tab => tab.name);
        let tabIndex = tabNameList.findIndex(elem => elem == tab);
        
        // Only apply animation if tabSetNative is available
        if (this.tabSetNative && this.tabSetNative.nativeElement) {
          let tabHeaderBar = this.tabSetNative.nativeElement.querySelector(".tabset");
          let rawTabs = tabHeaderBar.querySelectorAll("li");
          
          if (tabIndex != -1 && rawTabs[tabIndex]) {
            if (!targetValue) {
              // Hiding tab - add collapsed class (no animation on initial load)
              rawTabs[tabIndex].classList.add("collapsed");
            } else {
              // Showing tab - remove collapsed class and add decollapsing animation
              if (rawTabs[tabIndex].classList.contains("collapsed")) {
                rawTabs[tabIndex].classList.add("decollapsing");
                rawTabs[tabIndex].classList.remove("collapsed");
                
                setTimeout(() => {
                  rawTabs[tabIndex].classList.remove("decollapsing");
                }, 900);
              }
            }
          }
        }

             //Kick user out of active tab and go back to root if it gets disabled here
        if (!targetValue && this.global.getGlobalVar("generatorSettingsObj")) {

          if (this.activeTab == this.global.getGlobalVar("generatorSettingsObj")[tab].text) {
            //console.log("Kick user out of tab");
            this.tabSet.selectTab(this.tabSet.tabs.first);
          }
        }
        
        // Update scroll indicator visibility after tab visibility change
        setTimeout(() => {
          this.checkScrollIndicatorVisibility();
        }, 100);
      });
    }

  private triggerSectionVisibility(targetSetting: any, targetValue: boolean, triggeredChange: boolean) {
      targetSetting["controls_visibility_section"].split(",").forEach(section => {

        let targetSection = null;

        //Find section
        for (let i = 0; i < this.global.getGlobalVar('generatorSettingsArray').length; i++) {
          let tab = this.global.getGlobalVar('generatorSettingsArray')[i];

          for (let n = 0; n < tab.sections.length; n++) {

            if (tab.sections[n].name === section) {
              targetSection = tab.sections[n];
              break;
            }
          }

          if (targetSection)
            break;
        }

        //Disable/Enable entire section
        if (targetSection) {

          targetSection.settings.forEach(setting => {

            //Ignore settings that don't exist in this specific app
            if (!(setting.name in this.global.generator_settingsVisibilityMap))
              return;

            let enabledChildren = false;

            //If a setting gets disabled, re-enable all the settings that this setting caused to deactivate. The later full check will fix any potential issues
            if (targetValue == false && this.global.generator_settingsVisibilityMap[setting.name] == true) {
              enabledChildren = this.clearDeactivationsOfSetting(setting);
            }

            if ((targetValue == true && this.global.generator_settingsVisibilityMap[setting.name] == false) || (enabledChildren)) //Only trigger change if a (sub) setting gets re-enabled
              triggeredChange = true;

            this.global.generator_settingsVisibilityMap[setting.name] = targetValue;
          });
        }
      });

      return triggeredChange;
    }

  private triggerSettingVisibility(targetSetting: any, targetValue: boolean, triggeredChange: boolean) {
      targetSetting["controls_visibility_setting"].split(",").forEach(setting => {

        //Ignore settings that don't exist in this specific app
        if (!(setting in this.global.generator_settingsVisibilityMap))
          return;

        let enabledChildren = false;

        if (targetValue == false && this.global.generator_settingsVisibilityMap[setting] == true) {
          enabledChildren = this.clearDeactivationsOfSetting(this.global.findSettingByName(setting));
        }

        if ((targetValue == true && this.global.generator_settingsVisibilityMap[setting] == false) || (enabledChildren)) //Only trigger change if a (sub) setting gets re-enabled
          triggeredChange = true;

        this.global.generator_settingsVisibilityMap[setting] = targetValue;
      });

    return triggeredChange;
  }

  clearDeactivationsOfSetting(setting: any) {

    let enabledChildren = false;

    if (setting["type"] === "Checkbutton" || setting["type"] === "Radiobutton" || setting["type"] === "Combobox" || setting["type"] === "SearchBox") {

      if (setting["type"] === "SearchBox") { //Special handling for type "SearchBox"

        //Get every option currently added to the list
        if (this.global.generator_settingsMap[setting.name] && this.global.generator_settingsMap[setting.name].length > 0) {
          this.global.generator_settingsMap[setting.name].forEach(selectedItem => {
            if (this.executeVisibilityForSetting(selectedItem, true))
              enabledChildren = true;
          });
        }
      }
      else { //Every other settings type
        //Get currently selected option
        let currentOption = this.findOption(setting.options, this.global.generator_settingsMap[setting.name]);

        if (currentOption) {
          if (this.executeVisibilityForSetting(currentOption, true))
            enabledChildren = true;
        }
      }
    }

    return enabledChildren;
  }

  recheckAllSettings(skipSetting: string = "", disableOnly: boolean = true, noValueChange: boolean = false) {

    this.global.getGlobalVar('generatorSettingsArray').forEach(tab => tab.sections.forEach(section => section.settings.forEach(checkSetting => {

      if (skipSetting && checkSetting.name === skipSetting || !this.global.generator_settingsVisibilityMap[checkSetting.name]) //Disabled settings can not alter visibility anymore
        return;

      if (checkSetting["type"] === "Checkbutton" || checkSetting["type"] === "Radiobutton" || checkSetting["type"] === "Combobox" || checkSetting["type"] === "SearchBox") {

        if (checkSetting["type"] === "SearchBox") { //Special handling for type "SearchBox"
          //Call checkVisibility right away which will perform a full list check
          this.checkVisibility({ value: this.global.generator_settingsMap[checkSetting.name] }, checkSetting, null, null, disableOnly, noValueChange);
        }
        else { //Every other settings type

          let targetOption = checkSetting.options.find(option => {

            if (option.name === this.global.generator_settingsMap[checkSetting.name])
              return true;

            return false;
          });

          if (targetOption) {
            this.checkVisibility({ value: this.global.generator_settingsMap[checkSetting.name] }, checkSetting, targetOption, null, disableOnly, noValueChange);
          }
        }
      }
    })));
  }

  revertToPriorValue(settingName: string, forceChangeDetection: boolean, forcePriorValue: any = null, subSettingName: string = null) {

    let priorValue = forcePriorValue !== undefined ? forcePriorValue : subSettingName ? this.global.generator_settingsMap[settingName][subSettingName] : this.global.generator_settingsMap[settingName];

    setTimeout(() => {

      if (subSettingName)
        this.global.generator_settingsMap[settingName][subSettingName] = priorValue;
      else
        this.global.generator_settingsMap[settingName] = priorValue;

      if (forceChangeDetection)
        this.cd.markForCheck();
    }, 0);
  }

  inputFocusIn(settingName: string, subSettingName: string = null) {

    //Save current value on entering any input field
    if (subSettingName)
      this.inputOldValue = this.global.generator_settingsMap[settingName][subSettingName];
    else
      this.inputOldValue = this.global.generator_settingsMap[settingName];
  }

  inputFocusOut(settingName: string, saveOnly: boolean, forceNewValue: any = undefined, subSettingName: string = null) {

    let newValue = forceNewValue !== undefined ? forceNewValue : subSettingName ? this.global.generator_settingsMap[settingName][subSettingName] : this.global.generator_settingsMap[settingName];

    //Only update if the value actually changed
    if (newValue != this.inputOldValue) {
      setTimeout(() => {

        if (subSettingName)
          this.global.generator_settingsMap[settingName][subSettingName] = newValue;
        else
          this.global.generator_settingsMap[settingName] = newValue;

        this.afterSettingChange(saveOnly);
      }, 0);
    }
  }

  numberInputFocusOut(setting: object, forceAdjust: boolean, subSetting: object = null) {

    let newValue = subSetting ? this.global.generator_settingsMap[setting["name"]][subSetting["name"]] : this.global.generator_settingsMap[setting["name"]];
    let settingName = setting["name"];

    //Skip checks if null value with nullable allowed
    if (setting["nullable"] === true && (newValue === undefined || newValue === null || newValue === "")) {
      this.inputFocusOut(settingName, false, undefined, subSetting ? subSetting["name"] : null);
      return;
    }

    let oldValue = this.inputOldValue;

    //Make sure we don't push any null values when they are not valid 
    if (!setting["nullable"] && (oldValue === undefined || oldValue === null || oldValue === ""))
      oldValue = setting["min"];

    //Existence check
    if (!newValue || newValue.length == 0) {

      if (forceAdjust)
        this.revertToPriorValue(settingName, true, oldValue, subSetting ? subSetting["name"] : null);

      return;
    }

    //Number check
    if (setting["is_decimal"]) {

      //Decimals
      if (Number(parseFloat(newValue)) != newValue) {

        if (forceAdjust)
          this.revertToPriorValue(settingName, true, oldValue, subSetting ? subSetting["name"] : null);

        return;
      }
    }
    else {

      //Non decimals
      if (Number(parseInt(newValue)) != newValue) {

        if (forceAdjust)
          this.revertToPriorValue(settingName, true, oldValue, subSetting ? subSetting["name"] : null);

        return;
      }
    }

    //Min/Max check
    let settingMin: number = setting["min"];
    let settingMax: number = setting["max"];

    if (("min" in setting) && newValue < settingMin) {
      if (forceAdjust) {
        setTimeout(() => {

          if (subSetting)
            this.global.generator_settingsMap[setting["name"]][subSetting["name"]] = settingMin;
          else
            this.global.generator_settingsMap[setting["name"]] = settingMin;

          this.cd.markForCheck();
          this.afterSettingChange();
        }, 0);
      }
    }
    else if (("max" in setting) && newValue > settingMax) {
      if (forceAdjust) {
        setTimeout(() => {

          if (subSetting)
            this.global.generator_settingsMap[setting["name"]][subSetting["name"]] = settingMax;
          else
            this.global.generator_settingsMap[setting["name"]] = settingMax;

          this.cd.markForCheck();
          this.afterSettingChange();
        }, 0);
      }
    }
    else { //Update setting with new number value

      if (setting["is_decimal"]) {
        //Decimals
        this.inputFocusOut(settingName, false, parseFloat(newValue), subSetting ? subSetting["name"] : null);
      }
      else {
        //Non decimals
        this.inputFocusOut(settingName, false, parseInt(newValue), subSetting ? subSetting["name"] : null);
      }
    }
  }

  seedInputFocusIn() {
    //Save current seed on entering the input field
    this.inputOldValue = this.seedString;
  }

  seedInputFocusOut() {

    //Test if seed string is valid
    let newValue = this.seedString;

    //Existence check
    if (!newValue || newValue.length == 0) {
      return;
    }

    //Numbers test
    if (Number(parseInt(newValue)) != Number(newValue)) {

      this.seedString = this.inputOldValue;

      this.dialogService.open(DialogWindowComponent, {
        autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: "The seed may only contain numbers!" }
      });

      return;
    }
    
    //Min/Max check
    let settingMin: number = 0;
    let settingMax: number = 2147483647;

    if (Number(newValue) < settingMin) {

      this.seedString = settingMin.toString();
      this.dialogService.open(DialogWindowComponent, {
        autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: "The smallest seed possible is 0!" }
      });
    }
    else if (Number(newValue) > settingMax) {
   
      this.seedString = settingMax.toString();
      this.dialogService.open(DialogWindowComponent, {
        autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: "The largest seed possible is 2147483647!" }
      });
    }
    else { //Update setting with new seed value

      if (newValue != this.inputOldValue) {
        setTimeout(() => {
          this.seedString = newValue;
        }, 0);
      }
    }
  }

  afterSettingChange(saveOnly: boolean = false) {
    if (this.global.getGlobalVar('electronAvailable')) { //Electron

      //Show waiting spinner if another settings action is currently running and delay the task
      if (this.settingsLocked) {
        this.settingsBusy = true;

        if (!saveOnly)
          this.settingsBusySaveOnly = false;

        this.cd.markForCheck();
        this.cd.detectChanges();
        return;
      }

      this.settingsLocked = true;

      setTimeout(() => {
        //console.log(this.global.generator_settingsMap);
        //console.log(this.global.generator_customColorMap);

        if (saveOnly) {
          this.global.saveCurrentSettingsToFile();

          this.settingsLocked = false;

          if (this.settingsBusy) { //Execute delayed task
            this.settingsBusy = false;
            this.afterSettingChange(this.settingsBusySaveOnly);
            this.settingsBusySaveOnly = true;
          }

          this.cd.markForCheck();
          this.cd.detectChanges();
        }
        else {
          this.getSettingsString();
        }
      }, 0);
    }
    else { //Web

      setTimeout(() => {

        //console.log(this.global.generator_settingsMap);
        //console.log(this.global.generator_customColorMap);

        this.global.saveCurrentSettingsToFile();

        this.settingsLocked = false;
        this.cd.markForCheck();
        this.cd.detectChanges();
      }, 0);
    }
  }

  // Mobile scroll indicator methods
  private initializeMobileScrollIndicator() {

    if (!this.tabSetNative?.nativeElement) {
      return;
    }
    
    // Start observing the tabset - try different selectors
    let tabsetElement = this.tabSetNative.nativeElement.querySelector('.tabset');
    if (!tabsetElement) {
      tabsetElement = this.tabSetNative.nativeElement.querySelector('ul');
    }
    if (!tabsetElement) {
      tabsetElement = this.tabSetNative.nativeElement.querySelector('nb-tabset');
    }
    
    if (tabsetElement) {
      
      // Add scroll event listener to detect scroll position changes
      tabsetElement.addEventListener('scroll', () => {
        // Mark as currently scrolling
        this.isScrolling = true;
        
        // Clear existing timeout
        if (this.scrollTimeout) {
          clearTimeout(this.scrollTimeout);
        }
        
        // Add fade-out class for smooth transition
        if (this.scrollIndicatorElement) {
          this.scrollIndicatorElement.classList.add('fade-out');
        }
        
        // Debounce: show indicator again after scrolling stops
        this.scrollTimeout = setTimeout(() => {
          this.isScrolling = false;
          if (this.scrollIndicatorElement) {
            this.scrollIndicatorElement.classList.remove('fade-out');
          }
          this.checkScrollIndicatorVisibility();
        }, 150); // 150ms delay after scrolling stops
      });
    }
    
    // Initial check
    this.checkScrollIndicatorVisibility();
    
    // Get reference to scroll indicator element for fade transitions
    setTimeout(() => {
      this.scrollIndicatorElement = this.tabSetNative.nativeElement.querySelector('.mobile-scroll-indicator');
    }, 100);
  }
  
  private checkScrollIndicatorVisibility() {
    if (!this.tabSetNative?.nativeElement) return;
    
    // Try different selectors to find the tabset element
    let tabsetElement = this.tabSetNative.nativeElement.querySelector('.tabset');
    if (!tabsetElement) {
      tabsetElement = this.tabSetNative.nativeElement.querySelector('ul');
    }
    if (!tabsetElement) {
      tabsetElement = this.tabSetNative.nativeElement.querySelector('nb-tabset');
    }
    
    if (!tabsetElement) return;
    
    // Check if we're in generate_from_file mode (only 2 tabs)
    const isGenerateFromFileMode = this.global.generator_settingsMap['Web.generate_from_file'] === true;
    
    // Check if we're under 650px width
    const isMobileWidth = window.innerWidth <= 650;
    
    // Check if scrolling is actually needed
    const needsScrolling = tabsetElement.scrollWidth > tabsetElement.clientWidth;
    
    // Check scroll position to determine if we're at the end
    const isAtEnd = tabsetElement.scrollLeft >= (tabsetElement.scrollWidth - tabsetElement.clientWidth - 1); // -1 for rounding errors
    const isAtStart = tabsetElement.scrollLeft <= 1;
    
    // Show indicator only when all conditions are met AND not at the end
    this.showMobileScrollIndicator = isMobileWidth && needsScrolling && !isGenerateFromFileMode && !isAtEnd;
    
    // Handle fade-out class for smooth transitions
    if (this.scrollIndicatorElement) {
      if (!this.showMobileScrollIndicator || isAtEnd) {
        this.scrollIndicatorElement.classList.add('fade-out');
      } else {
        this.scrollIndicatorElement.classList.remove('fade-out');
      }
    }
    
    // Trigger change detection
    this.cd.markForCheck();
  }
  

  checkAutoImportSettings() { //Web only

    let userSettings = null;
    let isGenerator = this.global.getGlobalVar("appType") == "generator";
    let storageSettingsKey = isGenerator ? "generatorSettings_" : "patcherSettings_";
    let currentVersion = this.global.getGlobalVar("webSourceVersion");
    let currentBranch = currentVersion.startsWith("dev") && currentVersion.includes("_") ? currentVersion.split("_")[0] : "master";

    try {
      userSettings = localStorage.getItem(storageSettingsKey + currentVersion);
    } catch (err) {
      console.error("Local storage not available");
      return;
    }

    if (!userSettings) {

      //Check if we have a prior version settings map and find the closest one from the same branch (master/dev are compatible with each other)
      if (localStorage.length) {

        let closestFoundVersion = "";

        for (let i = 0; i < localStorage.length; i++) {
          let key = localStorage.key(i);

          if (key && key.startsWith(storageSettingsKey)) {
            let version = key.replace(storageSettingsKey, "");
            let branch = version.startsWith("dev") && version.includes("_") ? version.split("_")[0] : "master";

            if ((branch === currentBranch) || (branch === "master" && currentBranch === "dev") || (branch === "dev" && currentBranch === "master")) {

              if (this.global.isVersionNewer(version, currentVersion) == false)
                if (!closestFoundVersion || this.global.isVersionNewer(version, closestFoundVersion))
                  closestFoundVersion = version;
            }
          }
        }

        if (closestFoundVersion) {

          //Import settings
          let importedSettings = JSON.parse(localStorage.getItem(storageSettingsKey + closestFoundVersion));

          this.global.applySettingsObject(importedSettings);

          //While presets and settings strings ignore the Web namespace, cached settings do include it
          //So we need to ensure we don't accidentally import a wrong active tab from a different appType
          if (this.global.getGlobalVar('appType') == 'generator') {
            this.global.generator_settingsMap["Web.generate_from_file"] = false;
          }
          else {
            this.global.generator_settingsMap["Web.generate_from_file"] = true;
          }

          this.recheckAllSettings("", false, true);

          console.log("Imported settings from prior version:", closestFoundVersion, importedSettings);

          //Import presets from the found version in generator mode (if present and current version has none)
          if (isGenerator) {

            let userPresets = localStorage.getItem("generatorPresets_" + currentVersion);

            if (!userPresets) {

              let importedPresets = localStorage.getItem("generatorPresets_" + closestFoundVersion);

              if (importedPresets && importedPresets.length > 0) {

                importedPresets = JSON.parse(importedPresets);

                //Only import user presets that don't exist yet globally
                Object.keys(importedPresets).forEach(presetName => {
                  if (!(presetName in this.global.generator_presets))
                    this.global.generator_presets[presetName] = { settings: importedPresets[presetName] };
                });

                this.global.saveCurrentPresetsToFile();

                console.log("Imported presets from prior version:", closestFoundVersion, importedPresets);
              }
            }
          }

          //Refresh GUI
          this.afterSettingChange();
        }
      }
    }
  }

  onPatchFileInputChange(event: any) {
    // Handle the input change and call the original afterSettingChange method
    this.afterSettingChange(true);
  }
}
