import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmitenteService } from '../shared/emitente.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EmitenteDataCommand } from '../shared/emitente.model';
import { EmitenteValidators } from '../shared/emitente.validator';

@Component({
    templateUrl: './emitente-add.component.html',
})
export class EmitenteAddComponent {
    public data: any[];
    public title: string = 'Novo Emitente';
    public form: FormGroup = this.fb.group({
        nomeRazaoSocial: ['', Validators.required],
        numeroDoDocumento: ['', [Validators.required]],
        inscricaoEstadual: ['', [Validators.required]],
        inscricaoMunicipal: ['', Validators.required],
        logradouro: ['', Validators.required],
        bairro: ['', Validators.required],
        numero: ['', Validators.required],
        municipio: ['', Validators.required],
        estado: ['', Validators.required],
    }, {validator: EmitenteValidators.checkCPNJ });
    constructor(private service: EmitenteService,
        private fb: FormBuilder,
        private router: Router) { }

        private submit(): void {
            const productAdd: EmitenteDataCommand = new EmitenteDataCommand(this.form.value);
            this.service.add(productAdd)
                .take(1)
                .do(() => this.redirect())
                .subscribe((res: boolean) => res);
                // tslint:disable-next-line:no-console
                console.log('adicionado emitente');
        }
        private redirect(): void {
            this.router.navigate(['./emitente']);
        }
}
