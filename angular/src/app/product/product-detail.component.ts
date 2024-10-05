import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {
  ProductCategoriesService,
  ProductCategoryInListDto,
} from '@proxy/erp/tandung/admin/catalog/product-categories';
import { ProductDto, ProductsService } from '@proxy/erp/tandung/admin/catalog/products';
// import { ProductCategoryInListDto } from '@proxy/erp/tandung/admin/product-categories';
// import { ProductCategoriesService } from '@proxy/erp/tandung/admin/product-categories-app-service';
// import { ProductDto, ProductInListDto, ProductsService } from '@proxy/erp/tandung/admin/products';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent implements OnInit, OnDestroy {
  private ngUnSubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  btnDisabled = false;
  public form: FormGroup;
  //drop down
  //filter
  productCategories: any[] = [];
  selectedEntity = {} as ProductDto;
  categoryId: string = '';
  manufacturers: any[] = [];
  productType: any[] = [];
  constructor(
    private productService: ProductsService,
    private productCategoriesService: ProductCategoriesService,
    private fb: FormBuilder
  ) {}
  validationMessages = {
    code: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
    name: [
      { type: 'required', message: 'Bạn phải nhập tên' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    slug: [{ type: 'required', message: 'Bạn phải URL duy nhất' }],
    sku: [{ type: 'required', message: 'Bạn phải mã SKU sản phẩm' }],
    manufacturerID:[{ type: 'required', message: 'Bạn phải chọn nhà cung cấp' }],
    categoryId: [{ type: 'required', message: 'Bạn phải chọn danh mục' }],
    productType: [{ type: 'required', message: 'Bạn phải chọn loại sản phẩm' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
    sellPrice: [{ type: 'required', message: 'Bạn phải nhập giá bán' }]
  };
  ngOnInit(): void {
    this.buildForm();
  }
  ngOnDestroy(): void {}
  loadFormDetail(id: string) {
    this.toggleBlockUI(true);
    this.productService
      .get(id)
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: (response: ProductDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  saveChange() {}
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
  pageChanged(event: any): void {}

  private buildForm() {
    this.form = this.fb.group({
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
      slug: new FormControl(this.selectedEntity.slug || null, Validators.required),
      sku: new FormControl(this.selectedEntity.sku || null, Validators.required),
      manufacturerID: new FormControl(
        this.selectedEntity.manufacturerID || null,
        Validators.required
      ),
      categoryId: new FormControl(this.selectedEntity.categoryId || null, Validators.required),
      productType: new FormControl(this.selectedEntity.productType || null, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      sellPrice: new FormControl(this.selectedEntity.sellPrice || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || true),
      isActive: new FormControl(this.selectedEntity.isActive || true),
      seoMetaDescription: new FormControl(this.selectedEntity.seoMetaDescription || null),
      description: new FormControl(this.selectedEntity.description || null),
    });
  }
  private toggleBlockUI(enable: boolean) {
    if (enable == true) {
      this.blockedPanel = true;
      this.btnDisabled = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
        this.btnDisabled = false;
      }, 1000);
    }
  }
}
