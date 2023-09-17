import { BackendErrorsInterface } from 'src/app/shared/types/backendErrors.interface';
import { CurrentUserInterface } from 'src/app/shared/types/currentUser.interface';

export interface AuthStateInterface {
  isSubmitted: boolean;
  currentUser: CurrentUserInterface | null | undefined; //null - not logged in; undefined - when we don't need user;
  isLoading: boolean;
  validationErrors: BackendErrorsInterface | null;
}
