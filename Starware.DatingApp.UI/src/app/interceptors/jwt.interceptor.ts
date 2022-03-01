import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../services/account.service';
import { UserDto } from '../models/users/UserDto';
import { take } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  private token: string;
  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    let currentUser: UserDto;

    this.accountService.$currentUser.pipe(take(1)).subscribe(user => currentUser = user);
    if (currentUser) {
      currentUser.lastActive = new Date();
      localStorage.removeItem('user');
      console.log(currentUser);
      localStorage.setItem('user',JSON.stringify(currentUser));
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${currentUser.token}` }
      });
    }
    return next.handle(request);
  }


}
