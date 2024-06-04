import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { chatsPage } from './chats.page';

const routes: Routes = [
  {
    path: '',
    component: chatsPage,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class chatsPageRoutingModule {}
