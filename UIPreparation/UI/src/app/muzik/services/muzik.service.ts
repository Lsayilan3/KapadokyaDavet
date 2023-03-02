import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Muzik } from '../models/Muzik';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MuzikService {

  constructor(private httpClient: HttpClient) { }


  getMuzikList(): Observable<Muzik[]> {

    return this.httpClient.get<Muzik[]>(environment.getApiUrl + '/muziks/getall')
  }

  getMuzikById(id: number): Observable<Muzik> {
    return this.httpClient.get<Muzik>(environment.getApiUrl + '/muziks/getbyid?muzikId='+id)
  }

  addMuzik(muzik: Muzik): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/muziks/', muzik, { responseType: 'text' });
  }

  updateMuzik(muzik: Muzik): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/muziks/', muzik, { responseType: 'text' });

  }

  deleteMuzik(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/muziks/', { body: { muzikId: id } });
  }
  
  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/muziks/addPhoto', formData, { responseType: 'text' });
  }


}