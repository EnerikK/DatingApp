import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { environment } from '../../environments/environment'
import { AccountService} from './account.service';
import {Member} from '../_models/Member';
@Injectable({
  providedIn: 'root'
})
export class MembersService {

  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  baseUrl = environment.apiUrl;

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'v1.0/Users', this.getHttpOptions());
  }

  getMember(userProfileId: string) {
    return this.http.get<Member>(this.baseUrl + 'v1.0/Users/' + userProfileId), this.getHttpOptions();
  }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.accountService.currentUser()?.token}`
      })
    }
  }
}
