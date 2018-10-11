import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotaFiscalListComponent } from './notafiscal-list/notafiscal-list.component';

const notafiscalRoutes: Routes = [
    {
        path: '',
        component: NotaFiscalListComponent,
    },
]
@NgModule({
    declarations: [],
    imports: [RouterModule.forChild(notafiscalRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class NotaFiscalRoutingModule {}