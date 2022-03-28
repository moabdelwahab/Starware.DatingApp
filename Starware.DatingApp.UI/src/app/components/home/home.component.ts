import { Component, OnInit } from '@angular/core';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { AppUser } from 'src/app/models/users/AppUser';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { RegisterDto } from 'src/app/models/users/RegisterDto';
import { UserDto } from 'src/app/models/users/UserDto';
import { AccountService } from 'src/app/services/account.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  systemUsers: MemberDto[] = [] ; 

  registerMode = false;

  constructor(private userService:UsersService) { }

  ngOnInit(): void {
  }

}
