import {Component, HostListener, inject, OnInit, ViewChild} from '@angular/core';
import {Member} from "../../_models/Member";
import {AccountService} from "../../_services/account.service";
import {MembersService} from "../../_services/members.service";
import {TabsModule} from "ngx-bootstrap/tabs";
import {FormsModule, NgForm} from "@angular/forms";
import {ToastrService} from "ngx-toastr";
import {UserProfileUpdate} from "../../_models/UserProfileUpdate";
import {PhotoEditorComponent} from "../photo-editor/photo-editor.component";
import {DatePipe} from "@angular/common";
import {TimeagoModule} from "ngx-timeago";

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [
    TabsModule, FormsModule, PhotoEditorComponent,TimeagoModule,DatePipe
  ],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event:any){
    if (this.editForm?.dirty)
    {
      $event.returnValue = true;
    }
  }

  member?: Member;
  UserProfileUpdate?: UserProfileUpdate;
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  private toastr = inject(ToastrService);

  ngOnInit() {
  this.loadMember();
  }

  loadMember() {
    const user = this.accountService.currentUser();
    if (!user) return;
    this.memberService.getMember(user.userProfileId).subscribe({
      next: member => this.member = member,
    })
  }

  updateMember(){
    const user = this.accountService.currentUser();
    const updateProfile = this.editForm?.value;
    if (!user) return;
    this.memberService.updateMember(user.userProfileId,updateProfile).subscribe({
      next: _ => {
        this.toastr.success('Profile Updated');
        this.editForm?.reset(this.member);
      }
    })
    /*console.log(this.member);*/
  }

  onMemberChange(event: Member){
    this.member = event;
  }

}
