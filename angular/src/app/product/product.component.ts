import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';

import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ProductDetailComponent } from './product-detail.component';
import { NotificationService } from '../shared/services/notification.service';
import { ProductDto, ProductInListDto, ProductsService } from '@proxy/erp/tandung/admin/catalog/products';
import { ProductCategoriesService, ProductCategoryInListDto } from '@proxy/erp/tandung/admin/catalog/product-categories';

@Component({
  selector: 'app-home',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit, OnDestroy {
  private ngUnSubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: ProductInListDto[] = [];
  //paging variable
  public maxResultCount: number = 10;
  public skipCount: number = 0;
  public totalCount: number;
  //filter
  productCategories: any = [];
  keyword: string = '';
  categoryId: string = '';
  constructor(
    private productService: ProductsService,
    private productCategoriesService: ProductCategoriesService,
    private dialogService: DialogService,
    private notificationService: NotificationService
  ) {}
  ngOnInit(): void {
    this.loadProductCategory();
    this.loadData();
  }
  ngOnDestroy(): void {}
  loadData() {
    this.toggleBlockUI(true);
    this.productService
      .getListFilter({
        keyword: this.keyword,
        categoryId: this.categoryId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: (response: PagedResultDto<ProductInListDto>) => {
          (this.items = response.items), (this.totalCount = response.totalCount);
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  loadProductCategory() {
    this.productCategoriesService.getListAll().subscribe((response: ProductCategoryInListDto[]) => {
      response.forEach(element => {
        this.productCategories.push({
          value: element.id,
          name: element.name,
        });
      });
    });
  }
  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }
  showAddModel() {
    const ref = this.dialogService.open(ProductDetailComponent, {
      header: 'Add new product',
      width: '70%',
    });
    ref.onClose.subscribe((data: ProductDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Add Product Success');
      }
    });
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
