// src/app/pages/chats/chats.page.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { MatchService } from 'src/app/services/api/match.service';
import { SocketService } from 'src/app/services/utils/socket.service';

@Component({
  selector: 'app-chats',
  templateUrl: 'chats.page.html',
  styleUrls: ['chats.page.scss'],
})
export class chatsPage {
  error: any = null;
  messages: { [key: string]: { text: string, user: boolean }[] } = {};
  dogsWithMsg: any[] = [];
  dogsWithoutMsg: any[] = [];
  myDogId = 1; // ID de tu perro

  constructor(
    private socketService: SocketService,
    private dogService: DogService,
    private matchService: MatchService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadMatchesWithMessages();
    this.loadMatchesWithoutMessages();

    // Configura el WebSocket para recibir mensajes
    this.socketService.onMessage((message) => {
      if (!this.messages[message.roomId]) {
        this.messages[message.roomId] = [];
      }
      this.messages[message.roomId].push({ text: message.text, user: false });
      localStorage.setItem(message.roomId, JSON.stringify(this.messages[message.roomId]));
    });
  }

  loadMatchesWithoutMessages(): void {
    this.matchService.getMatchesWithoutMsg(this.myDogId).subscribe({
      next: (data) => {
        data.forEach((match : any) => {
          const otherDogId = match.DogId1 === this.myDogId ? match.DogId2 : match.DogId1;
          this.dogService.getDogById(otherDogId).subscribe({
            next: (dog) => {
              this.dogsWithoutMsg.push(dog);
            },
            error: (error) => {
              console.error('Error al obtener el perro:', error);
            }
          });
        });
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

  loadMatchesWithMessages(): void {
    this.matchService.getMatchesWithMsg(this.myDogId).subscribe({
      next: (data) => {
        data.forEach((match : any) => {
          const otherDogId = match.DogId1 === this.myDogId ? match.DogId2 : match.DogId1;
          this.dogService.getDogById(otherDogId).subscribe({
            next: (dog) => {
              this.dogsWithMsg.push(dog);
            },
            error: (error) => {
              console.error('Error al obtener el perro:', error);
            }
          });
        });
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

  openChat(dog: any) {
    this.router.navigate([
      '/tabs/chat-modal',
      {
        dogId: dog.Id,
        dogName: dog.DogName,
        dogPhoto: dog.Photo,
      },
    ]);
  }

  getLastMessageText(dogId: string): string {
    const messages = this.messages[dogId];
    if (messages && messages.length > 0) {
      return messages[messages.length - 1].text;
    }
    return '';
  }
}
