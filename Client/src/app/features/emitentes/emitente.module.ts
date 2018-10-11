import { EmitenteListComponent } from './emitente-list/emitente-list.component';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { GridModule } from '@progress/kendo-angular-grid';
import { EmitenteRoutingModule } from './emitente-routing.module';
import { NDDTitlebarModule } from '../../shared/ndd-ng-titlebar/component';
import { NDDTabsbarModule } from '../../shared/ndd-ng-tabsbar/component';
import { EmitenteGridService, EmitenteService, EmitenteResolveService } from './shared/emitente.service';
import { EmitenteViewComponent } from './emitente-view/emitente-view.component';
import { EmitenteDetailComponent } from './emitente-view/emitenten-detail/emitente-detail.component';
import { EmitenteAddComponent } from './emitente-add/emitente-add.component';
import { EmitenteEditComponent } from './emitente-view/emitente-edit/emitente-edit.component';

@NgModule({
    declarations: [EmitenteAddComponent, EmitenteEditComponent, EmitenteListComponent, EmitenteViewComponent, EmitenteDetailComponent],
    imports: [ SharedModule, GridModule, EmitenteRoutingModule, NDDTitlebarModule, NDDTabsbarModule ],
    exports: [],
    providers: [EmitenteGridService, EmitenteService, EmitenteResolveService],
})

export class EmitenteModule { }
