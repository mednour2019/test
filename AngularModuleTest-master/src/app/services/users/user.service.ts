import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { user } from '../../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseApiUrl = environment.baseApiUrl;

  constructor(private httpClient: HttpClient) {}
  // get list users
  getUsers(): Observable<user[]> {
    return this.httpClient.get<user[]>(this.baseApiUrl + '/Users/get-users');
  }

  //create new user
  AddUser(user: user): Observable<any> {
    return this.httpClient.post<any>(
      this.baseApiUrl + '/Users/create-user',
      user
    );
  }

  //Update new user
  UpdateUser(user: user): Observable<any> {
    return this.httpClient.post<any>(
      this.baseApiUrl + '/Users/update-user',
      user
    );
  }
  DeleteUser(id: number): Observable<any> {
    return this.httpClient.delete<any>(
      `${this.baseApiUrl}/Users/delete-user/${id}`
    );
  }
}
