import {Component, computed, effect, inject, input, OnInit, } from '@angular/core';
import {Member} from "../../_models/Member";
import {RouterLink} from "@angular/router";
import {LikesService} from "../../_services/likes.service";
import {AccountService} from "../../_services/account.service";
import {CommonModule} from "@angular/common";
import {BehaviorSubject} from "rxjs";


@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink,CommonModule],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
})
export class MemberCardComponent implements OnInit{
  private likeService = inject(LikesService);
  private accountService = inject(AccountService);
  member = input.required<Member>();
  hasLiked: boolean = false;

  ngOnInit(): void {
    this.updateHasLiked();
  }

  private updateHasLiked() {
    this.hasLiked = this.likeService.likeIds().includes(this.member().userProfileId);
  }

  addLike(){
    const user = this.accountService.currentUser();
    if (!user) return;

    this.likeService.addLike(user.userProfileId, this.member().userProfileId).subscribe({
      next: () => {
        if (this.hasLiked) {
          this.likeService.likeIds.update(ids => ids.filter(x => x !== this.member().userProfileId));
        } else {
          this.likeService.likeIds.update(ids => [...ids, this.member().userProfileId]);
        }
        this.updateHasLiked();
      },
      error: err => {
        console.error('Failed to like the user:', err);
      }
    })
  }

}
