import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MachineService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMachine(): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'machines');
  }

  updateMachine(id, data): Observable<any> {
    return this.http.put(this.baseUrl + 'users/' + id, data);
  }

  addMachine(data): Observable<any> {
    return this.http.put(this.baseUrl + 'machines', data);
  }
  
}
