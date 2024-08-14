import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { GlobalDogService } from '../utils/globalDog.service';

@Injectable({
  providedIn: 'root',
})
export class DogService {
  private apiUrl =
    'https://982bb0teq7.execute-api.eu-west-3.amazonaws.com/Prod';

  constructor(private http: HttpClient, private globalDogService: GlobalDogService) {}

  getDogsNoMatched(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/getUnmatchedDogs/${id}`);
  }
  getDogById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/getDog/${id}`);
  }

  getDogByUserId(userId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/dog/${userId}`).pipe(
      tap(dog => {
        if (dog) {
          this.globalDogService.setDog(dog);  // Guarda el perro en el estado global
        }
      })
    );
  }
  getDogsInMeeting(meetingId: string): Observable<any> {
    return this.http.get(
      `${this.apiUrl}/meeting/${meetingId}/getDogsInMeeting`
    );
  }

  updateDog(dogId: string, updatedDog: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateDog/${dogId}`, updatedDog);
  }
}
