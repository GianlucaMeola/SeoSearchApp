import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EngineResultDto } from '../Dtos/engine-result.model';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EngineSearchService {
    private apiUrl = environment.apiUrl + '/search';

  constructor(private http: HttpClient) { }

  // API call to fetch search results
  getSearchResults(keywords: string, url: string, engineNames: string[]): Observable<EngineResultDto[]> {
    const apiUrl = `${this.apiUrl}/${keywords}/${url}/${engineNames.join(',')}`;

    return this.http.get<EngineResultDto[]>(apiUrl)
      .pipe(
        catchError((error) => {
          console.error('Error fetching search results:', error);
          return [];
        })
      );
  }
}
