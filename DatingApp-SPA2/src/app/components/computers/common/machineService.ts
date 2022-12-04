import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from 'app/components/_services/base-service';

@Injectable({
  providedIn: 'root'
})
export class MachineService extends BaseService {

  getMachine(): Observable<any> {
    return this.get<any>('machines');
  }

  updateMachine(id, data): Observable<any> {
    return this.put(`machines/${id}`, data);
  }

  addMachine(data): Observable<any> {
    return this.post('machines', data);
  }
  
}
