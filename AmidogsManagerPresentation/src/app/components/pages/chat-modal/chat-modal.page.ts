import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/services/utils/socket.service';
import { NavController, ToastController } from '@ionic/angular';
import { MatchService } from 'src/app/services/api/match.service';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'app-chat-modal',
  templateUrl: './chat-modal.page.html',
  styleUrls: ['./chat-modal.page.scss'],
})
export class ChatModalPage implements OnInit, OnDestroy {
  @ViewChild('messageInput', { static: false }) messageInput?: ElementRef;

  dogId: string = ''
  dogName?: string;
  dogPhoto?: string;

  messages: { text: string; user: boolean }[] = [];
  newMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private socketService: SocketService,
    private navCtrl: NavController,
    private matchService: MatchService,
    private router: Router,
    private alertController: AlertController,
    private toastController: ToastController
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
    this.router.navigate(['/tabs/chats']);
  }

  onDogClick(): void {
    this.router.navigate(['/tabs/dog-profile', this.dogId]);
  }

  redirectToReportPage() {
    const reportId = this.dogId;
    this.router.navigate(['/report', reportId]);
  }

  async presentDeleteConfirm(dogId: string) {
    const alert = await this.alertController.create({
      header: 'Confirmar eliminación',
      message: '¿Estás seguro de que quieres eliminar este match?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          cssClass: 'secondary'
        },
        {
          text: 'Eliminar',
          handler: () => {
            this.deleteMatch(dogId);
          }
        }
      ]
    });

    await alert.present();
  }

  async deleteMatch(dogId: string) {
    this.matchService.getMatchByDogs('1', dogId).subscribe({
      next: async (match) => {
        if (match) {
          this.matchService.deleteMatch(match.Id).subscribe({
            next: async () => {
              await this.presentToast('Match eliminado exitosamente');
              this.goBack();
            },
            error: async (error) => {
              console.error('Error deleting match:', error);
              await this.presentToast('Error al eliminar el match');
            }
          });
        } else {
          console.error('No match found');
          await this.presentToast('No se encontró el match');
        }
      },
      error: async (error) => {
        console.error('Error fetching match:', error);
        await this.presentToast('Error al buscar el match');
      }
    });
  }

  async presentToast(message: string) {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000, // Duración del toast en milisegundos
    });
    await toast.present();
  }
}
