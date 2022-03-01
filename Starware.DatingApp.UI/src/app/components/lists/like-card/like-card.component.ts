import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HttpStatusCode } from 'src/app/common/StatusCode';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { LikeDto } from 'src/app/models/users/LikeDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-like-card',
  templateUrl: './like-card.component.html',
  styleUrls: ['./like-card.component.css']
})
export class LikeCardComponent implements OnInit {

  @Input() like: LikeDto = new LikeDto();
  @Output() likeRemoved: EventEmitter<number> = new EventEmitter();

  constructor(private userService: UsersService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  unlike() {
    this.userService.deleteLike(this.like.id).subscribe(
      (res) => {
      this.toastrService.success('You have unliked ' + this.like.name + ' Successfuly');
      this.likeRemoved.emit(this.like.id);
    });
  }

}
