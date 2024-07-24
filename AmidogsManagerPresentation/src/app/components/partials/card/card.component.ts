import { Component, EventEmitter, Input, Output} from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';
import { MatchComponent } from '../match/match.component';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  standalone: true,
  imports: [
    IonicModule
  ]
})
export class CardComponent {
  @Input() name! : string;
  @Input() photoUrl! : string;

  @Output() close = new EventEmitter<void>();
  @Output() paw = new EventEmitter<void>();
  @Output() cardClick = new EventEmitter<void>();

  constructor(private modalController: ModalController) {}

  onCardClick() {
    this.cardClick.emit();
  }

  closeClick() {
    this.close.emit();
  }

  pawClick() {
    this.paw.emit();
  }

}

