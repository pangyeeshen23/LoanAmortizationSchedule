import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { URL_CONFIG, urlConfig } from './shared/token/api-url';

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: URL_CONFIG, useValue: urlConfig},
    provideHttpClient(withFetch()), 
    provideRouter(routes), 
    provideClientHydration(), DatePipe
  ],
};
