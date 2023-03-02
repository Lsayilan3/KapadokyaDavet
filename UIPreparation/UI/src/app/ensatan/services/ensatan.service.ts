import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ensatan } from '../models/Ensatan';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EnsatanService {

  constructor(private httpClient: HttpClient) { }


  getEnsatanList(): Observable<Ensatan[]> {

    return this.httpClient.get<Ensatan[]>(environment.getApiUrl + '/ensatans/getall')
  }

  getEnsatanById(id: number): Observable<Ensatan> {
    return this.httpClient.get<Ensatan>(environment.getApiUrl + '/ensatans/getbyid?ensatanId='+id)
  }

  addEnsatan(ensatan: Ensatan): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ensatans/', ensatan, { responseType: 'text' });
  }

  updateEnsatan(ensatan: Ensatan): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ensatans/', ensatan, { responseType: 'text' });

  }

  deleteEnsatan(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ensatans/', { body: { ensatanId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/ensatans/addPhoto', formData, { responseType: 'text' });
  }

}