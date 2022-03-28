import { HttpClient, HttpParams } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHttpParams } from '../common/PaginationHelper';
import { ApiResponse } from '../models/common/ApiResponse';
import { PaginatedResult, Pagination } from '../models/common/Pagination';
import { Message } from '../models/users/Message';
import { UserDto } from '../models/users/UserDto';
import { AccountService } from './account.service';

const messageApiUrl: string = environment.apiRoot + 'message/';

@Injectable({
  providedIn: 'root'
})

export class MessagesService {

  private hubConnection: HubConnection;
  hubUrl = environment.hubUrl;
  messagesSource = new BehaviorSubject<Message[]>([]);
  $messagesThread = this.messagesSource.asObservable();
  MessageRecived : EventEmitter<boolean> = new EventEmitter();

  messagePaginatinoResult: PaginatedResult<ApiResponse<Message[]>> = new PaginatedResult<ApiResponse<Message[]>>();

  constructor(private httpClient: HttpClient,
    private accountService: AccountService) {
  }

  createHubConnection(user: UserDto, otherUserName: string) {
    
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?user=' + otherUserName, {
        accessTokenFactory: () => user.token
      }).withAutomaticReconnect().build();

    this.hubConnection.start().catch(error=> console.log(error));

    this.hubConnection.on("ReciveMessageThread", messages => {
      this.messagesSource.next(messages);
    });

    this.hubConnection.on("NewMessage", message => {
      this.$messagesThread.pipe(take(1)).subscribe(messages => 
        {
          this.messagesSource.next([...messages, message]);
          this.MessageRecived.emit(true);
        });
    });
  }

  async sendMessage(message:any)
  {
   return this.hubConnection.invoke('AddMessage',message).catch(error => console.log(error));
  }


  StopHubConnection()
  {
    this.hubConnection.stop();
  }

  getMessageThread(senderUsername: string): Observable<ApiResponse<Message[]>> {
    let params = new HttpParams();
    params = params.set('senderUsername', senderUsername);
    return this.httpClient.get<ApiResponse<Message[]>>(messageApiUrl + 'get-thread',
      { params: params });
  }

  getUserMessages(page: number, pageSize: number, container: string) {
    let params = new HttpParams();
    params = getPaginationHttpParams(page, pageSize).set('Container', container);
    console.log(params);

    return getPaginatedResult<ApiResponse<Message[]>>(messageApiUrl + 'get-messages', params, this.httpClient).pipe(
      (map(response => {
        this.messagePaginatinoResult.pagination = response.pagination;
        this.messagePaginatinoResult.result = response.result;
        return this.messagePaginatinoResult;
      }))
    );
  }

  // sendMessage(message: { Content: string, RecipientUsername: string }): Observable<ApiResponse<Message>> {
  //   return this.httpClient.post<ApiResponse<Message>>(messageApiUrl + 'add-message', message);
  // }


}
