import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterRequestInterface } from '../types/registerRequest.interface';
import { Observable, map } from 'rxjs';
import { environment } from 'src/app/environments/environment';
import { GenericBackendResponseInterface } from 'src/app/shared/types/genericBackendResponse.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  register(
    data: RegisterRequestInterface
  ): Observable<GenericBackendResponseInterface> {
    const url = `${environment.apiURL}auth/sign-up`;
    return this.http
      .post<GenericBackendResponseInterface>(url, data)
      .pipe(map((response) => response));
  }
}
