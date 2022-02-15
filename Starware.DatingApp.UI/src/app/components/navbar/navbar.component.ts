import { Component, OnInit } from '@angular/core';
import { LoginDto } from 'src/app/models/users/LoginDto';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  loginData : LoginDto = new LoginDto() ;
  loggedIn:boolean;
  
  constructor(public accountService:AccountService) { }
  ngOnInit(): void {
    this.getCurrentUser();
  }


  Login()
  {
     this.accountService.Login(this.loginData).subscribe();
     this.getCurrentUser();
  }

  logout()
  {
    this.accountService.Logout();
  }

  getCurrentUser()
  {
    this.accountService.$currentUser.subscribe(
      (user) =>{
        this.loggedIn = !!user;
      }
    )
  }
}
