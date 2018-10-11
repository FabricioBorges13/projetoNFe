import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LayoutComponent } from './core/layout/layout.component';

const appRoutes: Routes = [
    {
        path: 'page-not-found',
        loadChildren: './features/error-pages/page-not-found/page-not-found.module#PageNotFoundModule',
    },
    {
        path: '',
        component: LayoutComponent,
        children: [
            {
                path: '',
                redirectTo: 'notafiscal',
                pathMatch: 'full',
            },
            {
                path: 'notafiscal',
                loadChildren: './features/notafiscal/notafiscal.module#NotaFiscalModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Nota Fiscal',
                    },
                },
            },
            {
                path: '',
                redirectTo: 'emitente',
                pathMatch: 'full',
            },
            {
                path: 'emitente',
                loadChildren: './features/emitentes/emitente.module#EmitenteModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Emitente',
                    },
                },
            },
            {
                path: '',
                redirectTo: 'destinatario',
                pathMatch: 'full',
            },
            {
                path: 'destinatario',
                loadChildren: './features/destinatario/destinatario.module#DestinatarioModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Destinatario',
                        redirectTo: 'transportador',
                        pathMatch: 'full',
                    },
                },
            },
            {
                path: 'transportador',
                loadChildren: './features/transportador/transportador.module#TransportadorModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Transportador',
                    },
                },
            },
            {
                path: 'produto',
                loadChildren: './features/produto/produto.module#ProdutoModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'produto',
                    },
                },
            },
        ],
    },
    { path: '**', redirectTo: 'page-not-found', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes, {
        paramsInheritanceStrategy: 'always',
    })],
    exports: [RouterModule],
})
export class AppRoutingModule { }
