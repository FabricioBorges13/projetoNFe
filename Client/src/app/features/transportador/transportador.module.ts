import { TransportadorEditComponent } from './transportador-edi/transportador-edit.component';
import { TransportadorAddComponent } from './transportador-add/transportador-add.component';
import { TransportadorDetailComponent } from './transportador-view/transportador-detail/transportador-detail.component';
import { TransportadorListComponent } from './transportador-list/transportador-list.component';
import { NgModule } from '@angular/core';
import { NDDTitlebarModule } from '../../shared/ndd-ng-titlebar/component';
import { NDDTabsbarModule } from '../../shared/ndd-ng-tabsbar/component';
import { GridModule } from '@progress/kendo-angular-grid';
import { SharedModule } from '../../shared/shared.module';
import { TransportadorRouting } from './transportador-routing.module';
import { TransportadorGridService, TransportadorService, TransportadorResolveService } from './shared/transportador.service';
import { TransportadorViewComponent } from './transportador-view/transportador-view.component';
import {Ng2BRPipesModule} from 'ng2-brpipes';

@NgModule({
    imports: [
        TransportadorRouting,
        SharedModule,
        GridModule,
        NDDTabsbarModule,
        NDDTitlebarModule,
        Ng2BRPipesModule,
    ],
    declarations: [
        TransportadorListComponent,
        TransportadorViewComponent,
        TransportadorDetailComponent,
        TransportadorAddComponent,
        TransportadorEditComponent,
    ],
    providers: [
        TransportadorGridService,
        TransportadorService,
        TransportadorResolveService,
    ],

    exports:[
        Ng2BRPipesModule,
    ],
})

export class TransportadorModule {

}
