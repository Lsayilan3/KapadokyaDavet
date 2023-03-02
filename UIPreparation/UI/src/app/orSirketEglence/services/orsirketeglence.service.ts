import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrSirketEglence } from '../models/OrSirketEglence';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrSirketEglenceService {

  constructor(private httpClient: HttpClient) { }


  getOrSirketEglenceList(): Observable<OrSirketEglence[]> {

    return this.httpClient.get<OrSirketEglence[]>(environment.getApiUrl + '/orSirketEglences/getall')
  }

  getOrSirketEglenceById(id: number): Observable<OrSirketEglence> {
    return this.httpClient.get<OrSirketEglence>(environment.getApiUrl + '/orSirketEglences/getbyid?orSirketEglenceId='+id)
  }

  addOrSirketEglence(orSirketEglence: OrSirketEglence): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orSirketEglences/', orSirketEglence, { responseType: 'text' });
  }

  updateOrSirketEglence(orSirketEglence: OrSirketEglence): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orSirketEglences/', orSirketEglence, { responseType: 'text' });

  }

  deleteOrSirketEglence(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orSirketEglences/', { body: { orSirketEglenceId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orSirketEglences/addPhoto', formData, { responseType: 'text' });
  }


}