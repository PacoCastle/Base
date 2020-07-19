import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class BalancingAttemptsService extends BaseService {

  getBalancingAttempts(): Observable<any> {
    return this.get<any>('machines');
  }

  updateBalancigAttempt(id, data): Observable<any> {
    return this.put(`machines/${id}`, data);
  }

  addBalancingAttempt(data): Observable<any> {
    return this.post('machines', data);
  }
  
}
