import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from '../shared/services/utility.service';
import { NotificationService } from '../shared/services/notification.service';
import {
  ProductAttributeDto,
  ProductAttributesService,
} from '@proxy/erp/tandung/admin/catalog/product-attributes';
import { attributeTypeOptions } from '@proxy/erp/tandung/product-attributes';

@Component({
  selector: 'app-attribute-detail',
  templateUrl: './attribute-detail.component.html',
  styleUrls: ['./attribute-detail.component.scss'],
})
export class AttributeDetailComponent implements OnInit, OnDestroy {
  private ngUnSubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  btnDisabled = false;
  public form: FormGroup;
  //filter
  productCategories: any[] = [];
  selectedEntity = {} as ProductAttributeDto;

  dataTypes: any[] = [];
  constructor(
    private attributeService: ProductAttributesService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private utility: UtilityService,
    private notificationService: NotificationService,
  ) {}
  validationMessages = {
    code: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
    label: [
      { type: 'required', message: 'Bạn phải nhập nhãn hiển thị' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    dataType: [{ type: 'required', message: 'Bạn phải chọn kiểu dữ liệu' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
  };
  ngOnInit(): void {
    this.buildForm();
    this.loadAttributeType();
    this.initFormData();
    // load data to form
  }
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnSubscribe.next();
    this.ngUnSubscribe.complete();
  }

  initFormData() {
    // load edit to form
    if (this.utility.isEmpty(this.config.data?.id) == true) {
      this.toggleBlockUI(false);
    } else {
      this.loadFormDetail(this.config.data?.id);
    }
  }

  loadFormDetail(id: string) {
    this.toggleBlockUI(true);
    this.attributeService
      .get(id)
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: (response: ProductAttributeDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  saveChange() {
    // console.log(this.form.value);
    // console.log(this.config.data);
    this.toggleBlockUI(true);
    if (this.utility.isEmpty(this.config.data?.id) == true) {
      this.attributeService
        .create(this.form.value)
        .pipe(takeUntil(this.ngUnSubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);
            this.ref.close(this.form.value);
          },
          error: err => {
            this.notificationService.showError(err.error.error.message);
            this.toggleBlockUI(false);
          },
        });
    } else {
      this.attributeService
        .update(this.config.data?.id, this.form.value)
        .pipe(takeUntil(this.ngUnSubscribe))
        .subscribe({
          next: () => {
            this.ref.close(this.form.value);
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    }
  }

  loadAttributeType() {
    //  var productType=
    attributeTypeOptions.forEach(e => {
      this.dataTypes.push({
        value: e.value,
        label: e.key,
      });
    });
  }

  private buildForm() {
    this.form = this.fb.group({
      label: new FormControl(
        this.selectedEntity.label || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
      dataType: new FormControl(this.selectedEntity.dataType || null, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || true),
      isActive: new FormControl(this.selectedEntity.isActive || true),
      note: new FormControl(this.selectedEntity.note || null),
      isRequired:new FormControl(this.selectedEntity.isRequired || true, Validators.required),
      isUnique:new FormControl(this.selectedEntity.isUnique || false, Validators.required),
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
