import { Component, Input, ElementRef, ViewChild, OnInit } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';

@Component({
  selector: 'mmr-text-input-window',
  template: `
    <nb-card class="textInput-window">
      <nb-card-header>
      {{ dialogHeader }}
      <button nbButton class="headerButton" size="xsmall" status="danger" (click)="cancelDialog()">X</button>
      </nb-card-header>
      <nb-card-body>
        {{ dialogMessage }}
        <p></p>
        <input #inputBar class="textInput" type="text" maxlength="100" nbInput fieldSize="small" [(ngModel)]="inputText">
      </nb-card-body>
      <nb-card-footer>
        <div class="footerButtonWrapper">
          <button nbButton size="small" status="primary" (click)="confirmDialog()">OK</button>
        </div>
      </nb-card-footer>
    </nb-card>
  `,
  styleUrls: ['./textInputWindow.scss'],
  standalone: false
})
export class TextInputWindowComponent implements OnInit {

  @Input() dialogHeader: string = "Info";
  @Input() dialogMessage: string = "Message";
  @Input() inputText: string = "";

  constructor(protected ref: NbDialogRef<TextInputWindowComponent>) {}

  ngOnInit() {
    // Implementation for OnInit interface
  }

  confirmDialog() {
    this.ref.close(this.inputText);
  }

  cancelDialog() {
    this.ref.close(null);
  }
}
