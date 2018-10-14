import { TransportadorUpdateCommand } from './../shared/transportador.model';
import { TransportadorResolveService } from './../shared/transportador.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TransportadorService } from '../shared/transportador.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Transportador } from '../shared/transportador.model';
import { Subject } from 'rxjs/Subject';

@Component({
    templateUrl: 'transportador-edit.component.html',
})

export class TransportadorEditComponent implements OnInit, OnDestroy {

    public transportador: Transportador;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    private form: FormGroup = this.fb.group({
        nomeRazaoSocial: ['', Validators.required],
        inscricaoEstadual: ['', Validators.required],
        numeroDocumento: ['', Validators.required],
        logradouro: ['', Validators.required],
        numero: ['', Validators.required],
        bairro: ['', Validators.required],
        municipio: ['', Validators.required],
        estado: ['', Validators.required],
    });

    constructor(private fb: FormBuilder, private service: TransportadorService, private router: Router,
        private resolver: TransportadorResolveService, private route: ActivatedRoute) {
    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((transportador: Transportador) => {
                this.transportador = Object.assign(new Transportador(), transportador);
                this.form.patchValue({
                    nomeRazaoSocial: transportador.nomeRazaoSocial,
                    inscricaoEstadual: transportador.inscricaoEstadual,
                    numeroDocumento: transportador.numeroDoDocumento,
                    logradouro: transportador.endereco.logradouro,
                    numero: transportador.endereco.numero,
                    bairro: transportador.endereco.bairro,
                    municipio: transportador.endereco.municipio,
                    estado: transportador.endereco.estado,
                });
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    public onSubmit(): void {
        const cmd: TransportadorUpdateCommand = new TransportadorUpdateCommand(this.form.value);
        cmd.id = this.transportador.id,
            this.service.put(cmd)
                .take(1)
                .subscribe(() => {
                    this.resolver.resolveFromRouteAndNotify();
                    this.router.navigate(['../'], { relativeTo: this.route });
                });
    }
    private redirect(): void {
        this.router.navigate(['../'], { relativeTo: this.route });
    }
}
