import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular';

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

  constructor(private modalController: ModalController,private router: Router) { }

  ngOnInit() {}

  async closeModal() {
    await this.modalController.dismiss();
  }

  async openChat() {
    this.router.navigate([
      '/tabs/chat-modal',
      {
        dogId: this.dog.id,
        dogName: this.dog.nombre,
        dogPhoto: this.dog.foto,
      },
    ]);
    await this.modalController.dismiss();
  }

}
