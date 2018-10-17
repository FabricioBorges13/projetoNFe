import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICoreConfig, CORE_CONFIG_TOKEN } from '../../../core/core.config';
import { State, toODataString } from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';
import { BaseService } from '../../../core/utils';
import { Emitente, EmitenteDataCommand, EmitenteDeleteCommand } from './emitente.model';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { NDDBreadcrumbService } from '../../../shared/ndd-ng-breadcrumb';
import { Router } from '@angular/router';

@Injectable()
export class EmitenteGridService extends BehaviorSubject<GridDataResult>{
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
            .get(`${this.config.apiEndpoint}api/emitente?${queryStr}`)
            .map((response: any): GridDataResult => ({
                data: response.items,
                total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class EmitenteService extends BaseService {
    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/emitente`;
    }

    public get(id: number): Observable<Emitente> {
        return this.http.get(`${this.api}/${id}`).map((response: Emitente) => response);
    }

    public getByName(filterValue: string): Observable<Emitente[]> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(nomeRazaoSocial), tolower('${filterValue}'))`;

        return this.http
            .get(`${this.api}?${queryStr}`)
            .map((response: any) => response.items);
    }

    public add(emitente: EmitenteDataCommand): Observable<boolean> {
        return this.http.post(this.api, emitente).map((response: boolean) => response);
    }

    public update(emitente: EmitenteDataCommand): Observable<boolean> {
        return this.http.put(this.api, emitente).map((response: boolean) => response);
    }

    public delete(emitente: EmitenteDeleteCommand): Observable<boolean> {

        return this.deleteRequestWithBody(this.api, emitente);
    }
}

@Injectable()
export class EmitenteResolveService extends AbstractResolveService<Emitente> {
    constructor(
        private emitenteService: EmitenteService,
        private breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'emitenteId';
    }
    protected loadEntity(emitenteId: number): Observable<Emitente> {
        return this.emitenteService
            .get(emitenteId)
            .take(1)
            .do((emitente: Emitente) => {
                this.breadcrumbService.setMetadata({
                    id: 'emitente',
                    label: emitente.nomeRazaoSocial,
                    sizeLimit: true,
                });
            });
    }
}
