import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHttpParams } from '../common/PaginationHelper';
import { ApiResponse } from '../models/common/ApiResponse';
import { PaginatedResult, Pagination } from '../models/common/Pagination';
import { Message } from '../models/users/Message';
import { AccountService } from './account.service';

const  messageApiUrl:string = environment.apiRoot+'message/';

@Injectable({
  providedIn: 'root'
})

export class MessagesService {

messagePaginatinoResult : PaginatedResult<ApiResponse<Message[]>> = new PaginatedResult<ApiResponse<Message[]>>() ;

  constructor(private httpClient:HttpClient,
     private accountService:AccountService) {
   }

  getMessageThread(senderUsername:string) : Observable<ApiResponse<Message[]>>
  {
    let params = new HttpParams();
    params = params.set('senderUsername',senderUsername);
    return this.httpClient.get<ApiResponse<Message[]>>(messageApiUrl + 'get-thread',
    {params:params});    
  }

  getUserMessages(page:number ,pageSize:number,container:string)
  {
    let params = new HttpParams();
    params= getPaginationHttpParams(page,pageSize).set('Container',container);
    console.log(params);

    return getPaginatedResult<ApiResponse<Message[]>>(messageApiUrl+'get-messages',params,this.httpClient).pipe(
    (map(response => 
      {
        this.messagePaginatinoResult.pagination = response.pagination;
        this.messagePaginatinoResult.result = response.result;
        return this.messagePaginatinoResult;
      }))
    );
  }

  sendMessage(message :{Content:string ,RecipientUsername:string}) : Observable<ApiResponse<Message>>
  {
    return this.httpClient.post<ApiResponse<Message>>(messageApiUrl+'add-message', message);
  }


}
