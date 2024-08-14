import { Component, OnInit } from '@angular/core';
import { DogService } from 'src/app/services/api/dog.service';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { GlobalDogService } from 'src/app/services/utils/globalDog.service';
// Enumerados en TypeScript
enum Breed {
  Mestizo,
  Labrador,
  Pastor_Alemán,
  Golden_Retrierver,
  Bulldog_Francés,
  Beagle,
  Boxer,
  Teckel,
  Caniche,
  Chihuahua,
  Husky,
  Doberman,
  Rottwiler,
  ShihTzu,
  Yorkshire,
  Pomerania,
  Shiba_Inu,
  Cocker_Spaniel,
  Pug,
  Mastín,
}

enum AgeCategory {
  Joven,
  Adulto,
  Senior,
}

enum Personaliity {
  Tímido,
  Sociable,
  Enérgico,
  Tranquilo,
  Miedoso,
  Curioso,
}

enum Size {
  Pequeño,
  Mediano,
  Grande,
  Gigante,
}
@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {
  dog: any;
  error: any = null;
  myDogId: string | null = null;
  isEditable: boolean = false;
  isUserDog: boolean = false;

  // Definir opciones después de que los enumerados se han declarado
  breedOptions = Object.keys(Breed)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      value: Breed[key as keyof typeof Breed],
      label: key.replace(/_/g, ' '),
    }));

  ageCategoryOptions = Object.keys(AgeCategory)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      value: AgeCategory[key as keyof typeof AgeCategory],
      label: key,
    }));

  personalityOptions = Object.keys(Personaliity)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      value: Personaliity[key as keyof typeof Personaliity],
      label: key,
    }));

  sizeOptions = Object.keys(Size)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      value: Size[key as keyof typeof Size],
      label: key,
    }));

  constructor(
    private dogService: DogService,
    private route: ActivatedRoute,
    private navCtrl: NavController,
    private globalDogService: GlobalDogService
  ) {}

  ngOnInit(): void {
    const dogId = this.route.snapshot.paramMap.get('id');

    this.globalDogService.dog$.subscribe((dog) => {
      if (dog) {
        this.myDogId = dog.Id.toString();
        console.log('Perro actual:', this.myDogId);
        this.isUserDog = this.myDogId === dogId;
      } else {
        console.log('No hay perro en el estado global.');
      }
    });

    if (dogId) {
      this.dogService.getDogById(dogId).subscribe({
        next: (data) => {
          this.dog = data;
          console.log(this.dog);
        },
        error: (error) => {
          this.error = error;
        },
      });
    } else {
      this.error = 'No se proporcionó un ID de perro válido.';
    }
  }

  toggleEditable() {
    if (this.isEditable) {
      // Si el modo de edición está activado, entonces se va a guardar la información
      this.dogService.updateDog(this.dog.Id, this.dog).subscribe({
        next: (response) => {
          console.log('Dog updated successfully:', response);
          this.isEditable = false;  // Desactiva el modo de edición
        },
        error: (error) => {
          console.error('Error updating dog:', error);
        }
      });
    } else {
      // Si el modo de edición está desactivado, lo activamos
      this.isEditable = true;
    }
  }

  goBack() {
    this.navCtrl.back();
  }

  getSexText(sex: boolean): string {
    return sex ? 'Masculino' : 'Femenino';
  }

  getSterilizedText(sterilized: boolean): string {
    return sterilized ? 'Sí' : 'No';
  }

  getDominantText(dominant: boolean): string {
    return dominant ? 'Sí' : 'No';
  }

  // Método para formatear los nombres de los enumerados
  formatEnumText(enumValue: number, enumType: any): string {
    const enumName = enumType[enumValue];
    return enumName.replace(/_/g, ' ');
  }

  // Métodos de conversión usando el método de formateo
  getBreedText(breed: number): string {
    return this.formatEnumText(breed, Breed);
  }

  getAgeCategoryText(ageCategory: number): string {
    return this.formatEnumText(ageCategory, AgeCategory);
  }

  getPersonalityText(personality: number): string {
    return this.formatEnumText(personality, Personaliity);
  }

  getSizeText(size: number): string {
    return this.formatEnumText(size, Size);
  }
}