import { DestinatarioViewComponent } from './destinatario-view/destinatario-view.component';
import { DestinatarioListComponent } from './destinatario-list/destinatario-list.component';
import { DestinatarioEditComponent } from './destinatario-view/destinatario-edit/destinatario-edit.component';
import { DestinatarioDetailComponent } from './destinatario-view/destinatario-detail/destinatario-detail.component';
import { DestinatarioAddComponent } from './destinatario-add/destinatario-add.component';
import { DestinatarioResolveService, DestinatarioGridService, DestinatarioService } from './shared/destinatario.service';
import { NDDTabsbarModule } from './../../shared/ndd-ng-tabsbar/component/ndd-ng-tabsbar.module';
import { NDDTitlebarModule } from './../../shared/ndd-ng-titlebar/component/ndd-ng-titlebar.module';
import { GridModule } from '@progress/kendo-angular-grid';
import { SharedModule } from './../../shared/shared.module';
import { DestinatarioRoutingModule } from './destinatario-routing.module';
import { NgModule } from '@angular/core';

@NgModule({
    declarations: [
        DestinatarioAddComponent,
        DestinatarioDetailComponent,
        DestinatarioEditComponent,
        DestinatarioListComponent,
        DestinatarioViewComponent],
    imports: [
        DestinatarioRoutingModule,
        SharedModule,
        GridModule,
        NDDTitlebarModule,
        NDDTabsbarModule,
    ],
    exports: [],
    providers: [DestinatarioService, DestinatarioResolveService, DestinatarioGridService],
})
export class DestinatarioModule { }
