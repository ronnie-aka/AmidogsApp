import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController, ToastController } from '@ionic/angular';
import { UserService } from 'src/app/services/api/user.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.page.html',
  styleUrls: ['./report.page.scss'],
})
export class ReportPage implements OnInit {
  reportText: string = '';
  dogId: string = ''; // Variable para almacenar el ID del usuario

  constructor(
    private route: ActivatedRoute,
    private navCtrl: NavController,
    private toastController: ToastController,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.dogId = this.route.snapshot.paramMap.get('id') ?? '';
  }

  async submitReport() {
    const userHasThreeComplaints = await this.checkIfUserHasThreeComplaints();

    if (userHasThreeComplaints) {
      await this.presentToast('El usuario ya tiene 3 denuncias. Este pasará a ser bloqueado.', 4000);
    } else {
      await this.updateReportCount();
      await this.presentToast('El usuario ha sido denunciado.', 2000);
    }

    this.goBack();
  }

  async checkIfUserHasThreeComplaints(): Promise<boolean> {
    try {
      const userResponse = await this.userService.getUserByDogId(this.dogId).toPromise();
      const userId = userResponse?.Id; // Ajusta según la estructura de tu respuesta

      if (userId) {
        // Verificar el número de denuncias
        return userResponse?.Complaint>= 2; // Suponiendo que la respuesta tiene un campo `count`
      } else {
        await this.presentToast('No se pudo encontrar el usuario.',200);
        return false;
      }
    } catch (error) {
      console.error('Error verificando el número de denuncias:', error);
      await this.presentToast('Error al realizar la denuncia.',200);
      return false;
    }
  }

  async updateReportCount() {
    try {
      const userResponse = await this.userService.getUserByDogId(this.dogId).toPromise();
      const userId = userResponse?.Id; // Ajusta según la estructura de tu respuesta

      if (userId) {
        // Luego, actualizar el número de denuncias usando updateComplaintNumber
        await this.userService.updateComplaintNumber(userId).toPromise();
      } else {
        await this.presentToast('No se pudo encontrar el usuario.',200);
      }
    } catch (error) {
      console.error('Error actualizando el conteo de reportes:', error);
      await this.presentToast('Error al realizar la denuncia.',200);
    }
  }

  async presentToast(message: string, duration: number) {
    const toast = await this.toastController.create({
      message: message,
      duration: duration, // Duración del toast en milisegundos
    });
    await toast.present();
    return toast.onDidDismiss(); // Esperar a que el toast se cierre
  }

  goBack() {
    this.navCtrl.back();
  }
}
