import { HttpClient } from "@angular/common/http";
import {EventEmitter, Injectable} from "@angular/core";

@Injectable({providedIn: 'root'})
export class TestService


{
    autPut = new EventEmitter<any>();
    mojUrl='https://localhost:44325/api/Location';

    constructor(private http: HttpClient)
    {

    }
    ClickOnLocation()
    {
        this.http.get(this.mojUrl).subscribe(
            (povratna) => {
                this.autPut.emit(povratna);
            }
        )
    }

   
}