import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { NDDBreadcrumbService } from './../../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.service';
import { DestinatarioDeleteCommand, Destinatario, DestinatarioAddCommand, DestinatarioUpdateCommand } from './destinatario.model';
import { BaseService } from './../../../core/utils/base-service';
import { toODataString } from '@progress/kendo-data-query/dist/es/main';
import { Observable } from 'rxjs/Observable';
import { State } from '@progress/kendo-data-query';
import { ICoreConfig, CORE_CONFIG_TOKEN } from './../../../core/core.config';
import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Router } from '@angular/router';

export class DestinatarioGridService extends BehaviorSubject<GridDataResult> {
    public loading: boolean;

    constructor(private httpClient: HttpClient,
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
            .get(`${this.config.apiEndpoint}api/destinatario?${queryStr}`)
            .map((response: any): GridDataResult => ({
                data: response.items,
                total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class DestinatarioService extends BaseService {
    private api: string;

    constructor( @Inject(CORE_CONFIG_TOKEN) config: ICoreConfig,

        public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/destinatario`;
    }

    public delete(cmd: DestinatarioDeleteCommand): Observable<boolean> {
        return this.deleteRequestWithBody(this.api, cmd);
    }

    public get(id: number): Observable<Destinatario> {
        return this.http.get(`${this.api}/${id}`).map((response: Destinatario) => response);
    }

    public getByNome(filterValue: string): any {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(name), tolower('${filterValue}'))`;

        return this.http
            .get(`${this.api}?${queryStr}`)
            .map((response: any) => response.items);
    }

    public add(cmd: DestinatarioAddCommand): Observable<boolean> {
        return this.http.post(this.api, cmd).map((response: boolean) => response);
    }

    public update(cmd: DestinatarioUpdateCommand): Observable<boolean> {
        return this.http.put(this.api, cmd).map((response: boolean) => response);
    }

}

@Injectable()
export class DestinatarioResolveService extends AbstractResolveService<Destinatario>{
    constructor(private destinatarioService: DestinatarioService,
        router: Router,
        private breadcrumbService: NDDBreadcrumbService) {
        super(router);
        this.paramsProperty = 'destinatarioId';
    }

    protected loadEntity(destinatarioId: number): Observable<Destinatario> {
        return this.destinatarioService
            .get(destinatarioId)
            .take(1)
            .do((destinatario: Destinatario) => {
                this.breadcrumbService.setMetadata({
                    id: 'destinatario',
                    label: destinatario.nomeRazaoSocial,
                    sizeLimit: true,
                });
            });
    }
}
