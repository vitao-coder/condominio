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
  username: string;
  password: string;
  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) {
    if (this.authenticationService.currentUserValue) {
        this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    this.loading = true;
    this.error ='';

    if (this.form.valid) {
    this.authenticationService.login(this.form.controls.username.value, this.form.controls.password.value).pipe(first())
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

