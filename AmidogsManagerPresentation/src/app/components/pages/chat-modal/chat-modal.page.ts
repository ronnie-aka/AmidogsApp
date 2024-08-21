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
export class ChatModalPage implements OnInit {
  @ViewChild('messageInput', { static: false }) messageInput?: ElementRef;

  dogId: string = ''
  dogName?: string;
  dogPhoto?: string;

  messages: { text: string; user: boolean }[] = [];
  newMessage: string = '';
  messageIndex: number = 0; 

  constructor(
    private route: ActivatedRoute,
    private matchService: MatchService,
    private router: Router,
    private alertController: AlertController,
    private toastController: ToastController
  ) {}

  ngOnInit() {
    this.dogId = this.route.snapshot.paramMap.get('dogId') ?? '';
    this.dogName = this.route.snapshot.paramMap.get('dogName') ?? '';
    this.dogPhoto = this.route.snapshot.paramMap.get('dogPhoto') ?? '';

    // Cargar mensajes desde el almacenamiento local
    const storedMessages = localStorage.getItem(this.dogId);
    if (storedMessages) {
      this.messages = JSON.parse(storedMessages);
      this.messageIndex = this.messages.length;
    }
  }

  sendMessage() {
    if (this.newMessage.trim()) {
        // Si no hay mensajes previos, el primer mensaje se muestra a la derecha
        const isUser = this.messageIndex === 0 || this.messageIndex % 2 === 0;
        
        // Añadir el mensaje al array con la propiedad user: true o false
        this.messages.push({ text: this.newMessage, user: isUser });
        
        // Incrementar el índice
        this.messageIndex++;
        
        // Guardar los mensajes en el almacenamiento local
        localStorage.setItem(this.dogId, JSON.stringify(this.messages));

        const matchUpdate = {
          dogId: this.dogId,
          chat: 'hola'
        };
  
        this.matchService.createOrUpdateMatch(matchUpdate).subscribe({
          next: (response) => {
            console.log('Match updated successfully:', response);
          },
          error: (error) => {
            console.error('Error updating match:', error);
          }
        });
        
        // Limpiar el campo de entrada
        this.newMessage = '';
        
        // Desplazarse hacia abajo para mostrar el último mensaje
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
