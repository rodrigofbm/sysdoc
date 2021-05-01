import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { ReportsComponent } from './reports/reports.component';
import { DoctorItemsComponent } from './doctor-items/doctor-items.component';
import { DoctorComponent } from './doctor.component';
import { ManageComponent } from './manage/manage.component';

const routes: Routes = [
  {
    path: 'doctors',
    component: DoctorComponent,
    children: [
      { path: '', component: DoctorItemsComponent },
      { path: 'new', component: ManageComponent },
      { path: 'reports', component: ReportsComponent },
      { path: ':id/edit', component: ManageComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DoctorRoutingModule {}
