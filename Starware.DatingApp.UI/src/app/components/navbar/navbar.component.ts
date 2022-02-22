import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { LoginDto } from 'src/app/models/users/LoginDto';
import { UserDto } from 'src/app/models/users/UserDto';
import { AccountService } from 'src/app/services/account.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  
  
  modalRef?: BsModalRef;
  loginData: LoginDto = new LoginDto();

  @Input() loggedIn: boolean;
  @Input() user: UserDto;
  @ViewChild('loginError') loginError; 
  loginErrorMessage : string ;

  constructor(public accountService: AccountService,

    private router: Router,
    private toastr: ToastrService,
    private modalService: BsModalService) {
  }

  ngOnInit(): void {
  }

  Login() {
    this.accountService.Login(this.loginData).subscribe((user: ApiResponse<UserDto>) => {
      if (user?.data) {
        this.loggedIn = true;
      }
    },
    (error) => {
      this.loginErrorMessage = error.message;
      this.openModal(this.loginError);
    } ,
    () => {
      if(this.loggedIn){
        this.router.navigateByUrl('/members');
      }
    });
  }

  logout() {
    this.accountService.Logout();
    localStorage.clear();
    this.loggedIn = false;
    this.router.navigateByUrl('/');
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}
