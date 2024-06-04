import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { meetingsPage } from './meetings.page';

const routes: Routes = [
  {
    path: '',
    component: meetingsPage,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class meetingsPageRoutingModule {}
