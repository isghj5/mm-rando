import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production || !environment.hmr) {
  enableProdMode();
}

const bootstrap = () => {
  return platformBrowserDynamic().bootstrapModule(AppModule);
};

if (environment.hmr) {
  if (module['hot']) {
    // Modern Angular HMR approach
    module['hot'].accept();
    module['hot'].dispose(() => {
      // Clean up if needed
    });
    bootstrap().catch(err => console.error(err));
  } else {
    bootstrap().catch(err => console.error(err));
  }
} else {
  bootstrap().catch(err => console.error(err));
}
