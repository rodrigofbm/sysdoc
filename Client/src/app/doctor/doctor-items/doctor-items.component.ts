import { Component, OnInit } from '@angular/core';

import { DoctorService } from '../doctor.service';
import { IDoctor } from './../../shared/models/IDoctor';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor-items.component.html',
  styleUrls: ['./doctor-items.component.css']
})
export class DoctorItemsComponent implements OnInit {
  doctors: IDoctor[];
  error: string;

  constructor(private doctorService: DoctorService) { }

  ngOnInit(): void {
    this.doctorService.getDoctors().subscribe(docs => {
      this.doctors = docs;
    });
  }

  onRemove(id: string): void {
    this.error = null;
    this.doctorService.removeDoctor(id).subscribe(resp => {
      this.doctors = this.doctors.filter(d => d.id !== id);
    },
    error => {
      this.error = error.error.message;
    });
  }
}
