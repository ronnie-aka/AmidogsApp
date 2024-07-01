import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./components/partials/tabs/tabs.module').then(m => m.TabsPageModule)
  },
  {
    path: 'report',
    loadChildren: () => import('./components/pages/report/report.module').then( m => m.ReportPageModule)
  },
  {
    path: 'meeting-detail',
    loadChildren: () => import('./components/pages/meeting-detail/meeting-detail.module').then( m => m.MeetingDetailPageModule)
  },
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
