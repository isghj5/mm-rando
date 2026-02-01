import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NbDialogModule, NbPopoverModule } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';

import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent, BypassSecurityPipe } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ThemeModule } from './@theme/theme.module';

import { CdkTableModule } from '@angular/cdk/table';
import { DragDropModule } from '@angular/cdk/drag-drop';
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

//Pages
import { GUIGlobal } from './providers/GUIGlobal';

//Custom Components
import { ProgressWindowComponent } from './pages/generator/progressWindow/progressWindow.component';
import { DialogWindowComponent } from './pages/generator/dialogWindow/dialogWindow.component';
import { ErrorDetailsWindowComponent } from './pages/generator/errorDetailsWindow/errorDetailsWindow.component';
import { ConfirmationWindowComponent } from './pages/generator/confirmationWindow/confirmationWindow.component';
import { TextInputWindowComponent } from './pages/generator/textInputWindow/textInputWindow.component';

//MMR only
import { MMRGuiDetailedConfigWindowComponent } from './components/mmr/guiDetailedConfigWindow/guiDetailedConfigWindow.component';
import { MMRItemSelectorWindowComponent } from './components/mmr/itemSelectorWindow/itemSelectorWindow.component';


@NgModule({
    declarations: [
        AppComponent,
        BypassSecurityPipe,
        ProgressWindowComponent,
        DialogWindowComponent,
        ErrorDetailsWindowComponent,
        ConfirmationWindowComponent,
        TextInputWindowComponent,
        MMRGuiDetailedConfigWindowComponent,
        MMRItemSelectorWindowComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        CdkTableModule,
        DragDropModule,
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
        ThemeModule.forRoot(),
        NbDialogModule.forRoot(),
        NbPopoverModule,
        NbEvaIconsModule
    ],
    exports: [
        MatButtonModule,
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
        MatTableModule
    ],
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
    ],
    providers: [
        { provide: APP_BASE_HREF, useValue: '/' },
        GUIGlobal
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
  constructor() { }
}
