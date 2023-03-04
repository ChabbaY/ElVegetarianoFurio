import { Dish } from "../dishes/dish";

export interface Category {
    id: number;
    name: string;
    description: string;
    dishes: Dish[];
}