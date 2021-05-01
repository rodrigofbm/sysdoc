import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DoctorRoutingModule } from './doctor-routing.module';
import { DoctorComponent } from './doctor.component';
import { DoctorItemsComponent } from './doctor-items/doctor-items.component';
import { ManageComponent } from './manage/manage.component';



@NgModule({
  declarations: [
    DoctorItemsComponent,
    DoctorComponent,
    ManageComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    DoctorRoutingModule
  ]
})
export class DoctorModule { }
