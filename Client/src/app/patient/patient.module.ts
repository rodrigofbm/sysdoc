import { SharedModule } from './../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
defineLocale('pt-br', ptBrLocale);

import { ManagerPatientComponent } from './manager-patient/manager-patient.component';
import { PatientsListComponent } from './patients-list/patients-list.component';
import { PatientRoutingModule } from './patient-routing.module';



@NgModule({
  declarations: [
    ManagerPatientComponent,
    PatientsListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BrowserAnimationsModule,
    PatientRoutingModule,
    SharedModule,
    BsDatepickerModule.forRoot(),
  ]
})
export class PatientModule {}
