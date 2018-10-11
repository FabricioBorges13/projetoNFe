import { Router, ActivatedRoute } from '@angular/router';
import { DestinatarioResolveService } from './../../shared/destinatario.service';
import { Subject } from 'rxjs/Subject';
import { Destinatario } from './../../shared/destinatario.model';
import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
    selector: 'ndd-destinatario-detail',
    templateUrl: './destinatario-detail.component.html',
})

export class DestinatarioDetailComponent implements OnInit, OnDestroy {

    public destinatario: Destinatario;
    public availabilityText: string = '';
    public isLoading: boolean = false;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: DestinatarioResolveService, private router: Router, private route: ActivatedRoute) {
        this.isLoading = true;
    }

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((destinatario: Destinatario) => {
                this.isLoading = false;
                this.destinatario = Object.assign(new Destinatario(), destinatario);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    public onEdit(): void {
        this.router.navigate(['./edit'],
            { relativeTo: this.route });
    }
    public redirect(): void {
        this.router.navigate(['./destinatarios']);
    }

}
