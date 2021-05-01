import { NgForm } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { listLocales } from 'ngx-bootstrap/chronos';
import { v4 as uuidv4 } from 'uuid';

import { IPatient } from './../../shared/models/IPatients';
import { IDoctor } from './../../shared/models/IDoctor';
import { DoctorService } from './../../doctor/doctor.service';
import { PatientService } from './../patient.service';

@Component({
  selector: 'app-manager-patient',
  templateUrl: './manager-patient.component.html',
  styleUrls: ['./manager-patient.component.css']
})
export class ManagerPatientComponent implements OnInit {
  locales = listLocales();
  maxDate = new Date();
  doctors: IDoctor[];
  error: string;
  requestError: string;
  requestSuccess: string;

  constructor(private localeService: BsLocaleService, private patientService: PatientService, private doctorService: DoctorService) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.doctorService.getDoctors().subscribe(docs => {
      this.doctors = docs;
    });
  }

  onSubmit(form: NgForm): void {
    this.error = null;
    if (form.invalid) {
      this.error = 'Todos os campos são obrigatórios';
      return;
    }
    const patient: IPatient = {
      name: form.control.get('name').value,
      birthDate: new Date(form.control.get('birthdate').value),
      cpf: form.control.get('cpf').value,
      doctorId: form.control.get('doctor').value,
      id: uuidv4()
    };


    this.requestError = null;
    this.requestSuccess = null;
    this.patientService.createPatient(patient).subscribe(resp => {
      this.requestSuccess = 'Paciente cadastrado com sucesso';
    },
    e => {
      this.requestError = e.error.message;
    });
  }
}
