import { Produto } from './../../shared/produto.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Router, ActivatedRoute } from '@angular/router';

import { ProdutoResolveService } from '../../shared/produto.service';

@Component({
    templateUrl: './produto-detail.component.html',
})
export class ProdutoDetailComponent implements OnInit, OnDestroy {
    public produto: Produto;
    public isLoading: boolean;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolver: ProdutoResolveService,
        private router: Router, private route: ActivatedRoute) { }

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .do(() => this.isLoading = false)
            .subscribe((produto: Produto) => {
                this.produto = Object.assign(new Produto(), produto);
            });
    }
    public onEdit(): void {
        this.router.navigate(['./edit'], { relativeTo: this.route });
    }

    public redirect(): void {
        this.router.navigate(['./produto']);
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

}
