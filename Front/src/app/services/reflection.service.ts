import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ReflectionService {
  private apiUrl = '/api/reflection/importers';

  constructor(private http: HttpClient) {}

  getImporterNames(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl);
  }
}
