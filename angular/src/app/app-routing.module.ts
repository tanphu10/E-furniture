import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './layout/app.layout.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
    component: AppLayoutComponent
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'product',
    loadChildren: () => import('./product/product.module').then(m => m.ProductModule),
    component: AppLayoutComponent
  },
  {
    path: 'attribute',
    loadChildren: () => import('./attribute/attribute.module').then(m => m.AttributeModule),
    component: AppLayoutComponent
  },
  {
    path: 'role',
    loadChildren: () => import('./role/role.module').then(m => m.RoleModule),
    component: AppLayoutComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
