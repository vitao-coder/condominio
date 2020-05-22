import { Login } from './../../models/login';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../services/authentication.service';
import { FormGroup, FormControl } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loading = false;  returnUrl: string;
  error = '';
  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });
  loginModel: Login = new Login();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) {
    if (this.authenticationService.currentUserValue) {
        this.router.navigate(['/apartamento']);
    }
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    this.loading = true;
    this.error ='';
    if (this.form.valid) {
    this.loginModel.username = this.form.controls.username.value;
    this.loginModel.password = this.form.controls.password.value;
    this.authenticationService.login(this.loginModel).pipe(first())
    .subscribe(data => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
            this.error = error;
            this.loading = false;
        });
      }
  }
}

