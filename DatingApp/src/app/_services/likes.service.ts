import {inject, Injectable, signal} from '@angular/core';
import {environment} from "../../environments/environment";
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LikesService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  likeIds = signal<number[]>([]);

  addLike(source:string ,target:string){
    return this.http.post(`${this.baseUrl}v1.0/Like/AddLike?sourceUserId=${source}&targetUserId=${target}`,{});
  }

  getLikedUsers(id : string){
    return this.http.get(`${this.baseUrl}v1.0/Like/GetLikedUsers?id=${id}`);
  }

  getLikedUsersLikedBy(id:string){
    return this.http.get(`${this.baseUrl}v1.0/Like/GetLikedUsersLikedBy?id=${id}`);
  }

  constructor() { }
}
