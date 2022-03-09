import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RegisterComponent } from './components/home/register/register.component';
import { SharedModule } from './shared.module';
import { ErrorTestComponent } from './components/error-test/error-test.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ListsComponent } from './components/lists/lists.component';
import { MemberCardComponent } from './components/members/member-card/member-card.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MemberDetailComponent } from './components/members/member-detail/member-detail.component';
import { UsersService } from './services/users.service';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { TextInputComponent } from './components/forms/text-input/text-input.component';
import { MemberEditComponent } from './components/members/member-edit/member-edit.component';
import { PhotoEditorComponent } from './components/members/member-edit/photo-editor/photo-editor.component';
import { LikeCardComponent } from './components/lists/like-card/like-card.component';
import { MessagesComponent } from './components/messages/messages.component';
import { MemberMessagesComponent } from './components/members/member-detail/member-messages/member-messages.component';
import { AdminComponent } from './components/admin/admin.component';
import { HasRolesDirective } from './directives/has-roles.directive';
import { UserManagementComponent } from './components/admin/user-management/user-management.component';
import { PhotoManagementComponent } from './components/admin/photo-management/photo-management.component';
import { EditUserRolesModalComponent } from './components/admin/user-management/edit-user-roles-modal/edit-user-roles-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    ErrorTestComponent,
    NotFoundComponent,
    ServerErrorComponent,
    ListsComponent,
    MemberCardComponent,
    MemberListComponent,
    MemberDetailComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent,
    LikeCardComponent,
    MessagesComponent,
    MemberMessagesComponent,
    AdminComponent,
    HasRolesDirective,
    UserManagementComponent,
    PhotoManagementComponent,
    EditUserRolesModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
],
  providers: [
              {provide : HTTP_INTERCEPTORS , useClass : ErrorInterceptor , multi: true},
              {provide : HTTP_INTERCEPTORS , useClass : JwtInterceptor , multi: true},
              {provide : HTTP_INTERCEPTORS , useClass : LoaderInterceptor , multi: true},
              UsersService],
  bootstrap: [AppComponent],
})
export class AppModule { }
