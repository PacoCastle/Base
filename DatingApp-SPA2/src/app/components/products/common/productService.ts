import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseService {

  getProducts(): Observable<any> {
    return this.get<any>('machines');
  }

  updateProduct(id, data): Observable<any> {
    return this.put(`machines/${id}`, data);
  }

  addProduct(data): Observable<any> {
    return this.post('machines', data);
  }
  
}
