import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss'],
  standalone: true,
  imports: [
    IonicModule
  ]
})
export class SliderComponent {
  @Input() name! : string;
  @Input() photoUrl! : string;

  @Output() dogClick = new EventEmitter<void>();

  onDogClick() {
    this.dogClick.emit();
  }
}
