import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MatchService {
  private apiUrl =
    'https://982bb0teq7.execute-api.eu-west-3.amazonaws.com/Prod';

  constructor(private http: HttpClient) {}

  deleteMatch(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/deleteMatch/${id}`);
  }

  createOrUpdateMatch(match: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/createMatch`, match);
  }

  getMatchesWithoutMsg(dogId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/getMatchesNotMessages/${dogId}`);
  }

  getMatchesWithMsg(dogId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/getMatchesWithMessages/${dogId}`);
  }

  getMatchByDogs(dog1Id: string, dog2Id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/getMatchByDogs/${dog1Id}/${dog2Id}`);
  }
}
