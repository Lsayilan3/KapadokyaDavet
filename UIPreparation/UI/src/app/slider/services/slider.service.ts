import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Slider } from '../models/Slider';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SliderService {

  constructor(private httpClient: HttpClient) { }


  getSliderList(): Observable<Slider[]> {

    return this.httpClient.get<Slider[]>(environment.getApiUrl + '/sliders/getall')
  }

  getSliderById(id: number): Observable<Slider> {
    return this.httpClient.get<Slider>(environment.getApiUrl + '/sliders/getbyid?sliderId='+id)
  }

  addSlider(slider: Slider): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/sliders/', slider, { responseType: 'text' });
  }

  updateSlider(slider: Slider): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/sliders/', slider, { responseType: 'text' });

  }

  deleteSlider(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/sliders/', { body: { sliderId: id } });
  }

    addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/sliders/addPhoto', formData, { responseType: 'text' });
  }


}