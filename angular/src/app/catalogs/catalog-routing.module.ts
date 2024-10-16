import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './product/product.component';
import { AttributeComponent } from './attribute/attribute.component';
import { permissionGuard } from '@abp/ng.core/public-api';

const routes: Routes = [
  { path: 'product', component: ProductComponent ,
    canActivate: [permissionGuard],
    data: { requiredPolicy: 'AdminCatalog.Product' },
  },
  { path: 'attribute', component: AttributeComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
