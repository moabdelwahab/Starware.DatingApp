import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UserDto } from 'src/app/models/users/UserDto';
import { AccountService } from 'src/app/services/account.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any)
  {
    if(this.updateFrom.dirty)
    {
      $event.returnValue = true;
    }
  }

  userDto:UserDto ;
  user:MemberDto;
  @ViewChild('updateForm') updateFrom :NgForm;

  constructor(private userService:UsersService,
     private accountService:AccountService,
     private toastr:ToastrService,
     private router:Router) {

   }

  ngOnInit(): void {
    this.accountService.$currentUser.pipe(take(1)).subscribe(
      (user)=>
      {
        this.userDto = user;
        this.userService.getUserByUsername(user.userName).subscribe((data:ApiResponse<MemberDto>)=>
        {
          this.user = data.data;
        });
      }
    );
  }



  updateMember()
  {
    this.userService.updateUser(this.user).subscribe((reponse)=>{}
    ,error=>{console.log(error)}
    ,()=>{
    this.router.navigateByUrl("/members");
    });
    this.toastr.success("your info has been updated Successfuly...");
  }  

  photoChanged()
  {
    var memberIndex =this.userService.usersResponse.data.findIndex(x => x.userName == this.userDto.userName);
    this.user = this.userService.usersResponse.data[memberIndex];
  }

}
