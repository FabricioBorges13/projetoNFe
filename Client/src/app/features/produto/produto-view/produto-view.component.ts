import { Subject } from 'rxjs/Subject';
import { Produto } from './../shared/produto.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProdutoResolveService } from '../shared/produto.service';

@Component({
    selector: 'ndd-produto-view',
    templateUrl: './produto-view.component.html',
})
export class ProdutoViewComponent implements OnInit, OnDestroy {

    public produto: Produto;

    public infoItems: object[];

    public title: string;

    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolver: ProdutoResolveService) { }
    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((produto: Produto) => {
                this.produto = produto;
                this.createProperty();
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public createProperty(): void {
        this.title = this.produto.codigoProduto;
    }
}
