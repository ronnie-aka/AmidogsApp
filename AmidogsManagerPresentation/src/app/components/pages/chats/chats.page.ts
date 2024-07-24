// src/app/pages/chats/chats.page.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DogService } from 'src/app/services/api/dog.service';
import { SocketService } from 'src/app/services/utils/socket.service';

@Component({
  selector: 'app-chats',
  templateUrl: 'chats.page.html',
  styleUrls: ['chats.page.scss'],
})
export class chatsPage {
  dogs: any[] = [];
  error: any = null;
  messages: { [key: string]: { text: string, user: boolean }[] } = {};

  constructor(
    private dogService: DogService,
    private socketService: SocketService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.dogService.getDogs().subscribe({
      next: (data) => {
        this.dogs = data;
        this.dogs.forEach(dog => {
          const storedMessages = JSON.parse(localStorage.getItem(dog.id) || '[]');
          this.messages[dog.id] = storedMessages;
        });
      },
      error: (error) => {
        this.error = error;
      },
    });
  
    this.socketService.onMessage((message) => {
      if (!this.messages[message.roomId]) {
        this.messages[message.roomId] = [];
      }
      this.messages[message.roomId].push({ text: message.text, user: false });
      localStorage.setItem(message.roomId, JSON.stringify(this.messages[message.roomId]));
    });
  }

  onDogClick(dog: any): void {
    this.router.navigate(['/tabs/dog-profile', dog.id]);
  }

  openChat(dog: any) {
    this.router.navigate([
      '/tabs/chat-modal',
      {
        dogId: dog.id,
        dogName: dog.nombre,
        dogPhoto: dog.foto,
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
