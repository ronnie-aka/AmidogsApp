import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GlobalDogService {
  private dogSubject = new BehaviorSubject<any>(null);  // Comportamiento de observable inicializado con null
  dog$ = this.dogSubject.asObservable();  // Observable que las componentes pueden suscribirse para recibir cambios

  constructor() {}

  setDog(dog: any) {
    this.dogSubject.next(dog);  // Actualiza el perro global
  }

  getDog() {
    return this.dogSubject.getValue();  // Obtiene el perro actual
  }
}
