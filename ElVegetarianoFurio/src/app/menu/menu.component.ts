import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from '../categories/category';
import { DishService } from '../dish.service';
import { Dish } from '../dishes/dish';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  providers: [DishService]
})
export class MenuComponent implements OnInit, OnDestroy {
    categories: Category[] = null!;
    private sub1: Subscription = null!;
    private subs: Subscription[] = [];
    constructor(private dishService: DishService, private router: Router) {
        
    }

    ngOnInit(): void {
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

    gotoDish(dish: Dish): void {
        this.router.navigate(['/dish', dish.id]);
    }

    addDish(): void {
        this.router.navigate(['/dish/add']);
    }
}
