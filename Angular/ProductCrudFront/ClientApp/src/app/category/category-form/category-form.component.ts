import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/model/category';

@Component({
	selector: 'app-category-form',
	templateUrl: './category-form.component.html',
	styleUrls: [ './category-form.component.css' ]
})
export class CategoryFormComponent implements OnInit {
	model: Category;
	parentCategories: Array<any>;
	constructor(
		private categoryService: CategoryService,
		private _snackBar: MatSnackBar,
		private router: Router,
		private activatedRoute: ActivatedRoute
	) {
		this.model = new Category();
	}

	ngOnInit() {
		this.activatedRoute.paramMap.subscribe((params) => {
			if (params != null) {
				this.model.id = Number(params.get('id'));
				this.model.name = params.get('name');
				this.model.parentId = Number(params.get('parentId'));
			}
		});
		this.getParentCategories();
	}

	getParentCategories() {
		this.categoryService.getParentCategories().subscribe(
			(res) => {
				this.parentCategories = res;
				if (this.model.id > 0) {
					let categoryId = this.model.id;
					debugger;
					this.parentCategories = this.parentCategories.filter(function(value) {
						return value.id != categoryId;
					});
					console.log(this.parentCategories);
				}
			},
			(error) => {
				this._snackBar.open('Erro ao carregar categorias!', 'Ok', {
					duration: 3000
				});
			}
		);
	}

	save() {
		if (this.model.id > 0) {
			this.categoryService.patch(this.model).subscribe(
				(res) => {
					this._snackBar.open('Categoria editada com sucesso!', 'Ok', {
						duration: 2000
					});
					this.router.navigate([ '/category' ]);
				},
				(error) => {
					this._snackBar.open('Erro ao editar categoria!', 'Ok', {
						duration: 2000
					});
				}
			);
		} else {
			this.categoryService.post(this.model).subscribe(
				(res) => {
					this._snackBar.open('Categoria cadastrado com sucesso!', 'Ok', {
						duration: 2000
					});
					this.router.navigate([ '/category' ]);
				},
				(error) => {
					this._snackBar.open('Erro ao cadastrar categoria!', 'Ok', {
						duration: 2000
					});
				}
			);
		}
	}
}
