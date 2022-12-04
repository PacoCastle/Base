import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseService {
  /**
   * Method that takes care of the roles consultation
   *
   * @return {*}  {Observable<any>}
   * @memberof RoleService
   */
  getRoles(): Observable<any> {
    return this.get<any>('Roles');
  }

  /**
   * Method that takes care of the pages consultation
   *
   * @return {*}  {Observable<any>}
   * @memberof RoleService
   */
  getPages(): Observable<any> {
    return this.get<any>('Menus');
  }

  /**
   * Method that takes care of the pages per role consultation
   *
   * @return {*}  {Observable<any>}
   * @memberof RoleService
   */
  getPagesPerRole(): Observable<any> {
    return this.get<any>('Roles');
  }

  /**
   *  Method that takes care of the role update
   *
   * @param {*} id
   * @param {*} data
   * @return {*}  {Observable<any>}
   * @memberof RoleService
   */
  updateRole(id, data): Observable<any> {
    return this.put(`Roles/${id}`, data);
  }

  /**
   *  Method that takes care of the role add
   *
   * @param {*} data
   * @return {*}  {Observable<any>}
   * @memberof RoleService
   */
  addRole(data): Observable<any> {
    return this.post('Roles', data);
  }
  
}
