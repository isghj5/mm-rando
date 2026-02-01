import { Component } from '@angular/core';
import { MENU_ITEMS } from './pages-menu';

@Component({
  selector: 'ngx-pages',
  template: `
    <mmr-gui-layout>
      <router-outlet></router-outlet>
    </mmr-gui-layout>
  `,
  standalone: false
})
export class PagesComponent {
  menu = MENU_ITEMS;
}
