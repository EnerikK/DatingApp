import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from '../../environments/environment'
import {Member} from '../_models/Member';
import { UserProfileUpdate } from '../_models/UserProfileUpdate'
import {of, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  members = signal<Member[]>([]);

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'v1.0/Users').subscribe({
        next: members => this.members.set(members)
      })
  }
  getMember(userProfileId: string) {
    const member = this.members().find( x => x.userProfileId === userProfileId);
    if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'v1.0/Users/' + userProfileId);
  }
  updateMember( userProfileId: string , updateProfile: UserProfileUpdate){
    return this.http.patch<Member>(this.baseUrl + `v1.0/Users/${userProfileId}`, updateProfile);
  }
}
