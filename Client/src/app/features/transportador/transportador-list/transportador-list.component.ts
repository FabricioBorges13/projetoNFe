import { TransportadorService } from './../shared/transportador.service';
import { Transportador, TransportadorRemoveCommand } from './../shared/transportador.model';
import { Component } from '@angular/core';
import { State } from '@progress/kendo-data-query/dist/es/main';
import { TransportadorGridService } from '../shared/transportador.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
@Component({
    templateUrl: 'transportador-list.component.html',
})

export class TransportadorListComponent extends GridUtilsComponent{
    [x: string]: any;
    public state: State = {
        skip: 0,
        take: 10,
    };

    constructor(private gridService: TransportadorGridService, private transportador: TransportadorService,
                private router: Router, private route: ActivatedRoute) {
        super();
        this.gridService.query(this.createFormattedState());
    }

    public redirectOpenTransportador(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`], { relativeTo: this.route });
    }

    public redirectAddTransportador(): void {
        this.router.navigate(['./', `adicionar`], { relativeTo: this.route });
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridService.query(this.createFormattedState());
        this.selectedRows = [];
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public deleteTransportador(): void {
        const entities: Transportador[] = this.getSelectedEntities();

        const transportadorToDelete: TransportadorRemoveCommand = new TransportadorRemoveCommand();

        transportadorToDelete.transportadorIds = entities.map((p: Transportador) => p.id);

        this.transportador.deleteTransportador(transportadorToDelete)
            .take(1)
            .do(() => this.gridService.loading = false)
            .subscribe(() => {
                this.selectedRows = [];
                this.gridService.query(this.createFormattedState());
            });
    }

}
