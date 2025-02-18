import { Component, Inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { user } from '../../models/user.model';
import { UserComponent } from '../user/user.component';

@Component({
  selector: 'app-add-user-dialog',
  templateUrl: './add-user-dialog.component.html',
  styleUrl: './add-user-dialog.component.css',
})
export class AddUserDialogComponent {
  userForm: FormGroup;
  user: user;
  localUser: user; // Local copy for editing

  constructor(
    public dialogRef: MatDialogRef<AddUserDialogComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: user
  ) {
    this.user = data || {};
    this.localUser = { ...this.user };

    // Initialize the form with validators
    this.userForm = this.fb.group({
      id: [],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      status: [0],
    });
  }
  ngOnInit(): void {
    this.loadUserData();
  }
  loadUserData(): void {
    if (this.localUser) {
      this.userForm.patchValue({
        id: this.localUser.id || '',
        name: this.localUser.name || '',
        email: this.localUser.email || '',
        status: this.localUser.status || 0,
      });
    }
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  onStatusChange(checked: boolean): void {
    const statusValue = checked ? 1 : 0;
    console.log('Selected status:', statusValue);

    const statusControl = this.userForm.get('status');
    if (statusControl) {
      statusControl.setValue(statusValue);
    } else {
      console.error('Status control is null');
    }
  }
  onSaveClick(): void {
    if (this.userForm.valid) {
      console.log('Form Submitted', this.userForm.value);
      this.userForm.value.id = this.localUser.id;
      this.dialogRef.close(this.userForm.value);
    } else {
      console.log('Form is invalid');
    }
  }
}
