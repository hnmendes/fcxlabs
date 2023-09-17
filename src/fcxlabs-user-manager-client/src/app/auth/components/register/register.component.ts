import { Component } from '@angular/core';
import {
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Store } from '@ngrx/store';
import { NgxMaskDirective } from 'ngx-mask';
import { authActions } from '../../store/actions';
import { RegisterRequestInterface } from '../../types/registerRequest.interface';
import { RouterLink } from '@angular/router';
import {
  selectIsSubmitted,
  selectValidationErrors,
} from '../../store/reducers';
import { CommonModule } from '@angular/common';
import { combineLatest } from 'rxjs';
import { BackendErrorMessages } from 'src/app/shared/components/backendErrorMessages.component';

@Component({
  selector: 'fcxlabs-register',
  templateUrl: './register.component.html',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    NgxMaskDirective,
    RouterLink,
    CommonModule,
    BackendErrorMessages,
  ],
})
export class RegisterComponent {
  form = this.fb.nonNullable.group({
    username: ['', [Validators.minLength(15), Validators.required]],
    name: ['', [Validators.required]],
    email: ['', [Validators.email, Validators.required]],
    password: ['', [Validators.required]],
    cpf: ['', [Validators.required]],
    motherName: ['', [Validators.required]],
    mobilePhone: ['', [Validators.required]],
    birthDate: ['', [Validators.required]],
  });

  data$ = combineLatest({
    isSubmitted: this.store.select(selectIsSubmitted),
    backendErrors: this.store.select(selectValidationErrors),
  });

  constructor(private fb: FormBuilder, private store: Store) {}

  public onSubmit(): void {
    var dateForm = this.form.get('birthDate')?.value;
    let date = null;

    if (dateForm) {
      const [day, month, year] = dateForm.split('/');
      date = new Date(+year, +month - 1, +day).toJSON();
    }

    const request: RegisterRequestInterface = {
      name: this.form.get('name')!.value,
      username: this.form.get('username')!.value,
      email: this.form.get('email')!.value,
      password: this.form.get('password')!.value,
      cpf: this.form.get('cpf')!.value,
      birthDate: date,
      mobilePhone: this.form.get('mobilePhone')!.value,
      motherName: this.form.get('motherName')!.value,
    };

    this.store.dispatch(authActions.register({ request }));
  }
}
