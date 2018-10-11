import { Emitente } from './../../shared/emitente.model';
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
    public data: any[];
    public title: string = 'Novo Emitente';
    public form: FormGroup = this.fb.group({
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
    private ngUnsubscribe: Subject<void> = new Subject<void>();
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
                    this.form.patchValue(new EmitenteFormModel(this.emitente));
                });
        }
        public submit(): void {
            const emitenteUpdate: EmitenteDataCommand = new EmitenteDataCommand(this.form.value);
            emitenteUpdate.id = this.emitente.id;
            this.service.update(emitenteUpdate)
                .take(1)
                .do(() => this.redirect())
                .subscribe((res: boolean) => res);
        }
        public ngOnDestroy(): void {
            this.ngUnsubscribe.next();
            this.ngUnsubscribe.complete();
        }
        private redirect(): void {
            this.resolver.resolveFromRouteAndNotify();
            this.router.navigate(['../'], { relativeTo: this.route });
        }
    }
