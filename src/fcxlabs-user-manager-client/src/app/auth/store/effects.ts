import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { AuthService } from '../services/auth.service';
import { authActions } from './actions';
import { catchError, map, of, switchMap, tap } from 'rxjs';
import { GenericBackendResponseInterface } from 'src/app/shared/types/genericBackendResponse.interface';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

export const registerEffect = createEffect(
  (actions$ = inject(Actions), authService = inject(AuthService)) => {
    return actions$.pipe(
      ofType(authActions.register),
      switchMap(({ request }) => {
        return authService.register(request).pipe(
          map((genericResponse: GenericBackendResponseInterface) => {
            return authActions.registerSuccess({ request: genericResponse });
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(
              authActions.registerFailed({ errors: errorResponse.error.errors })
            );
          })
        );
      })
    );
  },
  { functional: true }
);

export const redirectAfterRegisterEffect = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(authActions.registerSuccess),
      tap(() => {
        router.navigateByUrl('/');
      })
    );
  },
  { functional: true, dispatch: false }
);
