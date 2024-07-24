import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-report',
  templateUrl: './report.page.html',
  styleUrls: ['./report.page.scss'],
})
export class ReportPage implements OnInit {

  reportText: string = '';
  userId: string = ''; // Variable para almacenar el ID del usuario

  constructor(private route: ActivatedRoute, private navCtrl: NavController,) { }

  ngOnInit() {
    // Obtener el parámetro 'id' de la ruta
    this.userId = this.route.snapshot.paramMap.get('id') ?? '';
  }

  submitReport() {
    if (this.reportText.trim().length > 0) {
      // Aquí podrías añadir el código para enviar el reporte
      console.log("Reporte enviado para el usuario con ID:", this.userId);
      console.log("Contenido del reporte:", this.reportText);

      // Vaciar el contenido del textarea
      this.reportText = '';

      // Aquí podrías agregar la lógica para actualizar el número de reportes del usuario
      // Puedes llamar a un servicio que se encargue de actualizar esta información en tu backend
      this.updateReportCount();
    }
  }

  updateReportCount() {
    // Método para actualizar el número de reportes del usuario
    console.log("Actualizando número de reportes del usuario con ID:", this.userId);
    // Aquí podrías implementar la lógica para enviar la actualización al backend
  }

  goBack() {
    this.navCtrl.back();
  }
}
