import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EngineResultDto } from '../Dtos/engine-result.model';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class HistoryService {
    private apiUrl = environment.apiUrl +'/history';

    constructor(private http: HttpClient) { }

    getSearchResults(): Observable<EngineResultDto[]> {
        return this.http.get<EngineResultDto[]>(this.apiUrl);
    }
}
