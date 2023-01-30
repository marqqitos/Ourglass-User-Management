import { Component, OnInit } from '@angular/core';
import { SearchCriteria } from 'src/app/models/searchCriteria';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user-service/user.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  users: User[] = [];
  user: User = new User();
  showAddNewUser: boolean = false;
  searchCriteria: string = '';
  lastScrolledPosition: number = 0;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  displayFullname(user: User): string {
    return user.firstName + ' ' + user.lastName;
  }

  getUsers(): void {
    this.userService.getFirst10Users().subscribe(users => this.users = users);
  }

  searchUsers(): void {
    if (this.searchCriteria == '') {
      this.lastScrolledPosition = 0;
      this.getUsers();
    }
    else {
      this.userService.searchUsers(this.searchCriteria).subscribe(users => {
        this.users = users
        this.lastScrolledPosition = this.users.length;
      });

    }
  }

  toggleAddNewUser(): void {
    this.showAddNewUser = !this.showAddNewUser;
  }

  registerUser(): void {
    this.userService.registerUser(this.user).subscribe(
      data => {
        console.log(data);
        this.user = new User();
        this.toggleAddNewUser();
        this.getUsers();
      },
      err => {
        alert(err.error.message);
      }
    )
  }

  setSelectedUser(user: User): void {
    this.userService.setSelectedUser(user);
  }

  areThereUsers() {
    return this.users.length > 0;
  }

  loadMoreUsers(): void {
    let nextScrollPosition = this.users.length;

    if (this.lastScrolledPosition < nextScrollPosition) {
      this.lastScrolledPosition = nextScrollPosition;

      let criteria = new SearchCriteria(
        this.searchCriteria,
        nextScrollPosition
      );

      this.userService.searchUsersWithCriteria(criteria).subscribe(extraUsers => this.users = this.users.concat(extraUsers));
    }
  }
}
