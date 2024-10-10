import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../shared/services/notification.service';
import {
  ProductAttributeDto,
  ProductAttributeInListDto,
  ProductAttributesService,
} from '@proxy/erp/tandung/admin/catalog/product-attributes';

import { AttributeType } from '@proxy/erp/tandung/product-attributes';
import { ConfirmationService } from 'primeng/api';
import { AttributeDetailComponent } from './attribute-detail.component';

@Component({
  selector: 'app-attribute',
  templateUrl: './attribute.component.html',
  styleUrls: ['./attribute.component.scss'],
})
export class AttributeComponent implements OnInit, OnDestroy {
  private ngUnSubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: ProductAttributeInListDto[] = [];
  public selectedItems: ProductAttributeInListDto[] = [];
  //paging variable
  public maxResultCount: number = 10;
  public skipCount: number = 0;
  public totalCount: number;
  //filter
  AttributeCategories: any = [];
  keyword: string = '';
  categoryId: string = '';
  constructor(
    private attributeService: ProductAttributesService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit(): void {
    // this.loadAttributeCategory();
    this.loadData();
  }
  ngOnDestroy(): void {}
  loadData() {
    this.toggleBlockUI(true);
    this.attributeService
      .getListFilter({
        keyword: this.keyword,
        // categoryId: this.categoryId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: (response: PagedResultDto<ProductAttributeInListDto>) => {
          (this.items = response.items), (this.totalCount = response.totalCount);
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }
  showAddModel() {
    const ref = this.dialogService.open(AttributeDetailComponent, {
      header: 'Add new Attribute',
      width: '70%',
    });
    ref.onClose.subscribe((data: ProductAttributeDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Add Attribute Success');
        this.selectedItems = [];
      }
    });
  }
  showEditModel() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('You choose record');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(AttributeDetailComponent, {
      data: {
        id: id,
      },
      header: 'Update Attribute',
      width: '70%',
    });
    ref.onClose.subscribe((data: ProductAttributeDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess('Update Attribute Success');
      }
    });
  }
  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('you must choose record');
    }
    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message: 'Do you want to Delete this record ?',
      accept: () => {
        this.deleteItemConfirm(ids);
      },
    });
  }
  deleteItemConfirm(ids: string[]) {
    this.toggleBlockUI(true);
    this.attributeService
      .deleteMultiple(ids)
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess('Delete Success');
          this.loadData();
          this.selectedItems = [];
          this.toggleBlockUI(false);
        },
        error:()=>{
          this.toggleBlockUI(false)
        }
      });
  }
  getAttributeTypeName(value: number) {
    return AttributeType[value];
  }
  private toggleBlockUI(enable: boolean) {
    if (enable == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }
}
