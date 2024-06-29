import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { DogService } from 'src/app/services/api/dog.service';

@Component({
  selector: 'app-vista-quedada',
  templateUrl: './vista-quedada.page.html',
  styleUrls: ['./vista-quedada.page.scss'],
  standalone: true,
  imports: [
    IonicModule
  ]
})
export class VistaQuedadaPage implements OnInit {
  meeting: any;
  error: any = null;
  isEditable: boolean = false;

  constructor(private dogService:DogService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const meetingId = this.route.snapshot.paramMap.get('id');

    if (meetingId) {
      this.dogService.getMeetingById(meetingId).subscribe({
        next: (data) => {
          this.meeting = data;
          console.log(this.meeting);
        },
        error: (error) => {
          this.error = error;
        }
      });
    } else {
      this.error = 'No se proporcionó un ID de quedada válido.';
    }
  }

  
  toggleEditable() {
    this.isEditable = !this.isEditable;
  }
}
