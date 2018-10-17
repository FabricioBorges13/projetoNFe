import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { DestinatarioService, DestinatarioResolveService } from './../../shared/destinatario.service';
import { DestinatarioUpdateCommand, Destinatario } from './../../shared/destinatario.model';
import { OnInit, Component } from '@angular/core';

@Component({
    templateUrl: './destinatario-edit.component.html',
})
export class DestinatarioEditComponent implements OnInit {

    public destinatario: Destinatario;
    public title: string = 'Criar Destinatarios';
    public isLoading: boolean;
    public formModel: FormGroup = this.fb.group({
        nomeRazaoSocial: ['', Validators.required],
        inscricaoEstadual: ['', Validators.required],
        numeroDeDocumento: ['', Validators.required],
        endereco: ['', Validators.required],
    },
    );

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private fb: FormBuilder,
        private service: DestinatarioService, private resolver: DestinatarioResolveService, private router: Router, private route: ActivatedRoute) {
    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((destinatario: Destinatario) => {
                this.destinatario = Object.assign(new Destinatario(), destinatario);
                this.formModel.patchValue({
                    id: destinatario.id,
                    nomeRazaoSocial: destinatario.nomeRazaoSocial,
                    inscricaoEstadual: destinatario.inscricaoEstadual,
                    numeroDeDocumento: destinatario.numeroDoDocumento,
                });
            });
    }

    public onSubmit(): void {
        this.isLoading = true;
        const cmdDestinatarioAdd: DestinatarioUpdateCommand = new DestinatarioUpdateCommand(this.formModel.value);
        this.service.update(cmdDestinatarioAdd)
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
