import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-loggin',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  registerMode = false;
  login: FormGroup;
  model = {}

  constructor(private http: HttpClient,
    private router: Router,
    public authService: AuthService,
    private alertify: AlertifyService) {}

  ngOnInit() {
    this.login = new FormGroup({
      user: new FormControl(null,[Validators.required]),
      pass: new FormControl(null,[Validators.required])
    });
  }

  get formLogin() {
    return this.login.controls;
  }

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

  loginUser() {
    this.model = {
      username: this.formLogin.user.value,
      password: this.formLogin.pass.value
    }
    this.authService.login(this.model).subscribe(
      next => {
        this.alertify.success('Logged in successfully');
      },
      error => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/home']);
      }
    );
  }
}
