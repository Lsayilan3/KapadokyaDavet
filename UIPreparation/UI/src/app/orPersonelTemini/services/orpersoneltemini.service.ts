import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrPersonelTemini } from '../models/OrPersonelTemini';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrPersonelTeminiService {

  constructor(private httpClient: HttpClient) { }


  getOrPersonelTeminiList(): Observable<OrPersonelTemini[]> {

    return this.httpClient.get<OrPersonelTemini[]>(environment.getApiUrl + '/orPersonelTeminis/getall')
  }

  getOrPersonelTeminiById(id: number): Observable<OrPersonelTemini> {
    return this.httpClient.get<OrPersonelTemini>(environment.getApiUrl + '/orPersonelTeminis/getbyid?orPersonelTeminiId='+id)
  }

  addOrPersonelTemini(orPersonelTemini: OrPersonelTemini): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orPersonelTeminis/', orPersonelTemini, { responseType: 'text' });
  }

  updateOrPersonelTemini(orPersonelTemini: OrPersonelTemini): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orPersonelTeminis/', orPersonelTemini, { responseType: 'text' });

  }

  deleteOrPersonelTemini(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orPersonelTeminis/', { body: { orPersonelTeminiId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orPersonelTeminis/addPhoto', formData, { responseType: 'text' });
  }

}