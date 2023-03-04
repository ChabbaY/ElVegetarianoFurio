import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forEach } from 'cypress/types/lodash';
import { Subscription } from 'rxjs';
import { Category } from '../../categories/category';
import { DishService } from '../../dish.service';
import { Dish } from '../dish';

@Component({
  selector: 'app-add-dish',
  templateUrl: './add-dish.component.html',
  styleUrls: ['./add-dish.component.scss'],
  providers: [DishService]
})
export class AddDishComponent implements OnInit, OnDestroy {
    public dish: Dish = null!;
    public categories: Category[] = [];
    private subs: Subscription[] = [];
    constructor(private dishService: DishService,
        private router: Router) {
        this.dish = { id:0, name:"", description:"", price:0, categoryId:0};
    }
    
    ngOnInit() {
        this.subs.push(this.dishService.getCategories().subscribe(
            (response) => { this.categories = response; },
            (error) => { console.log(error); }
        ));
    }

    ngOnDestroy(): void {
        this.subs.forEach((sub) => {
            sub.unsubscribe();
        });
    }

    save(): void {
        this.subs.push(this.dishService.saveDish(this.dish).subscribe());
        this.router.navigate(['/']);
    }
}
