import {
  AfterViewInit,
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { user } from '../../models/user.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserService } from '../../services/users/user.service';
import { SharedMethodsService } from '../../services/shared-services/shared-methods.service';
import { UserStatus } from '../../models/UserStatusEnum.model';
import { MatDialog } from '@angular/material/dialog';
import { AddUserDialogComponent } from '../add-user-dialog/add-user-dialog.component';
import { DeleteUserComponent } from '../delete-user/delete-user.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
})
export class UserComponent implements OnInit, AfterViewInit, OnDestroy {
  users: user[] = [];
  allColumns: string[] = ['name', 'email', 'status', 'action'];
  displayedColumns: string[] = ['name', 'email', 'status', 'action'];
  dataSource: MatTableDataSource<user> = new MatTableDataSource<user>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;
  filterString = '';
  paginatorBackgroundColor = '#797d6c26';
  paginatorTextColor = '#3f51b5';
  showColumnList: boolean = false; // Control visibility of the checkbox list

  constructor(
    private userService: UserService,
    private sharedMethodService: SharedMethodsService,
    public dialog: MatDialog
  ) {}
  ngOnDestroy(): void {}
  ngAfterViewInit() {
    // this.dataSource.paginator = this.matPaginator;
    // this.dataSource.sort = this.matSort;
  }
  loadUsers() {
    this.userService.getUsers().subscribe({
      next: (successResponse) => {
        this.users = successResponse;
        console.log('list user', this.users);
        this.dataSource = new MatTableDataSource<user>(this.users);
        this.dataSource.paginator = this.matPaginator;
        this.dataSource.sort = this.matSort;
      },
      error: (errorResponse) => {
        console.log('error:', errorResponse);
      },
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }
  // search filter input
  filterUsers() {
    this.dataSource.filter = this.filterString.trim().toLowerCase();
  }
  selectedRow: any;

  selectRow(row: any) {
    this.selectedRow = row;
  }
  toggleColumn(event: any, column: string): void {
    var isChecked = (event.target as HTMLInputElement).checked;
    if (isChecked) {
      this.displayedColumns.push(column);
    } else {
      this.displayedColumns = this.displayedColumns.filter((c) => c !== column);
    }
    this.sortDisplayedColumns();
  }
  // whecn check or uncheck item from checkbox list ensure the original order of column in displayed columns array
  sortDisplayedColumns(): void {
    this.displayedColumns.sort(
      (a, b) => this.allColumns.indexOf(a) - this.allColumns.indexOf(b)
    );
  }
  // Method to get the status string: from enumerate
  getStatusString(status: UserStatus): string {
    switch (status) {
      case UserStatus.Active:
        return 'Active';
      case UserStatus.Inactive:
        return 'Inactive';
      default:
        return 'Unknown';
    }
  }
  openDialog(user?: user): void {
    // initialoize my dilaog here
    const bool = true;

    var dialogRef = this.dialog.open(AddUserDialogComponent, {
      //  width: '1000px',
      // height:'500px',
      position: { top: '5%' },
      // panelClass: 'custom-dialog-container'
      panelClass: 'custom-dialog-container',
      data: user, ///pass user data to the dialog
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');

      if (result) {
        if (user) {
          this.updateUser(result);
          console.log('result-updating', result);
        } else {
          this.addUser(result);
        }
      }
    });
  }
  addUser(user: user): void {
    this.userService.AddUser(user).subscribe({
      next: (successResponse) => {
        this.sharedMethodService.showSnackbar(
          'User Added Succefully',
          'Success',
          'succ-snackbar'
        );
        this.users = successResponse;
        console.log('list user', this.users);
        this.loadUsers();
      },
      error: (errorResponse) => {
        console.log(errorResponse);
        this.sharedMethodService.showSnackbar(
          'Error in Added User',
          'Error',
          ''
        );
      },
    });
  }
  editUser(user: user): any {
    this.openDialog(user);
  }
  updateUser(user: any): void {
    console.log('updating user into updatemethod', user);
    this.userService.UpdateUser(user).subscribe({
      next: (successResponse) => {
        this.sharedMethodService.showSnackbar(
          'User Updated Successfully',
          'Success',
          'succ-snackbar'
        );
        this.loadUsers();
        // console.log('updated user',user)
      },
      error: (errorResponse) => {
        console.log('Error updating user:', errorResponse);
        this.sharedMethodService.showSnackbar(
          'Error in Updating User',
          'Error',
          ''
        );
      },
    });
  }
  openConfirmDialog(id: number): void {
    var dialogRef = this.dialog.open(DeleteUserComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result === true) {
        this.deleteElement(id);
      }
    });
  }
  deleteElement(id: number): void {
    this.userService.DeleteUser(id).subscribe({
      next: (successResponse) => {
        this.sharedMethodService.showSnackbar(
          'User deleted Successfully',
          'Success',
          'succ-snackbar'
        );
        this.loadUsers();
      },
      error: (errorResponse) => {
        console.log('Error updating user:', errorResponse);
        this.sharedMethodService.showSnackbar(
          'Error in Deleting User',
          'Error',
          ''
        );
      },
    });
  }
  // Toggle the visibility of the column list
  toggleColumnList() {
    this.showColumnList = !this.showColumnList;
  }
}
