import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.page.html',
  styleUrls: ['./cards.page.scss'],
})
export class CardsPage implements OnInit {
  @Input() name! : string;
  @Input() photoUrl! : string;

 
  constructor() { }


  ngOnInit() {
  }

}
