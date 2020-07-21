import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class BalancingAttemptsService extends BaseService {

  searchPartsPerMachine(machine): Observable<any> {
    return this.get<any>(`MachinePartsAttempts`);
  }

  updateBalancigAttempt(id, data): Observable<any> {
    return this.put(`MachinePartsAttempts/${id}`, data);
  }
  
}
