import {Component, inject, input, OnInit} from '@angular/core';
import {LikesService} from "../_services/likes.service";
import { Member } from '../_models/Member';

@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit{

  private likeService = inject(LikesService);
  member = input.required<Member>();
  members: Member[] = [];

  ngOnInit(): void {
    this.loadLikes();
  }


  loadLikes(){
    this.likeService.getLikedUsers(this.member().userProfileId);
  }
}
