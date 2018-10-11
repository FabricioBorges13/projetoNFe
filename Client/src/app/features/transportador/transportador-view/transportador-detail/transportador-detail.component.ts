import { Transportador } from './../../shared/transportador.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { TransportadorResolveService } from '../../shared/transportador.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
@Component({
    templateUrl: 'transportador-detail.component.html',
})

export class TransportadorDetailComponent implements OnInit, OnDestroy {
    public availabilityText: string = '';
    public transportador: Transportador;
    public isLoading: boolean = false;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolver: TransportadorResolveService, private route: ActivatedRoute, public router: Router) {
        this.isLoading = true;
    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((transportador: Transportador) => {
                this.transportador = Object.assign(new Transportador(), transportador);
                this.isLoading = false;
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public onEdit(): void {
        this.router.navigate(['./', `editar`], { relativeTo: this.route });
    }
}
