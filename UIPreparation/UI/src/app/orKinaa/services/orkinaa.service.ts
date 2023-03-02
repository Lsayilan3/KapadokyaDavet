import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrKinaa } from '../models/OrKinaa';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrKinaaService {

  constructor(private httpClient: HttpClient) { }


  getOrKinaaList(): Observable<OrKinaa[]> {

    return this.httpClient.get<OrKinaa[]>(environment.getApiUrl + '/orKinaas/getall')
  }

  getOrKinaaById(id: number): Observable<OrKinaa> {
    return this.httpClient.get<OrKinaa>(environment.getApiUrl + '/orKinaas/getbyid?orKinaaId='+id)
  }

  addOrKinaa(orKinaa: OrKinaa): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orKinaas/', orKinaa, { responseType: 'text' });
  }

  updateOrKinaa(orKinaa: OrKinaa): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orKinaas/', orKinaa, { responseType: 'text' });

  }

  deleteOrKinaa(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orKinaas/', { body: { orKinaaId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orKinaas/addPhoto', formData, { responseType: 'text' });
  }

}