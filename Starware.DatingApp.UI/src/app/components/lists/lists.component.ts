import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {


  constructor(private userService:UsersService) { }

  ngOnInit(): void {
  }
  
}
