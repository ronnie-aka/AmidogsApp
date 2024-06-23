import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report',
  templateUrl: './report.page.html',
  styleUrls: ['./report.page.scss'],
})
export class ReportPage implements OnInit {

  reportText: string = '';

  constructor() { }

  ngOnInit() {
  }

  submitReport() {
    if (this.reportText.trim().length > 0) {
      // Aquí podrías añadir el código para enviar el reporte
      console.log("Reporte enviado:", this.reportText);

      // Vaciar el contenido del textarea
      this.reportText = '';
    }
  }
}
