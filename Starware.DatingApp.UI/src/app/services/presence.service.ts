import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserDto } from '../models/users/UserDto';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnetion: HubConnection;
  private onlineUSersSource = new BehaviorSubject<string[]>([]);
  $onlineUsers = this.onlineUSersSource.asObservable();

  constructor(private toats: ToastrService) {

  }

  createHubConnection(user: UserDto) {
    this.hubConnetion = new HubConnectionBuilder().withUrl(this.hubUrl + 'presence',
      {
        accessTokenFactory: () => user.token
      }).withAutomaticReconnect().build();

    this.hubConnetion.start().catch(error => console.log(error));

    this.hubConnetion.on('UserIsOnline', username => {
      this.toats.info(username + ' has connected');
    });

    this.hubConnetion.on('UserIsOffline', username => {
      this.toats.warning(username + ' has disconnected');
    });

    this.hubConnetion.on('GetOnlineUsers', users => {
      this.onlineUSersSource.next(users);
    });

  }

  StopConnection() {
    this.hubConnetion.stop().catch(error => console.log(error));
    this.hubConnetion.on('GetOnlineUsers', users => {
      this.onlineUSersSource.next(users);
    });
  }
}
