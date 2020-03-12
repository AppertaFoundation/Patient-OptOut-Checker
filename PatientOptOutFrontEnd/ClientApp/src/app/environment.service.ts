import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';

@Injectable()
export class EnvironmentService {

  apiUrl: string;
  trustDisclaimer: string;
  initialDisclaimer: string;
  noAccess: string;

  constructor(private _http: HttpClient) {}

  setAllSettings(){
    return this._http.get('./../assets/settings.json').pipe(
      tap((settings: any) => {
        this.apiUrl = settings.apiUrl;  
        this.trustDisclaimer = settings.trustDisclaimer;
        this.initialDisclaimer = settings.initialDisclaimer;  
        this.noAccess = settings.noAccess;
      })      
    );
  }
}
