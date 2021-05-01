import { DoctorItemsComponent } from './doctor-items/doctor-items.component';
import { DoctorComponent } from './doctor.component';
import { ManageComponent } from './manage/manage.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: 'doctors',
    component: DoctorComponent,
    children: [
      { path: '', component: DoctorItemsComponent },
      { path: 'new', component: ManageComponent },
      { path: ':id/edit', component: ManageComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DoctorRoutingModule {}
