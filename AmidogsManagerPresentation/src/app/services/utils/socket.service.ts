// src/app/services/socket.service.ts
import { Injectable } from '@angular/core';
import { io, Socket } from 'socket.io-client';

@Injectable({
  providedIn: 'root'
})
export class SocketService {
  private socket: Socket;

  constructor() {
    this.socket = io('https://192.168.106.162:8443', {
      transports: ['websocket']
    });
  }

  sendMessage(message: string, roomId: string) {
    this.socket.emit('message', { text: message, roomId, user: true });
  }

  onMessage(callback: (message: any) => void) {
    this.socket.on('message', callback);
  }

  joinRoom(roomId: string) {
    this.socket.emit('join', roomId);
  }

  leaveRoom(roomId: string) {
    this.socket.emit('leave', roomId);
  }
}
