import { Component, OnInit } from '@angular/core';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  systemUsers:MemberDto[]=[];

  constructor(private userService:UsersService) { }

  ngOnInit(): void {
    this.getAllUsers();
  }

  
  getAllUsers()
  {
    this.userService.getAllUsers().subscribe((reponse)=>
    {
      this.systemUsers = reponse.data ;
    });
  }
}
