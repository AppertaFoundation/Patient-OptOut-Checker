import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment'
import { PatientOptOutService } from '../patient-opt-out.service';
import { NumberModel } from '../number-model';
import { AuthorizationModel } from '../authorization-model';
import { DisclaimerComponent } from '../disclaimer/disclaimer.component';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']

})

export class HomeComponent implements OnInit{
  public textInput: string;
  public hasAccess: boolean;
  public username: string;
  public trustDisclaimer: string = environment.trustDisclaimer;
  public noAccess: string = environment.noAccess;
  public form: FormGroup;
  public btnShowEmptyMessage = false;
  public btnShowCopyMessage = false;
  public resultsReplaced: number;
  public resultsReplacedMessage = false;

  constructor(
    private patientOptOutService: PatientOptOutService,
    public disclaimer: MatDialog,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.getAccess();
  }

  public getAccess() {
    this.patientOptOutService.getAuthorization().subscribe((response: AuthorizationModel) => {
      this.hasAccess = response.access;
      this.username = response.username;

      if (!this.hasAccess) {
        this.showDisclaimer();
      }
    });
  }

  public showDisclaimer() {
    this.disclaimer.open(DisclaimerComponent, {
      width: '500px',
      height: '325px'
    });
  }

  public btnCopyClicked(textInput) {
    textInput.select();
    document.execCommand('copy')
    textInput.setSelectionRange(0, 0);

    //Show message
    this.btnShowCopyMessage = true;

    setTimeout(() => {
      this.btnShowCopyMessage = false;
    }, 2000); //Hides message after 2 seconds
  }

  public btnCheckClicked(text: string) {
    this.spinner.show();
    this.resultsReplaced = 0;
    this.btnShowCopyMessage = false;
    this.resultsReplacedMessage = false;
    let modelArray: string[] = [];

    if (text) {
      text = text.trim();
      this.btnShowEmptyMessage = false;
      let lines = text.split(/[\n]+/);
      let regFindNHSLong =  (/\d{3}-\d{3}-\d{4}/g);
      let regFindNHSShort = (/\d{10}/g);
      let regFindHospShort = (/[A-Za-z]?[0-9]{6}/g)
      let regFindHospTwoLetters = (/[A-Za-z]{2}[0-9]{6}/g);
      let regReplace = (/[^\dA-Z-]/g);

      lines.forEach(line => {
        line = line.replace('-- OPT OUT', '');
        line = line.replace(regReplace, '');

        if (line.match(regFindNHSLong)){
          modelArray.push(line);

        } else if(line.match(regFindNHSShort)){
          modelArray.push(line);

        } else if(line.match(regFindHospShort)){
          modelArray.push(line);

        } else if(line.match(regFindHospTwoLetters)){
          modelArray.push(line);

        } else {
          line = line.replace(regReplace, '');
          this.resultsReplaced = this.resultsReplaced + 1;
          this.resultsReplacedMessage = true;
        }
      });

    } else {
      this.btnShowEmptyMessage = true;
      this.spinner.hide();
    }

    this.patientOptOutService.checkPatientNumbers(modelArray).subscribe((response: NumberModel[]) => {
      this.textInput = '';

      function compare(a) {
        if (a.optOut) {
          return -1;

        } else if (!a.optOut) {
          return 1;
        }

        return 0;
      }

      response.sort(compare);

      response.forEach(i => {
        this.textInput += i.number + ' ';

        if (i.optOut == true) {
          this.textInput += '-- OPT OUT';

        } else {
          this.textInput += '';
        }

        this.textInput += '\n';
      });

      this.spinner.hide();

    }, () => {
      this.spinner.hide();
    });
  }
}
