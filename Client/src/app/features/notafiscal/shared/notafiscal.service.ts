import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { HttpClient } from '@angular/common/http';
import { ICoreConfig, CORE_CONFIG_TOKEN } from '../../../core/core.config';
import { State, toODataString } from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';
import { BaseService } from '../../../core/utils';
import { NotaFiscal, NotaFiscalDeleteCommand, NotaFiscalDataCommand, ListProdutosInNotaFiscal } from './notafiscal.model';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { Router } from '@angular/router';
import { NDDBreadcrumbService } from '../../../shared/ndd-ng-breadcrumb';

@Injectable()
export class NotaFiscalGridService extends BehaviorSubject<GridDataResult>{

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
            .get(`${this.config.apiEndpoint}api/notafiscal?${queryStr}`)
            .map((response: any): GridDataResult => ({
                data: response.items,
                total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class NotaFiscalService extends BaseService {
    private api: string;
    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/notafiscal`;
    }

    public get(id: number): Observable<NotaFiscal> {
        return this.http.get(`${this.api}/${id}`).map((response: NotaFiscal) => response);
    }

    public getListOfProducts(id: number): Observable<ListProdutosInNotaFiscal> {
        return this.http.get(`${this.api}/${id}/produtos`).map((response: ListProdutosInNotaFiscal) => response);
    }

    public add(notafiscal: NotaFiscalDataCommand): Observable<boolean> {
        return this.http.post(this.api, notafiscal).map((response: boolean) => response);
    }

    public update(notafiscal: NotaFiscalDataCommand): Observable<boolean> {
        return this.http.put(this.api, notafiscal).map((response: boolean) => response);
    }

    public delete(notafiscal: NotaFiscalDeleteCommand): Observable<boolean> {

        return this.deleteRequestWithBody(this.api, notafiscal);
    }
}

@Injectable()
export class NotaFiscalResolveService extends AbstractResolveService<NotaFiscal> {
    constructor(
        private notaFiscalService: NotaFiscalService,
        private breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'notafiscalId';
    }
    protected loadEntity(notaFiscalId: number): Observable<NotaFiscal> {
        return this.notaFiscalService
            .get(notaFiscalId)
            .take(1)
            .do((notaFiscal: NotaFiscal) => {
                this.breadcrumbService.setMetadata({
                    id: 'notaFiscal',
                    label: notaFiscal.chaveAcesso,
                    sizeLimit: true,
                });
            });
    }
}

@Injectable()
export class ListProdutosInNotaFiscalResolveService extends AbstractResolveService<ListProdutosInNotaFiscal> {
    constructor(
        private notaFiscalService: NotaFiscalService,
        private breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'notafiscalId';
    }
    protected loadEntity(notaFiscalId: number): Observable<ListProdutosInNotaFiscal> {
        return this.notaFiscalService
            .getListOfProducts(notaFiscalId)
            .take(1)
            .do((listProdutosInNotaFiscal: ListProdutosInNotaFiscal) => {
                this.breadcrumbService.setMetadata({
                    id: 'notaFiscal',
                    label: listProdutosInNotaFiscal.items.length.toString(),
                    sizeLimit: true,
                });
            });
    }
}
