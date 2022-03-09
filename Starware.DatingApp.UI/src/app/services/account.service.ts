import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/common/ApiResponse';
import { LoginDto } from '../models/users/LoginDto';
import { UserDto } from '../models/users/UserDto';
import { map } from 'rxjs/operators'
import { RegisterDto } from '../models/users/RegisterDto';
import { HttpStatusCode } from '../common/StatusCode';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  apiUrl: string = environment.apiRoot + 'Account/';

  currentUser: ReplaySubject<UserDto> = new ReplaySubject<UserDto>(1);
  $currentUser: Observable<UserDto> = this.currentUser.asObservable();

  constructor(private httpClient: HttpClient, 
    private toastr: ToastrService,
     private router: Router,
     private presenceService:PresenceService) {

  }

  Login(loginDto: LoginDto): Observable<ApiResponse<UserDto>> {
    return this.httpClient.post<ApiResponse<UserDto>>(this.apiUrl + 'Login', loginDto).pipe(
      map((response: ApiResponse<UserDto>) => {
        response.data.roles = [];
        console.log(response.statusCode);
        if (response.statusCode == HttpStatusCode.OK) {
          this.setCurrentUser(response.data);
          this.presenceService.createHubConnection(response.data);
          localStorage.setItem('user', JSON.stringify(response.data));
          return response;
        } else if (response.statusCode == HttpStatusCode.UNAUTHORIZED) {
          throw response;
        }
      })
    );
  }

  Register(registerDto: RegisterDto): Observable<ApiResponse<UserDto>> {
    return this.httpClient.post<ApiResponse<UserDto>>(this.apiUrl + 'Register', registerDto).pipe(
      map((response: ApiResponse<UserDto>) => {
        if (response.data) {
          response.data.roles = [];
          this.setCurrentUser(response.data);
          this.presenceService.createHubConnection(response.data);
          localStorage.setItem('user', JSON.stringify(response.data));
          return response;
        }
      })
    );
  }

  setCurrentUser(user: UserDto) {
    const roles = this.decodeToken(user.token).role;
    console.log(roles);
    console.log(user);
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    this.toastr.success("Welcome " + user.name);
    this.currentUser.next(user);
    this.router.navigateByUrl('/members');
  }

  Logout() {
    this.presenceService.StopConnection();
    this.currentUser.next(null);
  }

  decodeToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

}
