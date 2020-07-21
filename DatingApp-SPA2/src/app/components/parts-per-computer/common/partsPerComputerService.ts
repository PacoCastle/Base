import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class PartsPerComputerService extends BaseService {

  getProductsPerMachine(): Observable<any> {
    return this.get<any>('parts');
  }

  updateProduct(id, data): Observable<any> {
    return this.put(`parts/${id}`, data);
  }

  addProduct(data): Observable<any> {
    return this.post('parts', data);
  }
  
}
