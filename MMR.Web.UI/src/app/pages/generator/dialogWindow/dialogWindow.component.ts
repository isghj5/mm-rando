import { Component, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { OnInit } from '@angular/core';

@Component({
  selector: 'mmr-dialog-window',
  template: `
    <nb-card class="dialog-window">
      <nb-card-header>
      {{ dialogHeader }}
      <button nbButton class="headerButton" size="xsmall" status="danger" (click)="closeDialog()">X</button>
      </nb-card-header>
      <nb-card-body>
        {{ dialogMessage }}
      </nb-card-body>
      <nb-card-footer>
        <div class="footerButtonWrapper">
          <button nbButton size="small" status="primary" (click)="closeDialog()">OK</button>
        </div>
      </nb-card-footer>
    </nb-card>
  `,
  styleUrls: ['./dialogWindow.scss'],
  standalone: false
})
export class DialogWindowComponent implements OnInit {

  @Input() dialogHeader: string = "Info";
  @Input() dialogMessage: string = "Message";

  constructor(protected ref: NbDialogRef<DialogWindowComponent>) {}

  ngOnInit() {
    // Implementation for OnInit interface
  }

  closeDialog() {
    this.ref.close();
  }
}
