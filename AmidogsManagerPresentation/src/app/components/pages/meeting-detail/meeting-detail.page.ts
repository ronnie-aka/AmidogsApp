import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import * as moment from 'moment';
import { AlertController, ModalController, ToastController } from '@ionic/angular';
import { DatePickerModalComponent } from '../../partials/date-picker-modal/date-picker-modal.component';
import { NavController } from '@ionic/angular';
import { MeetingService } from 'src/app/services/api/meeting.service';
import { DogMeetingService } from 'src/app/services/api/dogMeeting.service';

@Component({
  selector: 'app-meeting-detail',
  templateUrl: './meeting-detail.page.html',
  styleUrls: ['./meeting-detail.page.scss'],
})
export class MeetingDetailPage implements OnInit {
  meeting: any;
  error: any = null;
  isEditable: boolean = false;
  editButton: boolean = false;
  isNewMeeting: boolean = false;
  valid: boolean = true;
  dogs : any[] = [];
  maxParticipantes : any;
  mydog = "1"

  constructor(
    private dogService: DogService,
    private meetingService : MeetingService,
    private route: ActivatedRoute,
    private router: Router,
    private modalController: ModalController,
    private navCtrl: NavController,
    private alertController: AlertController,
    private toastController: ToastController,
    private dogMeetingService: DogMeetingService
  ) {}

  ngOnInit(): void {
    const meetingId = this.route.snapshot.paramMap.get('id') || '';
    const isEditableParam = this.route.snapshot.paramMap.get('editable');
    const isNewMeetingParam = this.route.snapshot.paramMap.get('isNewMeeting');
    
    this.isEditable = false;
    this.editButton = isEditableParam === 'true';
    this.isNewMeeting = isNewMeetingParam === 'true';

    if (this.isNewMeeting) {
      this.initializeNewMeeting();
      this.isEditable = true;
    } else if (meetingId !== 'new') {
      this.loadExistingMeeting(meetingId);
      this.loadDogsInMeeting(meetingId);
    }
  }

  initializeNewMeeting() {
    this.meeting = {
      id: '',  // ID inicial vacío para una nueva reunión
      MeetingTitle: '',  // Título de la reunión, inicializado vacío
      Date: new Date().toISOString(),  // Fecha y hora inicializada a la fecha y hora actuales
      Location: '',  // Ubicación inicializada vacía
      Description: '',  // Descripción inicializada vacía
      maxParticpants: 2,  // Valor predeterminado para el número máximo de participantes (ajustar según sea necesario) // Lista de perros inicializada con el perro por defecto
    };
  }
  

  loadExistingMeeting(meetingId: string) {
    this.meetingService.getMeetingById(meetingId).subscribe({
      next: (data) => {
        this.meeting = data;
        this.maxParticipantes =  parseInt(this.meeting.MaxParticpants);
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

  loadDogsInMeeting(meetingId: string){
    this.dogService.getDogsInMeeting(meetingId).subscribe({
      next: (data) => {
        this.dogs = data;
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

  async openDateTimePicker() {
    const modal = await this.modalController.create({
      component: DatePickerModalComponent,
      componentProps: {
        selectedDate: this.meeting.Date, // Pasar la fecha actual de la reunión
      },
    });
  
    modal.onWillDismiss().then(({ data }) => {
      if (data) {
        this.meeting.Date = new Date(data.date);  // Ahora usamos 'data.date'
      }
    });
  
    return await modal.present();
  }
  

  formatDateTime(dateTime: Date | string): string {
    if (!dateTime) {
      return '';
    }
  
    const formattedDateTime = moment(new Date(dateTime)).format('DD/MM/YYYY HH:mm');
    return formattedDateTime !== "Invalid date" ? formattedDateTime : "Fecha inválida";
  }
  

  onDogClick(dog: any, event: Event): void {
    event.stopPropagation();
    this.router.navigate(['/tabs/dog-profile', dog.Id]);
  }

  saveChanges() {
    if(this.validateMaxParticipants()){
      if (this.isNewMeeting) {
        this.createMeeting();
      } 
      else if(this.isEditable){
        this.updateMeeting();
        console.log('ha entrado por aquí');
      }
      else {
        this.toggleEditMode();
      }
    }
  }

  createMeeting() {
    const newMeeting = {
      MeetingTitle: this.meeting.MeetingTitle,
      Date: this.meeting.Date,
      Location: this.meeting.Location,
      Description: this.meeting.Description,
      maxParticpants: this.maxParticipantes,
      perros: this.meeting.perros
    };
  
    // Primero, crear la reunión
    this.meetingService.createMeeting(newMeeting).subscribe({
      next: (createdMeeting) => {
        // La creación de la reunión fue exitosa, obtenemos el ID de la reunión creada
        const meetingId = createdMeeting.Id;
        console.log()
        
        // Ahora añadir el perro al meeting
        this.dogMeetingService.AddDogToMeeting(parseInt(this.mydog), meetingId, true).subscribe({
          next: () => {
            this.presentToast('Reunión y perro añadidos exitosamente');
            this.isNewMeeting = false;
            this.isEditable = false;
          },
          error: (error) => {
            this.presentToast('Error al añadir el perro al meeting');
            this.error = error;
          }
        });
      },
      error: (error) => {
        console.error('Error al crear la reunión:', error);
        if (error.error) {
          console.error('Detalles del error:', error.error);
        }
        this.presentToast('Error al crear la reunión');
      }
    });
  }
  
  toggleEditMode() {
    this.isEditable = !this.isEditable;
  }

  async deleteMeetingClick(meetingId: any) {
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
              next:  () => {
                this.goBack();
                this.presentToast('Quedada eliminada exitosamente').then(() => {
                });
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

  updateMeeting() {
    // Asegúrate de que el objeto tenga todas las propiedades requeridas por el tipo Meeting
    const updatedMeeting = {
      id: this.meeting.id,  // Incluye el ID si es necesario
      MeetingTitle: this.meeting.MeetingTitle,
      Date: this.meeting.Date,
      Location: this.meeting.Location,
      Description: this.meeting.Description,
      maxParticpants: this.maxParticipantes,  // Asegúrate de que coincida con el nombre en tu modelo
    };
  
    // Llamar al servicio para actualizar la reunión
    this.meetingService.updateMeeting(this.meeting.Id, updatedMeeting).subscribe({
      next: () => {
        this.presentToast('Cambios guardados exitosamente');
        this.isEditable = false;  // Salir del modo de edición
      },
      error: (error) => {
        this.presentToast('Error al guardar los cambios');
        this.error = error;
      }
    });
  }
  
  
  

  async presentToast(message: string) {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000, // Duración del toast en milisegundos
    });
    await toast.present();
    return toast.onDidDismiss();
  }

  goBack() {
    this.navCtrl.back();
  }

  validateMaxParticipants(): boolean {
    const numPerros = this.dogs.length || 0;

    this.maxParticipantes = Number(this.maxParticipantes);
    console.log('Tipo de dato de maxParticipantes:', typeof this.maxParticipantes);

    if (!Number.isInteger(this.maxParticipantes) || this.maxParticipantes < 2 || this.maxParticipantes > 10) {
      this.presentToast('El número de participantes debe ser un número entero entre 2 y 10.');
      console.log('is integer ', !Number.isInteger(this.maxParticipantes));
      console.log('menor 2 ', this.maxParticipantes < 2);
      console.log('mayor 10 ', this.maxParticipantes > 10);
      return false;
    }

    if (numPerros > 1 && this.maxParticipantes < numPerros) {
      this.presentToast(`El número máximo participantes no puede ser menor que el número de perros (${numPerros}).`);
      return false;
    }

    this.valid = true;
    return true;
  }
}
