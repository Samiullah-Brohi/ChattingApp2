import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { CommonModule } from '@angular/common';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Observable, of } from 'rxjs';
import { User } from '../_interfaces/User';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

  /*loggedIn = false;
  currentUser$: Observable<User | null> = of(null); this is second method we can user, as we have same thing in account service so we can directly access that insted of using this line 
  previously using to check user is logged in  now we will use obserable to dispose or unsubscribe current user using | async
  and while assigning null to obserable types we must use off(null) and this <User | null> with pipe means auto unsubscribe  */

  constructor(
    public myaccountServices: AccountService,
    private rout: Router,
    private toastrMessages: ToastrService
  ) {}

  ngOnInit(): void {
    /* 
    this.currentUser$ = this.myaccountServices.currentUser$;
    this.getCurrentUser(); */
  }

  login() {
    this.myaccountServices.login(this.model).subscribe({
      next: () => this.rout.navigateByUrl('/members'),
      error: (error) => this.toastrMessages.error(error.error)

      // next: (response) => {
      //   this.rout.navigateByUrl('/members');
      //   //console.log(response);
      //   /* this.loggedIn = true; */
      // },
      // error: (error) => console.log(error),
    });
  }

  logout() {
    this.myaccountServices.logout();
    this.rout.navigateByUrl('/');
    /* this.loggedIn = false; */
  }

  /* old method to for getting users 
    getCurrentUser() {
    this.myaccountServices.currentUser$.subscribe({
      next: (newuser) =>
        (this.loggedIn = !!newuser) !! before any object makes it boolean ,
      error: (error) => console.log(error),
    });
  } */
}
