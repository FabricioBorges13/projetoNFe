import { Subject } from 'rxjs/Subject';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Produto, ProdutoUpdateCommand } from '../../shared/produto.model';
import { ProdutoService, ProdutoResolveService  } from '../../shared/produto.service';

@Component({
    selector: 'ndd-produto-edit',
    templateUrl: './produto-edit.component.html',
})
export class ProdutoEditComponent implements OnInit {

    public data: any[];
    public produto: Produto;
    public title: string = 'Criar Produtos';
    public isLoading: boolean;
    public formModel: FormGroup = this.fb.group({
        id: ['', Validators.required],
        codigoProduto: ['', Validators.required],
        descricao: ['', Validators.required],
        quantidade: [0, Validators.min(0)],
        valorUnitario: [0, Validators.min(0)],
        valorIcms: [0,  Validators.min(0)],
        valorIpi: [0, Validators.min(0)],
    },
    );

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private fb: FormBuilder,
        private service: ProdutoService, private router: Router, private route: ActivatedRoute,
        private resolver: ProdutoResolveService) {
    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((produto: Produto) => {
                this.produto = Object.assign(new Produto(), produto);
                this.formModel.patchValue(new ProdutoUpdateCommand(this.produto));
            });
    }

    public onSubmit(): void {
        this.isLoading = true;
        const cmdprodutoEdit: ProdutoUpdateCommand = new ProdutoUpdateCommand(this.formModel.value);

        this.service.update(cmdprodutoEdit)
            .take(1)
            .subscribe(() => {
                this.isLoading = false;
                this.redirectOneMoreStep();
            });
    }

    public redirect(): void {
        this.router.navigate(['./../../'],
            { relativeTo: this.route });
    }
    public redirectOneMoreStep(): void {
        this.router.navigate(['./../../../'],
            { relativeTo: this.route });
    }
}
