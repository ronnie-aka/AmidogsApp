import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, Component, Input, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule, ModalController } from '@ionic/angular';

@Component({
  selector: 'app-date-picker-modal',
  templateUrl: './date-picker-modal.component.html',
  styleUrls: ['./date-picker-modal.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    FormsModule,
    CommonModule  
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class DatePickerModalComponent {
  selectedDateTime: string;
  minDate: string;
  maxDate: string; 

  constructor(private modalController: ModalController) {
    const currentDate = new Date();

    // Ejemplo de establecimiento de fechas mínima y máxima (puedes ajustar según tus necesidades)
    this.minDate = currentDate.toISOString(); // Fecha mínima (1 de enero del año anterior)
    this.maxDate = new Date(currentDate.getFullYear() + 1, 11, 31).toISOString(); // Fecha máxima (31 de diciembre del próximo año)

    // Para seleccionar una fecha y hora predeterminada, podrías inicializar selectedDateTime con la fecha actual, por ejemplo:
    this.selectedDateTime = new Date().toISOString(); // Fecha y hora actual
  }

  dismissModal() {
    this.modalController.dismiss();
  }

  confirmDateTime() {
    const selectedDate = new Date(this.selectedDateTime);
    this.modalController.dismiss({
      date: selectedDate,
    });
  }
  
}