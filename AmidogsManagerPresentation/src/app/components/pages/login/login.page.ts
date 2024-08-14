import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { UserService } from 'src/app/services/api/user.service';
import { GlobalDogService } from 'src/app/services/utils/globalDog.service';
// Asegúrate de que la ruta sea correcta

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  email: string = 'ronald.hernandez@a.maux.org';  // Vinculado a través de [(ngModel)]

  constructor(
    private router: Router,
    private userService: UserService,
    private dogService: DogService,
    private globalDogService: GlobalDogService
  ) {}

  ngOnInit() {}

  login() {
    if (this.email) {  // Verifica que el correo electrónico no esté vacío
      this.userService.getUserByEmail(this.email).subscribe(
        (user) => {
          if (user) {
            this.dogService.getDogByUserId(user.Id).subscribe(
              (dog) => {
                this.globalDogService.setDog(dog); // Guarda el perro en el estado global
                this.router.navigate(['/tabs/home']);  // Redirige después de obtener el perro
              },
              (error) => {
                console.error('Error al obtener el perro:', error);
              }
            );
          } else {
            console.log('Usuario no encontrado');
          }
        },
        (error) => {
          console.error('Error al recuperar el usuario:', error);
        }
      );
    } else {
      console.log('Por favor, ingrese un correo electrónico');
    }
  }
}
