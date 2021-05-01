import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { IDoctor } from './../shared/models/IDoctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  baseUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getDoctors(): Observable<IDoctor[]> {
    return this.http.get<IDoctor[]>(`${this.baseUrl}/doctor`);
  }

  getDoctor(id: string): Observable<IDoctor> {
    return this.http.get<IDoctor>(`${this.baseUrl}/doctor/${id}`);
  }

  createDoctor(doctor: IDoctor): Observable<IDoctor> {
    return this.http.post<IDoctor>(`${this.baseUrl}/doctor/`, doctor);
  }

  editDoctor(doctor: IDoctor): Observable<IDoctor> {
    return this.http.put<IDoctor>(`${this.baseUrl}/doctor`, doctor);
  }

  removeDoctor(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/doctor/${id}`);
  }
}
