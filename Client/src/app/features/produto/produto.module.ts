import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { GridModule } from '@progress/kendo-angular-grid';
import { NDDTitlebarModule } from '../../shared/ndd-ng-titlebar/component';
import { NDDTabsbarModule } from '../../shared/ndd-ng-tabsbar/component';

import { ProdutoGridService, ProdutoService, ProdutoResolveService } from './shared/produto.service';
import { ProdutoListComponent } from './produto-list/produto-list.component';
import { ProdutoRoutingModule } from './produto-routin.module';
import { ProdutoDetailComponent } from './produto-view/produto-detail/produto-detail.component';
import { ProdutoViewComponent } from './produto-view/produto-view.component';
import { ProdutoAddComponent } from './produto-add/produto-add.component';
import { ProdutoEditComponent } from './produto-view/produto-edit/produto-edit.component';
@NgModule({
    declarations: [ProdutoListComponent, ProdutoViewComponent,
        ProdutoDetailComponent, ProdutoAddComponent, ProdutoEditComponent],
    imports: [SharedModule, GridModule, ProdutoRoutingModule, NDDTitlebarModule, NDDTabsbarModule],
    exports: [],
    providers: [ProdutoGridService, ProdutoService, ProdutoResolveService],
})

export class ProdutoModule { }
