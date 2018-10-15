import { DestinatarioService } from './../destinatario/shared/destinatario.service';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { GridModule } from '@progress/kendo-angular-grid';
import { NDDTitlebarModule } from '../../shared/ndd-ng-titlebar/component';
import { NDDTabsbarModule } from '../../shared/ndd-ng-tabsbar/component';

import { NotaFiscalRoutingModule } from './notafiscal-routing.module';
import { NotaFiscalGridService, NotaFiscalService } from './shared/notafiscal.service';
import { NotaFiscalAddComponent } from './notafiscal-add/notafiscal-add.component';
import { NotaFiscalDetailComponent } from './notafiscal-view/notafiscal-detail/notafiscal-detail.component';
import { NotaFiscalListComponent } from './notafiscal-list/notafiscal-list.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { EmitenteService } from '../emitentes/shared/emitente.service';
import { TransportadorService } from '../transportador/shared/transportador.service';

@NgModule({
    declarations: [NotaFiscalListComponent, NotaFiscalAddComponent, NotaFiscalDetailComponent],
    imports: [ SharedModule, GridModule, NotaFiscalRoutingModule, NDDTitlebarModule, NDDTabsbarModule, DropDownsModule ],
    exports: [],
    providers: [NotaFiscalGridService, NotaFiscalService, EmitenteService, TransportadorService, DestinatarioService],
})
export class NotaFiscalModule {}
