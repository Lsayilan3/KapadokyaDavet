import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrDugun } from '../models/OrDugun';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrDugunService {

  constructor(private httpClient: HttpClient) { }


  getOrDugunList(): Observable<OrDugun[]> {

    return this.httpClient.get<OrDugun[]>(environment.getApiUrl + '/orDuguns/getall')
  }

  getOrDugunById(id: number): Observable<OrDugun> {
    return this.httpClient.get<OrDugun>(environment.getApiUrl + '/orDuguns/getbyid?orDugunId='+id)
  }

  addOrDugun(orDugun: OrDugun): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orDuguns/', orDugun, { responseType: 'text' });
  }

  updateOrDugun(orDugun: OrDugun): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orDuguns/', orDugun, { responseType: 'text' });

  }

  deleteOrDugun(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orDuguns/', { body: { orDugunId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orDuguns/addPhoto', formData, { responseType: 'text' });
  }


}