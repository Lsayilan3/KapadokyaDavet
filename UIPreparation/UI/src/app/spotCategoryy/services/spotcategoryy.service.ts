import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SpotCategoryy } from '../models/SpotCategoryy';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SpotCategoryyService {

  constructor(private httpClient: HttpClient) { }


  getSpotCategoryyList(): Observable<SpotCategoryy[]> {

    return this.httpClient.get<SpotCategoryy[]>(environment.getApiUrl + '/spotCategoryies/getall')
  }

  getSpotCategoryyById(id: number): Observable<SpotCategoryy> {
    return this.httpClient.get<SpotCategoryy>(environment.getApiUrl + '/spotCategoryies/getbyid?spotCategoryyId='+id)
  }

  addSpotCategoryy(spotCategoryy: SpotCategoryy): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/spotCategoryies/', spotCategoryy, { responseType: 'text' });
  }

  updateSpotCategoryy(spotCategoryy: SpotCategoryy): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/spotCategoryies/', spotCategoryy, { responseType: 'text' });

  }

  deleteSpotCategoryy(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/spotCategoryies/', { body: { spotCategoryyId: id } });
  }


}