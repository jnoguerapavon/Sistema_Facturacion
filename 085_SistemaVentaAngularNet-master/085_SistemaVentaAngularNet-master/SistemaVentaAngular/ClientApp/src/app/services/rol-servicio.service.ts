import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseApi } from '../interfaces/response-api';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RolServicioService {
  private apiUrl = environment.apiUrl;
  apiBase: string = this.apiUrl + '/api/rol/'
  constructor(private http: HttpClient) { }

  getRoles(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.apiBase}Lista`)
  }
}
