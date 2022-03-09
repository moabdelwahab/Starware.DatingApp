import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { UserDto } from 'src/app/models/users/UserDto';
import { AdminService } from 'src/app/services/admin.service';
import { EditUserRolesModalComponent } from './edit-user-roles-modal/edit-user-roles-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  users: Partial<UserDto>;

  modalRef: BsModalRef;

  constructor(private adminService: AdminService,
    private modalService: BsModalService) {

  }

  ngOnInit(): void {
    this.adminService.getUsersWithRoles().subscribe(
      (users) => {
        this.users = users;
      }
    );
  }

  openEditRolesModal(user: UserDto) {

    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        user,
        roles: this.getRolesArray(user)
      }
    }
    this.modalRef = this.modalService.show(EditUserRolesModalComponent, config);
    this.modalRef.content.userRolesHasUpdated.subscribe((values:{ Name: string, isChecked: boolean }[]) => {
      console.log(values);
      let selectedRoles = [...values.filter(role => role.isChecked).map(el => el.Name)];
      console.log(selectedRoles);
      this.adminService.updateUserRoles(user.userName,selectedRoles.join(",")).subscribe(()=>
      {
        user.roles = selectedRoles;
      });

    });
  }

  getRolesArray(user: UserDto) {
    console.log(user);

    const roles: { Name: string, isChecked: boolean }[] =
      [
        { Name: "Admin", isChecked: false },
        { Name: "Moderate", isChecked: false },
        { Name: "Member", isChecked: false }
      ]

    user.roles.forEach(userRole => {
      let isMatch = false;
      for (var role of roles) {
        if (role.Name == userRole) {
          role.isChecked = true;
          isMatch = true
        }
      }
    });
    return roles;
  }

}
