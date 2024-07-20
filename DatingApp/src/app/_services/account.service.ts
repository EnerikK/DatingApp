import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { User } from '../_models/user';
import {map} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = 'http://localhost:5118/api/';
  currentUser = signal<User | null>(null);

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'v1.0/Identity/login',model).pipe(
      map(user => {
        if(user) {
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }
  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'v1.0/Identity/registration',model).pipe(
      map(user => {
        if(user) {
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}