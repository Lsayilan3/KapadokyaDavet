import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrNisan } from '../models/OrNisan';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrNisanService {

  constructor(private httpClient: HttpClient) { }


  getOrNisanList(): Observable<OrNisan[]> {

    return this.httpClient.get<OrNisan[]>(environment.getApiUrl + '/orNisans/getall')
  }

  getOrNisanById(id: number): Observable<OrNisan> {
    return this.httpClient.get<OrNisan>(environment.getApiUrl + '/orNisans/getbyid?orNisanId='+id)
  }

  addOrNisan(orNisan: OrNisan): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orNisans/', orNisan, { responseType: 'text' });
  }

  updateOrNisan(orNisan: OrNisan): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orNisans/', orNisan, { responseType: 'text' });

  }

  deleteOrNisan(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orNisans/', { body: { orNisanId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orNisans/addPhoto', formData, { responseType: 'text' });
  }

}