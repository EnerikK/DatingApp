import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient);
  title = 'DatingApp';
  users: any;
  ngOnInit(): void {
      this.http.get("http://localhost:5118/api/v1.0/Users").subscribe({
        next: response => this.users = response,
        error: error => console.log(error),
        complete: () => console.log("Request has completed")
      })
  }

}
