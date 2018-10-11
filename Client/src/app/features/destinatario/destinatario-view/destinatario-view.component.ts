import { DestinatarioResolveService } from './../shared/destinatario.service';
import { Subject } from 'rxjs/Subject';
import { Destinatario } from './../shared/destinatario.model';
import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
    selector: 'ndd-destinatario-view',
    templateUrl: './destinatario-view.component.html',
})
export class DestinatarioViewComponent implements OnInit, OnDestroy {

    public destinatario: Destinatario;

    public infoItems: object[];

    public title: string;

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: DestinatarioResolveService) {

    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((destinatario: Destinatario) => {
                this.destinatario = destinatario;
                this.createProperty();
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public createProperty(): void {
        this.title = this.destinatario.nomeRazaoSocial;
        const inscricaoEstadualDescription: string =
            'Inscrição Estadual: ' + this.destinatario.inscricaoEstadual;
        const numeroDoDocumentoDescription: string =
            'Número de Documento:' + this.destinatario.numeroDoDocumento;

        this.infoItems = [
            {
                value: inscricaoEstadualDescription,
                description: inscricaoEstadualDescription,
            },
            {
                value: numeroDoDocumentoDescription,
                description: numeroDoDocumentoDescription,
            },
        ];
    }
}
