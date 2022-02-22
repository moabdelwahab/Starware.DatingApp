import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastrService: ToastrService, private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        switch (error.status) {

          case 404:
            this.router.navigateByUrl('/not-found');
            this.toastrService.error("Page request has not been found");
            break;

            case 400:
            if (error.error.errors) {
              const modalStateErrors = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key]) {
                  modalStateErrors.push(error.error.errors[key]);
                }
              }
              throw modalStateErrors.flat();
            }
            break;

            case 401:
            this.toastrService.error("you are not authorized !");
            break;

          case 500:
            const navigationExtra: NavigationExtras = { state: { error: error.error } }
            this.router.navigateByUrl('server-error', navigationExtra);
            this.toastrService.error(error.statusText, error.status);
            break;

          default:
            break;
        }
        return throwError(error);
      })
    );
  }
}
