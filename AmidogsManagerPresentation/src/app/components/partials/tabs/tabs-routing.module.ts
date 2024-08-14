import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
      {
        path: 'home',
        loadChildren: () => import('../../pages/home/home.module').then(m => m.HomePageModule)
      },
      {
        path: 'meetings',
        loadChildren: () => import('../../pages/meetings/meetings.module').then(m => m.meetingsPageModule)
      },
      {
        path: 'chats',
        loadChildren: () => import('../../pages/chats/chats.module').then(m => m.chatsPageModule)
      },
      {
        path: 'information',
        loadChildren: () => import('../../pages/information/information.module').then(m => m.InformationPageModule)
      },
      {
        path: 'dog-profile/:id',
        loadChildren: () => import('../../pages/profile/profile.module').then(m => m.ProfilePageModule)
      },
      {
        path: 'meeting-detail/:id',
        loadChildren: () => import('../../pages/meeting-detail/meeting-detail.module').then(m => m.MeetingDetailPageModule)
      },
      {
        path: 'chat-modal',
        loadChildren: () => import('../../pages/chat-modal/chat-modal.module').then( m => m.ChatModalPageModule)
      },
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      }
      
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
})
export class TabsPageRoutingModule {}
