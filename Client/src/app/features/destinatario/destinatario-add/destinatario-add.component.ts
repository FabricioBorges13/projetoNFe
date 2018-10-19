import { DestinatarioAddCommand } from '../../../features/destinatario/shared/destinatario.model';
import { Endereco } from './../../endereco/endereco.model';
import { Router, ActivatedRoute } from '@angular/router';
import { DestinatarioService } from './../shared/destinatario.service';
import { Component, OnInit, Optional } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
@Component({
    selector: 'ndd-destinatario-add',
    templateUrl: './destinatario-add.component.html',
})
export class DestinatarioAddComponent {

    public title: string = 'Cadastrar Destinatario';
    public isLoading: boolean;
    public typeDocument: boolean = false;
    public formModel: FormGroup = this.fb.group({
        nomeRazaoSocial: ['', Validators.required],
        logradouro: ['', Validators.required],
        bairro: ['', Validators.required],
        numero: ['', Validators.required],
        municipio: ['', Validators.required],
        estado: ['', Validators.required],
        numeroDeDocumento: ['', Validators.required],
        inscricaoEstadual: [''],
    });

    constructor(private fb: FormBuilder,
        private service: DestinatarioService, private router: Router, private route: ActivatedRoute) {
    }

    public onSubmit(): void {
        const cmd: DestinatarioAddCommand = new DestinatarioAddCommand(this.formModel.value);
        this.service.post(cmd)
        .take(1)
        .subscribe(() => {
            this.router.navigate(['destinatario']);
        });
    }

    public onChange(): void {
        this.typeDocument = !this.typeDocument;
    }

    public redirect(): void {
        this.router.navigate(['./../'],
            { relativeTo: this.route });
    }

}
