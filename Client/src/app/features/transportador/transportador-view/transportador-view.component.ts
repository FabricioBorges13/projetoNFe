import { Component, OnInit, OnDestroy } from '@angular/core';
import { Transportador } from '../shared/transportador.model';
import { Subject } from 'rxjs/Subject';
import { TransportadorResolveService } from '../shared/transportador.service';

@Component({
    templateUrl: 'transportador-view.component.html',
})

export class TransportadorViewComponent implements OnInit, OnDestroy {

    public infoItems: object[];
    public title: string;
    public transportador: Transportador;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: TransportadorResolveService) {
    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((transportador: Transportador) => {
                this.transportador = transportador;
                this.createProperty();
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public createProperty(): void {
        this.title = this.transportador.nomeRazaoSocial;
    }
}
