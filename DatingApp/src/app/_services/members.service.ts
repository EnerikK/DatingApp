import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { environment } from '../../environments/environment'
import {Member} from '../_models/Member';
import {BasicInfo} from "../_models/BasicInfo";

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'v1.0/Users');
  }
  getMember(userProfileId: string) {
    return this.http.get<Member>(this.baseUrl + 'v1.0/Users/' + userProfileId);
  }

}
