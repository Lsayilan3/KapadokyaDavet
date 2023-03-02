import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrPiknik } from '../models/OrPiknik';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrPiknikService {

  constructor(private httpClient: HttpClient) { }


  getOrPiknikList(): Observable<OrPiknik[]> {

    return this.httpClient.get<OrPiknik[]>(environment.getApiUrl + '/orPikniks/getall')
  }

  getOrPiknikById(id: number): Observable<OrPiknik> {
    return this.httpClient.get<OrPiknik>(environment.getApiUrl + '/orPikniks/getbyid?orPiknikId='+id)
  }

  addOrPiknik(orPiknik: OrPiknik): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orPikniks/', orPiknik, { responseType: 'text' });
  }

  updateOrPiknik(orPiknik: OrPiknik): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orPikniks/', orPiknik, { responseType: 'text' });

  }

  deleteOrPiknik(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orPikniks/', { body: { orPiknikId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orPikniks/addPhoto', formData, { responseType: 'text' });
  }


}