import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  getUsers(): Observable<any> {
    return this.get<any>('users');
  }

  updateUser(id, data): Observable<any> {
    return this.put(`users/${id}`, data);
  }

  addUser(data): Observable<any> {
    return this.post('users', data);
  }
  
}
