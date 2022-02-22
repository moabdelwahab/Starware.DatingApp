import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorTestComponent } from './components/error-test/error-test.component';
import { HomeComponent } from './components/home/home.component';
import { ListsComponent } from './components/lists/lists.component';
import { MemberEditComponent } from './components/member-edit/member-edit.component';
import { MemberDetailComponent } from './components/members/member-detail/member-detail.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MessagesComponent } from './components/messages/messages.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { AuthGuard } from './guards/auth.guard';
import { PreventUnsavedChangesGuard } from './guards/prevent-unsaved-changes.guard';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'error',component:ErrorTestComponent},
  {path:'not-found',component:NotFoundComponent},
  {path:'server-error',component:ServerErrorComponent},
  {
    path:'',
    runGuardsAndResolvers:'always',
    canActivate: [AuthGuard],
    children:[
      {path:'members',component : MemberListComponent},
      {path:'members/:username',component : MemberDetailComponent},
      {path:'member/edit',component : MemberEditComponent,canDeactivate:[PreventUnsavedChangesGuard]},
      {path:'lists',component : ListsComponent},
      {path:'messages',component:MessagesComponent},
    ]
  },
  {path:'**',component: HomeComponent ,pathMatch : 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }