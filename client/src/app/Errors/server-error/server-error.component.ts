import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent {
  catchErrors: any;
  
  constructor(private router: Router) {
    const navigation = router.getCurrentNavigation();
    this.catchErrors = navigation?.extras?.state?.["error"];
    
  }
}
