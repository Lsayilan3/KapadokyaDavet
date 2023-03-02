import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cikolata } from '../models/Cikolata';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CikolataService {

  constructor(private httpClient: HttpClient) { }


  getCikolataList(): Observable<Cikolata[]> {

    return this.httpClient.get<Cikolata[]>(environment.getApiUrl + '/cikolatas/getall')
  }

  getCikolataById(id: number): Observable<Cikolata> {
    return this.httpClient.get<Cikolata>(environment.getApiUrl + '/cikolatas/getbyid?cikolataId='+id)
  }

  addCikolata(cikolata: Cikolata): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/cikolatas/', cikolata, { responseType: 'text' });
  }

  updateCikolata(cikolata: Cikolata): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/cikolatas/', cikolata, { responseType: 'text' });

  }

  deleteCikolata(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/cikolatas/', { body: { cikolataId: id } });
  }
  
  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/cikolatas/addPhoto', formData, { responseType: 'text' });
  }


}