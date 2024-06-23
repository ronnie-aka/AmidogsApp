import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {DogService } from 'src/app/services/api/dog.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss']
})
export class HomePage {

  dogs: any[] = [];
  error: any = null;

  constructor(private dogService: DogService, private router: Router) {}

  ngOnInit(): void {
    this.dogService.getDogs().subscribe({
      next: (data) => {
        this.dogs = data;
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

  onCardClick(dog : any) {
    this.router.navigate(['/tabs/profile', dog.id]); // asumiendo que cada perro tiene un id único
  }
  
  onCloseClick(dog: any): void {
    console.log('Close icon clicked for dog:', dog);
    // Aquí puedes poner la lógica que quieras que ocurra cuando se haga clic en el icono de cerrar
  }

  onPawClick(dog: any): void {
    console.log('Paw icon clicked for dog:', dog);
    // Aquí puedes poner la lógica que quieras que ocurra cuando se haga clic en el icono de la pata
  }

}
