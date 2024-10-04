import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductCategoryInListDto } from '@proxy/erp/tandung/admin/product-categories';
import { ProductCategoriesService } from '@proxy/erp/tandung/admin/product-categories-app-service';
import { ProductInListDto, ProductsService } from '@proxy/erp/tandung/admin/products';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit, OnDestroy {
  private ngUnSubscribe= new Subject<void>();
  blockedPanel: boolean = false;
  items:ProductInListDto[] = [];
  //paging variable
  public maxResultCount: number = 10;
  public skipCount: number = 0;
  public totalCount: number;
  //filter
  productCategories:any=[];
  keyword:string='';
  categoryId:string='';
  constructor(private productService: ProductsService,
    private productCategoriesService:ProductCategoriesService
  ) {}
  ngOnInit(): void {
    this.loadProductCategory();
    this.loadData();
  }
  ngOnDestroy(): void {
    
  }
  loadData() {
    this.productService
      .getListFilter({
        keyword: this.keyword,
        categoryId:this.categoryId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      }).pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({ 
        next:(response: PagedResultDto<ProductInListDto>) =>{
        this.items=response.items,
        this.totalCount=response.totalCount
        },
        error:()=>{
      },
    });
  }
  loadProductCategory(){
    this.productCategoriesService.getListAll().subscribe(
      (response:ProductCategoryInListDto[])=>{
    
        response.forEach(element=>{
          this.productCategories.push({
            value:element.id,
            name:element.name
        })
     
      })
    })

  }
  pageChanged(event:any):void{
    this.skipCount=(event.page-1)*this.maxResultCount;
    this.maxResultCount=event.rows;
    this.loadData()
  }
}
