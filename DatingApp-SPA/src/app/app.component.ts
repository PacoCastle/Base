import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import { User } from './_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  pageTitle: any;

  constructor(private authService: AuthService,
    private router: Router,) {}

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    localStorage.setItem("title", "Home");
    if (token) {

      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
      if (user) {
        this.authService.currentUser = user;
        this.authService.changeMemberPhoto(user.photoUrl);
      }
    } else {
      this.router.navigate(["/login"])
    }
    
    
  }

  setTitle(title){
    this.pageTitle= title;
  }
}
