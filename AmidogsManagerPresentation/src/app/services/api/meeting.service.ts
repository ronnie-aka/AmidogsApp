import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meeting } from 'src/app/shared/models/meeting';

@Injectable({
  providedIn: 'root',
})
export class MeetingService {
  private apiUrl =
    'https://982bb0teq7.execute-api.eu-west-3.amazonaws.com/Prod';

  constructor(private http: HttpClient) {}

  getMeetingsByDogOwner(dogId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/getMeetingsDogIsOwner/${dogId}`);
  }

  getMeetingsWithDog(userId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/getMeetingsWithDog/${userId}`);
  }

  getMeetingsWithoutDog(userId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/getMeetingsWithOutDog/${userId}`);
  }

  getMeetingById(meetingId: string) {
    return this.http.get(`${this.apiUrl}/getMeetingById/${meetingId}`);
  }

  updateMeeting(meetingId: string, updatedMeeting: any): Observable<any> {
    const url = `${this.apiUrl}/UpdateMeeting/${meetingId}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(url, updatedMeeting, { headers });
  }

  deleteMeeting(meetingId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/deleteMeeting/${meetingId}`);
  }

  createMeeting(newMeeting: any): Observable<any> {
    const url = `${this.apiUrl}/createMeeting`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(url, newMeeting, { headers });
  }
}
