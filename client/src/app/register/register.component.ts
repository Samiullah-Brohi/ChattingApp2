import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
 // @Input() usersFromParentHomeComponent: any; // make a container varaible to get data from parent component
 
  @Output() cancelRegister = new EventEmitter(); // while cancel event is clicked then this child componment sent response to parent
  model: any = {};

  constructor(private _accountServices: AccountService) {}

  ngOnInit(): void {}

  register() {
    this._accountServices.register(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.cancel();
      },
      error: (error) => console.log(error)
    })

    //console.log(this.model);
  }

  cancel() {
    console.log('Cancelled');
    this.cancelRegister.emit(false);
  }
}
