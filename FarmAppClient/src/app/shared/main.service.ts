import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { User, RequestBody, ResponseSuccess } from '../models/allmodel';
import { timeout, finalize } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Injectable({
  providedIn: 'root'
})

export class MainService implements OnInit {

  constructor(private http: HttpClient, private router: Router, private toast: ToastrService) { }

  @BlockUI() blockUI: NgBlockUI;
  private getToken = 'http://localhost:5000/gettoken';

  ngOnInit() {
    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/login');
    }

    if (localStorage.getItem('token') != null) {
      // this.router.navigateByUrl('/home');
      console.log('crf');
    }
  }

  loginUser(user: User) {
    this.blockUI.start('Авторизация...');
    const login: RequestBody  = new RequestBody();
    login.method = 'LoginUser';
    login.param = JSON.stringify(user);

    return this.http.post(this.getToken, login, { observe: 'response' })
      .pipe(
        timeout(5000),
        finalize(() => {
          this.blockUI.stop();
        })).subscribe(
          (res: HttpResponse<ResponseSuccess>) => {
            localStorage.setItem('token', res.body.result);
            this.router.navigateByUrl('/home');
            this.toast.success('Успешно', 'Аутентификация');
          },
          (err: HttpErrorResponse) => {
            if (err.status === 400 || err.status === 404) {
              this.toast.error(err.error.errMsg, err.error.errHeader);
            } else if (err.status === 0) {
              this.toast.error('Сервер недоступен!', 'Аутентификация');
            } else if (err.statusText === 'TimeoutError' ) {
              this.toast.error('Превышен лимит ожидания!', 'Аутентификация');
            } else {
              console.log(err);
            }
          }
        );
  }

  getUser() {
    return this.http.get('http://localhost:5000/getuser');
  }
  
  // getUsers() {
  //   return this.http.get(this.url);
  // }

  // createUser(user: User) {
  //   return this.http.post(this.url, user);
  // }

  // updateUser(id: number, user: User) {
  //   const urlParams = new HttpParams().set('id', id.toString());
  //   return this.http.put(this.url, user, { params: urlParams});
  // }

  // deleteUser(id: number) {
  //     return this.http.delete(this.url + '/' + id);
  // }
}
