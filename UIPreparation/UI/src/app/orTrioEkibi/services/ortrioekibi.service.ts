import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrTrioEkibi } from '../models/OrTrioEkibi';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrTrioEkibiService {

  constructor(private httpClient: HttpClient) { }


  getOrTrioEkibiList(): Observable<OrTrioEkibi[]> {

    return this.httpClient.get<OrTrioEkibi[]>(environment.getApiUrl + '/orTrioEkibis/getall')
  }

  getOrTrioEkibiById(id: number): Observable<OrTrioEkibi> {
    return this.httpClient.get<OrTrioEkibi>(environment.getApiUrl + '/orTrioEkibis/getbyid?orTrioEkibiId='+id)
  }

  addOrTrioEkibi(orTrioEkibi: OrTrioEkibi): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orTrioEkibis/', orTrioEkibi, { responseType: 'text' });
  }

  updateOrTrioEkibi(orTrioEkibi: OrTrioEkibi): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orTrioEkibis/', orTrioEkibi, { responseType: 'text' });

  }

  deleteOrTrioEkibi(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orTrioEkibis/', { body: { orTrioEkibiId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orTrioEkibis/addPhoto', formData, { responseType: 'text' });
  }


}