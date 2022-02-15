import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/common/ApiResponse';
import { LoginDto } from '../models/users/LoginDto';
import { UserDto } from '../models/users/UserDto';
import { map } from 'rxjs/operators'
import { RegisterDto } from '../models/users/RegisterDto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  apiUrl: string = environment.apiRoot + 'Account/';

  currentUser  : ReplaySubject<UserDto> = new ReplaySubject<UserDto>(1);
  $currentUser : Observable<UserDto> = this.currentUser.asObservable();

  constructor(private httpClient: HttpClient) {

  }

  Login(loginDto: LoginDto): Observable<ApiResponse<UserDto>> {
    return this.httpClient.post<ApiResponse<UserDto>>(this.apiUrl + 'Login', loginDto).pipe(
      map((response: ApiResponse<UserDto>) => {
        if (response.data) {
          localStorage.setItem('user', JSON.stringify(response.data));
          this.currentUser.next(response.data);
          return response;
        }
      })
    );
  }

  Register(registerDto: RegisterDto): Observable<ApiResponse<UserDto>> {
    return this.httpClient.post<ApiResponse<UserDto>>(this.apiUrl + 'Register', registerDto).pipe(
      map((response: ApiResponse<UserDto>) => {
        if (response.data) {
          localStorage.setItem('user', JSON.stringify(response.data));
          this.currentUser.next(response.data);
          return response;
        }
      })
    );
  }
  setCurrentUser(user: UserDto)
  {
    this.currentUser.next(user);
  }

  Logout()
  {
    this.currentUser.next(null);
  }

  isLoggedIn() {
  }


}
