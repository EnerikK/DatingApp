import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { User } from '../_models/user';
import {map} from "rxjs";
import {environment} from "../../environments/environment";
import {Member} from "../_models/Member";
import {MembersService} from "./members.service";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'v1.0/Identity/login',model).pipe(
      map(user => {
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }
  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'v1.0/Identity/registration',model).pipe(
      map(user => {
        if(user) {
          this.setCurrentUser(user);
        }
        return user
      })
    )
  }

  setCurrentUser(user : User)
  {
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUser.set(user);
  }
  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
