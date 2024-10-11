import { Component, EventEmitter, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from '../shared/services/utility.service';
import { NotificationService } from '../shared/services/notification.service';
import { RoleDto, RolesService } from '@proxy/erp/tandung/admin/system/roles';
import { AuthService } from '@abp/ng.core';

@Component({
  selector: 'app-role-detail',
  templateUrl: './role-detail.component.html',
})
export class RoleDetailComponent implements OnInit, OnDestroy {
  private ngUnSubscribe = new Subject<void>();
  // Default
  public blockedPanel: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as RoleDto;

  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    private roleService: RolesService,
    private authService: AuthService,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private utility: UtilityService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.buildForm();
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
  public generateSlug() {
    var slug = this.utilService.MakeSeoTitle(this.form.get('name').value);
    this.form.controls['slug'].setValue(slug);
  }

  initFormData() {
    // load edit to form
    if (this.utility.isEmpty(this.config.data?.id) == false) {
      this.loadFormDetail(this.config.data?.id);
      (this.saveBtnName = 'Update'), (this.closeBtnName = 'Cancle');
      this.toggleBlockUI(false);
    } else {
      (this.saveBtnName = 'Add'), (this.closeBtnName = 'Close');
    }
  }
  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên nhóm' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    description: [{ type: 'required', message: 'Bạn phải mô tả' }],
  };

  loadFormDetail(id: string) {
    this.toggleBlockUI(true);
    this.roleService
      .get(id)
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: (response: RoleDto) => {
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
      this.roleService
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
      this.roleService
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

  private buildForm() {
    this.form = this.fb.group({
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(255),
          Validators.minLength(3),
        ])
      ),
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
