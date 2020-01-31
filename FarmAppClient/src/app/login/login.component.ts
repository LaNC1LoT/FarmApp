import { RequestBody } from './../models/user';
import { UserService } from '../shared/user.service';
import { Component, OnInit } from '@angular/core';
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
  formModel: User = new User();

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }

  onSubmit() {
    const login: RequestBody  = new RequestBody();
    login.method = 'LoginUser';
    login.param = JSON.stringify( this.formModel );
    console.log(login);
    this.blockUI.start('Авторизация...');
    this.service.loginUser(login).pipe(
      timeout(5000),
      finalize(() => {
        this.blockUI.stop();
      }))
      .subscribe(
        (res: any) => {
          localStorage.setItem('token', res.token);
          this.router.navigateByUrl('/home');
          this.toast.success('Успешно', 'Аутентификация');
        },
        err => {
          if (err.status === 400 || err.status === 404) {
            this.toast.error(err.error.message, 'Аутентификация');
          } else if (err.status === 0) {
            this.toast.error('Сервер недоступен!', 'Аутентификация');
          } else if (err.name === 'TimeoutError' ) {
            this.toast.error('Превышен лимит ожидания!', 'Аутентификация');
          } else {
            console.log(err);
          }
        });
  }
}
