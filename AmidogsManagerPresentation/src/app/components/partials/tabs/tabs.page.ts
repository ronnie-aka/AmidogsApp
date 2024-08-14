import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalDogService } from 'src/app/services/utils/globalDog.service';

@Component({
  selector: 'app-tabs',
  templateUrl: 'tabs.page.html',
  styleUrls: ['tabs.page.scss'],
})
export class TabsPage {
  userDogId: any;

  constructor(
    private globalDogService: GlobalDogService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Suscríbete al perro desde el servicio global
    this.globalDogService.dog$.subscribe((dog) => {
      if (dog) {
        this.userDogId = dog.Id.toString(); // Almacena el perro obtenido
        console.log('Perro actual:', this.userDogId);
      } else {
        console.log('No hay perro en el estado global.');
      }
    });
  }

  goToDogProfile(): void {
    if (this.userDogId) {
      // Navega a la página de dog-profile pasando el userDogId
      this.router.navigate(['/tabs/dog-profile', this.userDogId]);
    } else {
      console.log('No hay un perro en el estado global.');
    }
  }
}
