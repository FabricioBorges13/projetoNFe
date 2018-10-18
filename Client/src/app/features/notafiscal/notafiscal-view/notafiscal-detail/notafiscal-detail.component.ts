import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Router, ActivatedRoute } from '@angular/router';

import { NotaFiscalResolveService, ListProdutosInNotaFiscalResolveService } from '../../shared/notafiscal.service';
import { NotaFiscal, ListProdutosInNotaFiscal } from './../../shared/notafiscal.model';

@Component({
    templateUrl: './notaFiscal-detail.component.html',
})
export class NotaFiscalDetailComponent implements OnInit, OnDestroy {
    public notaFiscal: NotaFiscal;
    public listProdutosInNotaFiscal: ListProdutosInNotaFiscal;
    public isLoading: boolean;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolverNotaFIscal: NotaFiscalResolveService,
        private listProdutosInNotaFiscalResolveService: ListProdutosInNotaFiscalResolveService,
        private router: Router, private route: ActivatedRoute) { }

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolverNotaFIscal.onChanges
            .takeUntil(this.ngUnsubscribe)
            .do(() => this.isLoading = false)
            .subscribe((notaFiscal: NotaFiscal) => {
                this.notaFiscal = Object.assign(new NotaFiscal(), notaFiscal);
            });
        this.listProdutosInNotaFiscalResolveService.onChanges
            .takeUntil(this.ngUnsubscribe)
            .do(() => this.isLoading = false)
            .subscribe((listProdutosInNotaFiscal: ListProdutosInNotaFiscal) => {
                this.listProdutosInNotaFiscal = Object.assign(new ListProdutosInNotaFiscal(listProdutosInNotaFiscal));
            });
    }
    public onEdit(): void {
        this.router.navigate(['./edit'], { relativeTo: this.route });
    }

    public redirect(): void {
        this.router.navigate(['./notafiscal']);
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

}
