import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleComponent } from './role/role.component';
import { UserComponent } from './user/user.component';
import { permissionGuard } from '@abp/ng.core/public-api';
// import { RoleComponent } from './role.component';

const routes: Routes = [
  {
    path: 'role',
    component: RoleComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: 'AbpIdentity.Roles' },
  },
  {
    path: 'user',
    component: UserComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: 'AbpIdentity.Users' },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RoleRoutingModule {}
