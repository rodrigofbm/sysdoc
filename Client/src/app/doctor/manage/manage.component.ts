import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IDoctor } from './../../shared/models/IDoctor';
import { IPatient } from './../../shared/models/IPatients';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { v4 as uuidv4 } from 'uuid';

import { PatientService } from './../../patient/patient.service';
import { DoctorService } from './../doctor.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent implements OnInit {
  @ViewChild('form') form: NgForm;
  error: string;
  requestError: string;
  requestSuccess: string;
  isEdit = false;
  doctorId: string;
  isLoading = false;

  constructor(private doctorService: DoctorService, private patientService: PatientService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.isEdit = params.id !== undefined;
      if (this.isEdit) {
        this.doctorService.getDoctor(params.id).subscribe(doctor => {
          this.doctorId = doctor.id;
          this.form.control.get('name').setValue(doctor.name);
          this.form.control.get('crm').setValue(doctor.crm);
          this.form.control.get('crmuf').setValue(doctor.crmUf);
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

    const doctor: IDoctor = {
      name: form.control.get('name').value,
      crm: form.control.get('crm').value,
      crmUf: form.control.get('crmuf').value,
      patients: [],
      id: this.doctorId || uuidv4(),
    };

    let obsRequest: Observable<IDoctor>;
    if (this.isEdit) {
      obsRequest = this.doctorService.editDoctor(doctor);
    } else {
      obsRequest = this.doctorService.createDoctor(doctor);
    }

    this.requestError = null;
    this.requestSuccess = null;
    obsRequest.subscribe(
      (resp) => {
        this.isLoading = false;
        this.requestSuccess = 'Médico salvo com sucesso';
      },
      (e) => {
        this.isLoading = false;
        this.requestError = e.error.message;
      }
    );
  }
}
