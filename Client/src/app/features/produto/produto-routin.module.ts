import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutoListComponent } from './produto-list/produto-list.component';
import { ProdutoViewComponent } from './produto-view/produto-view.component';
import { ProdutoResolveService } from './shared/produto.service';
import { ProdutoDetailComponent } from './produto-view/produto-detail/produto-detail.component';
import { ProdutoAddComponent } from './produto-add/produto-add.component';
import { ProdutoEditComponent } from './produto-view/produto-edit/produto-edit.component';

const produtoRoutes: Routes = [
    {
        path: '',
        component: ProdutoListComponent,
    },
    {
        path: 'add',
        component: ProdutoAddComponent,
    },
    {
        path: ':produtoId',
        resolve: {
            produto: ProdutoResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'produto',
            },
        },
        children: [
            {
                path: '',
                component: ProdutoViewComponent,
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
                                component: ProdutoDetailComponent,
                            },
                            {
                                path: 'edit',
                                component: ProdutoEditComponent,
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
    imports: [RouterModule.forChild(produtoRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class ProdutoRoutingModule {}
