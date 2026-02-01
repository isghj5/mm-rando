import {Inject, Injectable, Renderer2, RendererFactory2} from '@angular/core';
import {DOCUMENT} from '@angular/common';
import {GUIGlobal} from './GUIGlobal';

export enum MMR_THEME {
  DARK = 'nb-theme-mmr-dark',
  LIGHT = 'nb-theme-mmr-light'
}

@Injectable({
  providedIn: 'root'
})
export class ThemeSwitcher {

  themeReady: boolean;
  isLightThemeActive: boolean;

  private renderer: Renderer2;
  private body: Element;
  private generatorContainer: Element;

  constructor(
    private readonly rendererFactory: RendererFactory2,
    private readonly global: GUIGlobal,
    @Inject(DOCUMENT) private readonly document: Document,
  ) {
    this.renderer = rendererFactory.createRenderer(null, null);
  }

  initTheme() {
    this.body = this.document.getElementsByTagName('body')[0];
    this.generatorContainer = this.document.querySelector('div#generator');

    const themeFromSettings = this.global.generator_settingsMap["theme"];
    this.isLightThemeActive = themeFromSettings === 'mmr-light';

    this.removeAllThemes(this.body);
    this.removeAllThemes(this.generatorContainer);

    this.renderer.addClass(this.generatorContainer, this.isLightThemeActive ? MMR_THEME.LIGHT : MMR_THEME.DARK );
    this.themeReady = true;

    //Subscribe to external event
    this.global.globalEmitter.subscribe(eventObj => {
      if (eventObj?.name === 'theme_switch') {

        let theme = eventObj?.message;

        //Ensure the theme actually needs to switch
        if ((theme === 'mmr-light' && this.isLightThemeActive) || (theme === 'mmr-dark' && !this.isLightThemeActive))
          return;

        this.switchTheme();
      }
    });
  }

  switchTheme() {

    if (!this.themeReady)
      return;

    if (this.isLightThemeActive) {
      this.renderer.addClass(this.generatorContainer, MMR_THEME.DARK);
      this.renderer.removeClass(this.generatorContainer, MMR_THEME.LIGHT);
      this.global.generator_settingsMap["theme"] = 'mmr-dark';
    } else {
      this.renderer.addClass(this.generatorContainer, MMR_THEME.LIGHT);
      this.renderer.removeClass(this.generatorContainer, MMR_THEME.DARK);
      this.global.generator_settingsMap["theme"] = 'mmr-light';
    }

    this.isLightThemeActive = !this.isLightThemeActive;
    this.global.saveCurrentSettingsToFile();
  }

  private removeAllThemes(body: Element): void {
    body?.classList?.value
      ?.split(' ')
      .filter(c => c.startsWith('nb-theme'))
      .forEach(c => this.renderer.removeClass(body, c));
  }
}
