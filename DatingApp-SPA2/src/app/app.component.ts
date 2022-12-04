import { Component, OnInit} from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './components/_services/auth.service';
import { Router } from '@angular/router';
import { User } from './components/_models/user';


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
      }
    } else {
      this.router.navigate([""])
    }
  }
}
