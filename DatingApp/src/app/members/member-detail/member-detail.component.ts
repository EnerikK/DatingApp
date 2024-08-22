import {Component, inject, OnInit} from '@angular/core';
import {MembersService} from "../../_services/members.service";
import {ActivatedRoute} from "@angular/router";
import {Member} from "../../_models/Member";
import { TabsModule } from 'ngx-bootstrap/tabs';
import {GalleryItem, GalleryModule, ImageItem} from "ng-gallery";
import {TimeagoModule, TimeagoPipe} from "ngx-timeago";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [TabsModule,GalleryModule,TimeagoModule,DatePipe],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  member?: Member;
  images: GalleryItem[] = [];

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    const username = this.route.snapshot.paramMap.get('userProfileId');
    if (!username) return;
    this.memberService.getMember(username).subscribe(
      {
        next: member => {
          this.member = member;
          member.photos.map(p => {
            this.images.push(new ImageItem({src: p.url,thumb: p.url}))
          })
        }
      });

  }
}
