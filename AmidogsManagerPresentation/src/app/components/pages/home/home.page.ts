import { Component } from '@angular/core';
import {DogService } from 'src/app/services/api/dog.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss']
})
export class HomePage {

  dogs: any[] = [];
  error: any = null;

  constructor(private dogService: DogService) {}

  ngOnInit(): void {
    this.dogService.getDogs().subscribe({
      next: (data) => {
        this.dogs = data;
        console.log(this.dogs);
      },
      error: (error) => {
        this.error = error;
      }
    });
  }

}
