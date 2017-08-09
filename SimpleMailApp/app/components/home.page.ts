import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'welcome-page',
    templateUrl: 'home.page.html',
    styleUrls: [ './../styles/basic.css'],
    moduleId: module.id
})
export class HomePageComponent 
{ 
    constructor(private router:Router){

    }

    onNewMailClicked(){
        this.router.navigateByUrl("create");
    }

}