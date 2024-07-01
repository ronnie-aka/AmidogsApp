import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import * as moment from 'moment';
import { ModalController } from '@ionic/angular';
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

  constructor(
    private dogService: DogService,
    private route: ActivatedRoute,
    private router: Router,
    private modalController: ModalController,
    private navCtrl: NavController
  ) {}

  ngOnInit(): void {
    const meetingId = this.route.snapshot.paramMap.get('id');
  
    if (meetingId) {
      this.dogService.getMeetingById(meetingId).subscribe({
        next: (data) => {
          this.meeting = data;
        },
        error: (error) => {
          this.error = error;
        }
      });
    } else {
      this.error = 'No se proporcionó un ID de quedada válido.';
    }
  
    // Leer el parámetro 'editable' de la URL
    const isEditableParam = this.route.snapshot.paramMap.get('editable');
    this.isEditable = isEditableParam === 'true'; // Convertir el string en booleano
    this.editButton = isEditableParam == 'true';
  }

  onDogClick(dog: any, event: Event): void {
    event.stopPropagation();
    this.router.navigate(['/tabs/dog-profile', dog.id]);
  }

  async openDateTimePicker() {
    const modal = await this.modalController.create({
      component: DatePickerModalComponent,
      componentProps: {
        selectedDate: this.meeting.fecha,
        selectedTime: this.meeting.hora,
        mode: 'datetime'
      }
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
    const formattedHour= moment(time, 'HH:mm').format('HH:mm');
    return `${formattedDate} ${formattedHour}`;
  }

  saveChanges() {
    this.isEditable = !this.isEditable;
  }

  goBack() {
    this.navCtrl.back();
  }
}
