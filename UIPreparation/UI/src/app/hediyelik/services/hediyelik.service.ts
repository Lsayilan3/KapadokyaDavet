import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hediyelik } from '../models/Hediyelik';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HediyelikService {

  constructor(private httpClient: HttpClient) { }


  getHediyelikList(): Observable<Hediyelik[]> {

    return this.httpClient.get<Hediyelik[]>(environment.getApiUrl + '/hediyeliks/getall')
  }

  getHediyelikById(id: number): Observable<Hediyelik> {
    return this.httpClient.get<Hediyelik>(environment.getApiUrl + '/hediyeliks/getbyid?hediyelikId='+id)
  }

  addHediyelik(hediyelik: Hediyelik): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/hediyeliks/', hediyelik, { responseType: 'text' });
  }

  updateHediyelik(hediyelik: Hediyelik): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/hediyeliks/', hediyelik, { responseType: 'text' });

  }

  deleteHediyelik(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/hediyeliks/', { body: { hediyelikId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/hediyeliks/addPhoto', formData, { responseType: 'text' });
  }




}