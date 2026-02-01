import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';

@Component({
  selector: 'mmr-confirmation-window',
  template: `
    <nb-card class="confirmation-window">
      <nb-card-header>
      {{ dialogHeader }}
      <button nbButton class="headerButton" size="xsmall" status="danger" (click)="closeDialogNo()">X</button>
      </nb-card-header>
      <nb-card-body>
        {{ dialogMessage }}
      </nb-card-body>
      <nb-card-footer>
        <div class="footerButtonWrapper">
          <button nbButton size="small" status="primary" (click)="closeDialogYes()">Yes</button>
          <button nbButton size="small" status="danger" (click)="closeDialogNo()">No</button>
        </div>
      </nb-card-footer>
    </nb-card>
  `,
  styleUrls: ['./confirmationWindow.scss'],
  standalone: false
})
export class ConfirmationWindowComponent implements OnInit {

  @Input() dialogHeader: string = "Confirm";
  @Input() dialogMessage: string = "Are you sure?";

  constructor(protected ref: NbDialogRef<ConfirmationWindowComponent>) {}

  ngOnInit() {
    // Implementation for OnInit interface
  }

  closeDialogYes() {
    this.ref.close(true);
  }

  closeDialogNo() {
    this.ref.close(false);
  }
}
