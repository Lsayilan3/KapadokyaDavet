import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrAcilis } from '../models/OrAcilis';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrAcilisService {

  constructor(private httpClient: HttpClient) { }


  getOrAcilisList(): Observable<OrAcilis[]> {

    return this.httpClient.get<OrAcilis[]>(environment.getApiUrl + '/orAcilises/getall')
  }

  getOrAcilisById(id: number): Observable<OrAcilis> {
    return this.httpClient.get<OrAcilis>(environment.getApiUrl + '/orAcilises/getbyid?orAcilisId='+id)
  }

  addOrAcilis(orAcilis: OrAcilis): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orAcilises/', orAcilis, { responseType: 'text' });
  }

  updateOrAcilis(orAcilis: OrAcilis): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orAcilises/', orAcilis, { responseType: 'text' });

  }

  deleteOrAcilis(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orAcilises/', { body: { orAcilisId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orAcilises/addPhoto', formData, { responseType: 'text' });
  }

}