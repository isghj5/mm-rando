import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { NbDialogRef, NbDialogService } from '@nebular/theme';
import { MMRItemSelectorWindowComponent } from '../itemSelectorWindow/itemSelectorWindow.component';

interface DetailedConfigTier {
  id: string;
  items: string[];
  indicateImportance?: boolean; // hintPriorities only
  hintCount?: number;           // hintPriorities only
  amount?: number;              // randomStartingItemGroups only
  order: number;
}

@Component({
  templateUrl: './guiDetailedConfigWindow.html',
  styleUrls: ['./guiDetailedConfigWindow.scss'],
  standalone: false
})
export class MMRGuiDetailedConfigWindowComponent implements OnInit, OnDestroy {
  @Input() dialogHeader: string = 'Customize';
  @Input() setting: any = null;
  @Input() assignmentSettingsMap: any = null;
  @Input() assignmentSettingName: string = '';
  @Input() settingTooltip: string = '';
  @Input() sectionSettings: any[] = [];
  @Input() itemList: any[] = [];

  // 'hintPriorities' | 'randomStartingItemGroups'
  @Input() configMode: string = 'hintPriorities';

  tiers: DetailedConfigTier[] = [];
  nextTierId: number = 1;
  validationError: string = '';

  // Default tooltip for hint priorities
  hintPrioritiesTooltip: string = `Note: this list will be used exclusively if any locations are added.<br>
Settings are not taken into account, but non-randomized and junked locations will not be hinted.<br>
The higher a location appears in this list, the higher its priority. Locations with the same priority will be chosen randomly.<br>
If a location should combine with another location, all the combined locations should be added.<br>
The checkbox indicates that this tier of locations should indicate their importance.<br>
The number (if non-zero) indicates how many of the locations will be hinted, and the rest will be forced to be junk.`;

  // Default tooltip for random starting item groups
  randomStartingGroupsTooltip: string = `Create groups of items and set the amount to draw randomly from each group.<br>
Each group selects that many items at seed generation time that are handed to the player as starting items. <br>
Amount must be ≤ items selected in the group.`;

  private addedScrollBlockClass: boolean = false;

  get enablePriorities(): boolean { return this.configMode === 'hintPriorities'; }
  get enableOrdering(): boolean { return this.configMode === 'hintPriorities'; }
  get showAmount(): boolean { return this.configMode === 'randomStartingItemGroups'; }
  get effectiveTooltip(): string {
    switch (this.configMode) {
      case 'hintPriorities':
        return this.hintPrioritiesTooltip;
      case 'randomStartingItemGroups':
        return this.randomStartingGroupsTooltip;
      default:
        return '';
    }
  }

  get buttonLabel(): string {
    switch (this.configMode) {
      case 'hintPriorities':
        const tierCount = this.tiers.length;
        return `Override Hint Priorities: ${tierCount} ${tierCount === 1 ? 'Sphere' : 'Spheres'}`;
      case 'randomStartingItemGroups':
        const totalAmount = this.tiers.reduce((sum, tier) => sum + (tier.amount || 0), 0);
        const totalItems = this.tiers.reduce((sum, tier) => sum + tier.items.length, 0);
        return `Random Starting Items: ${totalAmount} / ${totalItems}`;
      default:
        return this.dialogHeader;
    }
  }

  ngOnInit() {
    if (!document.body.classList.contains('cdk-global-scrollblock')) {
      document.body.classList.add('cdk-global-scrollblock');
      this.addedScrollBlockClass = true;
    }
    this.loadItemListFromSetting();
    this.initializeTiersFromExistingData();
  }

  ngOnDestroy() {
    if (document.body && this.addedScrollBlockClass) {
      document.body.classList.remove('cdk-global-scrollblock');
      this.addedScrollBlockClass = false;
    }
  }

  private findSettingByName(name: string): any | null {
    if (!Array.isArray(this.sectionSettings)) return null;
    return this.sectionSettings.find((s: any) => s && s.name === name) || null;
  }

  loadItemListFromSetting() {
    if (this.configMode === 'hintPriorities') {
      const s = this.findSettingByName('GameplaySettings.OverrideHintPriorities');
      if (s && s.options) {
        this.itemList = s.options.map((item: any) => ({
          label: item.text,
          value: item.name,
          tags: item.tags || {}
        }));
      } else {
        this.itemList = [];
      }
      return;
    }

    // randomStartingItemGroups: get from GameplaySettings.RandomStartingItemGroups
    const gameplayRsi = this.findSettingByName('GameplaySettings.RandomStartingItemGroups');
    if (gameplayRsi && gameplayRsi.options) {
      this.itemList = gameplayRsi.options.map((item: any) => ({
        label: item.text,
        value: item.name,
        tags: item.tags || {}
      }));
      return;
    }
    this.itemList = [];
  }

