import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrBaby } from '../models/OrBaby';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrBabyService {

  constructor(private httpClient: HttpClient) { }


  getOrBabyList(): Observable<OrBaby[]> {

    return this.httpClient.get<OrBaby[]>(environment.getApiUrl + '/orBabies/getall')
  }

  getOrBabyById(id: number): Observable<OrBaby> {
    return this.httpClient.get<OrBaby>(environment.getApiUrl + '/orBabies/getbyid?orBabyId='+id)
  }

  addOrBaby(orBaby: OrBaby): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orBabies/', orBaby, { responseType: 'text' });
  }

  updateOrBaby(orBaby: OrBaby): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orBabies/', orBaby, { responseType: 'text' });

  }

  deleteOrBaby(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orBabies/', { body: { orBabyId: id } });
  }
  
  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orBabies/addPhoto', formData, { responseType: 'text' });
  }


}