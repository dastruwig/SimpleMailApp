import { Component } from '@angular/core';
import { Response } from '@angular/http'
import { MailService } from './../services/mail.service';
import MessageModel from './../models/message.model';
import { Router } from '@angular/router';

@Component({
    providers: [MailService],
    selector: 'Create-page',
    templateUrl: 'create.page.html',
    styleUrls: [ './../styles/basic.css'],
    moduleId: module.id
})
export class CreatePageComponent 
{ 
    // Email address pattern regex
    // http://emailregex.com/
    emailPattern = /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/
    
    toAddress : string;
    ccAddress : string;
    bccAddress : string;
    message : string;
    subject : string;
    selectedService : string;

    // Describes the state of the form
    sending : boolean;
    sent : boolean;
    hasError : boolean;

    constructor(private mailService : MailService, private router:Router){
        
    }

    ngOnInit(){
        // Reset view
        this.toAddress = "";
        this.subject = "";
        this.ccAddress = "";
        this.bccAddress = "";
        this.message = "";
        this.sending = false;
        this.sent = false;
        this.hasError = false;
        this.selectedService = "";
    }

    onSendClicked(){
        
        // Validate fields
        if (this.selectedService == ""){
            alert("No service selected");
            return;
        }

        if (!this.validateAddress(this.toAddress)){
            alert("Invalid To address");
            return;
        }
        
        if (this.ccAddress != "" && !this.validateAddress(this.ccAddress)){
            alert("Invalid CC address");
            return;
        }
        
        if (this.bccAddress != "" && !this.validateAddress(this.bccAddress)){
            alert("Invalid BCC address");
            return;
        }

        if (this.message == ""){
            alert("Message cannot be empty!");
            return;
        }

        if (this.subject == ""){
            alert("Subject cannot be empty!");
            return;
        }

        // Build response
        var message = new MessageModel();
        message.Service = this.selectedService;
        message.ToAddresses = this.toAddress.replace(" ","").split(";");
        
        if (this.ccAddress != "")
            message.CcAddresses = this.ccAddress.replace(" ","").split(";");

        if (this.bccAddress != "")
            message.BccAddresses = this.bccAddress.replace(" ","").split(";");
        
        message.Subject = this.subject;
        message.Message = this.message;
        
        // Send message
        this.sending = true;
        this.mailService.sendMessage(message).subscribe(rsp => this.parseResponse(rsp), err => this.parseResponse(err));
    }



    private parseResponse(response : Response){
        
        this.sending = false;

        if (response.status == 200){
            
            if (response.text() == "")
            {
                this.sent = true;
                this.hasError = false;
                return;
            }
        }
        
        
        this.sent = false;
        this.hasError = true;
        return;
    }

    private validateAddress(address:string) : boolean{
        
        address = address.replace(" ","");
        var splitted = address.split(";");

        if (splitted.length == 0)
            return false;

        for(var i = 0; i < splitted.length; i++){
            if (!this.emailPattern.test(splitted[i]))
                return false;
        }

        return true;
    }

    onValueChangeEventHandler(event:any){
        this.message = event.target.value;
    }

    onCancelClicked(){
        this.router.navigateByUrl("home");
    }

    onRetryClicked(){
        this.hasError = false;
        this.sending = false;
        this.sent = false;
    }
}