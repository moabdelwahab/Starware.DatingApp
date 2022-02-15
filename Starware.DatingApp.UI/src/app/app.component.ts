import { Component, OnInit } from '@angular/core';
import { AppUser } from './models/users/AppUser';
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
  systemUsers : AppUser[] = [] ;
  constructor(private userService :UsersService , private accountService: AccountService ) {
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser()
  {
    const user : UserDto = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  getAllUsers()
  {

    this.userService.getAllUsers().subscribe((appUsers)=>
    {
      this.systemUsers = appUsers ;
    });
  }

}
