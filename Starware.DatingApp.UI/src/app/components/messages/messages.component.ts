import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Pagination } from 'src/app/models/common/Pagination';
import { Message } from 'src/app/models/users/Message';
import { UserDto } from 'src/app/models/users/UserDto';
import { AccountService } from 'src/app/services/account.service';
import { MessagesService } from 'src/app/services/messages.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  pagination:Pagination = new Pagination();
  container:string ="Inbox"; 
  messages:Message[]=[];
  user:UserDto;

  constructor(private messageService:MessagesService,
              private accountService:AccountService) {

               }

  ngOnInit(): void {
    this.pagination.PageSize = 50;
    this.getCurrentUser();
  }

  getMessages()
  {
    this.messageService.getUserMessages(this.pagination.PageNumber,this.pagination.PageSize,this.container)
    .subscribe((respons)=>
    {
      this.pagination = respons.pagination;
      this.messages = respons.result.data;
    })
  }

  pageChanged(e:any)
  {
    this.pagination.PageNumber = e.page;
    this.getMessages();
  }

  getCurrentUser(){
    this.accountService.$currentUser.pipe(take(1)).subscribe(user=>
    {
      this.user = user;
    })
  }

}
