import {inject, Injectable, signal} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from "@angular/common/http";
import {environment} from '../../environments/environment'
import {Member} from '../_models/Member';
import { UserProfileUpdate } from '../_models/UserProfileUpdate'
import {of, tap} from "rxjs";
import {Photo} from "../_models/Photo";
import {PaginatedResult} from "../_models/pagination";
import {UserParams} from "../_models/userParams";

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  /*members = signal<Member[]>([]);*/
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null);
  memberCache = new Map();


  /*Member Functions*/
  getMembers(userParams: UserParams) {
    const response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) return this.setPaginatedResponse(response);

    let params = this.setPaginationHeaders(userParams.pageNumber,userParams.pageSize);
    params = params.append('minAge',userParams.minAge);
    params = params.append('maxAge',userParams.maxAge);
    params = params.append('gender',userParams.gender);
    params = params.append('orderBy',userParams.orderBy);

    return this.http.get<Member[]>(this.baseUrl + 'v1.0/Users',{observe: 'response',params}).subscribe({
        next: response => {
          this.setPaginatedResponse(response);
          this.memberCache.set(Object.values(userParams).join('-'),response);
        }
    })
  }

  getMember(userProfileId: string) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.body),[])
      .find((m: Member) => m.userProfileId === userProfileId);

    return this.http.get<Member>(this.baseUrl + 'v1.0/Users/' + userProfileId);
  }
  updateMember( userProfileId: string , updateProfile: UserProfileUpdate){
    return this.http.patch<Member>(this.baseUrl + `v1.0/Users/${userProfileId}`, updateProfile);
  }


  /*Helper Functions*/
  private setPaginatedResponse(response: HttpResponse<Member[]>){
    this.paginatedResult.set({
      items: response.body as Member[],
      pagination : JSON.parse(response.headers.get('Pagination')!)
    })
  }

  private setPaginationHeaders(pageNumber : number , pageSize : number) {
    let params = new HttpParams();
    if (pageNumber && pageSize) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    return params;
  }


  /*Photo Functions*/
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
