import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrCatering } from '../models/OrCatering';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrCateringService {

  constructor(private httpClient: HttpClient) { }


  getOrCateringList(): Observable<OrCatering[]> {

    return this.httpClient.get<OrCatering[]>(environment.getApiUrl + '/orCaterings/getall')
  }

  getOrCateringById(id: number): Observable<OrCatering> {
    return this.httpClient.get<OrCatering>(environment.getApiUrl + '/orCaterings/getbyid?orCateringId='+id)
  }

  addOrCatering(orCatering: OrCatering): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/orCaterings/', orCatering, { responseType: 'text' });
  }

  updateOrCatering(orCatering: OrCatering): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/orCaterings/', orCatering, { responseType: 'text' });

  }

  deleteOrCatering(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/orCaterings/', { body: { orCateringId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/orCaterings/addPhoto', formData, { responseType: 'text' });
  }


}