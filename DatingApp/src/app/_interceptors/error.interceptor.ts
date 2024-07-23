import { HttpInterceptorFn } from '@angular/common/http';
import {inject} from "@angular/core";
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError(error =>
    {
      if(error) {
        switch (error.statusCode){
          case 400:
            if(error.error.errors)
            {
              toastr.error(error.error,error.statusCode)
            }
            break;
          case 1:
            toastr.error('UknownError',error.statusCode)
            break;

          default:
            break;
        }
      }
    })
  )
};
