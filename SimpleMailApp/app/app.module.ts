import { NgModule }      from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent }  from './app.component';
import { HomePageComponent }  from './components/home.page';
import { CreatePageComponent }  from './components/create.page';
import { MailService }  from './services/mail.service';

@NgModule({
  imports:      [ 
    BrowserModule,
    HttpModule,
    RouterModule,
    RouterModule.forRoot([
          { path: '', redirectTo: 'home', pathMatch: 'full' },
          { path: 'home', component: HomePageComponent },
          { path: 'create', component: CreatePageComponent }
        
        ]),
    FormsModule,
    
     ],
  declarations: [ AppComponent, HomePageComponent, CreatePageComponent  ],
  bootstrap:    [ AppComponent ],
  providers: [MailService]
})
export class AppModule { }
