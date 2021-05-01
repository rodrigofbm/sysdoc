import { PatientsListComponent } from './patients-list/patients-list.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { ManagerPatientComponent } from './manager-patient/manager-patient.component';
import { PatientComponent } from './patient.component';

const routes: Routes = [
  {
    path: 'patients',
    component: PatientComponent,
    children: [
      {path: '', component: PatientsListComponent},
      {path: 'new', component: ManagerPatientComponent},
      {path: ':id/edit', component: ManagerPatientComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PatientRoutingModule {}
