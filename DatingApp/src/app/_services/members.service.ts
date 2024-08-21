import {inject, Injectable, signal} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from '../../environments/environment'
import {Member} from '../_models/Member';
import { UserProfileUpdate } from '../_models/UserProfileUpdate'
import {of, tap} from "rxjs";
import {Photo} from "../_models/Photo";
import {PaginatedResult} from "../_models/pagination";

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  /*members = signal<Member[]>([]);*/
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null);

  getMembers(pageNumber?: number , pageSize?: number) {
    let params = new HttpParams();
    if (pageNumber && pageSize){
      params = params.append('pageNumber',pageNumber);
      params = params.append('pageSize',pageSize);
    }
    return this.http.get<Member[]>(this.baseUrl + 'v1.0/Users',{observe: 'response',params}).subscribe({
        next: response => {
          this.paginatedResult.set({
            items: response.body as Member[],
            pagination : JSON.parse(response.headers.get('Pagination')!)
          })
        }
      })
  }
  getMember(userProfileId: string) {
    /*const member = this.members().find( x => x.userProfileId === userProfileId);
    if (member !== undefined) return of(member);*/
    return this.http.get<Member>(this.baseUrl + 'v1.0/Users/' + userProfileId);
  }
  updateMember( userProfileId: string , updateProfile: UserProfileUpdate){
    return this.http.patch<Member>(this.baseUrl + `v1.0/Users/${userProfileId}`, updateProfile);
  }

  setMainPhoto(userProfileId: string, photo: Photo){
    return this.http.put(`${this.baseUrl}v1.0/Users/SetPhotoMain?identity=${userProfileId}&photoId=${photo.id}`, {}).pipe(
     /* tap(() => {
        this.members.update(members => members.map(m => {
          if (m.photos.includes(photo)){
            m.basicInfo.photoUrl = photo.url
          }
          return m;
        }))
      })*/
    )
  }

  deletePhoto(userProfileId: string, photo: Photo) {
    return this.http.delete(`${this.baseUrl}v1.0/Users/DeletePhoto?identity=${userProfileId}&photoId=${photo.id}`);
  }

}
