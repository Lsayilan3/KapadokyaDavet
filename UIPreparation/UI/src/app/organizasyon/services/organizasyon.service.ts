import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Organizasyon } from '../models/Organizasyon';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrganizasyonService {

  constructor(private httpClient: HttpClient) { }


  getOrganizasyonList(): Observable<Organizasyon[]> {

    return this.httpClient.get<Organizasyon[]>(environment.getApiUrl + '/organizasyons/getall')
  }

  getOrganizasyonById(id: number): Observable<Organizasyon> {
    return this.httpClient.get<Organizasyon>(environment.getApiUrl + '/organizasyons/getbyid?organizasyonId='+id)
  }

  addOrganizasyon(organizasyon: Organizasyon): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/organizasyons/', organizasyon, { responseType: 'text' });
  }

  updateOrganizasyon(organizasyon: Organizasyon): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/organizasyons/', organizasyon, { responseType: 'text' });

  }

  deleteOrganizasyon(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/organizasyons/', { body: { organizasyonId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/organizasyons/addPhoto', formData, { responseType: 'text' });
  }


}