import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/model/product';
import { ProductServiceService } from 'src/app/services/product-service.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';

@Component({
	selector: 'app-product-form',
	templateUrl: './product-form.component.html',
	styleUrls: [ './product-form.component.css' ]
})
export class ProductFormComponent implements OnInit {
	model: Product;
	categories: any;
	productCategories: Array<any>;
	constructor(
		private productService: ProductServiceService,
		private _snackBar: MatSnackBar,
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private categoryService: CategoryService
	) {
		this.model = new Product();
		this.productCategories = new Array<any>();
	}

	ngOnInit() {
		this.activatedRoute.paramMap.subscribe((params) => {
			if (params != null) {
				this.model.id = Number(params.get('id'));
				this.model.description = params.get('description');
				this.model.name = params.get('name');
				this.model.active = Boolean(params.get('active'));
				this.model.price = Number(params.get('price'));
			}
		});
		this.getCategories();
		this.productCategories.push({});
	}

	getCategories() {
		this.categoryService.get().subscribe(
			(res) => {
				this.categories = res;
			},
			(error) => {
				this._snackBar.open('Erro ao consultar categorias!', 'Ok', {
					duration: 2000
				});
			}
		);
	}

	addCategory() {
		this.productCategories.push({ categoryId: 0 });
	}

	removeCategory(index: any, category: any) {
		this.productService.deleteCategory({ categoryId: category.categoryId, productId: this.model.id }).subscribe(
			(res) => {
				this.productCategories.splice(index, 1);
			},
			(error) => {
				this._snackBar.open('Erro ao remover categoria!', 'Ok', {
					duration: 3000
				});
			}
		);
	}

	changeCategory(category: any) {
		console.log(this.productCategories);
	}

	save() {
		if (this.model.id > 0) {
			this.productService.patch(this.model).subscribe(
				(res) => {
					this._snackBar.open('Produto editado com sucesso!', 'Ok', {
						duration: 2000
					});
					this.router.navigate([ '/product' ]);
				},
				(error) => {
					this._snackBar.open('Erro ao editar produto!', 'Ok', {
						duration: 2000
					});
				}
			);
		} else {
			this.productService.post(this.model).subscribe(
				(res) => {
					this._snackBar.open('Produto cadastrado com sucesso!', 'Ok', {
						duration: 2000
					});
					this.router.navigate([ '/product' ]);
				},
				(error) => {
					this._snackBar.open('Erro ao cadastrar produto!', 'Ok', {
						duration: 2000
					});
				}
			);
		}
	}
}
