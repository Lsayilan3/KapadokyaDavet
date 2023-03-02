import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrPartiEglence } from '../models/OrPartiEglence';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrPartiEglenceService {

  constructor(private httpClient: HttpClient) { }


  getOrPartiEglenceList(): Observable<OrPartiEglence[]> {

    return this.httpClient.get<OrPartiEglence[]>(environment.getApiUrl + '/orPartiEglences/getall')
  }

  getOrPartiEglenceById(id: number): Observable<OrPartiEglence> {
    return this.httpClient.get<OrPartiEglence>(environment.getApiUrl + '/orPartiEglences/getbyid?orPartiEglenceId='+id)
  }

  addOrPartiEglence(orPartiEglence: OrPartiEglence): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orPartiEglences/', orPartiEglence, { responseType: 'text' });
  }

  updateOrPartiEglence(orPartiEglence: OrPartiEglence): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orPartiEglences/', orPartiEglence, { responseType: 'text' });

  }

  deleteOrPartiEglence(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orPartiEglences/', { body: { orPartiEglenceId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orPartiEglences/addPhoto', formData, { responseType: 'text' });
  }


}