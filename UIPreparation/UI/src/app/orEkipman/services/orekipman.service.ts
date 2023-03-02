import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrEkipman } from '../models/OrEkipman';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrEkipmanService {

  constructor(private httpClient: HttpClient) { }


  getOrEkipmanList(): Observable<OrEkipman[]> {

    return this.httpClient.get<OrEkipman[]>(environment.getApiUrl + '/orEkipmans/getall')
  }

  getOrEkipmanById(id: number): Observable<OrEkipman> {
    return this.httpClient.get<OrEkipman>(environment.getApiUrl + '/orEkipmans/getbyid?orEkipmanId='+id)
  }

  addOrEkipman(orEkipman: OrEkipman): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orEkipmans/', orEkipman, { responseType: 'text' });
  }

  updateOrEkipman(orEkipman: OrEkipman): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orEkipmans/', orEkipman, { responseType: 'text' });

  }

  deleteOrEkipman(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orEkipmans/', { body: { orEkipmanId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orEkipmans/addPhoto', formData, { responseType: 'text' });
  }

}