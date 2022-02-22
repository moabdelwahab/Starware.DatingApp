import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AppUser } from './models/users/AppUser';
import { MemberDto } from './models/users/MemberDto';
import { UserDto } from './models/users/UserDto';
import { AccountService } from './services/account.service';
import { UsersService } from './services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'DatingApp';
  systemUsers: MemberDto[] = [];
  loggedIn: boolean = false;
  user :UserDto ;
  constructor(private userService: UsersService,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: UserDto = JSON.parse(localStorage.getItem('user'));
    if (user) {

      this.user = user;
      this.accountService.setCurrentUser(user);
      this.loggedIn = !!user;
    }
  }
}
