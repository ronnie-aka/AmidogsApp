import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { MeetingDetailPageRoutingModule } from './meeting-detail-routing.module';

import { MeetingDetailPage } from './meeting-detail.page';
import { SliderComponent } from '../../partials/slider/slider.component';
import { DatePickerModalComponent } from '../../partials/date-picker-modal/date-picker-modal.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MeetingDetailPageRoutingModule,
    SliderComponent,
    RouterModule.forChild([{ path: '', component: MeetingDetailPage }])
  ],
  declarations: [MeetingDetailPage],
})
export class MeetingDetailPageModule {}
