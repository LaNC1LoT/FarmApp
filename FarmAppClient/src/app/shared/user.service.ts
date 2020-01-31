import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User, RequestBody } from '../models/user';


@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private http: HttpClient) { }

  private url = 'http://localhost:5000/gettoken';

  test() {
    return this.http.get('http://localhost:5000/api/user/test');
  }



  loginUser(user: RequestBody) {
    return this.http.post(this.url, user);
  }

  getUsers() {
    return this.http.get(this.url);
  }

  createUser(user: User) {
    return this.http.post(this.url, user);
  }

  updateUser(id: number, user: User) {
    const urlParams = new HttpParams().set('id', id.toString());
    return this.http.put(this.url, user, { params: urlParams});
  }

  deleteUser(id: number) {
      return this.http.delete(this.url + '/' + id);
  }
}
