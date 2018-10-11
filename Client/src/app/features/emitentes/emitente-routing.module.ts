import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmitenteListComponent } from './emitente-list/emitente-list.component';
import { EmitenteViewComponent } from './emitente-view/emitente-view.component';
import { EmitenteResolveService } from './shared/emitente.service';
import { EmitenteDetailComponent } from './emitente-view/emitenten-detail/emitente-detail.component';
import { EmitenteEditComponent } from './emitente-view/emitente-edit/emitente-edit.component';
import { EmitenteAddComponent } from './emitente-add/emitente-add.component';

const emitenteRoutes: Routes = [
    {
        path: '',
        component: EmitenteListComponent,
    },
    {
        path: 'add',
        component: EmitenteAddComponent,
    },
    {
        path: ':emitenteId',
        resolve: {
            emitente: EmitenteResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'emitente',
            },
        },
        children: [
            {
                path: '',
                component: EmitenteViewComponent,
                children: [
                    {
                        path: '',
                        redirectTo: 'info',
                        pathMatch: 'full',
                    },
                    {
                        path: 'info',
                        children: [
                            {
                                path: '',
                                component: EmitenteDetailComponent,
                            },
                            {
                                path: 'edit',
                                component: EmitenteEditComponent,
                            },
                        ],
                    },
                ],
            },
        ],
    },
];
@NgModule({
    declarations: [],
    imports: [RouterModule.forChild(emitenteRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class EmitenteRoutingModule {}
