import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import {  RoleRoutingModule } from './role-routing.module';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { ESharedModule } from '../shared/modules/e-shared.module';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { RoleDetailComponent } from './role-detail.component';
import { RoleComponent } from './role.component';


@NgModule({
  declarations: [RoleComponent, RoleDetailComponent],
  imports: [
    SharedModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    ESharedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    RoleRoutingModule,
  ],
  // entry:[
  //   ProductDetailComponent
  // ]
})
export class RoleModule {}
