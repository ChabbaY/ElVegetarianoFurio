import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { toNumber } from 'lodash';
import { Subscription } from 'rxjs';
import { Category } from '../../categories/category';
import { DishService } from '../../dish.service';
import { Dish } from '../dish';
import { DeleteDialogComponent } from '../../delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-dish-detail',
  templateUrl: './dish-detail.component.html',
  styleUrls: ['./dish-detail.component.scss'],
  providers: [DishService]
})
export class DishDetailComponent implements OnInit, OnDestroy {
    public dish: Dish = null!;
    public categories: Category[] = [];
    private subs: Subscription[] = [];
    constructor(private route: ActivatedRoute,
        private dishService: DishService,
        private router: Router,
        private dialog: MatDialog) {
        
    }

    ngOnInit() {
        this.subs.push(this.dishService.getCategories().subscribe(
            (response) => { this.categories = response; },
            (error) => { console.log(error); }
        ));
        this.subs.push(this.route.paramMap.subscribe(map => {
            if (map.has('id')) {
                this.subs.push(this.dishService.getDish(toNumber(map.get('id'))).subscribe(
                    (response) => { this.dish = response; },
                    (error) => { console.log(error); }
                ));
            }
        }));
    }

    ngOnDestroy() {
        this.subs.forEach((sub) => {
            sub.unsubscribe();
        });
    }

    save(): void {
        this.subs.push(this.dishService.saveDish(this.dish).subscribe());
        this.router.navigate(['/']);
    }

    delete(): void {
        let dialogRef = this.dialog.open(DeleteDialogComponent);
        this.subs.push(dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.subs.push(this.dishService.deleteDish(this.dish.id).subscribe());
                this.router.navigate(['/']);
            }
        }));
    }
}
