import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NumberModel } from './number-model';
import { AuthorizationModel } from './authorization-model';
import { Observable } from 'rxjs';
import { EnvironmentService } from './environment.service';

@Injectable()
export class PatientOptOutService {
  private headers: HttpHeaders;
  

  constructor(private http: HttpClient, private environmentService: EnvironmentService) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }

  public getAuthorization(): Observable<AuthorizationModel> {
    return this.http.get<AuthorizationModel>(this.environmentService.apiUrl + '/api/startup', { headers: this.headers, withCredentials: true });
  }

  public checkPatientNumbers(numbers: string[]): Observable<NumberModel[]>{
    return this.http.post<NumberModel[]>(this.environmentService.apiUrl + '/api/PatientOptOut', numbers, { headers: this.headers, withCredentials: true });
  }
}
