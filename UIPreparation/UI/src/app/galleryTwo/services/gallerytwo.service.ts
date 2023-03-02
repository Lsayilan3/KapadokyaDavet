import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GalleryTwo } from '../models/GalleryTwo';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GalleryTwoService {

  constructor(private httpClient: HttpClient) { }


  getGalleryTwoList(): Observable<GalleryTwo[]> {

    return this.httpClient.get<GalleryTwo[]>(environment.getApiUrl + '/galleryTwoes/getall')
  }

  getGalleryTwoById(id: number): Observable<GalleryTwo> {
    return this.httpClient.get<GalleryTwo>(environment.getApiUrl + '/galleryTwoes/getbyid?galleryTwoId='+id)
  }

  addGalleryTwo(galleryTwo: GalleryTwo): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/galleryTwoes/', galleryTwo, { responseType: 'text' });
  }

  updateGalleryTwo(galleryTwo: GalleryTwo): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/galleryTwoes/', galleryTwo, { responseType: 'text' });

  }

  deleteGalleryTwo(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/galleryTwoes/', { body: { galleryTwoId: id } });
  }
  
  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/galleryTwoes/addPhoto', formData, { responseType: 'text' });
  }



}