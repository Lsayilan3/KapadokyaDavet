import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrAnimasyone } from '../models/OrAnimasyone';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrAnimasyoneService {

  constructor(private httpClient: HttpClient) { }


  getOrAnimasyoneList(): Observable<OrAnimasyone[]> {

    return this.httpClient.get<OrAnimasyone[]>(environment.getApiUrl + '/orAnimasyones/getall')
  }

  getOrAnimasyoneById(id: number): Observable<OrAnimasyone> {
    return this.httpClient.get<OrAnimasyone>(environment.getApiUrl + '/orAnimasyones/getbyid?orAnimasyoneId='+id)
  }

  addOrAnimasyone(orAnimasyone: OrAnimasyone): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orAnimasyones/', orAnimasyone, { responseType: 'text' });
  }

  updateOrAnimasyone(orAnimasyone: OrAnimasyone): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orAnimasyones/', orAnimasyone, { responseType: 'text' });

  }

  deleteOrAnimasyone(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orAnimasyones/', { body: { orAnimasyoneId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orAnimasyones/addPhoto', formData, { responseType: 'text' });
  }

}