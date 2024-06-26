import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './Errors/test-errors/test-errors.component';
import { NotFoundComponent } from './Errors/not-found/not-found.component';
import { ServerErrorComponent } from './Errors/server-error/server-error.component';

// const routes: Routes = [ first method to use authguard to every one link
//   { path: '', component: HomeComponent },
//   { path: 'members', component: MemberListComponent, canActivate: [authGuard] },
//   { path: 'members/:id', component: MemberDetailComponent },
//   { path: 'lists', component: ListsComponent },
//   { path: 'messages', component: MessagesComponent },
//   { path: '**', component: HomeComponent, pathMatch: 'full' },
// ];

// second method
const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'members', component: MemberListComponent },
      { path: 'members/:id', component: MemberDetailComponent },
      { path: 'lists', component: ListsComponent },
      { path: 'messages', component: MessagesComponent },
    ],
  },
  { path: 'Errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }, // if type any wrong url page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