  initializeTiersFromExistingData() {
    const currentValues = this.assignmentSettingsMap || {};

    if (this.configMode === 'hintPriorities') {
      const overrideHintPriorities = currentValues['GameplaySettings.OverrideHintPriorities'] || [];
      const overrideImportanceIndicatorTiers = currentValues['GameplaySettings.OverrideImportanceIndicatorTiers'] || [];
      const overrideHintItemCaps = currentValues['GameplaySettings.OverrideHintItemCaps'] || [];

      if (overrideHintPriorities.length > 0) {
        this.tiers = overrideHintPriorities.map((tierItems: string[], index: number) => ({
          id: this.generateTierId(),
          items: tierItems || [],
          indicateImportance: overrideImportanceIndicatorTiers.includes(index),
          hintCount: overrideHintItemCaps[index] || 0,
          order: index
        }));
      } else {
        this.tiers = [
          { id: this.generateTierId(), items: [], indicateImportance: false, hintCount: 0, order: 0 }
        ];
      }
      return;
    }

    const randomGroups = currentValues['GameplaySettings.RandomStartingItemGroups'] || [];
    if (Array.isArray(randomGroups) && randomGroups.length > 0) {
      this.tiers = randomGroups.map((g: any, index: number) => ({
        id: this.generateTierId(),
        items: Array.isArray(g?.Items) ? g.Items : [],
        amount: typeof g?.Amount === 'number' ? g.Amount : 0,
        order: index
      }));
    } else {
      this.tiers = [ { id: this.generateTierId(), items: [], amount: 0, order: 0 } ];
    }
  }

  generateTierId(): string {
    return `tier_${this.nextTierId++}`;
  }

  addTier() {
    const newTier: DetailedConfigTier = {
      id: this.generateTierId(),
      items: [],
      indicateImportance: false,
      hintCount: 0,
      amount: 0,
      order: this.tiers.length
    };
    this.tiers.push(newTier);
  }

  removeTier(tierId: string) {
    this.tiers = this.tiers.filter(tier => tier.id !== tierId);
    this.reorderTiers();
  }

  reorderTiers() {
    this.tiers.forEach((tier, index) => { tier.order = index; });
  }

  moveTierUp(tierId: string) {
    const tierIndex = this.tiers.findIndex(tier => tier.id === tierId);
    if (tierIndex > 0) {
      const temp = this.tiers[tierIndex];
      this.tiers[tierIndex] = this.tiers[tierIndex - 1];
      this.tiers[tierIndex - 1] = temp;
      this.reorderTiers();
    }
  }

  moveTierDown(tierId: string) {
    const tierIndex = this.tiers.findIndex(tier => tier.id === tierId);
    if (tierIndex < this.tiers.length - 1) {
      const temp = this.tiers[tierIndex];
      this.tiers[tierIndex] = this.tiers[tierIndex + 1];
      this.tiers[tierIndex + 1] = temp;
      this.reorderTiers();
    }
  }

  addItemToTier(tierId: string) {
    const tier = this.tiers.find(t => t.id === tierId);
    if (tier) {
      const dialogRef = this.dialogService.open(MMRItemSelectorWindowComponent, {
        autoFocus: false,
        closeOnBackdropClick: true,
        closeOnEsc: true,
        hasBackdrop: true,
        hasScroll: false,
        context: {
          dialogHeader: 'Select Items for Tier',
          selectedItems: tier.items,
          itemList: this.itemList,
          tagDisplayMode: this.configMode === 'randomStartingItemGroups' ? 'category' : 'region',
          searchPlaceholder: this.configMode === 'randomStartingItemGroups' ? 'Search name or item category...' : 'Search name or region...',
          setting: this.setting
        }
      });

      this.resizeDialogToAppContainer('.mmrItemSelector-window', 0.78, 0.78);

      dialogRef.onClose.subscribe((selectedItems: string[]) => {
        if (selectedItems !== null && selectedItems !== undefined) {
          tier.items = selectedItems;
        }
      });
    }
  }

  removeItemFromTier(tierId: string, item: string) {
    const tier = this.tiers.find(t => t.id === tierId);
    if (tier) {
      tier.items = tier.items.filter(i => i !== item);
    }
  }

  getLocationsDisplay(tier: DetailedConfigTier): string {
    if (tier.items.length === 0) {
      return 'Click + to add items';
    }
    const count = tier.items.length;
    const noun = count === 1 ? 'item' : 'items';
    return `${count} ${noun} selected`;
  }

