import {ApplicationConfig, importProvidersFrom, provideZoneChangeDetection} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {provideAnimations} from "@angular/platform-browser/animations";
import { provideToastr } from 'ngx-toastr';
import {jwtInterceptor} from "./_interceptors/jwt.interceptor";
import {NgxSpinnerModule} from "ngx-spinner";
import {loadingInterceptor} from "./_interceptors/loading.interceptor";

export const appConfig: ApplicationConfig = {
  providers:
    [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes),
    provideHttpClient(withInterceptors([jwtInterceptor,loadingInterceptor])), provideAnimationsAsync() , provideAnimations(),
      provideToastr({
      positionClass: 'toast-bottom-right'
    }),
      importProvidersFrom(NgxSpinnerModule)
    ]


};
