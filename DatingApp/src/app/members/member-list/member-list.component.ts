import { Component , OnInit , inject} from '@angular/core';
import { Member } from '../../_models/Member';
import {MembersService} from "../../_services/members.service";
import {MemberCardComponent} from "../member-card/member-card.component";
import {PaginationModule} from "ngx-bootstrap/pagination";


@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [
    MemberCardComponent,
    PaginationModule
  ],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
  protected memberService = inject(MembersService);
  pageNumber = 1;
  pageSize = 1;

  ngOnInit(): void {
    if (!this.memberService.paginatedResult()) this.loadMembers();
    }

    loadMembers(){
    this.memberService.getMembers(this.pageNumber,this.pageSize)
    }

  pageChanged(event:any){
    if (this.pageNumber !== event.page){
      this.pageNumber = event.page;
      this.loadMembers();
    }
  }
}
