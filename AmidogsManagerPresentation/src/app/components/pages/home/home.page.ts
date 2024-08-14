import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { ModalController } from '@ionic/angular';
import { MatchComponent } from '../../partials/match/match.component';
import { MatchService } from 'src/app/services/api/match.service';
import { GlobalDogService } from 'src/app/services/utils/globalDog.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  dogs: any[] = [];
  error: any = null;
  myDogId : any;

  constructor(
    private dogService: DogService,
    private router: Router,
    private modalController: ModalController,
    private matchService: MatchService,
    private globalDogService : GlobalDogService
  ) {}

  ngOnInit(): void {
    // Suscríbete al perro desde el servicio global
    this.globalDogService.dog$.subscribe(dog => {
      if (dog) {
        this.myDogId = dog.Id.toString();; // Almacena el perro obtenido
        console.log('Perro actual:', this.myDogId);

        // Llama al método que obtiene perros no emparejados usando el ID del perro
        this.dogService.getDogsNoMatched(this.myDogId).subscribe({
          next: (data) => {
            this.dogs = data; // Almacena la lista de perros no emparejados
          },
          error: (error) => {
            this.error = error; // Maneja el error
          },
        });
      } else {
        console.log('No hay perro en el estado global.');
      }
    });
}


  onCardClick(dog: any) {
    console.log(dog.Id);
    this.router.navigate(['/tabs/dog-profile', dog.Id]);
  }

  onCloseClick(dog: any): void {
    console.log('Close icon clicked for dog:', dog);
    this.dogs = this.dogs.filter((d) => d.Id !== dog.Id);
  }

  async onPawClick(dog: any) {
    try {
      // Comprobar si ya existe un match entre DogId1 y DogId2
      this.matchService.getMatchByDogs('1', dog.Id).subscribe({
        next: (existingMatch) => {
          if (existingMatch) {
            // Solo actualiza el campo correspondiente basado en quién hizo clic
            console.log(existingMatch.DogId1, this.myDogId);
            const updatedMatch = {
              ...existingMatch,
              LikeDog1:
                existingMatch.DogId1 === this.myDogId
                  ? true
                  : existingMatch.LikeDog1,
              LikeDog2:
                existingMatch.DogId2 === this.myDogId
                  ? true
                  : existingMatch.LikeDog2,
            };

            // Llamada para actualizar el match
            this.matchService.createOrUpdateMatch(updatedMatch).subscribe({
              next: (response) => {
                console.log('Match actualizado con éxito:', response);
                // Verifica si ambos perros han dado like después de la actualización
                if (response.LikeDog1 && response.LikeDog2) {
                  console.log(
                    'LOS DOS PERROS HAN DADO LIKE DESPUÉS DE ACTUALIZAR',
                    response.LikeDog2,
                    response.LikeDog1
                  );
                  this.showMatchModal(dog);
                  // Actualiza la lista de perros
                  this.loadDogs();
                }
              },
              error: (error) => {
                console.error('Error al actualizar el match:', error);
              },
            });
            this.dogs = this.dogs.filter((d) => d.Id !== dog.Id);
          }
        },
        error: (error) => {
          if (error.status === 404) {
            // Si no existe un match, crear uno nuevo
            const newMatch = {
              MatchDate: new Date().toISOString(),
              Chat: '',
              DogId1: this.myDogId,
              DogId2: dog.Id,
              LikeDog1: true,
              LikeDog2: false,
            };
            this.matchService.createOrUpdateMatch(newMatch).subscribe({
              next: (response) => {
                console.log('Match creado con éxito:', response);
                // Actualiza la lista de perros
                this.loadDogsFiltered(dog.Id);
              },
              error: (error) => {
                console.error('Error al crear el match:', error);
              },
              
            });
          } else {
            console.error(
              'Error al comprobar la existencia de un match:',
              error
            );
          }
        },
      });
    } catch (error) {
      console.error('Error inesperado:', error);
    }
  }

  private loadDogs(): void {
    this.dogService.getDogsNoMatched('1').subscribe({
      next: (data) => {
        this.dogs = data;
      },
      error: (error) => {
        this.error = error;
      },
    });
  }
  private loadDogsFiltered(dogIdToRemove: string): void {
    this.dogService.getDogsNoMatched(this.myDogId).subscribe({
      next: (data: any[]) => {  // Especifica que 'data' es un array de cualquier tipo
        this.dogs = data.filter((d: any) => d.Id !== dogIdToRemove); // Especifica el tipo 'any' para 'd'
      },
      error: (error) => {
        this.error = error;
      },
    });
  }

  async showMatchModal(dog: any) {
    const modal = await this.modalController.create({
      component: MatchComponent,
      componentProps: { dog: dog },
      cssClass: 'paw-modal-class',
    });

    await modal.present();
  }
}
