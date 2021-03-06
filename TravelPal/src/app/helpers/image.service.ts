import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ImageService {
  _apiURL = "https://localhost:44325/api/image";

  constructor(private _http: HttpClient) {}

  addImages(id: number, images: FormData, type: string) {
    return this._http.post(`${this._apiURL}/${type}/${id}`, images);
  }

  deleteImage(id: number, type: string) {
    return this._http.delete(`${this._apiURL}/${type}/${id}`);
  }
}
