import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { EnvironmentService } from '../environment.service';

@Component({
  selector: 'app-disclaimer',
  templateUrl: './disclaimer.component.html',
  styleUrls: ['./disclaimer.component.css']
})
export class DisclaimerComponent implements OnInit {
  public initialDisclaimer: string;

  constructor(public disclaimerRef: MatDialogRef<DisclaimerComponent>, private environmentService: EnvironmentService) { }

  ngOnInit() {
    this.initialDisclaimer = this.environmentService.initialDisclaimer
  }
}
