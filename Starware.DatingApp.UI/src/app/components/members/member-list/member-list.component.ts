import { Component, OnInit } from '@angular/core';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  systemUsers:MemberDto[]=[];

  constructor(private userService:UsersService) { }

  ngOnInit(): void {
    this.getAllUsers();
  }
  getAllUsers()
  {
    this.userService.getAllUsers().subscribe((appUsers)=>
    {
      this.systemUsers = appUsers.data ;
    });
  }

}
