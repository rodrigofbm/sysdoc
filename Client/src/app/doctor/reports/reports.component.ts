import { IPatient } from './../../shared/models/IPatients';
import { DoctorService } from './../doctor.service';
import { IDoctor } from './../../shared/models/IDoctor';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  doctors: IDoctor[];
  patients: IPatient[];

  constructor(private doctorService: DoctorService) { }

  ngOnInit(): void {
    this.doctorService.getDoctors().subscribe(docs => {
      this.doctors = docs;
    });
  }

  onChange(event: any): void {
    this.doctorService.getDoctor(event.target.value).subscribe(doc => {
      this.patients = doc.patients;
    });
  }
}
