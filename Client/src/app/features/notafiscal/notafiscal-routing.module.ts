import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotaFiscalListComponent } from './notafiscal-list/notafiscal-list.component';
import { NotaFiscalAddComponent } from './notafiscal-add/notafiscal-add.component';

const notafiscalRoutes: Routes = [
    {
        path: '',
        component: NotaFiscalListComponent,
    },
    {
        path: 'add',
        component: NotaFiscalAddComponent,
    },
    {
        path: ':notafiscalId',
        resolve: {
            /*emitente: NotaFiscalResolveService,*/
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'notafiscal',
            },
        },
        children: [
            {
                path: '',
                /*component: EmitenteViewComponent,*/
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
                                /*component: EmitenteDetailComponent,*/
                            },
                            {
                                path: 'edit',
                                /*component: EmitenteEditComponent,*/
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
    imports: [RouterModule.forChild(notafiscalRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class NotaFiscalRoutingModule { }
