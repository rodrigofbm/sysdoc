import { Observable } from 'rxjs';
import { NgForm } from '@angular/forms';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { listLocales } from 'ngx-bootstrap/chronos';
import { v4 as uuidv4 } from 'uuid';

import { IPatient } from './../../shared/models/IPatients';
import { IDoctor } from './../../shared/models/IDoctor';
import { DoctorService } from './../../doctor/doctor.service';
import { PatientService } from './../patient.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-manager-patient',
  templateUrl: './manager-patient.component.html',
  styleUrls: ['./manager-patient.component.css'],
})
export class ManagerPatientComponent implements OnInit {
  @ViewChild('form') form: NgForm;
  locales = listLocales();
  maxDate = new Date();
  doctors: IDoctor[];
  error: string;
  requestError: string;
  requestSuccess: string;
  isEdit = false;
  patientId: string;
  isLoading = false;

  constructor(
    private localeService: BsLocaleService,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private route: ActivatedRoute
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.doctorService.getDoctors().subscribe((docs) => {
      this.doctors = docs;
    });

    this.route.params.subscribe(params => {
      this.isEdit = params.id !== undefined;
      if (this.isEdit) {
        this.patientService.getPatient(params.id).subscribe(patient => {
          this.patientId = patient.id;
          this.form.control.get('name').setValue(patient.name);
          this.form.control.get('cpf').setValue(patient.cpf);
          this.form.control.get('birthdate').setValue(new Date(patient.birthDate));
          this.form.control.get('doctor').setValue(patient.doctorId);
        });
      }
    });
  }

  onSubmit(form: NgForm): void {
    this.isLoading = true;
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
      id: this.patientId || uuidv4(),
    };

    let obsRequest: Observable<IPatient>;
    if (this.isEdit) {
      obsRequest = this.patientService.editPatient(patient);
    } else {
      obsRequest = this.patientService.createPatient(patient);
    }

    this.requestError = null;
    this.requestSuccess = null;
    obsRequest.subscribe(
      (resp) => {
        this.isLoading = false;
        this.requestSuccess = 'Paciente salvo com sucesso';
      },
      (e) => {
        this.isLoading = false;
        this.requestError = e.error.message;
      }
    );
  }
}
