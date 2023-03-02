import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrSokakLezzeti } from '../models/OrSokakLezzeti';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrSokakLezzetiService {

  constructor(private httpClient: HttpClient) { }


  getOrSokakLezzetiList(): Observable<OrSokakLezzeti[]> {

    return this.httpClient.get<OrSokakLezzeti[]>(environment.getApiUrl + '/orSokakLezzetis/getall')
  }

  getOrSokakLezzetiById(id: number): Observable<OrSokakLezzeti> {
    return this.httpClient.get<OrSokakLezzeti>(environment.getApiUrl + '/orSokakLezzetis/getbyid?orSokakLezzetiId='+id)
  }

  addOrSokakLezzeti(orSokakLezzeti: OrSokakLezzeti): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orSokakLezzetis/', orSokakLezzeti, { responseType: 'text' });
  }

  updateOrSokakLezzeti(orSokakLezzeti: OrSokakLezzeti): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orSokakLezzetis/', orSokakLezzeti, { responseType: 'text' });

  }

  deleteOrSokakLezzeti(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orSokakLezzetis/', { body: { orSokakLezzetiId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orSokakLezzetis/addPhoto', formData, { responseType: 'text' });
  }

}