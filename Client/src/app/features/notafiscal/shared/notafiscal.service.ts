import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { HttpClient } from '@angular/common/http';
import { ICoreConfig, CORE_CONFIG_TOKEN } from '../../../core/core.config';
import { State, toODataString } from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';
import { BaseService } from '../../../core/utils';
import { NotaFiscal, NotaFiscalDeleteCommand } from './notafiscal.model';

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
        this.api = `${config.apiEndpoint}api/emitente`;
    }

    public get(id: number): Observable<NotaFiscal> {
        return this.http.get(`${this.api}/${id}`).map((response: NotaFiscal) => response);
    }
/*
    public getByName(filterValue: string): Observable<Emitente[]> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(NomeRazaoSocial), tolower('${filterValue}'))`;

        return this.http
            .get(`${this.api}?${queryStr}`)
            .map((response: any) => response.items);
    }

    public add(emitente: EmitenteDataCommand): Observable<boolean> {
        return this.http.post(this.api, emitente).map((response: boolean) => response);
    }

    public update(emitente: EmitenteDataCommand): Observable<boolean> {
        return this.http.put(this.api, emitente).map((response: boolean) => response);
    }*/

    public delete(notafiscal: NotaFiscalDeleteCommand): Observable<boolean> {

        return this.deleteRequestWithBody(this.api, notafiscal);
    }

}
