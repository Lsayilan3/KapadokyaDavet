import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Yiyecek } from '../models/Yiyecek';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class YiyecekService {

  constructor(private httpClient: HttpClient) { }


  getYiyecekList(): Observable<Yiyecek[]> {

    return this.httpClient.get<Yiyecek[]>(environment.getApiUrl + '/yiyeceks/getall')
  }

  getYiyecekById(id: number): Observable<Yiyecek> {
    return this.httpClient.get<Yiyecek>(environment.getApiUrl + '/yiyeceks/getbyid?yiyecekId='+id)
  }

  addYiyecek(yiyecek: Yiyecek): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/yiyeceks/', yiyecek, { responseType: 'text' });
  }

  updateYiyecek(yiyecek: Yiyecek): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/yiyeceks/', yiyecek, { responseType: 'text' });

  }

  deleteYiyecek(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/yiyeceks/', { body: { yiyecekId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/yiyeceks/addPhoto', formData, { responseType: 'text' });
  }

}