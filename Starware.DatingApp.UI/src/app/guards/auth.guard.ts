import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDto } from '../models/users/UserDto';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private accountService:AccountService,private toastrService:ToastrService
    ,private router:Router)
  {

  }

  canActivate(): Observable<boolean> {
    return this.accountService.$currentUser.pipe(
      map((user:UserDto)=>{
        if(user){
          return true;
        }else
        {
          this.toastrService.error("You have no authorization to access this !");
          this.router.navigateByUrl('/');
        }
      })
    )
  }
  
}
