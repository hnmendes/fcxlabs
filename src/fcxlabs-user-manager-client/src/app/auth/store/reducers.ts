import { createFeature, createReducer, on } from '@ngrx/store';
import { AuthStateInterface } from '../types/authState.interface';
import { authActions } from './actions';

const initialState: AuthStateInterface = {
  isSubmitted: false,
  isLoading: false,
  currentUser: undefined,
  validationErrors: null,
};

const authFeature = createFeature({
  name: 'auth',
  reducer: createReducer(
    initialState,
    on(authActions.register, (state) => ({
      ...state,
      isSubmitted: true,
      validationErrors: null,
    })),
    on(authActions.registerSuccess, (state) => ({
      ...state,
      isSubmitted: false,
      validationErrors: null,
    })),
    on(authActions.registerFailed, (state, action) => ({
      ...state,
      isSubmitted: false,
      validationErrors: action.errors,
    }))
  ),
});

export const {
  name: authFeatureKey,
  reducer: authReducer,
  selectIsSubmitted,
  selectValidationErrors,
} = authFeature;
