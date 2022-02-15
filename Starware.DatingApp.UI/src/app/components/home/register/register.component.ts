import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ApiResponse } from 'src/app/models/common/ApiResponse';
import { AppUser } from 'src/app/models/users/AppUser';
import { RegisterDto } from 'src/app/models/users/RegisterDto';
import { UserDto } from 'src/app/models/users/UserDto';
import { AccountService } from 'src/app/services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit{


  registerDto : RegisterDto = new RegisterDto();
   
  @Input() systemUsers : AppUser[];
  @Output() userHasRegistered : EventEmitter<boolean> = new EventEmitter();

   constructor(private accountservice:AccountService) {
    
   }

  ngOnInit(): void {
  }

  register()
  {
    this.accountservice.Register(this.registerDto).subscribe((response : ApiResponse<UserDto>)=>
    {
      if(response.statusCode == 200)
      {
        this.accountservice.setCurrentUser(response.data);
        this.userHasRegistered.emit(true);
      }
    });    
  }


  cancel()
  {
    this.userHasRegistered.emit(false);
  }

}
