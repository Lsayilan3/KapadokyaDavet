import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrKokteyl } from '../models/OrKokteyl';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrKokteylService {

  constructor(private httpClient: HttpClient) { }


  getOrKokteylList(): Observable<OrKokteyl[]> {

    return this.httpClient.get<OrKokteyl[]>(environment.getApiUrl + '/orKokteyls/getall')
  }

  getOrKokteylById(id: number): Observable<OrKokteyl> {
    return this.httpClient.get<OrKokteyl>(environment.getApiUrl + '/orKokteyls/getbyid?orKokteylId='+id)
  }

  addOrKokteyl(orKokteyl: OrKokteyl): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orKokteyls/', orKokteyl, { responseType: 'text' });
  }

  updateOrKokteyl(orKokteyl: OrKokteyl): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orKokteyls/', orKokteyl, { responseType: 'text' });

  }

  deleteOrKokteyl(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orKokteyls/', { body: { orKokteylId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orKokteyls/addPhoto', formData, { responseType: 'text' });
  }

}