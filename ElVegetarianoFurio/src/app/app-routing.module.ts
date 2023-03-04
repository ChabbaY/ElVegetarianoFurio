import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddDishComponent } from './dishes/add-dish/add-dish.component';
import { DishDetailComponent } from './dishes/dish-detail/dish-detail.component';

import { MenuComponent } from './menu/menu.component';

const routes: Routes = [
    { path: '', component: MenuComponent, pathMatch: 'full' },
    { path: 'menu', component: MenuComponent },
    { path: 'dish/add', component: AddDishComponent },
    { path: 'dish/:id', component: DishDetailComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
