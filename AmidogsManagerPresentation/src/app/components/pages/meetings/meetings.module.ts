import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { meetingsPage } from './meetings.page';

import { meetingsPageRoutingModule } from './meetings-routing.module';
import { SliderComponent } from "../../partials/slider/slider.component";

@NgModule({
    declarations: [meetingsPage],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        meetingsPageRoutingModule,
        SliderComponent
    ]
})
export class meetingsPageModule {}
