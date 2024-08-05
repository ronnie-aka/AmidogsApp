import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DogService {
  private apiUrl = 'https://665f5ae81e9017dc16f3e2de.mockapi.io/amidogs';

  private newApiUrl = 'https://982bb0teq7.execute-api.eu-west-3.amazonaws.com/Prod/';

  constructor(private http: HttpClient) {}

  //MockUp apis
  getDogs(): Observable<any> {
    return this.http.get(`${this.apiUrl}/dogsNoMatch`);
  }

  //getDogById(id: string): Observable<any> {
    //return this.http.get(`${this.apiUrl}/dogsNoMatch/${id}`);
  //}

  getMeetings(): Observable<any> {
    return this.http.get(`${this.apiUrl}/meetings`);
  }

  getMeetingById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/meetings/${id}`);
  }

  createMeeting(meeting: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/meetings`, meeting);
  }

  deleteMeetingById(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/meetings/${id}`);
  }

  //new APIS

  getDogsNoMatched(id: string) : Observable<any> {
    return this.http.get(`${this.newApiUrl}/getUnmatchedDogs/${id}`);
  }
  getDogById(id: string) : Observable<any> {
    return this.http.get(`${this.newApiUrl}/getDog/${id}`);
  }
  getDogByUserId(id: string) : Observable<any> {
    return this.http.get(`${this.newApiUrl}/dog/${id}`);
  }
}
