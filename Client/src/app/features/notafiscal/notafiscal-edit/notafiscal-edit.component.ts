import { Component, OnInit, OnDestroy } from '@angular/core';
import { NotaFiscal } from '../shared/notafiscal.model';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { NotaFiscalService, NotaFiscalResolveService } from '../shared/notafiscal.service';
import { Router } from '@angular/router';
import { EmitenteService } from '../../emitentes/shared/emitente.service';
import { TransportadorService } from '../../transportador/shared/transportador.service';
import { DestinatarioService } from '../../destinatario/shared/destinatario.service';
import { Subject } from 'rxjs/Subject';
@Component({
    templateUrl: 'notafiscal-edit.component.html',
})

export class NotaFiscalEditComponent implements OnInit, OnDestroy {
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
        emitente: [null, Validators.required],
        transportador: [null, Validators.required],
        destinatario: [null, Validators.required],
        valorDoFrete: ['', Validators.required],
    });
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private service: NotaFiscalService,
        private fb: FormBuilder,
        private router: Router,
        private serviceEmitente: EmitenteService,
        private serviceTransportador: TransportadorService,
        private serviceDestinatario: DestinatarioService,
        private resolver: NotaFiscalResolveService) { }

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .do(() => this.isLoading = false)
            .subscribe((notaFiscal: NotaFiscal) => {
                this.notafiscal = Object.assign(new NotaFiscal(), notaFiscal);
                this.form.patchValue({
                    naturezaOperacao: notaFiscal.naturezaOperacao,
                    dataEntrada: notaFiscal.dataEntrada,
                    emitente: notaFiscal.emitente,
                    transportador: notaFiscal.transportador,
                    destinatario: notaFiscal.destinatario,
                    valorDoFrete: notaFiscal.valorDoFrete,
                });
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
