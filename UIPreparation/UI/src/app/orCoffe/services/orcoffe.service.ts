import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrCoffe } from '../models/OrCoffe';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrCoffeService {

  constructor(private httpClient: HttpClient) { }


  getOrCoffeList(): Observable<OrCoffe[]> {

    return this.httpClient.get<OrCoffe[]>(environment.getApiUrl + '/orCofves/getall')
  }

  getOrCoffeById(id: number): Observable<OrCoffe> {
    return this.httpClient.get<OrCoffe>(environment.getApiUrl + '/orCofves/getbyid?orCoffeId='+id)
  }

  addOrCoffe(orCoffe: OrCoffe): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orCofves/', orCoffe, { responseType: 'text' });
  }

  updateOrCoffe(orCoffe: OrCoffe): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orCofves/', orCoffe, { responseType: 'text' });

  }

  deleteOrCoffe(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orCofves/', { body: { orCoffeId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orCofves/addPhoto', formData, { responseType: 'text' });
  }

}