import {Component, input, ViewEncapsulation} from '@angular/core';
import {Member} from "../../_models/Member";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
})
export class MemberCardComponent {
  member = input.required<Member>();
}
