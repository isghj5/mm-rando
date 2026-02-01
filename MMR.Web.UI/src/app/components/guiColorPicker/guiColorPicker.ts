import { Component, Input, Output, EventEmitter, ChangeDetectorRef, AfterViewInit, ElementRef, Renderer2 } from '@angular/core';

@Component({
  selector: 'zsr-gui-color-picker',
  templateUrl: './guiColorPicker.html',
  styleUrls: ['./guiColorPicker.scss'],
  standalone: false
})
export class GUIColorPickerComponent implements AfterViewInit {

  @Input() tooltip: any = 'tooltip';
  @Input() tooltipComponent: any = null;

  @Input() disabled: boolean = false;
  @Input() colorArraySize: number = null;
  @Input() colorValue: any = null;

  @Output() colorValueChange: EventEmitter<any> = null;

  private observer: MutationObserver;

  constructor(
    private cd: ChangeDetectorRef,
    private elementRef: ElementRef,
    private renderer: Renderer2
  ) {
    this.colorValueChange = new EventEmitter<any>();
  }

  ngAfterViewInit() {
    // Set up a mutation observer to watch for color picker elements being added
    this.setupColorPickerPositioning();
  }

  ngOnDestroy() {
    // Clean up the mutation observer
    if (this.observer) {
      this.observer.disconnect();
    }
  }

  ngOnChanges(changeRecord) {
  }

  private setupColorPickerPositioning() {
    // Create a mutation observer to watch for color picker elements
    this.observer = new MutationObserver((mutations) => {
      mutations.forEach((mutation) => {
        if (mutation.type === 'childList') {
          mutation.addedNodes.forEach((node) => {
            if (node.nodeType === Node.ELEMENT_NODE) {
              const element = node as Element;
              if (element.classList && element.classList.contains('color-picker')) {
                if (!element.hasAttribute('data-positioned')) {
                  element.setAttribute('data-positioned', 'true');
                  this.fixColorPickerPosition(element as HTMLElement);
                }
              }
            }
          });
        }
      });
    });

    // Start observing the document body for color picker additions
    this.observer.observe(document.body, {
      childList: true,
      subtree: true
    });    
  }

  private fixColorPickerPosition(colorPicker: HTMLElement) {
    // Find the trigger element (the nb-user that was clicked)
    const triggerElement = this.findTriggerElement();
    if (!triggerElement) {
      return;
    }

    // Get the trigger element's position
    const triggerRect = triggerElement.getBoundingClientRect();
    const viewportHeight = window.innerHeight;
    const viewportWidth = window.innerWidth;
    const pickerHeight = colorPicker.offsetHeight || 300;
    const pickerWidth = colorPicker.offsetWidth || 230;

    // Position the color picker right next to the trigger element
    // Use the same top coordinate as the trigger element
    let top = triggerRect.top;
    let left = triggerRect.right + 10; // 10px to the right of the trigger

    // Check if there's enough space to the right
    if (left + pickerWidth > viewportWidth - 10) {
      // Not enough space to the right, position to the left
      left = triggerRect.left - pickerWidth - 10;
      
      // If still not enough space to the left, center it on the trigger
      if (left < 10) {
        left = Math.max(10, triggerRect.left + (triggerRect.width / 2) - (pickerWidth / 2));
      }
    }

    // Check if the picker would go off the bottom of the viewport
    if (top + pickerHeight > viewportHeight - 10) {
      // Adjust top position to keep picker in viewport
      top = Math.max(10, viewportHeight - pickerHeight - 10);
    }

    // Check if the picker would go off the top of the viewport
    if (top < 10) {
      top = 10;
    }

    // Final boundary checks to ensure we don't go off-screen
    if (left < 10) left = 10;
    if (left + pickerWidth > viewportWidth - 10) {
      left = viewportWidth - pickerWidth - 10;
    }

    // Apply the positioning
    this.renderer.setStyle(colorPicker, 'position', 'fixed');
    this.renderer.setStyle(colorPicker, 'top', `${top}px`);
    this.renderer.setStyle(colorPicker, 'left', `${left}px`);
    this.renderer.setStyle(colorPicker, 'z-index', '9999');

    setTimeout(() => {
      colorPicker.classList.add('positioned');
    }, 25);
  }

  private findTriggerElement(): HTMLElement | null {
    // Find the nb-user element within this component
    const nbUser = this.elementRef.nativeElement.querySelector('nb-user');
    return nbUser;
  }

  openColorPicker(refColorPicker: HTMLInputElement = null, colorIndex: number = null) {
    //Open color picker
    if (refColorPicker && !this.disabled) {
      refColorPicker.click();
      
      // The mutation observer will handle positioning automatically
      // Keep a small fallback delay just in case
      setTimeout(() => {
        const colorPicker = document.querySelector('.color-picker');
        if (colorPicker && !colorPicker.hasAttribute('data-positioned')) {
          this.fixColorPickerPosition(colorPicker as HTMLElement);
        }
      }, 50);
    }
  }

  afterColorPickerClosed(colorIndex: number = null) {
    //Emit event with full value so GUI saves settings
    this.colorValueChange.emit(this.colorValue);
  }

  pickNewColor(newColor: string, colorIndex: number = null) {

    //console.log("Picked new color:", newColor, "colorIndex:", colorIndex);

    if (colorIndex != null) {
      this.colorValue[colorIndex] = this.convertPickerRGBToValueRGB(newColor);
    }
    else {
      this.colorValue = this.convertPickerRGBToValueRGB(newColor);
    }
  }

  randomizeColors() {

    if (this.disabled)
      return;

    //Randomize all colors, then emit event
    if (this.colorArraySize != null) {
      //Array colors. Randomize each one
      for (let i = 0; i < this.colorArraySize; i++) {
        this.colorValue[i] = this.generateRandomColor();
      }
    }
    else {
      //Single color picker
      this.colorValue = this.generateRandomColor();
    }

    this.colorValueChange.emit(this.colorValue);
  }

  getRGBDisplayValue(inputColor: string) {
    //Remove spaces to optimize string length
    let stringSplit = inputColor.split(",");
    return stringSplit.join(",");
  }

  convertValueRGBToColorPickerRGB(inputColor: string) {
    return `rgb(${inputColor})`;
  }

  convertPickerRGBToValueRGB(pickerColor: string) {
    let reducedString = pickerColor.replace("rgb(", "").replace(")", "");

    //Add space after commas to match setting spec
    let stringSplit = reducedString.split(",");
    return stringSplit.join(", ");
  }

  generateRandomColor() {
    let colors = [Math.floor(Math.random() * 255), Math.floor(Math.random() * 255), Math.floor(Math.random() * 255)];
    return colors.join(", ");
  }
}
