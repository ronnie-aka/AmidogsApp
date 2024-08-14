import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular';
import { GlobalDogService } from 'src/app/services/utils/globalDog.service';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss'],
  standalone: true,
  imports: [
    IonicModule, 
  ],
})
export class MatchComponent  implements OnInit {

  @Input() dog: any;
  myDogId : any;
  myDogPhoto : any;

  constructor(private modalController: ModalController,private router: Router, private globalDogService : GlobalDogService) { }

  ngOnInit() {
    this.globalDogService.dog$.subscribe(dog => {
      if (dog) {
        this.myDogId = dog.Id.toString();
        this.myDogPhoto = dog.Photo
        
      } else {
        console.log('No hay perro en el estado global.');
      }
    });
  }

  async closeModal() {
    await this.modalController.dismiss();
  }

  async openChat() {
    this.router.navigate([
      '/tabs/chat-modal',
      {
        dogId: this.dog.Id,
        dogName: this.dog.DogName,
        dogPhoto: this.dog.Photo,
      },
    ]);
    await this.modalController.dismiss();
  }

}
