import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component } from '@angular/core';

import { ProdutoDataCommand } from '../shared/produto.model';
import { ProdutoService } from '../shared/produto.service';

@Component({
    templateUrl: './produto-add.component.html',
})
export class ProdutoAddComponent {

    public data: any[];
    public form: FormGroup;
    public title: string = 'Cadastrar Produto';
    public isLoading: boolean;

    public formModel: FormGroup = this.fb.group({
        codigoProduto: ['', Validators.required],
        descricao: ['', Validators.required],
        quantidade: [0, Validators.min(0)],
        valorUnitario: [0, Validators.min(0)],
        valorIcms: [0,  Validators.min(0)],
        valorIpi: [0, Validators.min(0)],
      },
    );
    constructor(private fb: FormBuilder,
        private service: ProdutoService, private router: Router,
         private route: ActivatedRoute) {
    }
    public onSubmit(): void {
        this.isLoading = true;
        const cmdProdutoAdd: ProdutoDataCommand = new ProdutoDataCommand(this.formModel.value);
        this.service.add(cmdProdutoAdd)
            .take(1)
            .subscribe(() => {
                this.isLoading = false;
            });
    }

    public redirect(): void {
        this.router.navigate(['./../'],
            { relativeTo: this.route });
    }
}
