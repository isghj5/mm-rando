import { Component, Input, OnInit, OnDestroy, AfterViewInit, ViewChild, ElementRef, ChangeDetectionStrategy } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { GUIGlobal } from '../../../providers/GUIGlobal';

interface SelectableItem {
  name: string;
  label: string;
  selected: boolean;
  regionDisplay: string;
  regionSearchLower: string;
}

@Component({
  templateUrl: './itemSelectorWindow.html',
  styleUrls: ['./itemSelectorWindow.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false
})
export class MMRItemSelectorWindowComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() dialogHeader: string = 'Select Items';
  @Input() setting: any = null;
  @Input() assignmentSettingsMap: any = null;
  @Input() assignmentSettingName: string = '';
  @Input() settingTooltip: string = '';
  @Input() itemList: any[] = [];
  @Input() tagDisplayMode: 'region' | 'category' = 'region';
  @Input() searchPlaceholder: string = 'Search name or region...';

  selectedItems: any[] = [];
  searchTerm: string = '';

  allItems: SelectableItem[] = [];
  filteredItems: SelectableItem[] = [];

  get tagDisplayPrefix(): string {
    return this.tagDisplayMode === 'category' ? 'Category: ' : 'Region: ';
  }

  @ViewChild('selectAllBtn', { static: false }) selectAllBtn: ElementRef;

  private resizeObserver: ResizeObserver | null = null;
  private resizeTimeout: any = null;

  ngOnInit() {
    this.loadItemListFromSetting();
    this.loadSelectedItemsFromSetting();
    // Initialize filteredItems to show all items initially
    this.filterItems();
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.adjustColumnsForDialogWidth();
      this.setupResizeListener();
      
      if (this.selectAllBtn && this.selectAllBtn.nativeElement) {
        this.selectAllBtn.nativeElement.focus();
      }
      
      setTimeout(() => {
        this.adjustColumnsForDialogWidth();
      }, 50);
    }, 50);
  }

  ngOnDestroy() {
    // Clean up resize listener
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
    }
    if (this.resizeTimeout) {
      clearTimeout(this.resizeTimeout);
    }
  }

  loadItemList() {
    if (this.itemList && this.itemList.length > 0) {
      this.allItems = this.itemList.map((item: any) => {
        const regions = item.tags?.Regions;
        const categories = item.tags?.ItemCategory;
        const regionList: string[] = Array.isArray(regions)
          ? regions.filter((r: any) => r != null).map((r: any) => String(r))
          : (typeof regions === 'string' || typeof regions === 'number') ? [String(regions)] : [];
        const categoryList: string[] = Array.isArray(categories)
          ? categories.filter((c: any) => c != null).map((c: any) => String(c))
          : (typeof categories === 'string' || typeof categories === 'number') ? [String(categories)] : [];
        const display = this.tagDisplayMode === 'category' ? categoryList.join(', ') : regionList.join(', ');

        return {
          name: item.value,
          label: item.label,
          selected: this.selectedItems.includes(item.value),
          regionDisplay: display || undefined,
          regionSearchLower: display ? display.toLowerCase() : undefined,
        } as SelectableItem;
      });
      return;
    }

    // If setting has options (for settings with ItemList), use those
    if (this.setting && this.setting.options) {
      this.allItems = this.setting.options.map((item: any) => {
        const regions = item.tags?.Regions;
        const categories = item.tags?.ItemCategory;
        const regionList: string[] = Array.isArray(regions)
          ? regions.filter((r: any) => r != null).map((r: any) => String(r))
          : (typeof regions === 'string' || typeof regions === 'number') ? [String(regions)] : [];
        const categoryList: string[] = Array.isArray(categories)
          ? categories.filter((c: any) => c != null).map((c: any) => String(c))
          : (typeof categories === 'string' || typeof categories === 'number') ? [String(categories)] : [];
        const display = this.tagDisplayMode === 'category' ? categoryList.join(', ') : regionList.join(', ');

        return {
          name: item.Value,
          label: item.Label,
          selected: this.selectedItems.includes(item.Value),
          regionDisplay: display || undefined,
          regionSearchLower: display ? display.toLowerCase() : undefined,
        } as SelectableItem;
      });
      return;
    }

    this.allItems = [];
  }

  loadItemListFromSetting() {
    // Check if we have itemList input first
    if (this.itemList && this.itemList.length > 0) {
      this.allItems = this.itemList.map((item: any) => {
        const regions = item.tags?.Regions || item.tags?.regions;
        const categories = item.tags?.ItemCategory;
        const regionList: string[] = Array.isArray(regions)
          ? regions.filter((r: any) => r != null).map((r: any) => String(r))
          : (typeof regions === 'string' || typeof regions === 'number') ? [String(regions)] : [];
        const categoryList: string[] = Array.isArray(categories)
          ? categories.filter((c: any) => c != null).map((c: any) => String(c))
          : (typeof categories === 'string' || typeof categories === 'number') ? [String(categories)] : [];
        const display = this.tagDisplayMode === 'category' ? categoryList.join(', ') : regionList.join(', ');

        const selectableItem = {
          name: item.value || item.Value || item.name || item.Name,
          label: item.label || item.Label || item.name || item.Name,
          selected: this.selectedItems.includes(item.value || item.Value || item.name || item.Name),
          regionDisplay: display || undefined,
          regionSearchLower: display ? display.toLowerCase() : undefined,
        } as SelectableItem;
        
        return selectableItem;
      });
      
      return;
    }
    
    if (this.setting && this.setting.options) {
      this.allItems = this.setting.options.map((item: any) => {
        const regions = item.tags?.Regions || item.tags?.regions;
        const categories = item.tags?.ItemCategory;
        const regionList: string[] = Array.isArray(regions)
          ? regions.filter((r: any) => r != null).map((r: any) => String(r))
          : (typeof regions === 'string' || typeof regions === 'number') ? [String(regions)] : [];
        const categoryList: string[] = Array.isArray(categories)
          ? categories.filter((c: any) => c != null).map((c: any) => String(c))
          : (typeof categories === 'string' || typeof categories === 'number') ? [String(categories)] : [];
        const display = this.tagDisplayMode === 'category' ? categoryList.join(', ') : regionList.join(', ');

        const selectableItem = {
          name: item.Value || item.value || item.name,
          label: item.Label || item.label || item.name,
          selected: this.selectedItems.includes(item.Value || item.value || item.name),
          regionDisplay: display || undefined,
          regionSearchLower: display ? display.toLowerCase() : undefined,
        } as SelectableItem;
        
        return selectableItem;
      });
    } else {
      this.allItems = [];
    }
  }

  loadSelectedItemsFromSetting() {
    if (this.assignmentSettingsMap && this.assignmentSettingName) {
      this.selectedItems = this.assignmentSettingsMap[this.assignmentSettingName] || [];
    }
  }


  filterItems() {
    const term = (this.searchTerm || '').toLowerCase();
    if (!term) {
      this.filteredItems = this.allItems;
      return;
    }

    this.filteredItems = this.allItems.filter((item) => {
      if (item.label && item.label.toLowerCase().includes(term)) return true;
      if (item.name && item.name.toLowerCase().includes(term)) return true;
      if (item.regionSearchLower && item.regionSearchLower.includes(term)) return true;
      return false;
    });
  }

  onSearchChange() {
    this.filterItems();
  }

  toggleItem(item: SelectableItem) {
    item.selected = !item.selected;
  }

  selectAll() {
    this.filteredItems.forEach(item => item.selected = true);
    this.filterItems();
  }

  deselectAll() {
    this.filteredItems.forEach(item => item.selected = false);
    this.filterItems();
  }

  getSelectedItems(): string[] {
    return this.allItems
      .filter(item => item.selected)
      .map(item => item.name);
  }

  getSelectedCount(): number {
    return this.allItems.filter(item => item.selected).length;
  }

  confirmDialog() {
    const selectedItems = this.getSelectedItems();
    this.ref.close(selectedItems);
  }

  cancelDialog() {
    this.ref.close(null);
  }

  constructor(protected ref: NbDialogRef<MMRItemSelectorWindowComponent>, public global: GUIGlobal) {}

  /**
   * Dynamically adjust the number of columns based on actual dialog width
   * This provides a fallback for browsers without container query support
   */
  private adjustColumnsForDialogWidth() {
    const dialogElement = document.querySelector('.mmrItemSelector-window') as HTMLElement;
    if (!dialogElement) return;

    const dialogWidth = dialogElement.offsetWidth;
    const itemsGrid = dialogElement.querySelector('.items-grid') as HTMLElement;
    if (!itemsGrid) return;

    const minColumnWidth = 180;
    const gap = 5;
    const padding = 24;
    
    const availableWidth = dialogWidth - padding;
    const maxColumns = Math.floor((availableWidth + gap) / (minColumnWidth + gap));
    
    const optimalColumns = Math.max(1, Math.min(3, maxColumns));
    
    itemsGrid.style.gridTemplateColumns = `repeat(${optimalColumns}, 1fr)`;
    
    if (dialogWidth < 500) {
      const adjustedMinWidth = Math.max(140, (availableWidth - (gap * (optimalColumns - 1))) / optimalColumns);
      itemsGrid.style.setProperty('--min-column-width', `${adjustedMinWidth}px`);
    }
    
    const itemLabels = dialogElement.querySelectorAll('.item-label') as NodeListOf<HTMLElement>;
    itemLabels.forEach(label => {
      label.style.whiteSpace = 'normal';
      label.style.wordWrap = 'break-word';
      label.style.overflowWrap = 'break-word';
      label.style.overflow = 'hidden';
    });
    
    itemsGrid.style.maxWidth = '100%';
    itemsGrid.style.overflowX = 'hidden';
  }


  /**
   * Set up resize listener for the dialog
   */
  private setupResizeListener() {
    const dialogElement = document.querySelector('.mmrItemSelector-window') as HTMLElement;
    if (!dialogElement) return;

    if (window.ResizeObserver) {
      this.resizeObserver = new ResizeObserver((entries) => {
        if (this.resizeTimeout) {
          clearTimeout(this.resizeTimeout);
        }
        this.resizeTimeout = setTimeout(() => {
          this.adjustColumnsForDialogWidth();
        }, 100);
      });
      this.resizeObserver.observe(dialogElement);
    } else {
      window.addEventListener('resize', () => {
        if (this.resizeTimeout) {
          clearTimeout(this.resizeTimeout);
        }
        this.resizeTimeout = setTimeout(() => {
          this.adjustColumnsForDialogWidth();
        }, 100);
      });
    }
    
    const resizeCheckInterval = setInterval(() => {
      if (dialogElement && dialogElement.offsetWidth > 0) {
        this.adjustColumnsForDialogWidth();
      } else {
        clearInterval(resizeCheckInterval);
      }
    }, 500);
    
    this.resizeTimeout = setTimeout(() => {
      clearInterval(resizeCheckInterval);
    }, 10000);
  }
} 