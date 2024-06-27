import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_interfaces/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Chatting App is Running';
  users: any;

  constructor(private accountServices: AccountService) {}

  ngOnInit(): void {
    // this.getUsers();
    this.setCurrentUser();
  }

  // getUsers() {
  //   this.http.get('https://localhost:5001/api/Users').subscribe({
  //     next: (response) => (this.users = response),
  //     error: (error) => console.log(error),
  //     complete: () => console.log('Request has been completed'),
  //   });
  // }

  setCurrentUser() {
    /* this is first type 
    const _loggedInUser: User = JSON.parse(localStorage.getItem('sessionuser')!);*/
    const _loggedInUser = localStorage.getItem('sessionuser');
    if (_loggedInUser == null) return;
    const myuser: User = JSON.parse(_loggedInUser);
    this.accountServices.setCurrentUser(myuser);
  }
}
