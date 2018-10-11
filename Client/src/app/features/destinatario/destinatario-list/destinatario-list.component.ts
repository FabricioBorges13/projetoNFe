import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { DestinatarioGridService, DestinatarioService } from './../shared/destinatario.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Destinatario, DestinatarioDeleteCommand } from './../shared/destinatario.model';
import { GridUtilsComponent } from './../../../shared/grid-utils/grid-utils-component';
import { Component, OnInit } from '@angular/core';

@Component({
    templateUrl: './destinatario-list.component.html',
})

export class DestinatarioListComponent extends GridUtilsComponent {
    public static idArray: number[];

    public state: State = {
        skip: 0,
        take: 10,
    };

    constructor(public gridService: DestinatarioGridService, private service: DestinatarioService,
        private router: Router, private route: ActivatedRoute) {
        super();
        this.gridService.query(this.state);
    }

    public redirectOpenDestinatario(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`],
            { relativeTo: this.route });
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridService.query(this.createFormattedState());
        this.selectedRows = [];
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
        this.SelectsIds();
    }

    public SelectsIds(): number[] {
        DestinatarioListComponent.idArray = [];
        for (const item of this.getSelectedEntities()) {
            DestinatarioListComponent.idArray.push(item.id);
        }

        return DestinatarioListComponent.idArray;
    }
    public deleteDestinatario(): void {
        this.gridService.loading = true;
        const destinatarioDeleteCommand: DestinatarioDeleteCommand = new DestinatarioDeleteCommand(this.getSelectedEntities());
        this.service.delete(destinatarioDeleteCommand)
            .take(1)
            .do(() => this.gridService.loading = false)
            .subscribe(() => {
                this.gridService.query(this.createFormattedState());
                this.selectedRows = [];
            });
    }

    public onClickAddButton(): void {
        this.router.navigate(['./adicionar'],
            { relativeTo: this.route });
    }
}
