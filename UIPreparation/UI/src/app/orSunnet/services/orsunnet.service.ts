import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrSunnet } from '../models/OrSunnet';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrSunnetService {

  constructor(private httpClient: HttpClient) { }


  getOrSunnetList(): Observable<OrSunnet[]> {

    return this.httpClient.get<OrSunnet[]>(environment.getApiUrl + '/orSunnets/getall')
  }

  getOrSunnetById(id: number): Observable<OrSunnet> {
    return this.httpClient.get<OrSunnet>(environment.getApiUrl + '/orSunnets/getbyid?orSunnetId='+id)
  }

  addOrSunnet(orSunnet: OrSunnet): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orSunnets/', orSunnet, { responseType: 'text' });
  }

  updateOrSunnet(orSunnet: OrSunnet): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orSunnets/', orSunnet, { responseType: 'text' });

  }

  deleteOrSunnet(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orSunnets/', { body: { orSunnetId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orSunnets/addPhoto', formData, { responseType: 'text' });
  }

}