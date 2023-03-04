import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Category } from './categories/category';
import { Dish } from './dishes/dish';

@Injectable({
  providedIn: 'root'
})
export class DishService {

    constructor(private http: HttpClient ) { }

    getCategories() {
        return this.http.get<Category[]>('https://localhost:7106/api/categories');
    }

    getDish(id: number) {
        return this.http.get<Dish>('https://localhost:7106/api/dishes/' + id);
    }

    saveDish(dish: Dish) {
        if (dish.id) {
            return this.http.put('https://localhost:7106/api/dishes/' + dish.id, dish);
        } else {
            return this.http.post('https://localhost:7106/api/dishes', dish);
        }
    }

    deleteDish(id: number) {
        return this.http.delete('https://localhost:7106/api/dishes/' + id)
    }
}
