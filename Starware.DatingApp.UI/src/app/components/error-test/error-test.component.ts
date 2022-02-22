import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LoginDto } from 'src/app/models/users/LoginDto';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-error-test',
  templateUrl: './error-test.component.html',
  styleUrls: ['./error-test.component.css']
})
export class ErrorTestComponent implements OnInit {

  apiUrl : string = "https://localhost:5001/api/buggy/";
  constructor(private httpClient:HttpClient, private accountService:AccountService) { }
  errors: [] = []
  ngOnInit(): void {
  }

  test404Error()
  {
    this.httpClient.get(this.apiUrl+'not-found').subscribe();
  }
  
  test401Error()
  {
    this.httpClient.get(this.apiUrl+'auth').subscribe();
  }
  test500Error()
  {
    this.httpClient.get(this.apiUrl+'server-error').subscribe();
  }

  test400Error()
  {
    this.accountService.Login(new LoginDto('m','123456')).subscribe(
      null,(error)=>{
        this.errors = error;
      }
    );
  }
}