  getItemLabel(itemValue: string): string {
    const item = this.itemList.find(i => i.value === itemValue);
    if (!item) { return itemValue; }

    const regions = item.tags && Array.isArray(item.tags.Regions) ? item.tags.Regions : null;
    if (regions && regions.length > 0) {
      const regionList: string[] = regions.filter((r: any) => r !== null && r !== undefined).map((r: any) => String(r));
      const regionsDisplay = regionList.join(', ');
      return regionsDisplay ? `${item.label} (${regionsDisplay})` : item.label;
    }
    return item.label;
  }

  confirmDialog() {
    if (this.configMode === 'hintPriorities') {
      const hasAnyItems = this.tiers.some(tier => tier.items && tier.items.length > 0);

      let overrideHintPriorities: string[][] | null;
      let overrideImportanceIndicatorTiers: number[] | null;
      let overrideHintItemCaps: number[] | null;

      if (!hasAnyItems) {
        overrideHintPriorities = null;
        overrideImportanceIndicatorTiers = null;
        overrideHintItemCaps = null;
      } else {
        const tiersWithItems = this.tiers.filter(tier => tier.items && tier.items.length > 0);

        overrideHintPriorities = tiersWithItems.map(tier => tier.items);
        overrideImportanceIndicatorTiers = tiersWithItems
          .map((tier, index) => tier.indicateImportance ? index : -1)
          .filter(index => index !== -1);
        overrideHintItemCaps = tiersWithItems.map(tier => tier.hintCount || 0);
      }

      const result = {
        'GameplaySettings.OverrideHintPriorities': overrideHintPriorities,
        'GameplaySettings.OverrideImportanceIndicatorTiers': overrideImportanceIndicatorTiers,
        'GameplaySettings.OverrideHintItemCaps': overrideHintItemCaps
      };

      this.assignmentSettingsMap['GameplaySettings.OverrideHintPriorities'] = overrideHintPriorities;
      this.assignmentSettingsMap['GameplaySettings.OverrideImportanceIndicatorTiers'] = overrideImportanceIndicatorTiers;
      this.assignmentSettingsMap['GameplaySettings.OverrideHintItemCaps'] = overrideHintItemCaps;

      this.ref.close(result);
      return;
    }

    const invalidTier = this.tiers.find(t => !t.items || t.items.length === 0 || !t.amount || t.amount <= 0);
    if (invalidTier) {
      this.validationError = 'Each group must have at least one item and an amount greater than 0.';
      return;
    }
    this.validationError = '';

    const randomStartingItemGroups = this.tiers.map(t => ({
      Items: t.items,
      Amount: typeof t.amount === 'number' ? t.amount : 0
    }));

    this.assignmentSettingsMap['GameplaySettings.RandomStartingItemGroups'] = randomStartingItemGroups;

    this.ref.close({
      'GameplaySettings.RandomStartingItemGroups': randomStartingItemGroups
    });
  }

  cancelDialog() {
    this.ref.close(null);
  }

  private resizeDialogToAppContainer(selector: string, widthRatio: number, heightRatio: number) {
    const appContainer = document.querySelector('.pageContainer') ||
                       document.querySelector('nb-card') ||
                       document.querySelector('#generator');
    if (appContainer) {
      const appRect = (appContainer as HTMLElement).getBoundingClientRect();
      const dialogElement = document.querySelector(selector) as HTMLElement;
      if (dialogElement) {
        const effectiveWidthRatio = window.innerWidth < 750 ? 0.99 : widthRatio;
        const targetWidth = appRect.width * effectiveWidthRatio;
        let targetHeight: number;
        if (window.innerWidth <= 800) {
          targetHeight = window.innerHeight * 0.85;
        } else {
          targetHeight = appRect.height * heightRatio;
        }
        const maxAllowedHeight = window.innerHeight * 0.9;
        targetHeight = Math.min(targetHeight, maxAllowedHeight);
        const centerX = appRect.left + (appRect.width / 2);
        let centerY = window.innerHeight / 2 + 50;
        dialogElement.style.width = `${targetWidth}px`;
        dialogElement.style.height = `${targetHeight}px`;
        dialogElement.style.maxWidth = `${targetWidth}px`;
        dialogElement.style.maxHeight = `${targetHeight}px`;
        dialogElement.style.position = 'fixed';
        dialogElement.style.left = `${centerX - (targetWidth / 2)}px`;
        dialogElement.style.top = `${centerY - (targetHeight / 2)}px`;
        dialogElement.style.transform = 'none';
        dialogElement.style.zIndex = '1000';
      }
    }
  }

  constructor(protected ref: NbDialogRef<MMRGuiDetailedConfigWindowComponent>, private dialogService: NbDialogService) {}
}
