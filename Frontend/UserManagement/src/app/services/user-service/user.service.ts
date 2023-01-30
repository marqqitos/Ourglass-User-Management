import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { SearchCriteria } from 'src/app/models/searchCriteria';
import { User } from 'src/app/models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  selectedUser: User = new User();

  constructor(
    private http: HttpClient
  ) { }

  getSelectedUser(): User {
    return this.selectedUser;
  }

  setSelectedUser(user: User) {
    this.selectedUser = user;
  }

  getFirst10Users(): Observable<User[]> {
    let route = environment.api.searchUser.url + environment.api.searchUser.first10Users;
    return this.http.get<User[]>(route);
  }

  registerUser(user: User): Observable<User> {
    user.username = this.cleanString(user.username);
    user.firstName = this.cleanString(user.firstName);
    user.lastName = this.cleanString(user.lastName);

    let route = environment.api.registerUser.url + environment.api.registerUser.register;
    return this.http.post<User>(route, user);
  }

  searchUsers(searchCriteria: string): Observable<User[]> {
    searchCriteria = this.cleanString(searchCriteria);

    let route = environment.api.searchUser.url + environment.api.searchUser.search + searchCriteria;
    return this.http.get<User[]>(route);
  }

  searchUsersWithCriteria(criteria: SearchCriteria): Observable<User[]> {
    criteria.searchValue = this.cleanString(criteria.searchValue);

    let route = environment.api.searchUser.url + environment.api.searchUser.next10Users;
    return this.http.post<User[]>(route, criteria);
  }

  cleanString(stringParam: string) {
    return stringParam.replace(/[^\w\s\']|_/g, "")
                                 .replace(/\s+/g, " ");
  }
}
