import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl =
    'https://982bb0teq7.execute-api.eu-west-3.amazonaws.com/Prod';

  constructor(private http: HttpClient) {}

  getUserByDogId(dogId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/getUserByDogId/${dogId}`);
  }

  updateComplaintNumber(userId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/updateComplaint/${userId}`);
  }

}
