import { createActionGroup, props } from '@ngrx/store';
import { RegisterRequestInterface } from '../types/registerRequest.interface';
import { GenericBackendResponseInterface } from 'src/app/shared/types/genericBackendResponse.interface';
import { BackendErrorsInterface } from 'src/app/shared/types/backendErrors.interface';

export const authActions = createActionGroup({
  source: 'auth',
  events: {
    Register: props<{ request: RegisterRequestInterface }>(),
    'Register success': props<{ request: GenericBackendResponseInterface }>(),
    'Register failed': props<{ errors: BackendErrorsInterface }>(),
  },
});
