import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
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

  isSubmitted:boolean;
  registerDto : RegisterDto = new RegisterDto();
  public registerForm:FormGroup;
  @Input() systemUsers : AppUser[];
  @Output() userHasRegistered : EventEmitter<boolean> = new EventEmitter();


  constructor(private accountservice:AccountService) {
   }

  ngOnInit(): void {
    this.intializeForm();
  }
  intializeForm() {
    this.registerForm =new FormGroup({
      firstName : new FormControl('',[Validators.required,Validators.minLength(5),Validators.maxLength(10)]),
      userName : new FormControl('',[Validators.required,Validators.minLength(5),Validators.maxLength(10)]),
      middleName: new FormControl('',[Validators.required,Validators.minLength(5),Validators.maxLength(10)]),
      lastName: new FormControl('',[Validators.required,Validators.minLength(5),Validators.maxLength(10)]),
      birthdate: new FormControl('',[Validators.required]),
      password: new FormControl('',[Validators.required,Validators.minLength(5),Validators.maxLength(10)]),
      confirmPassword : new FormControl('',[Validators.required,this.matchValues('password')]),
    });
    console.log(this.registerForm.valid);
  }

  matchValues(matchTo:string) : ValidatorFn
  {
    return (control: AbstractControl) =>{
      return control?.value === control?.parent?.controls[matchTo]?.value ? null : {isMatching:true} 
    }
  }

  register()
  {
    this.isSubmitted = true;
    if (this.registerForm.valid)
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
  }


  cancel()
  {
    this.userHasRegistered.emit(false);
  }

}
