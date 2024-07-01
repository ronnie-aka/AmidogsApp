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
  minDate: string; // Si se desea establecer una fecha mínima
  maxDate: string; // Si se desea establecer una fecha máxima

  constructor(private modalController: ModalController) {
    const currentYear = new Date().getFullYear(); // Año actual

    // Ejemplo de establecimiento de fechas mínima y máxima (puedes ajustar según tus necesidades)
    this.minDate = new Date(currentYear - 1, 0, 1).toISOString(); // Fecha mínima (1 de enero del año anterior)
    this.maxDate = new Date(currentYear + 1, 11, 31).toISOString(); // Fecha máxima (31 de diciembre del próximo año)

    // Para seleccionar una fecha y hora predeterminada, podrías inicializar selectedDateTime con la fecha actual, por ejemplo:
    this.selectedDateTime = new Date().toISOString(); // Fecha y hora actual
  }

  dismissModal() {
    this.modalController.dismiss();
  }

  confirmDateTime() {
    const selectedDate = new Date(this.selectedDateTime);
    this.modalController.dismiss({
      fecha: selectedDate,
      hora: selectedDate.toLocaleTimeString('es-ES', { hour: '2-digit', minute: '2-digit' })
    });
  }
}