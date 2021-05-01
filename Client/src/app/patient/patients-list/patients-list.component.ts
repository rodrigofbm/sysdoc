import { Component, OnInit } from '@angular/core';

import { PatientService } from './../patient.service';
import { IPatient } from './../../shared/models/IPatients';

@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent implements OnInit {
  patients: IPatient[];

  constructor(private patientService: PatientService) { }

  ngOnInit(): void {
    this.patientService.getPatients().subscribe(pats => {
      this.patients = pats;
    });
  }

  onRemove(id: string): void {
    this.patientService.removePatient(id).subscribe(pats => {
      this.patients = this.patients.filter(p => p.id !== id);
    },
    console.log);
  }
}
