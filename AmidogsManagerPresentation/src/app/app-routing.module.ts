import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'tabs',
    loadChildren: () => import('./components/partials/tabs/tabs.module').then(m => m.TabsPageModule)
  },
  {
    path: 'report/:id',
    loadChildren: () => import('./components/pages/report/report.module').then( m => m.ReportPageModule)
  },
  {
    path: 'meeting-detail',
    loadChildren: () => import('./components/pages/meeting-detail/meeting-detail.module').then( m => m.MeetingDetailPageModule)
  },
  {
    path: '',
    loadChildren: () => import('./components/pages/login/login.module').then( m => m.LoginPageModule)
  },
  {
    path: 'information',
    loadChildren: () => import('./components/pages/information/information.module').then( m => m.InformationPageModule)
  },

];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
