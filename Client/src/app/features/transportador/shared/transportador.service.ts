import { BaseService } from './../../../core/utils/base-service';
import { CORE_CONFIG_TOKEN, ICoreConfig } from './../../../core/core.config';
import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { State, toODataString } from '@progress/kendo-data-query/dist/es/main';
import { Observable } from 'rxjs/Observable';
import { AUTOCOMPLETE_VALUE_ACCESSOR } from '@progress/kendo-angular-dropdowns/dist/es/autocomplete.component';
import { Transportador, TransportadorRemoveCommand, TransportadorAddCommand, TransportadorUpdateCommand } from './transportador.model';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { NDDBreadcrumbService } from '../../../shared/ndd-ng-breadcrumb';
import { Router } from '@angular/router';

@Injectable()

export class TransportadorGridService extends BehaviorSubject<GridDataResult> {

    public loading: boolean;

    constructor(private httpClient: HttpClient, @Inject(CORE_CONFIG_TOKEN) private config: ICoreConfig) {
        super(null);
    }

    public query(state: State): void {
        this.fetch(state).subscribe((result: GridDataResult) => super.next(result));
    }

    protected fetch(state: any): Observable<GridDataResult> {
        const queryStr: string = `${toODataString(state)}&$count=true`;
        this.loading = true;

        return this.httpClient
            .get(`${this.config.apiEndpoint}api/transportador?${queryStr}`)
            .map((response: any): GridDataResult => ({
                data: response.items,
                total: parseInt(response.count, 10),
            })).do(() => this.loading = false);
    }
}

@Injectable()

export class TransportadorService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/transportador`;
    }
    public getByName(filterValue: string): Observable<Transportador[]> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(nomeRazaoSocial), tolower('${filterValue}'))`;

        return this.http
            .get(`${this.api}?${queryStr}`)
            .map((response: any) => response.items);
    }

    public get(id: number): Observable<Transportador> {
        return this.http.get(`${this.api}/${id}`).map((response: Transportador) => {
            return response;
        });
    }

   public post(cmd: TransportadorAddCommand): Observable<number> {
        return this.http.post(this.api, cmd).map((response: number)=> response);
    }

    public put(cmd: TransportadorUpdateCommand): Observable<number> {
        return this.http.put(this.api, cmd).map((response: number)=> response);
    }

    public deleteTransportador(cmd: TransportadorRemoveCommand): Observable<boolean> {
        return this.deleteRequestWithBody(this.api, cmd);
    }
}

@Injectable()
export class TransportadorResolveService extends AbstractResolveService<Transportador> {

    constructor(private service: TransportadorService, router: Router, private breadcrumbService: NDDBreadcrumbService) {
        super(router);
        this.paramsProperty = 'transportadorId';
    }
    protected loadEntity(entityId: number): Observable<Transportador> {
        return this.service
            .get(entityId)
            .take(1)
            .do((transportador: Transportador) => {
                this.breadcrumbService.setMetadata({
                    id: 'transportador',
                    label: transportador.nomeRazaoSocial,
                    sizeLimit: true,
                });
            });
    }
}
