import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { UserDto } from '../models/users/UserDto';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  user :UserDto;
  constructor(private accountService:AccountService,private toastrService:ToastrService)
  {

  }

  canActivate(): boolean  {
    var user = this.accountService.$currentUser.pipe(take(1)).subscribe(
      (user)=>
      {
        this.user = user;
      }
    )
    if(this.user.roles.find(x => x == "Admin"))
    {
      return true;
    }else
    {
      this.toastrService.error("You are not Authorized to See this !! ")
      return false;
    }
  }
  
}
