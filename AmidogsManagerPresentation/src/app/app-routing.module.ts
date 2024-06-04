import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./components/partials/tabs/tabs.module').then(m => m.TabsPageModule)
  },
  {
    path: 'cards',
    loadChildren: () => import('./components/partials/cards/cards.module').then( m => m.CardsPageModule)
  },
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
