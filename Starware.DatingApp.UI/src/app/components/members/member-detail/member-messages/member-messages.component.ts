import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { Message } from 'src/app/models/users/Message';
import { UserDto } from 'src/app/models/users/UserDto';
import { MessagesService } from 'src/app/services/messages.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {

  @Input() loggedUser: UserDto;
  @Input() member: MemberDto;
  @Output() messageAdded: EventEmitter<Message> = new EventEmitter();
  message: string;
  @ViewChild('scrollMe') private myScrollContainer: ElementRef;
  constructor(public messageService: MessagesService) { }

  ngOnInit(): void {
    this.messageService.MessageRecived.subscribe(()=>
    {
      //scroll here 
    })
  }

  sendMessage() {
    this.messageService.sendMessage({ Content: this.message, RecipientUsername: this.member.userName }).then(() => {
      this.message = '';
    })
  }
}
