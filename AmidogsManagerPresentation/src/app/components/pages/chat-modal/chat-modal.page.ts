// src/app/pages/chat-modal/chat-modal.page.ts

import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/services/utils/socket.service';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-chat-modal',
  templateUrl: './chat-modal.page.html',
  styleUrls: ['./chat-modal.page.scss'],
})
export class ChatModalPage implements OnInit, OnDestroy {
  @ViewChild('messageInput', { static: false }) messageInput?: ElementRef;

  dogId?: string;
  dogName?: string;
  dogPhoto?: string;

  messages: { text: string, user: boolean }[] = [];
  newMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private socketService: SocketService,
    private navCtrl: NavController,
    private router: Router
  ) {}

  ngOnInit() {
    this.dogId = this.route.snapshot.paramMap.get('dogId') ?? '';
    this.dogName = this.route.snapshot.paramMap.get('dogName') ?? '';
    this.dogPhoto = this.route.snapshot.paramMap.get('dogPhoto') ?? '';

    this.socketService.joinRoom(this.dogId!);

    this.socketService.onMessage((message) => {
      if (message.roomId === this.dogId) {
        this.messages.push({ text: message.text, user: message.user });
      }
    });
  }

  sendMessage() {
    if (this.newMessage.trim()) {
      this.socketService.sendMessage(this.newMessage, this.dogId!);
      this.newMessage = '';
      this.scrollToBottom();
    }
  }

  handleKeyPress() {
    this.sendMessage();
  }

  scrollToBottom() {
    setTimeout(() => {
      this.messageInput?.nativeElement.scrollIntoView({ behavior: 'smooth' });
    }, 100);
  }

  ngOnDestroy() {
    this.socketService.leaveRoom(this.dogId!);
  }

  isUserMessage(index: number): boolean {
    return this.messages[index].user;
  }

  goBack() {
    this.navCtrl.back();
  }

  onDogClick(): void {
    this.router.navigate(['/tabs/dog-profile', this.dogId]);
  }

  redirectToReportPage() {
    const reportId = this.dogId;
    this.router.navigate(['/report', reportId]);
  }
}
