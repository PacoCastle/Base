import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthService } from '../_services/auth.service';

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
) {}

  ngOnInit() {
    

      $('.conteiner').height($(window).height());
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
      userName: this.formLogin.user.value,
      password: this.formLogin.pass.value
    }
    this.authService.login(this.model).subscribe(
      () => {
        Swal.fire("Logged in successfully","","success" );
      },
      error => {
        if(error.error){
          Swal.fire("Error", error.error.errors[0], "error");
        } else {
          
        Swal.fire("Error", "", "error");
        }
      },
      () => {
        this.router.navigate(['/home']);
      }
    );
  }
}
