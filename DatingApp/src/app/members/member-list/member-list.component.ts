import { Component , OnInit , inject} from '@angular/core';
import { Member } from '../../_models/Member';
import {MembersService} from "../../_services/members.service";


@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
  private memberService = inject(MembersService);
  members: Member[] = [];
  ngOnInit(): void {
    this.loadMembers();
    }

    loadMembers(){
    this.memberService.getMembers().subscribe({
      next: member => this.members = member
    })
    }
}
