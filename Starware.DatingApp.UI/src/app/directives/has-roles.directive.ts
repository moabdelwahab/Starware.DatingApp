import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { take } from 'rxjs/operators';
import { UserDto } from '../models/users/UserDto';
import { AccountService } from '../services/account.service';

@Directive({
  selector: '[appHasRoles]'
})
export class HasRolesDirective implements OnInit {

  @Input() appHasRoles: string[];
  user: UserDto;

  constructor(private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private accountService: AccountService) {

    this.accountService.$currentUser.pipe(take(1)).subscribe(
      (user) => {
        this.user = user;
      }
    )
  }
  
  ngOnInit(): void {
    if (this.user == null || !this.user.roles) {
      this.viewContainerRef.clear();
    }

    if (this.user.roles.some(role => this.appHasRoles.includes(role))) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    }

  }

}
