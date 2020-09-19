import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class PageService extends BaseService {

  getPage(): Observable<any> {
    return this.get<any>('Menus');
  }

  updatePage(id, data): Observable<any> {
    return this.put(`Menus/${id}`, data);
  }

  addPage(data): Observable<any> {
    return this.post('Menus', data);
  }
  
}
