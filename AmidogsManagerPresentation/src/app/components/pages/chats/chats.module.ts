import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { chatsPage } from './chats.page';

import { chatsPageRoutingModule } from './chats-routing.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    chatsPageRoutingModule
  ],
  declarations: [chatsPage]
})
export class chatsPageModule {}
