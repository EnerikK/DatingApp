import {inject, Injectable, signal} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpParams} from '@angular/common/http';
import {Member} from "../_models/Member";

@Injectable({
  providedIn: 'root'
})
export class LikesService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  likeIds = signal<string[]>([]);

  addLike(source:string ,target:string){
    const params = new HttpParams()
      .set('sourceUserId', source)
      .set('targetUserId', target);
    return this.http.post(`${this.baseUrl}v1.0/Like/AddLike`, {}, { params });
  }

  getLikedUsers(id : string){
    return this.http.get<string[]>(`${this.baseUrl}v1.0/Like/GetLikedUsers?id=${id}`).subscribe({
      next: ids => this.likeIds.set(ids)
    });
  }

  getLikedUsersLikedBy(id:string){
    const params = new HttpParams().set('id', id);
    return this.http.get<Member[]>(`${this.baseUrl}v1.0/Like/GetLikedUsersLikedBy`, { params });
  }

}
