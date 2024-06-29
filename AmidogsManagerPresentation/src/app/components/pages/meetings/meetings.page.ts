import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';

@Component({
  selector: 'app-meetings',
  templateUrl: 'meetings.page.html',
  styleUrls: ['meetings.page.scss']
})
export class meetingsPage {

  meetings: any[] = [];
  error: any = null;
  currentTab: string = '';

  constructor(private dogService: DogService, private router: Router) {}

  ngOnInit(): void {
    this.dogService.getMeetings().subscribe({
      next: (data) => {
        this.meetings = data;
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

  toggleTab(tab: string) {
    if (tab === 'crearQuedada') {
      this.currentTab = this.currentTab === tab ? '' : tab;
      this.goToCrearQuedada();
    } else {
      this.currentTab = this.currentTab === tab ? '' : tab;
    }
  }

  goToCrearQuedada() {
    console.log("nos fuimos pa");
  }

  onDogClick(dog: any): void {
    this.router.navigate(['/tabs/dog-profile', dog.id]);
  }
  onMeetingClick(meetingId : any) {
    this.router.navigate(['/VistaQuedadaPage', meetingId]);
  }

}
