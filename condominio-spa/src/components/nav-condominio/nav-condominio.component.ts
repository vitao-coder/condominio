import { AuthenticationService } from './../../services/authentication.service';
import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Router } from '@angular/router'


@Component({
  selector: 'app-nav-condominio',
  templateUrl: './nav-condominio.component.html',
  styleUrls: ['./nav-condominio.component.css']
})
export class NavCondominioComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, private authenticationService: AuthenticationService, private router: Router)
  {
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
    this.router.navigate(['/login']);
  }

}
