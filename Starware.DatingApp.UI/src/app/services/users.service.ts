import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppUser } from '../models/AppUser';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private httpClient: HttpClient) { 

  }

  getAllUsers () : Observable<AppUser[]>
  {
    return this.httpClient.get<AppUser[]>('https://localhost:5001/api/Users/GetAll');
  }

}
