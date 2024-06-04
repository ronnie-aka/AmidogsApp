import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { meetingsPage } from './meetings.page';

import { meetingsPageRoutingModule } from './meetings-routing.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    meetingsPageRoutingModule
  ],
  declarations: [meetingsPage]
})
export class meetingsPageModule {}
