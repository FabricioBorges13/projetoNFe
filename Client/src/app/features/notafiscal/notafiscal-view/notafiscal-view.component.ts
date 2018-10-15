import { Subject } from 'rxjs/Subject';
import { NotaFiscal } from './../shared/notafiscal.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { NotaFiscalResolveService } from '../shared/notafiscal.service';

@Component({
    templateUrl: './notafiscal-view.component.html',
})
export class NotaFiscalViewComponent implements OnInit, OnDestroy {

    public notaFiscal: NotaFiscal;

    public infoItems: object[];

    public title: string;

    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolver: NotaFiscalResolveService) { }
    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((notaFiscal: NotaFiscal) => {
                this.notaFiscal = notaFiscal;
                this.createProperty();
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public createProperty(): void {
        this.title = this.notaFiscal.chaveAcesso;
    }
}
