import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { PaginatedResult, Pagination } from 'src/app/models/common/Pagination';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { memberSearch } from 'src/app/models/users/memberSearch';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  pageSize: number = 10;
  memberSearch : memberSearch;
  userResult: ApiResponse<MemberDto[]>;
  pagination: Pagination;

  constructor(private userService: UsersService) { 
    this.memberSearch = this.userService.getMemberSearch();
  }

  ngOnInit(): void {

    this.userService.getAllUsers(1, this.pageSize).subscribe(
      (res) => {
        this.userResult = res.result;
        this.pagination = res.pagination;
      }
    );
  }

  pageChanged(e: any) {
    this.getMembers(e.page);
  }

  getMembers(currentPage: number) {
    this.userService.setMemberSearch(this.memberSearch);
    this.userService.getAllUsers(currentPage, this.pageSize).subscribe(
      (res) => {
        this.pagination = res.pagination;
        this.userResult = res.result;
      }
    );
  }

  resetMemberSearch()
  {
    this.memberSearch = new memberSearch();
    this.userService.resetMemberSearch();
    this.getMembers(1);
  }

}
