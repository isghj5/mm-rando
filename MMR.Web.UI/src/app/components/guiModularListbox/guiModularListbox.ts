import { Component, Input, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';

import { GUIGlobal } from '../../providers/GUIGlobal';

import { NbDialogService } from '@nebular/theme';

import { DualListComponent } from 'zsr-dual-listbox';
import { BasicList } from 'zsr-dual-listbox';

import { DialogWindowComponent } from '../../pages/generator/dialogWindow/dialogWindow.component';

@Component({
  selector: 'gui-modular-listbox',
  templateUrl: './guiModularListbox.html',
  styleUrls: ['./guiModularListbox.scss'],
  standalone: false
})
export class GUIModularListboxComponent extends DualListComponent {

  @Input() tooltip: any = 'tooltip';
  @Input() tooltipComponent: any = null;
  @Input() tagFilter: any = null;

  @Input() valueIsHexString: boolean = true;
  @Input() coDependentFilters: boolean = false;
  @Input() presets: any = null;
  @Input() disabled: boolean = false;

  // Feature gates for backward compatibility
  @Input() enableHexString: boolean = true;
  @Input() enablePresets: boolean = true;
  @Input() enableCoDependentFilters: boolean = true;
  @Input() enableResponsiveDesign: boolean = true;

  // Tooltip configuration
  @Input() tooltipDelay: number = 1000;
  @Input() enableCrossListTooltips: boolean = true; // Enable tooltips that wrap to the other list

  selectedTag: any = {};
  currentHexString: string = "";
  currentHexStringOldValue: string = "";
  selectedPresets: any = [];
  currentDestinationList: any = [];
  destinationListChangeSub: any = null;

  reducedWindowSize: boolean = false;
  mobileWindowSize: boolean = false;

  constructor(private cd: ChangeDetectorRef, private breakpointObserver: BreakpointObserver, public global: GUIGlobal, private dialogService: NbDialogService) {
    super(cd);

    // Initialize selectedTag with default values
    if (this.enableCoDependentFilters && this.tagFilter) {
      this.selectedTag = {};
      for (let filter of this.tagFilter) {
        this.selectedTag[filter.name] = "(all)";
      }
    } else if (this.tagFilter) {
      this.selectedTag = "(all)";
    } else {
      this.selectedTag = {};
    }

    // Only initialize responsive design if enabled
    if (this.enableResponsiveDesign) {
      let breakpointMaxWidth = 1130;
      let breakpointElectronMaxWidth = 730;
      let breakpointMobile = 480;

      if (this.global.getGlobalVar('electronAvailable'))
        breakpointMaxWidth = breakpointElectronMaxWidth;

      this.breakpointObserver.observe([
        `(max-width: ${breakpointMaxWidth}px)`
      ]).subscribe((result: BreakpointState) => {
        this.reducedWindowSize = result.matches;
        this.cd.markForCheck();
      });

      this.breakpointObserver.observe([
        `(max-width: ${breakpointMobile}px)`
      ]).subscribe((result: BreakpointState) => {
        this.mobileWindowSize = result.matches;
        this.cd.markForCheck();
      });
    }
  }

  ngOnChanges(changeRecord) {
    super.ngOnChanges(changeRecord);

    if (changeRecord['tagFilter']) {

      //Rebuild available filter value list and set default value
      if (this.enableCoDependentFilters) {
        this.selectedTag = {};
        if (this.tagFilter) {
          for (let filter of this.tagFilter) {
            this.selectedTag[filter.name] = "(all)";
          }
        }
      } else {
        this.selectedTag = "(all)";
      }

    }

    if (changeRecord['destination']) {

      //console.log("Destination list external change, calc hex string and selected presets:", changeRecord['destination'].currentValue);

      this.setNewDestinationList(changeRecord['destination'].currentValue, false);

      //Subscribe to list change event here (only one active sub)
      if (this.destinationListChangeSub)
        this.destinationListChangeSub.unsubscribe();

      this.destinationListChangeSub = this.destinationChange.subscribe(destinationList => {

        //console.log("List changed by user input:", destinationList);
        this.setNewDestinationList(destinationList, false);
      });
    }
  }

  // Override buildAvailable to add tooltip support
  buildAvailable(source: Array<any>): boolean {
    const result = super.buildAvailable(source);

    // Only process items when the list actually changed
    if (result && (this.tooltip || this.tagFilter)) {
      // Create a Map for O(1) lookups instead of O(n) find operations
      const sourceMap = new Map();
      source.forEach(src => {
        const id = typeof src === 'object' ? src[this.key] : src;
        sourceMap.set(id, src);
      });

      this.available.list.forEach((item, index) => {
        if (item) {
          // O(1) lookup instead of O(n) find
          const sourceItem = sourceMap.get(item._id);

          if (sourceItem) {
            // Add tooltip if not present
            if (!item._tooltip && this.tooltip) {
              item._tooltip = this.makeTooltipExtended(sourceItem);
            }

            // Add tags if not present (needed for filtering)
            if (!item._tags && this.tagFilter) {
              item._tags = this.makeTagsExtended(sourceItem);
            }
          }
        }
      });
    }

    return result;
  }

  private makeTooltipExtended(item: any): string {
    if (typeof item === 'object') {
      return item[this.tooltip] ? item[this.tooltip] : "";
    }
    else {
      return item;
    }
  }

  private makeTagsExtended(item: any): any {
    if (typeof item === 'object') {
      return item["tags"] ? item["tags"] : "";
    }
    else {
      return item;
    }
  }

  // Override buildConfirmed to add tooltip support
  buildConfirmed(destination: Array<any>): boolean {
    const result = super.buildConfirmed(destination);

    // Add tooltip and tags support to the built items
    if (result && (this.tooltip || this.tagFilter)) {
      // Create a Map for O(1) lookups instead of O(n) find operations
      const sourceMap = new Map();
      this.source.forEach(src => {
        const id = typeof src === 'object' ? src[this.key] : src;
        sourceMap.set(id, src);
      });

      this.confirmed.list.forEach(item => {
        if (item) {
          // O(1) lookup instead of O(n) find
          const sourceItem = sourceMap.get(item._id);

          if (sourceItem) {
            // Add tooltip if not present
            if (!item._tooltip && this.tooltip) {
              item._tooltip = this.makeTooltipExtended(sourceItem);
            }

            // Add tags if not present (needed for filtering)
            if (!item._tags && this.tagFilter) {
              item._tags = this.makeTagsExtended(sourceItem);
            }
          }
        }
      });
    }

    return result;
  }

  hexInputFocusIn() {
    if (!this.enableHexString) return;
    //Save current value on entering any input field
    this.currentHexStringOldValue = this.currentHexString;
  }

  hexInputFocusOut() {
    if (!this.enableHexString) return;

    this.currentHexString = this.currentHexString.trim().toLowerCase();
    let newValue = this.currentHexString;

    //Only update if the value actually changed
    if (newValue != this.currentHexStringOldValue) {

      if (!this.applyHexString(newValue)) {
        this.currentHexString = this.currentHexStringOldValue;

        //Show error dialog
        this.dialogService.open(DialogWindowComponent, {
          autoFocus: true, closeOnBackdropClick: true, closeOnEsc: true, hasBackdrop: true, hasScroll: false, context: { dialogHeader: "Error", dialogMessage: "The entered hex string is invalid!" }
        });
      }
    }
  }

  selectedPresetListChange(newSelections: any) {
    if (!this.enablePresets) return;
    //console.log("Selected preset list changed, new:", newSelections);

    let oldSelections = this.selectedPresets;

    //Find added/removed presets
    let addedEntries = [];
    let deletedEntries = [];

    for (let preset of newSelections) {
      if (oldSelections.indexOf(preset) == -1)
        addedEntries.push(preset);
    }

    for (let preset of oldSelections) {
      if (newSelections.indexOf(preset) == -1)
        deletedEntries.push(preset);
    }

    //Clone existing list so we can add/remove from it
    let newList = JSON.parse(JSON.stringify(this.currentDestinationList));

    //Remove all items that are of one of the deleted presets type
    if (deletedEntries.length > 0) {

      for (let i = 0; i < newList.length; i++) {

        if (deletedEntries.indexOf(newList[i].preset) != -1) {
          newList.splice(i, 1);
          i--;
        }
      }
    }

    //Add all items that have one of the added preset types and are not already present
    if (addedEntries.length > 0) {

      for (let option of this.source) {

        if (addedEntries.indexOf(option.preset) == -1)
          continue;

        let alreadyAdded = newList.find(item => item.name === option.name);

        if (alreadyAdded)
          continue;

        newList.push(option);
      }
    }

    this.destination = newList;
    this.setNewDestinationList(this.destination);
  }

  applyHexString(hexString: string) {
    if (!this.enableHexString) return false;

    let result = this.global.decodeSearchBoxHexString(hexString, this.source);

    if (!result.success)
      return false;

    this.destination = result.decodedOptions;
    this.setNewDestinationList(this.destination);

    return true;
  }

  setNewDestinationList(destinationList: any, doEmit: boolean = true) {

    this.currentDestinationList = destinationList;

    if (doEmit) {
      this.destinationChange.emit(this.currentDestinationList); //Required so settings get saved
    }
    else {
      //Only need to calculate once after an emit happened
      if (this.enableHexString && this.valueIsHexString) {
        this.computeHexString();
      }
      if (this.enablePresets) {
        this.computeActivePresets();
      }
    }
  }

  computeHexString() {
    if (!this.enableHexString) return;
    this.currentHexString = this.global.encodeSearchBoxSelectionsAsHexString(this.currentDestinationList, this.source);
  }

  computeActivePresets() {
    if (!this.enablePresets) return;

    //ToDo: Performance slow with big list? If so, need to pre-built lookup lists on init. Only 5 ms at worst which seems fine?
    let newActivePresets = [];
    let travelledPresets = [];

    for (let option of this.currentDestinationList) {

      if (travelledPresets.indexOf(option.preset) != -1)
        continue;

      //Check if we have every option of this preset name that can be found in the source list in the destination list
      let presetComplete = true;

      for (let optionSource of this.source) {

        if (optionSource.name === option.name)
          continue;

        if (optionSource.preset != option.preset)
          continue;

        let targetDestination = this.currentDestinationList.find(item => item.name === optionSource.name);

        if (!targetDestination) {
          presetComplete = false;
          break;
        }
      }

      if (presetComplete)
        newActivePresets.push(option.preset);

      travelledPresets.push(option.preset);
    }

    this.selectedPresets = newActivePresets;
  }

  clearFilter(source: BasicList, isAvailable: boolean = false) {

    if (source) {
      source.picker = '';

      if (this.enableCoDependentFilters) {
        for (let key of Object.keys(this.selectedTag)) {
          this.selectedTag[key] = "(all)";
        }
      } else {
        this.selectedTag = "(all)";
      }

      this.onFilter(source);
    }
  }

  // Handle filter select changes explicitly
  onFilterChange() {
    this.onFilter(this.available);
  }

  onFilter(source: BasicList) {
    // Ensure selectedTag is properly initialized
    if (!this.selectedTag || Object.keys(this.selectedTag).length === 0) {
      if (this.enableCoDependentFilters && this.tagFilter) {
        this.selectedTag = {};
        for (let filter of this.tagFilter) {
          this.selectedTag[filter.name] = "(all)";
        }
      } else if (this.tagFilter) {
        this.selectedTag = "(all)";
      }
    }

    // Call the base class onFilter FIRST for search picker functionality
    super.onFilter(source);

    if (source.name === "available" && this.tagFilter && Object.keys(this.selectedTag).length > 0) {
      if (this.enableCoDependentFilters) {
        // Check if we really need to filter
        let doFilter = false;
        for (let key of Object.keys(this.selectedTag)) {
          if (this.selectedTag[key] != "(all)") {
            doFilter = true;
            break;
          }
        }

        if (doFilter) {
          // Start with the current sift (which includes text filtering from base class)
          let currentSift = source.sift || source.list;
          const filtered = currentSift.filter((item: any) => {
            if (Object.prototype.toString.call(item) === '[object Object]') {
              if (item._tags !== undefined) {
                for (let key of Object.keys(this.selectedTag)) {
                  if (this.selectedTag[key] != "(all)" && key in item._tags && item._tags[key].indexOf(this.selectedTag[key]) === -1)
                    return false;
                }
                return true;
              } else {
                return true;
              }
            } else {
              return false;
            }
          });
          source.sift = filtered;
          this.unpickExtended(source);
        }
      } else {
        // Simple tag filtering (original guiListbox behavior)
        if (this.selectedTag != "(all)") {
          let currentSift = source.sift || source.list;
          const filtered = currentSift.filter((item: any) => {
            if (Object.prototype.toString.call(item) === '[object Object]') {
              if (item._tags !== undefined) {
                // Handle both string and array tags
                if (Array.isArray(item._tags)) {
                  return item._tags.indexOf(this.selectedTag) !== -1;
                } else {
                  return item._tags === this.selectedTag;
                }
              } else {
                return true;
              }
            } else {
              return false;
            }
          });
          source.sift = filtered;
          this.unpickExtended(source);
        }
      }
    }
  }

  private unpickExtended(list: any): void {
    for (let i = list.pick.length - 1; i >= 0; i -= 1) {
      if (list.sift.indexOf(list.pick[i]) === -1) {
        list.pick.splice(i, 1);
      }
    }
  }

  getNbMultipleSelectLabel(setting: any): string {
    if (!this.enablePresets) return "";

    if (this.selectedPresets.length === this.presets.presets.length)
      return "All";

    if (this.mobileWindowSize && (this.selectedPresets.length > 1 || this.selectedPresets.join(", ").length > 16))
      return `Selected: ${this.selectedPresets.length}`;

    if ((this.reducedWindowSize && this.selectedPresets.length > 1) || this.selectedPresets.length > 2)
      return `Selected: ${this.selectedPresets.length}`;
    else
      return this.selectedPresets.join(", ");
  }

  getTooltipPlacement(listName: string): string {
    return listName === 'available' ? 'right' : 'left';
  }

  private tooltipTimeout: any = null;

  onTooltipMouseEnter(popover: any): void {
    if (this.tooltipTimeout) {
      clearTimeout(this.tooltipTimeout);
    }

    this.tooltipTimeout = setTimeout(() => {
      if (popover && typeof popover.show === 'function') {
        popover.show();
      }
    }, this.tooltipDelay);
  }

  onTooltipMouseLeave(popover: any): void {
    // Clear timeout if mouse leaves before delay expires
    if (this.tooltipTimeout) {
      clearTimeout(this.tooltipTimeout);
      this.tooltipTimeout = null;
    }
    if (popover && typeof popover.hide === 'function') {
      popover.hide();
    }
  }

  moveItem(source: BasicList, target: BasicList, item?: any, trueup?: boolean): void {
    super.moveItem(source, target, item, trueup);
    this.onFilter(source);
    this.onFilter(target);

    this.cd.markForCheck();
  }
}
