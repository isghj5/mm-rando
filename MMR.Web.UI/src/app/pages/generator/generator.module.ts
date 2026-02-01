import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ThemeModule } from '../../@theme/theme.module';
import { NbCardModule, NbSpinnerModule } from '@nebular/theme';
import { GeneratorComponent } from './generator.component';

import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';


import { ngfModule } from 'angular-file';
import { ColorPickerModule } from 'ngx-color-picker';
import { DualListComponent } from 'zsr-dual-listbox';


//Custom Directives
import { ResponsiveColsDirective } from '../../directives/responsiveCols.directive';

//Custom Components
import { GUITooltipComponent } from './guiTooltip/guiTooltip.component';
import { GUIModularListboxComponent } from '../../components/guiModularListbox/guiModularListbox';
import { GUIColorPickerComponent } from '../../components/guiColorPicker/guiColorPicker';
import { GUISettingsElement } from '../../components/guiSettingsElement/guiSettingsElement';

@NgModule({
    imports: [
        CommonModule,
        ThemeModule,
        NbCardModule,
        NbSpinnerModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatCardModule,
        MatCheckboxModule,
        MatDialogModule,
        MatGridListModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatProgressBarModule,
        MatProgressSpinnerModule,
        MatRadioModule,
        MatSelectModule,
        MatSlideToggleModule,
        MatSliderModule,
        MatTableModule,
        MatTooltipModule,

        ngfModule,
        ColorPickerModule,
        DualListComponent
    ],
    declarations: [
        GeneratorComponent,
        ResponsiveColsDirective,
        GUITooltipComponent,
        GUIColorPickerComponent,
        GUISettingsElement,
        GUIModularListboxComponent
    ],
    providers: [
      { provide: Window, useValue: window }
    ],
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
    ]
})
export class GeneratorModule { }
