import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  requestCounter:number = 0;
  constructor(private spinner:NgxSpinnerService) {
   }
  
  busy()
  {
    this.requestCounter++;
    this.spinner.show(undefined,
    {
      type:"line-scale-party",
      size:"large",
      bdColor:"rgba(255,255,255,0)",
      color:"#333333",
    });
  }

  idle()
  {
    this.requestCounter--;
    if (this.requestCounter == 0 )
    {
      this.spinner.hide();
    }
  }



}
