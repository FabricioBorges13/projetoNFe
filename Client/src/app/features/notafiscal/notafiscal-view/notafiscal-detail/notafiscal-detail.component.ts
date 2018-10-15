import { NotaFiscal } from './../../shared/notaFiscal.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Router, ActivatedRoute } from '@angular/router';

import { NotaFiscalResolveService } from '../../shared/notaFiscal.service';

@Component({
    templateUrl: './notaFiscal-detail.component.html',
})
export class NotaFiscalDetailComponent implements OnInit, OnDestroy {
    public notaFiscal: NotaFiscal;
    public isLoading: boolean;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolver: NotaFiscalResolveService,
        private router: Router, private route: ActivatedRoute) { }

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .do(() => this.isLoading = false)
            .subscribe((notaFiscal: NotaFiscal) => {
                this.notaFiscal = Object.assign(new NotaFiscal(), notaFiscal);
            });
    }
    public onEdit(): void {
        this.router.navigate(['./edit'], { relativeTo: this.route });
    }

    public redirect(): void {
        this.router.navigate(['./notaFiscal']);
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

}
