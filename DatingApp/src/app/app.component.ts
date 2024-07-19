import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from '../app/_services/account.service'


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient);
  private accountService = inject(AccountService);
  title = 'DatingApp';
  users: any;
  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  getUsers() {
    this.http.get("http://localhost:5118/api/v1.0/Users").subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("Request has completed")
    })
  }

}
