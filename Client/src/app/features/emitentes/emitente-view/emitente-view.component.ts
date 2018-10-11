import { Component, OnInit, OnDestroy } from '@angular/core';
import { Emitente } from '../shared/emitente.model';
import { Subject } from 'rxjs/Subject';
import { EmitenteResolveService } from '../shared/emitente.service';

@Component({
    templateUrl: './emitente-view.component.html',
})
export class EmitenteViewComponent implements OnInit, OnDestroy {
    public emitente: Emitente;
    public infoItems: object[];
    public title: string;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolver: EmitenteResolveService) { }
    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((emitente: Emitente) => {
                this.emitente = emitente;
                this.createProperty();
            });
    }
    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    public createProperty(): void {
        this.title = this.emitente.nomeRazaoSocial;
        const emitenteNumeroDocumento: string = 'CPF/CNPJ: ' + this.emitente.numeroDoDocumento;
        const emitenteInscricaoEstadual: string = 'Inscrição Estadual: ' + this.emitente.inscricaoEstadual;
        const emitenteInscricaoMunicipal: string = 'Inscrição Municipal: ' + this.emitente.inscricaoMunicipal;

        this.infoItems = [
            {
                value: emitenteNumeroDocumento,
                description: emitenteNumeroDocumento,
            },
            {
                value: emitenteInscricaoEstadual,
                description: emitenteInscricaoEstadual,
            },
            {
                value: emitenteInscricaoMunicipal,
                description: emitenteInscricaoMunicipal,
            },
        ];
    }
}
