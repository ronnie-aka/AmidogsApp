import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController, ToastController } from '@ionic/angular';

@Component({
  selector: 'app-report',
  templateUrl: './report.page.html',
  styleUrls: ['./report.page.scss'],
})
export class ReportPage implements OnInit {

  reportText: string = '';
  userId: string = ''; // Variable para almacenar el ID del usuario

  constructor(
    private route: ActivatedRoute,
    private navCtrl: NavController,
    private toastController: ToastController // Añadido ToastController aquí
  ) { }

  ngOnInit() {
    // Obtener el parámetro 'id' de la ruta
    this.userId = this.route.snapshot.paramMap.get('id') ?? '';
  }

  async submitReport() {
    if (this.reportText.trim().length > 0) {
      // Aquí podrías añadir el código para enviar el reporte
      console.log("Reporte enviado para el usuario con ID:", this.userId);
      console.log("Contenido del reporte:", this.reportText);
      
      this.reportText = '';

      this.updateReportCount();

      this.goBack();
      await this.presentToast('El usuario ha sido denunciado.');
    }
  }

  updateReportCount() {
    // Método para actualizar el número de reportes del usuario
    console.log("Actualizando número de reportes del usuario con ID:", this.userId);
    // Aquí podrías implementar la lógica para enviar la actualización al backend
  }

  async presentToast(message: string) {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000, // Duración del toast en milisegundos
    });
    await toast.present();
    return toast.onDidDismiss(); // Esperar a que el toast se cierre
  }

  goBack() {
    this.navCtrl.back();
  }
}
