import { Emitente } from './../../shared/emitente.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { EmitenteResolveService } from '../../shared/emitente.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    templateUrl: './emitente-detail.component.html',
})
export class EmitenteDetailComponent implements OnInit, OnDestroy {
    public emitente: Emitente;
    public isLoading: boolean;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    constructor(private resolver: EmitenteResolveService, private router: Router, private route: ActivatedRoute) { }

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .do(() => this.isLoading = false)
            .subscribe((emitente: Emitente) => {
                this.emitente = Object.assign(new Emitente(), emitente);
            });
    }
    public onEdit(): void {
        this.router.navigate(['./edit'], { relativeTo: this.route });
    }

    public redirect(): void {
        this.router.navigate(['./emitente']);
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

}
