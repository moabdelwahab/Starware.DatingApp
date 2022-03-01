import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxGalleryModule } from '@kolkov/ngx-gallery'; 
import { NgxSpinnerModule } from 'ngx-spinner';
import { AngularFileUploaderModule } from "angular-file-uploader";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimeagoModule } from 'ngx-timeago';


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
    NgxGalleryModule,
    AngularFileUploaderModule,
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    TimeagoModule.forRoot() 
  ],
  exports:[BsDropdownModule,
    BsDatepickerModule,
    ToastrModule,
    TabsModule,
    ModalModule,
    NgxGalleryModule,
    AngularFileUploaderModule,
    PaginationModule,
    ButtonsModule,
    TimeagoModule
  ]
})
export class SharedModule { }
