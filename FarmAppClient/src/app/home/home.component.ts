import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MainService } from '../shared/main.service';
import { User } from '../models/allmodel';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  constructor(private router: Router, private service: MainService) { 
    this.service.ngOnInit();
  }

  user : User;
  
  ngOnInit(): void {
    this.service.getUser().subscribe(
      (res: User) => {
        this.user = res;
      },
      err => {
        console.log(err);
      },
    );
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
