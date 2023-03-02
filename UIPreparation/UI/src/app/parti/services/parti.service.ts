import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Parti } from '../models/Parti';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PartiService {

  constructor(private httpClient: HttpClient) { }


  getPartiList(): Observable<Parti[]> {

    return this.httpClient.get<Parti[]>(environment.getApiUrl + '/partis/getall')
  }

  getPartiById(id: number): Observable<Parti> {
    return this.httpClient.get<Parti>(environment.getApiUrl + '/partis/getbyid?partiId='+id)
  }

  addParti(parti: Parti): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/partis/', parti, { responseType: 'text' });
  }

  updateParti(parti: Parti): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/partis/', parti, { responseType: 'text' });

  }

  deleteParti(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/partis/', { body: { partiId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/partis/addPhoto', formData, { responseType: 'text' });
  }

}