import { TransportadorEditComponent } from './transportador-edi/transportador-edit.component';

import { TransportadorViewComponent } from './transportador-view/transportador-view.component';
import { TransportadorDetailComponent } from './transportador-view/transportador-detail/transportador-detail.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TransportadorListComponent } from './transportador-list/transportador-list.component';
import { TransportadorResolveService } from './shared/transportador.service';
import { TransportadorAddComponent } from './transportador-add/transportador-add.component';

const transportadorRoutes: Routes = [
    {
        path: 'adicionar',
        component: TransportadorAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Adicionar',
            },
        },
    },
    {
        path: '',
        component: TransportadorListComponent,

    },
    {
        path: ':transportadorId',
        resolve: {
            transportador: TransportadorResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'transportador',
            },
        },
        children: [
            {
                path: '',
                component: TransportadorViewComponent,

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
                                component: TransportadorDetailComponent,
                            },
                              {
                                  path: 'editar',
                                  component: TransportadorEditComponent,
                              },
                        ],
                    },
                ],
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(transportadorRoutes)],
    exports: [RouterModule],
    declarations: [],
    providers: [],
})

export class TransportadorRouting {

}
