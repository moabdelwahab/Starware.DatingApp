import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpStatusCode } from '../common/StatusCode';
import { ApiResponse } from '../models/common/ApiResponse';
import { MemberDto } from '../models/users/MemberDto';
import { PhotoDto } from '../models/users/photoDto';

const  apiUrl : string = environment.apiRoot+'Users/'; 

@Injectable({
  providedIn: 'any'
})

export class UsersService {
  
  usersResponse : ApiResponse<MemberDto[]>;

  constructor(private httpClient: HttpClient) { 
  }

  getAllUsers () : Observable<ApiResponse<MemberDto[]>>
  {
    if(this.usersResponse?.data?.length > 0){
      return of(this.usersResponse);
    }else
    {
      return this.httpClient.get<ApiResponse<MemberDto[]>>(apiUrl + 'GetAll').pipe(
        map((usersResponse) => {
          this.usersResponse = usersResponse;
          return usersResponse;
        })
      );
    }
  }

  
  getUserByUsername (username:string) : Observable<ApiResponse<MemberDto>>
  {
    if(this.usersResponse?.data?.length > 0)
    {
      console.log("working fine");
      let user = this.usersResponse?.data.filter(x => x.userName == username)[0];
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
    return this.httpClient.put<boolean>( apiUrl+'updateUser',member).pipe(
      map((response)=>
      {
        if(this.usersResponse?.data?.length > 0 )
        {
          let memberIndex = this.usersResponse.data.indexOf(member);
          this.usersResponse.data[memberIndex]= member;
        }
        return response;
      })
    );
  }


  addPhoto(FormData:FormData): Observable<ApiResponse<PhotoDto>>{
    return this.httpClient.post<ApiResponse<PhotoDto>>(apiUrl+'add-photo',FormData);
  }

  deletePhoto(publicId:string) : Observable<ApiResponse<boolean>>
  {
    return this.httpClient.delete<ApiResponse<boolean>>(apiUrl+'delete-photo/'+publicId);
  }

  setMainPhoto(photoId:number) : Observable<ApiResponse<boolean>>
  {
    return this.httpClient.put<ApiResponse<boolean>>(apiUrl+'set-main-photo/'+photoId,null);
  }
}
