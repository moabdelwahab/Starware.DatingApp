import { Component, OnInit } from '@angular/core';
import { AppUser } from './models/AppUser';
import { UsersService } from './services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'DatingApp';
  systemUsers : AppUser[] = [] ;
  constructor(private userService :UsersService ) {
  }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers()
  {
    this.userService.getAllUsers().subscribe((appUsers)=>
    {
      this.systemUsers = appUsers;
    });
  }

}
