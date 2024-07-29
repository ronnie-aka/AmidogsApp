import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { ToastController, AlertController, NavController } from '@ionic/angular';

@Component({
  selector: 'app-meetings',
  templateUrl: 'meetings.page.html',
  styleUrls: ['meetings.page.scss'],
})
export class meetingsPage {
  meetings: any[] = [];
  error: any = null;
  currentTab: string = '';

  constructor(
    private dogService: DogService,
    private router: Router,
    private toastController: ToastController,
    private alertController: AlertController,
    private navCtrl: NavController
  ) {}

  ngOnInit(): void {
    this.dogService.getMeetings().subscribe({
      next: (data) => {
        this.meetings = data;
      },
      error: (error) => {
        this.error = error;
      },
    });
  }

  toggleTab(tab: string) {
    if (tab === 'crearQuedada') {
      this.goToCrearQuedada();
    } else {
      this.currentTab = this.currentTab === tab ? '' : tab;
    }
  }

  goToCrearQuedada() {
    this.router.navigate(['/tabs/meeting-detail', 'new', { editable: true, isNewMeeting: true }]);
  }

  async deleteMeetingClick(meetingId: any, event: Event) {
    event.stopPropagation();
  
    const alert = await this.alertController.create({
      header: 'Confirmar eliminación',
      message: '¿Estás seguro de que deseas eliminar esta quedada?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          cssClass: 'secondary'
        },
        {
          text: 'Eliminar',
          handler: () => {
            this.dogService.deleteMeetingById(meetingId).subscribe({
              next: () => {
                this.meetings = this.meetings.filter(m => m.id !== meetingId);
                this.presentToast('Quedada eliminada exitosamente');
              },
              error: (error) => {
                this.presentToast('Error al eliminar la quedada');
              }
            });
          }
        }
      ]
    });
  
    await alert.present();
  }

  async presentToast(message: string) {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000, // Duración del toast en milisegundos
    });
    toast.present();
  }

  onDogClick(dog: any, event: Event): void {
    event.stopPropagation();
    this.router.navigate(['/tabs/dog-profile', dog.id]);
  }

  onMeetingEditClick(meetingId: any, event: Event): void {
    event.stopPropagation();
    this.router.navigate([
      '/tabs/meeting-detail',
      meetingId,
      { editable: true },
    ]);
  }

  onMeetingClick(meetingId: any) {
    //Añadir comprobación de si PERTENECE al usuario o no.
    this.router.navigate([
      '/tabs/meeting-detail',
      meetingId,
      { editable: true },
    ]);
  }

  getHuecosLibres(meeting: any): number {
    return meeting.maxParticipantes - (meeting.perros ? meeting.perros.length : 0);
  }
}
