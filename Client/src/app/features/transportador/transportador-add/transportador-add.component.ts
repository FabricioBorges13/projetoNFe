import { Endereco } from './../../endereco/endereco.model';
import { TransportadorAddCommand } from './../shared/transportador.model';
import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TransportadorService } from '../shared/transportador.service';
import { Router } from '@angular/router';
@Component({
    templateUrl: 'transportador-add.component.html',
})

export class TransportadorAddComponent{

    public title: string = 'Transportador';
    public typeDocument: boolean = false;

    private form: FormGroup = this.fb.group({
        nomeRazaoSocial: ['', Validators.required],
        inscricaoEstadual: ['', Validators.required],
        numeroDoDocumento: ['', Validators.required],
        logradouro: ['', Validators.required],
        numero: ['', Validators.required],
        bairro: ['', Validators.required],
        municipio: ['', Validators.required],
        estado: ['', Validators.required],
    });

    constructor(private fb: FormBuilder, private service: TransportadorService, private router: Router) {
    }

    public onSubmit(): void {
        const cmd: TransportadorAddCommand = new TransportadorAddCommand(this.form.value);
        this.service.post(cmd)
        .take(1)
        .subscribe(() => {
            this.router.navigate(['transportador']);
        });
    }

    public onChange(): void {
        this.typeDocument = !this.typeDocument;
    }
}
