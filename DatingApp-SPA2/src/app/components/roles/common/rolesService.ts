import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseService {

  getPagesPerRole(): Observable<any> {
    return this.get<any>('parts');
  }

  updateProduct(id, data): Observable<any> {
    return this.put(`parts/${id}`, data);
  }

  addProduct(data): Observable<any> {
    return this.post('parts', data);
  }
  
}
