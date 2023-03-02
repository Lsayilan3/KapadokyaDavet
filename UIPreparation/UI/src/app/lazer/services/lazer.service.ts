import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Lazer } from '../models/Lazer';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class LazerService {

  constructor(private httpClient: HttpClient) { }


  getLazerList(): Observable<Lazer[]> {

    return this.httpClient.get<Lazer[]>(environment.getApiUrl + '/lazers/getall')
  }

  getLazerById(id: number): Observable<Lazer> {
    return this.httpClient.get<Lazer>(environment.getApiUrl + '/lazers/getbyid?lazerId='+id)
  }

  addLazer(lazer: Lazer): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/lazers/', lazer, { responseType: 'text' });
  }

  updateLazer(lazer: Lazer): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/lazers/', lazer, { responseType: 'text' });

  }

  deleteLazer(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/lazers/', { body: { lazerId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/lazers/addPhoto', formData, { responseType: 'text' });
  }


}