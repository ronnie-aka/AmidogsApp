import { Component, OnInit } from '@angular/core';
import { DogService } from 'src/app/services/api/dog.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {

  dog: any;
  error: any = null;

  constructor(private dogService:DogService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const dogId = this.route.snapshot.paramMap.get('id');

    if (dogId) {
      this.dogService.getDogById(dogId).subscribe({
        next: (data) => {
          this.dog = data;
          console.log(this.dog);
        },
        error: (error) => {
          this.error = error;
        }
      });
    } else {
      this.error = 'No se proporcionó un ID de perro válido.';
    }
  }




}
