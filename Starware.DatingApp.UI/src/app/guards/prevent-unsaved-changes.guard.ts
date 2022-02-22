import { HostListener, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../components/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  
 
  
  canDeactivate(
    component: MemberEditComponent) : boolean {
    if(component.updateFrom.dirty)
    {
      return confirm("Your updates has not been saved , Are you sure ? ");
    }
      return true;
  }
  
}
