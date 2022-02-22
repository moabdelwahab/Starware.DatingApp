import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpStatusCode } from '../common/StatusCode';
import { ApiResponse } from '../models/common/ApiResponse';
import { MemberDto } from '../models/users/MemberDto';

const  apiUrl : string = environment.apiRoot+'Users/'; 

@Injectable({
  providedIn: 'any'
})

export class UsersService {
  
  users : ApiResponse<MemberDto[]> ;
  constructor(private httpClient: HttpClient) { 
  }

  getAllUsers () : Observable<ApiResponse<MemberDto[]>>
  {
    if(this.users){
      return of(this.users);
    }else
    {
      return this.httpClient.get<ApiResponse<MemberDto[]>>(apiUrl + 'GetAll');
    }
  }

  
  getUserByUsername (username:string) : Observable<ApiResponse<MemberDto>>
  {
    if(this.users?.data)
    {
      console.log("working fine");
      let user = this.users.data.filter(x => x.userName == username)[0];
      let response = new ApiResponse<MemberDto>();
      response.data = user;
      response.statusCode = HttpStatusCode.OK;
      return of(response);
    }
    else{
      return this.httpClient.get<ApiResponse<MemberDto>>(apiUrl + 'GetUserByUsername/'+ username);
    }
  }

  updateUser(member:MemberDto): Observable<boolean>
  {
    return this.httpClient.put<boolean>( apiUrl+'updateUser',member);
  }
}
