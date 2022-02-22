import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxGalleryModule } from '@kolkov/ngx-gallery'; 



@NgModule({
  imports: [
    CommonModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    }) ,
    TabsModule.forRoot() ,
    ModalModule.forRoot() ,
    NgxGalleryModule
  ],
  exports:[BsDropdownModule,BsDatepickerModule,
    ToastrModule,TabsModule,ModalModule,NgxGalleryModule]
})
export class SharedModule { }
