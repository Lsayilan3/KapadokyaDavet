import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SliderTwo } from '../models/SliderTwo';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SliderTwoService {

  constructor(private httpClient: HttpClient) { }


  getSliderTwoList(): Observable<SliderTwo[]> {

    return this.httpClient.get<SliderTwo[]>(environment.getApiUrl + '/sliderTwoes/getall')
  }

  getSliderTwoById(id: number): Observable<SliderTwo> {
    return this.httpClient.get<SliderTwo>(environment.getApiUrl + '/sliderTwoes/getbyid?sliderTwoId='+id)
  }

  addSliderTwo(sliderTwo: SliderTwo): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/sliderTwoes/', sliderTwo, { responseType: 'text' });
  }

  updateSliderTwo(sliderTwo: SliderTwo): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/sliderTwoes/', sliderTwo, { responseType: 'text' });

  }

  deleteSliderTwo(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/sliderTwoes/', { body: { sliderTwoId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/sliderTwoes/addPhoto', formData, { responseType: 'text' });
  }

}