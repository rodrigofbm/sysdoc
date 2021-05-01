import { IPatient } from './../shared/models/IPatients';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  baseUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getPatients(): Observable<IPatient[]> {
    return this.http.get<IPatient[]>(`${this.baseUrl}/patient`);
  }

  getPatient(id: string): Observable<IPatient> {
    return this.http.get<IPatient>(`${this.baseUrl}/patient/${id}`);
  }

  removePatient(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/patient/${id}`);
  }

  createPatient(patient: IPatient): Observable<IPatient> {
    return this.http.post<IPatient>(`${this.baseUrl}/patient/`, patient);
  }

  editPatient(patient: IPatient): Observable<IPatient> {
    return this.http.put<IPatient>(`${this.baseUrl}/patient`, patient);
  }
}
