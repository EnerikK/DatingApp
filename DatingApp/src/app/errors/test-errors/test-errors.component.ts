import {Component, inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css'
})
export class TestErrorsComponent {
  baseUrl = 'http://localhost:5118/api/';
  private http = inject(HttpClient);

}
