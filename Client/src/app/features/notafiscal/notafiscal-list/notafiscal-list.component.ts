import { Component, OnInit } from '@angular/core';
import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';

@Component({
    templateUrl: './notafiscal-list.component.html',
})
export class NotaFiscalListComponent extends GridUtilsComponent  {
    constructor() {
        super();
    }
    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        /*this.gridService.query(this.createFormattedState());*/
        this.selectedRows = [];
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

}
