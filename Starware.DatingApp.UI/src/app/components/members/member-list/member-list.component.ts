import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  $systemUsers: Observable<ApiResponse<MemberDto[]>>;

  constructor(private userService:UsersService) { }

  ngOnInit(): void {
    
    this.$systemUsers = this.userService.getAllUsers();
  }
}
