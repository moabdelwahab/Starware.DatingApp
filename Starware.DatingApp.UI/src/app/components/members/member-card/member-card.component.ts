import { Component, Input, OnInit } from '@angular/core';
import { MemberDto } from 'src/app/models/users/MemberDto';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() user : MemberDto ;
  constructor() { 
    
  }

  ngOnInit(): void {
  }

}
