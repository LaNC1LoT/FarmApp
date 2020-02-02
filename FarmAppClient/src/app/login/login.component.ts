import { MainService } from '../shared/main.service';
import { Component } from '@angular/core';
import { User } from '../models/allmodel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent  {

  constructor(private service: MainService) {
    this.service.ngOnInit();
   }

  loginModel: User = new User();
  
  onSubmit() {
    this.service.loginUser(this.loginModel);
  }
}
