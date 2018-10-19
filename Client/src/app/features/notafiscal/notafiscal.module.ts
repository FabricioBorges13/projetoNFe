import { ProdutoService } from './../produto/shared/produto.service';
import { NotaFiscalViewComponent } from './notafiscal-view/notafiscal-view.component';
import { DestinatarioService } from './../destinatario/shared/destinatario.service';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { GridModule } from '@progress/kendo-angular-grid';
import { NDDTitlebarModule } from '../../shared/ndd-ng-titlebar/component';
import { NDDTabsbarModule } from '../../shared/ndd-ng-tabsbar/component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

import { NotaFiscalRoutingModule } from './notafiscal-routing.module';
import { NotaFiscalGridService, NotaFiscalService, NotaFiscalResolveService,
    ListProdutosInNotaFiscalResolveService } from './shared/notafiscal.service';
import { NotaFiscalAddComponent } from './notafiscal-add/notafiscal-add.component';
import { NotaFiscalDetailComponent } from './notafiscal-view/notafiscal-detail/notafiscal-detail.component';
import { NotaFiscalListComponent } from './notafiscal-list/notafiscal-list.component';
import { EmitenteService } from '../emitentes/shared/emitente.service';
import { TransportadorService } from '../transportador/shared/transportador.service';
import { NotaFiscalEditComponent } from './notafiscal-edit/notafiscal-edit.component';

@NgModule({
    declarations: [NotaFiscalListComponent,
        NotaFiscalAddComponent, NotaFiscalDetailComponent,
        NotaFiscalViewComponent, NotaFiscalEditComponent,
        ],
    imports: [ SharedModule, GridModule,
        NotaFiscalRoutingModule, NDDTitlebarModule, NDDTabsbarModule, DropDownsModule ],
    exports: [],
    providers: [NotaFiscalGridService, NotaFiscalService,
        EmitenteService, TransportadorService, DestinatarioService, ProdutoService, NotaFiscalResolveService,
        ListProdutosInNotaFiscalResolveService],
})
export class NotaFiscalModule {}
