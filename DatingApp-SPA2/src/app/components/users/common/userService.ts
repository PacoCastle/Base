import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  /**
   * Method that takes care of the users consultation
   *
   * @return {*}  {Observable<any>}
   * @memberof UserService
   */
  getUsers(): Observable<any> {
    return this.get<any>('users');
  }
 /**
   * Method that takes care of the roles consultation
   *
   * @return {*}  {Observable<any>}
   * @memberof UserService
   */
  getRoles(): Observable<any> {
    return this.get<any>('Roles');
  }
 /**
   * Method that takes care of the user update
   *
   * @return {*}  {Observable<any>}
   * @memberof UserService
   */
  updateUser(id, data): Observable<any> {
    return this.put(`users/${id}`, data);
  }
 /**
   * Method that takes care of the user registration
   *
   * @return {*}  {Observable<any>}
   * @memberof UserService
   */
  addUser(data): Observable<any> {
    return this.post('users', data);
  }
  
}
