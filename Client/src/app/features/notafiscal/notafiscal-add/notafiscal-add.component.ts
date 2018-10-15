import { DestinatarioService } from './../../destinatario/shared/destinatario.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotaFiscalService } from '../shared/notafiscal.service';
import { Router } from '@angular/router';
import { NotaFiscal, NotaFiscalDataCommand } from '../shared/notafiscal.model';
import { Observable } from 'rxjs/Observable';
import { EmitenteService } from '../../emitentes/shared/emitente.service';
import { TransportadorService } from '../../transportador/shared/transportador.service';

@Component({
    templateUrl: './notafiscal-add.component.html',
})
export class NotaFiscalAddComponent implements OnInit {
    public title: string = 'Nova Nota Fiscal';
    public isLoading: boolean;
    public notafiscal: NotaFiscal;
    public data: any[];
    public delay: number = 300;
    public form: FormGroup = this.fb.group({
        naturezaOperacao: ['', Validators.required],
        dataEntrada: ['', [Validators.required]],
        valorIpi: ['', [Validators.required]],
        valorIcms: ['', Validators.required],
        emitente: ['', Validators.required],
        transportador: ['', Validators.required],
        destinatario: ['', Validators.required],
        valorDoFrete: ['', Validators.required],
    });
    constructor(private service: NotaFiscalService,
        private fb: FormBuilder,
        private router: Router,
        private serviceEmitente: EmitenteService,
        private serviceTransportador: TransportadorService,
        private serviceDestinatario: DestinatarioService ) { }

    public ngOnInit(): void {
        //
     }
     public onAutoCompleteChangeEmitente(value: string): any {
        Observable.of(value)
          .delay(this.delay)
          .switchMap((value: any, index: number) => this.serviceEmitente.getByName(value))
          .subscribe((response: any) => {
            this.data = response;
          });
      }
      public onAutoCompleteChangeTransportador(value: string): any {
        Observable.of(value)
          .delay(this.delay)
          .switchMap((value: any, index: number) => this.serviceEmitente.getByName(value))
          .subscribe((response: any) => {
            this.data = response;
          });
      }
    public onSave(): void {
        this.isLoading = true;
        const notaFiscalAddCommand: NotaFiscalDataCommand = new NotaFiscalDataCommand(this.form.value);

        this.service.add(notaFiscalAddCommand)
        .take(1)
        .subscribe(() => {
            this.redirect();
            this.isLoading = false;
        });
    }
    public redirect(): void {
        this.router.navigate(['./']);
    }
}
