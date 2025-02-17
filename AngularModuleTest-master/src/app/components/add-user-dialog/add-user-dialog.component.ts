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
  user:user;
  localUser: user; // Local copy for editing

  constructor(
    public dialogRef: MatDialogRef<AddUserDialogComponent>,
    private fb: FormBuilder,@Inject(MAT_DIALOG_DATA) public data: user
  ) {
    this.user = data || {}; // Initialize user data
    this.localUser = { ...this.user }; // Create a local copy

    // Initialize the form with validators
    this.userForm = this.fb.group({
      id:[],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      status: [0],
    });
  }
  ngOnInit(): void {
    this.loadUserData(); // Load user data when the component initializes

  }
  loadUserData(): void {
    // Simulate fetching user data (e.g., from a service)
    if(this.localUser){
      this.userForm.patchValue({
        id:this.localUser.id || '',
        name: this.localUser.name || '', // Set name if available
        email: this.localUser.email || '', // Set email if available
        status: this.localUser.status || 0, // Set status to 1 or 0 based on user data
      });
    }

  }
  onNoClick(): void {
    this.dialogRef.close();
  }
   // Method to handle status toggle change
   onStatusChange(checked:boolean) : void {
    const statusValue = checked ? 1 : 0; // Convert boolean to 1 or 0
  console.log('Selected status:', statusValue);
// Check if the form control exists before setting the value
const statusControl = this.userForm.get('status');
if (statusControl) {
  statusControl.setValue(statusValue); // Update the form control value
} else {
  console.error('Status control is null');
}

}
onSaveClick(): void {
  if (this.userForm.valid) {

    console.log('Form Submitted', this.userForm.value);
    this.userForm.value.id=this.localUser.id;
    this.dialogRef.close(this.userForm.value);
} else {
    console.log('Form is invalid');
}
}

}
