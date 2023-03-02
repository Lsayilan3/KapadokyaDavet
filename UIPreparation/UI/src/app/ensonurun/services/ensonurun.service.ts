import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ensonurun } from '../models/Ensonurun';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EnsonurunService {

  constructor(private httpClient: HttpClient) { }


  getEnsonurunList(): Observable<Ensonurun[]> {

    return this.httpClient.get<Ensonurun[]>(environment.getApiUrl + '/ensonuruns/getall')
  }

  getEnsonurunById(id: number): Observable<Ensonurun> {
    return this.httpClient.get<Ensonurun>(environment.getApiUrl + '/ensonuruns/getbyid?ensonurunId='+id)
  }

  addEnsonurun(ensonurun: Ensonurun): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ensonuruns/', ensonurun, { responseType: 'text' });
  }

  updateEnsonurun(ensonurun: Ensonurun): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ensonuruns/', ensonurun, { responseType: 'text' });

  }

  deleteEnsonurun(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ensonuruns/', { body: { ensonurunId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/ensonuruns/addPhoto', formData, { responseType: 'text' });
  }

}