import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HttpStatusCode } from 'src/app/common/StatusCode';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UserDto } from 'src/app/models/users/UserDto';
import { PresenceService } from 'src/app/services/presence.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() user: MemberDto;
  constructor(private userService: UsersService,
    private toastr: ToastrService,
    public presenceService: PresenceService) {
  }

  ngOnInit(): void {
  }

  likeUser() {
    this.userService.addUserLike(this.user.id).subscribe((res: ApiResponse<boolean>) => {
      if (res.data) {
        this.toastr.success("You have liked " + this.user.firstName + "Successfuly");
      }
    }, error => {
      this.toastr.error(error.error);
    });
  }

}
