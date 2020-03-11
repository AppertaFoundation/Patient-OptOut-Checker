import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DisclaimerComponent } from './disclaimer/disclaimer.component'
import { RouterModule, Routes} from '@angular/router';
import { PatientOptOutService } from './patient-opt-out.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatButtonModule, MatDialogModule } from '@angular/material';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ClipboardModule } from 'ngx-clipboard';
import { MatIconModule } from '@angular/material/icon'

const appRoutes: Routes = [
  { path: '', component: HomeComponent   }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DisclaimerComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    NgxSpinnerModule,
    ClipboardModule,
    MatIconModule,
  ],
  entryComponents: [
    DisclaimerComponent
  ],
  providers: [
    PatientOptOutService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
