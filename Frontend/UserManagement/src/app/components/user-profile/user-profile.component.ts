import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user-service/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  user: User = new User();

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.user = this.userService.getSelectedUser();
  }
  
  displayFullname(user: User): string {
    return user.firstName + ' ' + user.lastName;
  }
}
