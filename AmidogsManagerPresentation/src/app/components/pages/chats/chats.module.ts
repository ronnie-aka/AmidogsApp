import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { chatsPage } from './chats.page';

import { chatsPageRoutingModule } from './chats-routing.module';
import { SliderComponent } from "../../partials/slider/slider.component";

@NgModule({
    declarations: [chatsPage],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        chatsPageRoutingModule,
        SliderComponent
    ]
})
export class chatsPageModule {}
