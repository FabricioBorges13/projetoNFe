import { Component, OnInit } from '@angular/core';
import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { NotaFiscalService, NotaFiscalGridService } from '../shared/notafiscal.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NotaFiscalDeleteCommand } from '../shared/notafiscal.model';

@Component({
    templateUrl: './notafiscal-list.component.html',
})
export class NotaFiscalListComponent extends GridUtilsComponent  {
    constructor(
        private notafiscalService: NotaFiscalService,
        private gridService: NotaFiscalGridService,
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

    public deleteNotaFiscal(): void {
        this.gridService.loading = true;
        const notasToDelete: NotaFiscalDeleteCommand = new NotaFiscalDeleteCommand(this.getSelectedEntities());
        this.notafiscalService.delete(notasToDelete)
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
    public redirectOpenNotaFiscal(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`], { relativeTo: this.route });
    }
}
