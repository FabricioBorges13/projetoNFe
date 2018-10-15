import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotaFiscalService } from '../shared/notafiscal.service';
import { Router } from '@angular/router';

@Component({
    templateUrl: './notafiscal-add.component.html',
})
export class NotaFiscalAddComponent implements OnInit {
    public title: string = 'Nova Nota Fiscal';
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
        private router: Router) { }

    public ngOnInit(): void {
        //
     }
}
