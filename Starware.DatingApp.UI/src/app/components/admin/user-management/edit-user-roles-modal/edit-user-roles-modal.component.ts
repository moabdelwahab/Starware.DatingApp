import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-edit-user-roles-modal',
  templateUrl: './edit-user-roles-modal.component.html',
  styleUrls: ['./edit-user-roles-modal.component.css']
})
export class EditUserRolesModalComponent implements OnInit {

  roles : {Name:string, isChecked:boolean}[];

  @Input() userRolesHasUpdated =new EventEmitter();

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
    console.log(this.roles);
  }

  updateUserRoles()
  {
    this.userRolesHasUpdated.emit(this.roles);
    this.bsModalRef.hide();
  }

}
