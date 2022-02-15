import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginDto } from 'src/app/models/users/LoginDto';
import { UserDto } from 'src/app/models/users/UserDto';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  loginData: LoginDto = new LoginDto();
  loggedIn: boolean;
  user :UserDto;

  constructor(public accountService: AccountService,
    private router: Router,
    private toastr:ToastrService
    ) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }


  Login() {
    this.accountService.Login(this.loginData).subscribe();
    this.getCurrentUser();
    this.router.navigateByUrl('/members');
  }

  logout() {
    this.accountService.Logout();
    localStorage.clear();
    this.router.navigateByUrl('/');
  }

  getCurrentUser() {
    this.accountService.$currentUser.subscribe(
      (user) => {
        this.loggedIn = !!user;
        if (this.loggedIn) {
          this.router.navigateByUrl('/members');
          this.toastr.success("Welcome "+ user.name);
        }
      }
    )
  }
}
