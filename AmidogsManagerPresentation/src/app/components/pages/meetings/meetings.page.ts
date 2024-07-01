import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { ToastController } from '@ionic/angular';

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
    private toastController: ToastController
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
    const newMeeting = {
      fecha: new Date().getTime(), // Ejemplo: fecha actual en formato timestamp
      hora: new Date().getTime(), // Ejemplo: hora actual en formato timestamp
      perros: [], // Aquí debes incluir los perros participantes de la quedada, según tu lógica de aplicación
      descripcion: '', // Aquí la descripción de la quedada
      titulo: '', // Título de la quedada
      ubicacion: '', // Ubicación de la quedada
    };

    this.dogService.createMeeting(newMeeting).subscribe({
      next: (data) => {
        // Puedes redirigir a la página de detalles de la nueva quedada si es necesario
        this.router.navigate(['/tabs/meeting-detail', data.id]);
      },
      error: (error) => {
        // Aquí podrías mostrar un mensaje de error utilizando ToastController
        this.presentToast('Error al crear la quedada');
      },
    });
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
    this.router.navigate([
      '/tabs/meeting-detail',
      meetingId,
      { editable: false },
    ]);
  }
}
