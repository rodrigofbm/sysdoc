import { SpinnerComponent } from './components/spinner/spinner.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CpfPipe } from './pipes/cpf.pipe';



@NgModule({
  declarations: [
    SpinnerComponent,
    CpfPipe
  ],
  imports: [
    CommonModule
  ],
  exports: [SpinnerComponent, CpfPipe]
})
export class SharedModule { }
