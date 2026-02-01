import { Directive, Input, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatGridList } from '@angular/material/grid-list';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

export interface IResponsiveColumnsMap {
  xs?: number;
  sm?: number;
  md?: number;
  lg?: number;
  xl?: number;
}

// Usage: <mat-grid-list [responsiveCols]="{xs: 2, sm: 2, md: 4, lg: 6, xl: 8}">
@Directive({
  selector: '[responsiveCols]',
  standalone: false
})
export class ResponsiveColsDirective implements OnInit {
  private countBySize: IResponsiveColumnsMap = {xs: 2, sm: 2, md: 4, lg: 6, xl: 8};

  public get cols(): IResponsiveColumnsMap {
    return this.countBySize;
  }

  @Input('responsiveCols')
  public set cols(map: IResponsiveColumnsMap) {

    if (map && ('object' === (typeof map))) {
      this.countBySize = map;
    }
  }

  public constructor(
    private grid: MatGridList,
    private breakpointObserver: BreakpointObserver,
    private cd: ChangeDetectorRef
  ) {

    this.initializeColsCount();

    //Default
    if (!this.grid.cols)
      this.grid.cols = 2;

    cd.markForCheck();
  }

  public ngOnInit(): void {
    this.cd.detectChanges();
    this.initializeColsCount();

    //Default
    if (!this.grid.cols)
      this.grid.cols = 2;

    this.cd.markForCheck();
    this.cd.detectChanges();

    this.breakpointObserver.observe([
      Breakpoints.XSmall,
      Breakpoints.Small,
      Breakpoints.Medium,
      Breakpoints.Large,
      Breakpoints.XLarge
    ]).subscribe(result => {
      if (result.matches) {
        // Find which breakpoint is active
        if (this.breakpointObserver.isMatched(Breakpoints.XSmall)) {
          this.grid.cols = this.countBySize.xs || 2;
        } else if (this.breakpointObserver.isMatched(Breakpoints.Small)) {
          this.grid.cols = this.countBySize.sm || 2;
        } else if (this.breakpointObserver.isMatched(Breakpoints.Medium)) {
          this.grid.cols = this.countBySize.md || 4;
        } else if (this.breakpointObserver.isMatched(Breakpoints.Large)) {
          this.grid.cols = this.countBySize.lg || 6;
        } else if (this.breakpointObserver.isMatched(Breakpoints.XLarge)) {
          this.grid.cols = this.countBySize.xl || 8;
        }
        this.cd?.markForCheck();
      }
    });
  }

  private initializeColsCount(): void {
    // Set initial columns based on current breakpoint
    if (this.breakpointObserver.isMatched(Breakpoints.XSmall)) {
      this.grid.cols = this.countBySize.xs || 2;
    } else if (this.breakpointObserver.isMatched(Breakpoints.Small)) {
      this.grid.cols = this.countBySize.sm || 2;
    } else if (this.breakpointObserver.isMatched(Breakpoints.Medium)) {
      this.grid.cols = this.countBySize.md || 4;
    } else if (this.breakpointObserver.isMatched(Breakpoints.Large)) {
      this.grid.cols = this.countBySize.lg || 6;
    } else if (this.breakpointObserver.isMatched(Breakpoints.XLarge)) {
      this.grid.cols = this.countBySize.xl || 8;
    }
  }
}
