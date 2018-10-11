import { Component } from '@angular/core';
import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { EmitenteService, EmitenteGridService } from '../shared/emitente.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EmitenteDeleteCommand } from '../shared/emitente.model';

@Component({
    templateUrl: './emitente-list.component.html',
})
export class EmitenteListComponent extends GridUtilsComponent  {
    constructor(
        private emitenteService: EmitenteService,
        private gridService: EmitenteGridService,
        private router: Router,
        private route: ActivatedRoute) {
        super();
        this.gridService.query(this.createFormattedState());
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

    public deleteEmitente(): void {
        this.gridService.loading = true;
        const emitentesToDelete: EmitenteDeleteCommand = new EmitenteDeleteCommand(this.getSelectedEntities());
        this.emitenteService.delete(emitentesToDelete)
            .take(1)
            .do(() => this.gridService.loading = false)
            .subscribe(() => {
                this.selectedRows = [];
                this.gridService.query(this.createFormattedState());
            });
    }

    public onClick(): void {
        this.router.navigate(['./add'], { relativeTo: this.route });
    }
    public redirectOpenEmitente(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`], { relativeTo: this.route });
    }
}
