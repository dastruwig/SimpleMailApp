import {Injectable, Inject} from '@angular/core';
import {Observable} from 'rxjs'
import {Http, Response, Headers, RequestOptions} from "@angular/http";
import MessageModel from './../models/message.model'

@Injectable()
export class MailService 
{
    constructor(private http: Http){

    }
    
    sendMessage(message : MessageModel){
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(message);
        return this.http.post("/api/mail", body, options);
    }

}
