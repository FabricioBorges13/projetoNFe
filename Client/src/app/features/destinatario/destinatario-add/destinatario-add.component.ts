import { DestinatarioAddCommand } from '../../../features/destinatario/shared/destinatario.model';
import { Endereco } from './../../endereco/endereco.model';
import { Router, ActivatedRoute } from '@angular/router';
import { DestinatarioService } from './../shared/destinatario.service';
import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
@Component({
    selector: 'ndd-destinatario-add',
    templateUrl: './destinatario-add.component.html',
})
export class DestinatarioAddComponent implements OnInit {

    public title: string = 'Cadastrar Destinatario';
    public isLoading: boolean;
    public formModel: FormGroup = this.fb.group({
        isEmpresa: [true],
        nomeRazaoSocial: ['', Validators.required],
        Endereco: ['', Validators.required],
    });

    public formModelEmpresa: FormGroup = this.fb.group({
        inscricaoEstadual: ['', Validators.required],
        inscricaoMunicipal: ['', Validators.required],
        numeroDeDocumento: ['', Validators.required],
    });

    public formModelPessoa: FormGroup = this.fb.group({
        cpf: ['', Validators.required],
    });

    constructor(private fb: FormBuilder,
        private service: DestinatarioService, private router: Router, private route: ActivatedRoute) {
    }

    public ngOnInit(): void {
        this.formModel.addControl('empresa', this.formModelEmpresa);
        this.formModel.updateValueAndValidity();
        this.formModel.get('isEmpresa').valueChanges.subscribe((isEmpresa: boolean) => {
            //
        });
    }

    public onSubmit(): void {
        this.isLoading = true;
        const cmdDestinatarioAdd: DestinatarioAddCommand = new DestinatarioAddCommand(this.formModel.value);
        this.service.add(cmdDestinatarioAdd)
            .take(1)
            .subscribe(() => {
                this.redirect();
                this.isLoading = false;
            });
    }
    public redirect(): void {
        this.router.navigate(['./../'],
            { relativeTo: this.route });
    }
}
