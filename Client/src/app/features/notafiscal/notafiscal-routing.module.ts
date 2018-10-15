import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotaFiscalListComponent } from './notafiscal-list/notafiscal-list.component';
import { NotaFiscalAddComponent } from './notafiscal-add/notafiscal-add.component';
import { NotaFiscalResolveService } from './shared/notafiscal.service';
import { NotaFiscalViewComponent } from './notafiscal-view/notafiscal-view.component';
import { NotaFiscalDetailComponent } from './notafiscal-view/notafiscal-detail/notafiscal-detail.component';

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
            notafiscal: NotaFiscalResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'notafiscal',
            },
        },
        children: [
            {
                path: '',
                component: NotaFiscalViewComponent,
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
                                component: NotaFiscalDetailComponent,
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
