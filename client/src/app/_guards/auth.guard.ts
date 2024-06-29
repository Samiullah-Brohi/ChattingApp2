import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const _accountSerices = inject(AccountService);
  const _toastr = inject(ToastrService);

  return _accountSerices.currentUser$.pipe(
    map((myuser) => {
      if (myuser) return true;
      else {
        _toastr.error('You are not authorize!');
        return false;
      }
    })
  );
};
