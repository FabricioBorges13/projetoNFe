import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { EmitenteValidators } from '../../shared/emitente.validator';
import { EmitenteService, EmitenteResolveService } from '../../shared/emitente.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Emitente, EmitenteFormModel, EmitenteDataCommand } from '../../shared/emitente.model';
import { Subject } from 'rxjs/Subject';

@Component({
    templateUrl: './emitente-edit.component.html',
})
export class EmitenteEditComponent implements OnInit, OnDestroy {
    public emitente: Emitente;
    public isLoading: boolean;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    private form: FormGroup = this.fb.group({
        nomeRazaoSocial: ['', Validators.required],
        numeroDoDocumento: ['', [Validators.required]],
        inscricaoEstadual: ['', [Validators.required]],
        inscricaoMunicipal: ['', Validators.required],
        logradouro: ['', Validators.required],
        bairro: ['', Validators.required],
        numero: ['', Validators.required],
        municipio: ['', Validators.required],
        estado: ['', Validators.required],
    }, {validator: EmitenteValidators.checkCPNJ });
    constructor(private resolver: EmitenteResolveService,
        private service: EmitenteService,
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute) { }

        public ngOnInit(): void {
            this.isLoading = true;
            this.resolver.onChanges
                .takeUntil(this.ngUnsubscribe)
                .do(() => this.isLoading = false)
                .subscribe((emitente: Emitente) => {
                    this.emitente = Object.assign(new Emitente(), emitente);
                    this.form.patchValue({
                        nomeRazaoSocial: emitente.nomeRazaoSocial,
                        numeroDoDocumento: emitente.numeroDoDocumento,
                        inscricaoEstadual: emitente.inscricaoEstadual,
                        inscricaoMunicipal: emitente.inscricaoMunicipal,
                        logradouro: emitente.endereco.logradouro,
                        numero: emitente.endereco.numero,
                        bairro: emitente.endereco.bairro,
                        municipio: emitente.endereco.municipio,
                        estado: emitente.endereco.estado,
                    });
                });
        }
        public submit(): void {
            const cmd: EmitenteDataCommand = new EmitenteDataCommand(this.form.value);
            cmd.id = this.emitente.id,
                this.service.update(cmd)
                    .take(1)
                    .subscribe(() => {
                        this.resolver.resolveFromRouteAndNotify();
                        this.router.navigate(['../'], { relativeTo: this.route });
                    });
        }
        public ngOnDestroy(): void {
            this.ngUnsubscribe.next();
            this.ngUnsubscribe.complete();
        }
        private redirect(): void {
            this.router.navigate(['../'], { relativeTo: this.route });
        }
    }
