"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var mail_service_1 = require("./../services/mail.service");
var message_model_1 = require("./../models/message.model");
var router_1 = require("@angular/router");
var CreatePageComponent = (function () {
    function CreatePageComponent(mailService, router) {
        this.mailService = mailService;
        this.router = router;
        // Email address pattern regex
        // http://emailregex.com/
        this.emailPattern = /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;
    }
    CreatePageComponent.prototype.ngOnInit = function () {
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
    };
    CreatePageComponent.prototype.onSendClicked = function () {
        var _this = this;
        // Validate fields
        if (this.selectedService == "") {
            alert("No service selected");
            return;
        }
        if (!this.validateAddress(this.toAddress)) {
            alert("Invalid To address");
            return;
        }
        if (this.ccAddress != "" && !this.validateAddress(this.ccAddress)) {
            alert("Invalid CC address");
            return;
        }
        if (this.bccAddress != "" && !this.validateAddress(this.bccAddress)) {
            alert("Invalid BCC address");
            return;
        }
        if (this.message == "") {
            alert("Message cannot be empty!");
            return;
        }
        if (this.subject == "") {
            alert("Subject cannot be empty!");
            return;
        }
        // Build response
        var message = new message_model_1.default();
        message.Service = this.selectedService;
        message.ToAddresses = this.toAddress.replace(" ", "").split(";");
        if (this.ccAddress != "")
            message.CcAddresses = this.ccAddress.replace(" ", "").split(";");
        if (this.bccAddress != "")
            message.BccAddresses = this.bccAddress.replace(" ", "").split(";");
        message.Subject = this.subject;
        message.Message = this.message;
        // Send message
        this.sending = true;
        this.mailService.sendMessage(message).subscribe(function (rsp) { return _this.parseResponse(rsp); }, function (err) { return _this.parseResponse(err); });
    };
    CreatePageComponent.prototype.parseResponse = function (response) {
        this.sending = false;
        if (response.status == 200) {
            if (response.text() == "") {
                this.sent = true;
                this.hasError = false;
                return;
            }
        }
        this.sent = false;
        this.hasError = true;
        return;
    };
    CreatePageComponent.prototype.validateAddress = function (address) {
        address = address.replace(" ", "");
        var splitted = address.split(";");
        if (splitted.length == 0)
            return false;
        for (var i = 0; i < splitted.length; i++) {
            if (!this.emailPattern.test(splitted[i]))
                return false;
        }
        return true;
    };
    CreatePageComponent.prototype.onValueChangeEventHandler = function (event) {
        this.message = event.target.value;
    };
    CreatePageComponent.prototype.onCancelClicked = function () {
        this.router.navigateByUrl("home");
    };
    CreatePageComponent.prototype.onRetryClicked = function () {
        this.hasError = false;
        this.sending = false;
        this.sent = false;
    };
    return CreatePageComponent;
}());
CreatePageComponent = __decorate([
    core_1.Component({
        providers: [mail_service_1.MailService],
        selector: 'Create-page',
        templateUrl: 'create.page.html',
        styleUrls: ['./../styles/basic.css'],
        moduleId: module.id
    }),
    __metadata("design:paramtypes", [mail_service_1.MailService, router_1.Router])
], CreatePageComponent);
exports.CreatePageComponent = CreatePageComponent;
//# sourceMappingURL=create.page.js.map