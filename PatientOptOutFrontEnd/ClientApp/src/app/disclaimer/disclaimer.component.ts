import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { environment } from '../../environments/environment'

@Component({
  selector: 'app-disclaimer',
  templateUrl: './disclaimer.component.html',
  styleUrls: ['./disclaimer.component.css']
})
export class DisclaimerComponent implements OnInit {
  public initialDisclaimer: string = environment.initialDisclaimer;

  constructor(public disclaimerRef: MatDialogRef<DisclaimerComponent>) { }

  ngOnInit() {
  }
}
