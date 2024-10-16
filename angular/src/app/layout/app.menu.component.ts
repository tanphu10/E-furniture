import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService) {}

  ngOnInit() {
    this.model = [
      {
        label: 'Home',
        items: [{ label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }],
      },
      {
        label: 'Product',
        items: [
          { label: 'Product List', icon: 'pi pi-fw pi-id-card', routerLink: ['/catalog/product'] },
          { label: 'Attribute List', icon: 'pi pi-fw pi-id-card', routerLink: ['/catalog/attribute'] },
        ],
      },
      {
        label: 'Systems',
        items: [
          { label: 'Role List', icon: 'pi pi-fw pi-id-card', routerLink: ['/system/role'] },
          { label: 'User List', icon: 'pi pi-fw pi-id-card', routerLink: ['/system/user'] },
        ],
      },
    ];
  }
}
