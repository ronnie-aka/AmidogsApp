import { Component } from '@angular/core';
import { DogService } from 'src/app/services/api/dog.service';

@Component({
  selector: 'app-chats',
  templateUrl: 'chats.page.html',
  styleUrls: ['chats.page.scss']
})
export class chatsPage {

  dogs: any[] = [];
  error: any = null;

  constructor(private dogService: DogService) {}

  ngOnInit(): void {
    this.dogService.getDogs().subscribe({
      next: (data) => {
        this.dogs = data;
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

  onDogClick(dog: any): void {
    console.log('Macota clickeda:', dog.nombre);
  }

}
