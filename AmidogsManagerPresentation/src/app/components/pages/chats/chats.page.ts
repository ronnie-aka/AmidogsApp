// src/app/pages/chats/chats.page.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { MatchService } from 'src/app/services/api/match.service';
import { GlobalDogService } from 'src/app/services/utils/globalDog.service';
import { SocketService } from 'src/app/services/utils/socket.service';

@Component({
  selector: 'app-chats',
  templateUrl: 'chats.page.html',
  styleUrls: ['chats.page.scss'],
})
export class chatsPage {
  error: any = null;
  messages: { [key: string]: { text: string; user: boolean }[] } = {};
  dogsWithMsg: any[] = [];
  dogsWithoutMsg: any[] = [];
  myDogId: any; // ID de tu perro
  hasInitialized: boolean = false; // Variable de control

  constructor(
    private socketService: SocketService,
    private dogService: DogService,
    private matchService: MatchService,
    private router: Router,
    private globalDogService: GlobalDogService
  ) {}

  ngOnInit(): void {
    this.ObtenerPerro();

    // Configura el WebSocket para recibir mensajes
    this.socketService.onMessage((message) => {
      if (!this.messages[message.roomId]) {
        this.messages[message.roomId] = [];
      }
      this.messages[message.roomId].push({ text: message.text, user: false });
      localStorage.setItem(
        message.roomId,
        JSON.stringify(this.messages[message.roomId])
      );
    });
  }

  ObtenerPerro(): void {
    this.globalDogService.dog$.subscribe(dog => {
      if (dog) {
        this.myDogId = dog.Id.toString();
        console.log('Perro actual:', this.myDogId);
        this.hasInitialized = true;
        this.loadMatchesWithMessages();
        this.loadMatchesWithoutMessages();

      } else {
        console.log('No hay perro en el estado global.');
      }
    });
  }

  ionViewWillEnter(): void {
    if (!this.hasInitialized) {
      this.dogsWithMsg = [];
      this.dogsWithoutMsg = [];
      this.ObtenerPerro();
    }
  }

  ionViewWillLeave(): void {
    this.hasInitialized = false;
  }

  loadMatchesWithoutMessages(): void {
    this.matchService.getMatchesWithoutMsg(this.myDogId).subscribe({
      next: (data) => {
        data.forEach((match: any) => {
          const otherDogId =
            match.DogId1 === parseInt(this.myDogId) ? match.DogId2 : match.DogId1;
          this.dogService.getDogById(otherDogId).subscribe({
            next: (dog) => {
              console.log("perro sin mensajes", dog);
              this.dogsWithoutMsg.push(dog);
            },
            error: (error) => {
              console.error('Error al obtener el perro:', error);
            },
          });
        });
      },
      error: (error) => {
        this.error = error;
      },
    });
  }

  loadMatchesWithMessages(): void {
    this.matchService.getMatchesWithMsg(this.myDogId).subscribe({
      next: (data) => {
        data.forEach((match: any) => {
          const otherDogId =
            match.DogId1 === parseInt(this.myDogId) ? match.DogId2 : match.DogId1;
          this.dogService.getDogById(otherDogId).subscribe({
            next: (dog) => {
              console.log("perro sin mensajes", dog);
              this.dogsWithMsg.push(dog);
            },
            error: (error) => {
              console.error('Error al obtener el perro:', error);
            },
          });
        });
      },
      error: (error) => {
        this.error = error;
      },
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