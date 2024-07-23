import { CanActivateFn } from '@angular/router';
import {inject} from "@angular/core";
import {AccountService} from '../_services/account.service';
import {ToastrService} from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  let accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  if(accountService.currentUser()){
    return true;
  }else {
    toastr.error("You cant do this :3");
    return false;
  }

  return true;
};
