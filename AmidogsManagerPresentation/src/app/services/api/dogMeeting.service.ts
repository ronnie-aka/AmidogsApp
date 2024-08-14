import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DogMeetingService {
  private apiUrl =
    'https://982bb0teq7.execute-api.eu-west-3.amazonaws.com/Prod';

  constructor(private http: HttpClient) {}

  AddDogToMeeting(
    dogId: number,
    meetingId: number,
    isOwner: boolean
  ): Observable<any> {
    const body = {
      dogId: dogId,
      meetingId: meetingId,
      isOwner: isOwner,
    };
    return this.http.post(`${this.apiUrl}/addDogToMeeting`, body);
  }

  removeDogFromMeeting(dogId: number, meetingId: number): Observable<any> {
    const body = {
      dogId: dogId,
      meetingId: meetingId,
    };
    return this.http.post(`${this.apiUrl}/removeDogFromMeeting`, body);
  }
}
