import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_interfaces/User';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseURL = 'https://localhost:5001/api/';
  private CurrentUserObject = new BehaviorSubject<User | null>(null);

  currentUser$ = this.CurrentUserObject.asObservable();

  constructor(private http: HttpClient) {}

  /* login(model: any) { this is a simple post request without obserable
    return this.http.post(this.baseURL + 'account/login', model); }*/

  /* followig method will store user session in browser storage area uisng pipe obserable method, to do what next*/
  login(model: any) {
    return this.http.post<User>(this.baseURL + 'account/login', model).pipe(
      map((response: User) => {
        const myuser = response;
        if (myuser) {
          localStorage.setItem('sessionuser', JSON.stringify(myuser));
          this.CurrentUserObject.next(myuser);
          //this.setCurrentUser(myuser);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseURL + 'account/register', model).pipe(
      map((user) => {
        if (user) {
          localStorage.setItem('sessionuser', JSON.stringify(user));
        }
        return user;
      })
    );
  }

  setCurrentUser(loggedinUser: User) {
    this.CurrentUserObject.next(loggedinUser);
  }

  logout() {
    localStorage.removeItem('sessionuser');
    this.CurrentUserObject.next(null);
  }
}
