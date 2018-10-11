import { DestinatarioEditComponent } from './destinatario-view/destinatario-edit/destinatario-edit.component';
import { DestinatarioDetailComponent } from './destinatario-view/destinatario-detail/destinatario-detail.component';
import { DestinatarioAddComponent } from './destinatario-add/destinatario-add.component';
import { DestinatarioListComponent } from './destinatario-list/destinatario-list.component';
import { Routes, RouterModule } from '@angular/router';
import { DestinatarioViewComponent } from './destinatario-view/destinatario-view.component';
import { DestinatarioResolveService } from './shared/destinatario.service';
import { NgModule } from '@angular/core';
const destinatarioRoutes: Routes = [
    {
        path: '',
        component: DestinatarioListComponent,
    },
    {
        path: 'adicionar',
        component: DestinatarioAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'Adicionar Destinatario',
            },
        },
    },
    {
        path: ':destinatarioId',
        resolve: {
            destinatario: DestinatarioResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'destinatario',
            },
        },
        children: [
            {
                path: '',
                component: DestinatarioViewComponent,
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
                                component: DestinatarioDetailComponent,
                            },
                            {
                                path: 'edit',
                                component: DestinatarioEditComponent,
                                data:{
                                    breadcrumbOptions: {
                                        breadcrumbId: 'Editar-Destinatario',
                                    },
                                },
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
    imports: [RouterModule.forChild(destinatarioRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class DestinatarioRoutingModule {

}
