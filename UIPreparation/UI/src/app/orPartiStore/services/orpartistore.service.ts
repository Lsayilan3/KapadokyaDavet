import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrPartiStore } from '../models/OrPartiStore';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrPartiStoreService {

  constructor(private httpClient: HttpClient) { }


  getOrPartiStoreList(): Observable<OrPartiStore[]> {

    return this.httpClient.get<OrPartiStore[]>(environment.getApiUrl + '/orPartiStores/getall')
  }

  getOrPartiStoreById(id: number): Observable<OrPartiStore> {
    return this.httpClient.get<OrPartiStore>(environment.getApiUrl + '/orPartiStores/getbyid?orPartiStoreId='+id)
  }

  addOrPartiStore(orPartiStore: OrPartiStore): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orPartiStores/', orPartiStore, { responseType: 'text' });
  }

  updateOrPartiStore(orPartiStore: OrPartiStore): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orPartiStores/', orPartiStore, { responseType: 'text' });

  }

  deleteOrPartiStore(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orPartiStores/', { body: { orPartiStoreId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orPartiStores/addPhoto', formData, { responseType: 'text' });
  }


}