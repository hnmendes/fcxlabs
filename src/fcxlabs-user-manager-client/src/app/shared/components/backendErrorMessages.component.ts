import { Component, Input, OnInit } from '@angular/core';
import { BackendErrorsInterface } from '../types/backendErrors.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'fcxlabs-backend-error-messages',
  templateUrl: './backendErrorMessages.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class BackendErrorMessages implements OnInit {
  @Input() backendErrors: BackendErrorsInterface = {};

  errorMessages: string[] = [];

  ngOnInit(): void {
    this.errorMessages = Object.keys(this.backendErrors).flatMap(
      (name: string) => {
        return this.backendErrors[name].map((message: string) => `${message}`);
      }
    );
  }
}
