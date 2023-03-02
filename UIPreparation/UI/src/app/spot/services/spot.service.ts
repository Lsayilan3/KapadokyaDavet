import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Spot } from '../models/Spot';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SpotService {

  constructor(private httpClient: HttpClient) { }


  getSpotList(): Observable<Spot[]> {

    return this.httpClient.get<Spot[]>(environment.getApiUrl + '/spots/getall')
  }

  getSpotById(id: number): Observable<Spot> {
    return this.httpClient.get<Spot>(environment.getApiUrl + '/spots/getbyid?spotId='+id)
  }

  addSpot(spot: Spot): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/spots/', spot, { responseType: 'text' });
  }

  updateSpot(spot: Spot): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/spots/', spot, { responseType: 'text' });

  }

  deleteSpot(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/spots/', { body: { spotId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/spots/addPhoto', formData, { responseType: 'text' });
  }

}