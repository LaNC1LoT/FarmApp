import { UserService } from '../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { ToastrService } from 'ngx-toastr';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { timeout, finalize } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  constructor(private service: UserService, private router: Router, private toast: ToastrService) { }
  
  @BlockUI() blockUI: NgBlockUI;
  logindUser: User = new User();

  formModel = {
    UserName: '',
    Password: ''
  };

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }

  onSubmit(form: NgForm) {
    this.blockUI.start("Авторизация...");
    this.service.loginUser(form.value).pipe(
      timeout(5000),
      finalize(() =>{
        this.blockUI.stop();
      }))
      .subscribe(
        (res: any) => {
          localStorage.setItem('token', res.token);
          this.router.navigateByUrl('/home');
          this.toast.success("Успешно", 'Аутентификация');
        },
        err => {
          if (err.status === 400 || err.status === 404) {
            this.toast.error(err.error.message, 'Аутентификация');
          } else if (err.status === 0) {
            this.toast.error('Сервер недоступен!', 'Аутентификация');
          } else if (err.name === "TimeoutError" ) {
            this.toast.error('Превышен лимит ожидания!', 'Аутентификация');
          } else {
            console.log(err);
          }
        });
  }
    
    // this.blockUI.start('Авторизация');
    // this.service.login(form.value).pipe(
      
    //     timeout(5000),
      
      
    //   finalize(() => {
    //     this.blockUI.stop();
    //   }))
    //   .subscribe(
        
    //   (res: any) => {
    //     localStorage.setItem('token', res.token);
    //     this.router.navigateByUrl('/home');
    //     this.toastr.success("Успешно", 'Аутентификация');
    //     this.blockUI.stop();
    //   },
    //   err => {
    //     if (err.status === 400) {
    //       this.toastr.error(err.error.message, 'Аутентификация');
    //     } else if (err.status === 0) {
    //       this.toastr.error('Сервер недоступен!', 'Аутентификация');
    //     } else if (err.name === "TimeoutError" ) {
    //       this.toastr.error('Превышен лимит ожидания!', 'Аутентификация');
    //     } else {
    //       console.log(err);
    //     }
    //   });
}
