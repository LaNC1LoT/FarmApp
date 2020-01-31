import { UserService } from './../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  user : any;

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/login');
    }
  }
  // ngOnInit() {
  //   this.service.getUserProfile().subscribe(
  //     (res: any) => {
  //       this.user = res;
  //     },
  //     (err: any) => {
  //       console.log(err);
  //     },
  //   );
  // }


  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
