import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { CategoriesComponent } from './Components/categories/categories.component';
import { RecordsComponent } from './Components/records/records.component';

const routes: Routes = [
  {path: 'home', component: AppComponent},
  {path: 'records', component: RecordsComponent},
  {path: 'categories', component: CategoriesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
