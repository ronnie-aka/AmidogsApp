import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import * as moment from 'moment';
import { AlertController, ModalController, ToastController } from '@ionic/angular';
import { DatePickerModalComponent } from '../../partials/date-picker-modal/date-picker-modal.component';
import { NavController } from '@ionic/angular';

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

  defaultDog = {
    nombre: "Tom",
    genero: "Macho",
    castrado: true,
    dominante: true,
    raza: "Mastin Español",
    rangoEdad: "Joven",
    personalidad: "Juguetón",
    tamano: "Grande",
    foto: "tom",
    presentacion: "Saludos a todos!! Este es mi perrito Tom, es un perrito muy cariñoso y jugetón. Busca nuevos amigos con los que jugar y divertirse. Tenemos muchos juguetes que podemos compartir en caso de que nos hagamos amigos.",
    id: "5"
  };

  constructor(
    private dogService: DogService,
    private route: ActivatedRoute,
    private router: Router,
    private modalController: ModalController,
    private navCtrl: NavController,
    private alertController: AlertController,
    private toastController: ToastController,
  ) {}

  ngOnInit(): void {
    const meetingId = this.route.snapshot.paramMap.get('id') || '';
    const isEditableParam = this.route.snapshot.paramMap.get('editable');
    const isNewMeetingParam = this.route.snapshot.paramMap.get('isNewMeeting');
    
    this.isEditable = isEditableParam === 'true';
    this.editButton = this.isEditable;
    this.isNewMeeting = isNewMeetingParam === 'true';

    if (this.isNewMeeting) {
      this.initializeNewMeeting();
    } else if (meetingId !== 'new') {
      this.loadExistingMeeting(meetingId);
    }
  }

  initializeNewMeeting() {
    this.meeting = {
      fecha: new Date().getTime(),
      hora: new Date().getTime(),
      perros: [this.defaultDog],
      descripcion: '',
      titulo: '',
      ubicacion: '',
      maxParticipantes: '',
    };
  }

  loadExistingMeeting(meetingId: string) {
    this.dogService.getMeetingById(meetingId).subscribe({
      next: (data) => {
        this.meeting = data;
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
        selectedDate: this.meeting.fecha,
        selectedTime: this.meeting.hora,
        mode: 'datetime',
      },
    });

    modal.onWillDismiss().then(({ data }) => {
      if (data) {
        this.meeting.fecha = data.fecha;
        this.meeting.hora = data.hora;
      }
    });

    return await modal.present();
  }

  formatDateTime(date: Date, time: string): string {
    const formattedDate = moment(date).format('DD/MM/YYYY');
    const formattedHour = moment(time, 'HH:mm').format('HH:mm');
    return `${formattedDate} ${formattedHour}`;
  }

  onDogClick(dog: any, event: Event): void {
    event.stopPropagation();
    this.router.navigate(['/tabs/dog-profile', dog.Id]);
  }

  saveChanges() {
    if(this.validateMaxParticipants()){
      if (this.isNewMeeting) {
        this.createMeeting();
      } else {
        this.toggleEditMode();
      }
    }
  }

  createMeeting() {
    this.dogService.createMeeting(this.meeting).subscribe({
      next: (data) => {
        this.presentToast('Reunión creada exitosamente');
        this.meeting.id = data.id; // Asignar el ID devuelto por el servidor
        this.isNewMeeting = false;
        this.isEditable = false;
      },
      error: (error) => {
        this.error = error;
      },
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
            this.dogService.deleteMeetingById(meetingId).subscribe({
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
    const maxParticipantes =  parseInt(this.meeting.maxParticipantes);
    const numPerros = this.meeting.perros?.length || 0;

    if (!Number.isInteger(maxParticipantes) || maxParticipantes < 2 || maxParticipantes > 10) {
      this.presentToast('El número de participantes debe ser un número entero entre 2 y 10.');
      return false;
    }

    if (numPerros > 1 && maxParticipantes < numPerros) {
      this.presentToast(`El número máximo participantes no puede ser menor que el número de perros (${numPerros}).`);
      return false;
    }

    this.valid = true;
    return true;
  }
}
