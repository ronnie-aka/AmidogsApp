import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { ToastController, AlertController, NavController } from '@ionic/angular';
import { MeetingService } from 'src/app/services/api/meeting.service';
import { Meeting } from 'src/app/shared/models/meeting';
import { DogMeetingService } from 'src/app/services/api/dogMeeting.service'; 
import { GlobalDogService } from 'src/app/services/utils/globalDog.service';

@Component({
  selector: 'app-meetings',
  templateUrl: 'meetings.page.html',
  styleUrls: ['meetings.page.scss'],
})
export class meetingsPage {
  meetingsWithoutDog: any[] = [];
  meetingsWithDog: any[] = [];
  meetingsDogOwner: any[] = [];
  error: any = null;
  currentTab: string = '';
  myDog : any;
  hasInitialized: boolean = false;

  dogsInMeetings: { [key: string]: any[] } = {};

  constructor(
    private dogService: DogService,
    private router: Router,
    private toastController: ToastController,
    private alertController: AlertController,
    private meetingService: MeetingService,
    private dogMeetingService: DogMeetingService,
    private globalDogService : GlobalDogService
  ) {}

  ngOnInit(): void {
    this.globalDogService.dog$.subscribe(dog => {
      if (dog) {
        this.myDog = dog.Id.toString();
        console.log('Perro actual:', this.myDog.DogName);
        this.loadMeetings();
      } else {
        console.log('No hay perro en el estado global.');
      }
    });
  }

  ionViewWillEnter(): void {
    if (!this.hasInitialized) {
      this.loadMeetings();
    }
  }

  ionViewWillLeave(): void {
    this.hasInitialized = false;
  }

  loadMeetings(): void {
    this.meetingsWithDog = [];
    this.meetingService.getMeetingsWithDog(this.myDog).subscribe({
      next: (data) => {
        this.meetingsWithDog = data;
        this.loadDogsForMeetings(this.meetingsWithDog);
      },
      error: (error) => {
        this.error = error;
      },
    });
    this.meetingsWithoutDog = [];
    this.meetingService.getMeetingsWithoutDog(this.myDog).subscribe({
      next: (data) => {
        this.meetingsWithoutDog = data;
        this.loadDogsForMeetings(this.meetingsWithoutDog);
      },
      error: (error) => {
        this.error = error;
      },
    });
    this.meetingsDogOwner = [];
    this.meetingService.getMeetingsByDogOwner(this.myDog).subscribe({
      next: (data) => {
        this.meetingsDogOwner = data;
        this.loadDogsForMeetings(this.meetingsDogOwner);
      },
      error: (error) => {
        this.error = error;
      },
    });
  }
  loadDogsForMeetings(meetings: any[]): void {
    meetings.forEach(meeting => {
      this.dogService.getDogsInMeeting(meeting.Id).subscribe({
        next: (dogs) => {
          this.dogsInMeetings[meeting.Id] = dogs;
        },
        error: (error) => {
          console.error(`Error loading dogs for meeting ${meeting.Id}`, error);
        }
      });
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
            this.meetingService.deleteMeeting(meetingId).subscribe({
              next: () => {
                this.loadMeetings();
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
    this.router.navigate(['/tabs/dog-profile', dog.Id]);
  }

  onMeetingEditClick(meetingId: any, event: Event): void {
    event.stopPropagation();
    this.router.navigate([
      '/tabs/meeting-detail',
      meetingId,
      { editable: true },
    ]);
  }

  onMeetingClick(meetingId: number) {
    // Comprobar si el meetingId está en la lista de meetingsDogOwner
    const isEditable = this.meetingsDogOwner.some(meeting => meeting.Id === meetingId);
  
    this.router.navigate([
      '/tabs/meeting-detail',
      meetingId,
      { editable: isEditable },
    ]);
  }

  getHuecosLibres(meeting: any): number {
    return meeting.MaxParticpants - (meeting.perros ? meeting.perros.length : 0);
  }

  onAddDogClick(meetingId: number, event: Event) {
    event.stopPropagation();
  
    const dogId = Number(this.myDog); // Asumiendo que `myDog` es el ID del perro que quieres añadir
    const isOwner = false; // Aquí puedes ajustar si el perro que estás añadiendo es del dueño de la reunión o no

    this.dogMeetingService.AddDogToMeeting(dogId, meetingId, isOwner).subscribe({
      next: () => {
        this.presentToast('Perro añadido a la quedada exitosamente');
        this.loadMeetings(); // Recargar las reuniones para reflejar los cambios
      },
      error: (error) => {
        console.error('Error añadiendo el perro a la quedada', error);
        this.presentToast('Error al añadir el perro a la quedada');
      }
    });
  }

  onRemoveDogClick(meetingId: number, event: Event) {
    event.stopPropagation();
  
    const dogId = Number(this.myDog); // Asumiendo que `myDog` es el ID del perro que quieres añadir // Aquí puedes ajustar si el perro que estás añadiendo es del dueño de la reunión o no

    this.dogMeetingService.removeDogFromMeeting(dogId, meetingId).subscribe({
      next: () => {
        this.presentToast('Perro eliminado de la quedada exitosamente');
        this.loadMeetings(); // Recargar las reuniones para reflejar los cambios
      },
      error: (error) => {
        console.error('Error eliminando el perro de la quedada', error);
        this.presentToast('Error al añadir el perro a la quedada');
      }
    });
  }

}
