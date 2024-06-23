import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DogService {
  private apiUrl = 'https://665f5ae81e9017dc16f3e2de.mockapi.io/amidogs';

  constructor(private http: HttpClient) {}

  getDogs(): Observable<any> {
    return this.http.get(`${this.apiUrl}/dogsNoMatch`);
  }

  getDogById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/dogsNoMatch/${id}`);
  }

  
}