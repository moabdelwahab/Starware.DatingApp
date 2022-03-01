import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpStatusCode } from 'src/app/common/StatusCode';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { Pagination } from 'src/app/models/common/Pagination';
import { LikeDto } from 'src/app/models/users/LikeDto';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  predicate: string = "Likes";
  userLikes: LikeDto[] = [];
  pagination: Pagination = new Pagination();

  constructor(private userService: UsersService) { }

  ngOnInit(): void {
    this.getLikes();
  }

  getLikes() {
    this.userService.getikes(this.pagination, this.predicate).subscribe(
      (res) => {
        if (res.result.statusCode == HttpStatusCode.OK) {
          this.userLikes = res.result.data;
          this.pagination = res.pagination;
        }
      }
    )
  }


  pageChanged(e:any)
  {
    this.pagination.PageNumber =e.page; 
    this.getLikes();
  }


  likeRemoved(id: number) {
    let userIndex = this.userLikes.findIndex(user => user.id == id)
    this.userLikes.splice(userIndex, 1);
  }

}
