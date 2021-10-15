import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Constants } from '../Config/constants';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private constants: Constants = new Constants();

  constructor(private http: HttpClient) { }

  public get(url: string, options?: any) {
    return this.http.get(this.constants.API_ENDPOINT + url, options);
  }

  public post(url: string, data: any, options?: any) {
    return this.http.post(this.constants.API_ENDPOINT + url, data, options);
  }

  public delete(url: string, options?: any) {
    return this.http.delete(this.constants.API_ENDPOINT + url, options);
  }
}
