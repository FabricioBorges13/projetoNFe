import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICoreConfig, CORE_CONFIG_TOKEN } from '../../../core/core.config';
import { State, toODataString } from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';
import { NDDBreadcrumbService } from '../../../shared/ndd-ng-breadcrumb';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { Router } from '@angular/router';
import { BaseService } from '../../../core/utils';

import { Produto, ProdutoDataCommand, ProdutoDeleteCommand } from './Produto.model';

@Injectable()
export class ProdutoGridService extends BehaviorSubject<GridDataResult>{
    public loading: boolean;

    constructor(
        private httpClient: HttpClient,
        @Inject(CORE_CONFIG_TOKEN) private config: ICoreConfig) {
        super(null);
    }
    public query(state: State): void {
        this.fetch(state).subscribe((result: GridDataResult) => super.next(result));
    }
    protected fetch(state: any): Observable<GridDataResult> {
        const queryStr: string = `${toODataString(state)}&$count=true`;
        this.loading = true;

        return this.httpClient
            .get(`${this.config.apiEndpoint}api/produto?${queryStr}`)
            .map((response: any): GridDataResult => ({
                data: response.items,
                total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class ProdutoService extends BaseService {
    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/produto`;
    }

    public get(id: number): Observable<Produto> {
        return this.http.get(`${this.api}/${id}`).map((response: Produto) => response);
    }

    public getByName(filterValue: string): Observable<Produto[]> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(Descricao), tolower('${filterValue}'))`;

        return this.http
            .get(`${this.api}?${queryStr}`)
            .map((response: any) => response.items);
    }

    public add(produto: ProdutoDataCommand): Observable<boolean> {
        return this.http.post(this.api, produto).map((response: boolean) => response);
    }

    public update(produto: ProdutoDataCommand): Observable<boolean> {
        return this.http.put(this.api, produto).map((response: boolean) => response);
    }

    public delete(produto: ProdutoDeleteCommand): Observable<boolean> {

        return this.deleteRequestWithBody(this.api, produto);
    }
}

@Injectable()
export class ProdutoResolveService extends AbstractResolveService<Produto> {
    constructor(
        private produtoService: ProdutoService,
        private breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'produtoId';
    }
    protected loadEntity(produtoId: number): Observable<Produto> {
        return this.produtoService
            .get(produtoId)
            .take(1)
            .do((produto: Produto) => {
                this.breadcrumbService.setMetadata({
                    id: 'produto',
                    label: produto.codigoProduto,
                    sizeLimit: true,
                });
            });
    }
}
