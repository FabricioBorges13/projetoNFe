import { Component } from '@angular/core';
import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { Router, ActivatedRoute } from '@angular/router';

import { ProdutoDeleteCommand } from '../shared/produto.model';
import { ProdutoService, ProdutoGridService } from '../shared/produto.service';

@Component({
    templateUrl: './produto-list.component.html',
})
export class ProdutoListComponent extends GridUtilsComponent  {
    constructor(
        private produtoService: ProdutoService,
        private gridService: ProdutoGridService,
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

    public deleteProduto(): void {
        this.gridService.loading = true;
        const productsToDelete: ProdutoDeleteCommand = new ProdutoDeleteCommand(this.getSelectedEntities());
        this.produtoService.delete(productsToDelete)
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
    public redirectOpenProduto(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`], { relativeTo: this.route });
    }
}
