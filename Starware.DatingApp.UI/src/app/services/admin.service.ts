import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserDto } from '../models/users/UserDto';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  apiUrl:string = environment.apiRoot+'admin/';


  constructor(private http:HttpClient) {
    
   }

   getUsersWithRoles()
   {
    return this.http.get<Partial<UserDto>>(this.apiUrl+'users-with-roles');
   }

   updateUserRoles(username:string, roles:string)
   {
     let httpParams = new HttpParams();
     httpParams = httpParams.set('roles',roles);
    console.log(roles);
    return this.http.post(this.apiUrl+'edit-roles/'+username+"?roles="+roles,{});
   }
}
