
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { Destinatario, DestinatarioUpdateCommand } from '../../shared/destinatario.model';
import { DestinatarioService, DestinatarioResolveService } from '../../shared/destinatario.service';

@Component({
    templateUrl: 'destinatario-edit.component.html',
})

export class DestinatarioEditComponent implements OnInit, OnDestroy {

    public destinatario: Destinatario;
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

    constructor(private fb: FormBuilder, private service: DestinatarioService, private router: Router,
        private resolver: DestinatarioResolveService, private route: ActivatedRoute) {
    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((destinatario: Destinatario) => {
                this.destinatario = Object.assign(new Destinatario(), destinatario);
                this.form.patchValue({
                    nomeRazaoSocial: destinatario.nomeRazaoSocial,
                    inscricaoEstadual: destinatario.inscricaoEstadual,
                    numeroDocumento: destinatario.numeroDocumento,
                    logradouro: destinatario.endereco.logradouro,
                    numero: destinatario.endereco.numero,
                    bairro: destinatario.endereco.bairro,
                    municipio: destinatario.endereco.municipio,
                    estado: destinatario.endereco.estado,
                });
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    public onSubmit(): void {
        const cmd: DestinatarioUpdateCommand = new DestinatarioUpdateCommand(this.form.value);
        cmd.id = this.destinatario.id,
            this.service.update(cmd)
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
