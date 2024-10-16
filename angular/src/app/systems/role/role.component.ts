import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { RoleDetailComponent } from './role-detail.component';
import { RoleDto, RoleInListDto, RolesService } from '@proxy/erp/tandung/admin/system/roles';
import { MessageConstants } from '../../shared/constants/messages.const';
import { PermissionGrantComponent } from './permission-grant.component';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss'],
})
export class RoleComponent implements OnInit, OnDestroy {
  private ngUnSubscribe = new Subject<void>();
  public blockedPanel: boolean = false;

  public items: RoleDto[];
  public selectedItems: RoleDto[] = [];
  public keyword: string = '';

  public maxResultCount: number = 10;
  public skipCount: number = 0;
  public totalCount: number;
  //filter

  constructor(
    private roleService: RolesService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit(): void {
    // this.loadAttributeCategory();
    this.loadData();
  }
  ngOnDestroy(): void {
    this.ngUnSubscribe.next();
    this.ngUnSubscribe.complete();
  }
  loadData(selectionId = null) {
    this.toggleBlockUI(true);

    this.roleService
      .getListFilter({
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
        keyword: this.keyword,
      })
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: (response: PagedResultDto<RoleInListDto>) => {
          this.items = response.items;
          (this.totalCount = response.totalCount), this.toggleBlockUI(false);
          if (selectionId != null && this.items.length > 0) {
            this.selectedItems = this.items.filter(x => x.id == selectionId);
          }
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
  showAddModal() {
    const ref = this.dialogService.open(RoleDetailComponent, {
      header: 'Add new Role',
      width: '70%',
    });
    ref.onClose.subscribe((data: RoleDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Add Role Success');
        this.selectedItems = [];
      }
    });
  }
  showEditModal() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('You choose record');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(RoleDetailComponent, {
      data: {
        id: id,
      },
      header: 'Update Role',
      width: '70%',
    });
    ref.onClose.subscribe((data: RoleDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
      }
    });
  }
  showPermissionModal(id: string, name: string) {
    const ref = this.dialogService.open(PermissionGrantComponent, {
      data: {
        id: id,
        name: name,
      },
      header: name,
      width: '70%',
    });
    ref.onClose.subscribe((data: RoleDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
      }
    });
  }
  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
    }
    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.deleteItemConfirm(ids);
      },
    });
  }
  deleteItemConfirm(ids: string[]) {
    this.toggleBlockUI(true);
    this.roleService
      .deleteMultiple(ids)
      .pipe(takeUntil(this.ngUnSubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
          this.loadData();
          this.selectedItems = [];
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
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
