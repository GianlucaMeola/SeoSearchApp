import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeSearchComponent } from './Components/home-search/home-search.component';
import { HistoryComponent } from './Components/history/history.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: '', component: HomeSearchComponent },
      { path: 'home-search', component: HomeSearchComponent },
      { path: 'history', component: HistoryComponent }
    ]),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule { }
