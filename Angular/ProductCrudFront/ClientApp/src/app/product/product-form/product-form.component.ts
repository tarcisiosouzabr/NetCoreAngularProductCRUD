import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/model/product';
import { ProductServiceService } from 'src/app/services/product-service.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';
import { environment } from 'src/environments/environment';

@Component({
	selector: 'app-product-form',
	templateUrl: './product-form.component.html',
	styleUrls: [ './product-form.component.css' ]
})
export class ProductFormComponent implements OnInit {
	model: Product;
	categories: any;
	productCategories: Array<any>;
	srcResult;
	images;
	env;
	constructor(
		private productService: ProductServiceService,
		private _snackBar: MatSnackBar,
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private categoryService: CategoryService
	) {
		this.model = new Product();
		this.productCategories = new Array<any>();
		this.env = environment;
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
		this.loadProductCategory();
		this.getProductImages();
		if (this.productCategories.length <= 0) {
			this.productCategories.push({});
		}
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
		this.productCategories.splice(index, 1);
	}

	loadProductCategory() {
		if (this.model.id > 0) {
			this.categoryService.getProductCategories(this.model.id).subscribe(
				(res) => {
					this.productCategories = res.map(function(x) {
						return { categoryId: x.id };
					});
					console.log(this.productCategories);
				},
				(error) => {
					this._snackBar.open('Erro ao carregar categorias.', 'Ok', {
						duration: 3000
					});
				}
			);
		}
	}

	getProductImages() {
		this.images = [];
		this.productService.getProductImages(this.model).subscribe(
			(res) => {
				this.images = res;
			},
			(error) => {
				this._snackBar.open('Erro ao consultar imagens!', 'Ok', {
					duration: 2000
				});
			}
		);
	}

	save() {
		if (this.model.id > 0) {
			this.productService.patch(this.model).subscribe(
				(res) => {
					this._snackBar.open('Produto editado com sucesso!', 'Ok', {
						duration: 3000
					});
					this.productService
						.updateCategory({
							productId: this.model.id,
							categories: this.productCategories.map((x) => x.categoryId)
						})
						.subscribe(
							(res) => {},
							(error) => {
								this._snackBar.open('Erro ao atualizar categorias!', 'Ok', {
									duration: 3000
								});
							}
						);
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
					this.productService
						.updateCategory({
							productId: res.id,
							categories: this.productCategories.map((x) => x.categoryId)
						})
						.subscribe(
							(res) => {},
							(error) => {
								this._snackBar.open('Erro ao atualizar categorias!', 'Ok', {
									duration: 3000
								});
							}
						);
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

	onFileSelected() {
		const inputNode: any = document.querySelector('#file');

		if (typeof FileReader !== 'undefined') {
			const reader = new FileReader();

			reader.onload = (e: any) => {
				this.srcResult = e.target.result;
			};
			reader.onloadend = (e) => {
				if (this.model.id > 0) {
					this.productService
						.uploadImage({
							productId: this.model.id,
							imageBase64: reader.result
						})
						.subscribe(
							(res) => {
								this.getProductImages();
								this._snackBar.open('Imagem enviada com sucesso!', 'Ok', {
									duration: 3000
								});
							},
							(error) => {
								this._snackBar.open('Erro ao enviar imagem!', 'Ok', {
									duration: 3000
								});
							}
						);
				}
			};
			console.log(reader.readAsDataURL(inputNode.files[0]));
		}
	}

	deleteImage(img: any) {
		this.productService.deleteImage({ Id: img.id, ProductId: this.model.id }).subscribe(
			(res) => {
				this.getProductImages();
				this._snackBar.open('Imagem excluída com sucesso!', 'Ok', {
					duration: 3000
				});
			},
			(error) => {
				this._snackBar.open('Erro ao excluir imagem!', 'Ok', {
					duration: 3000
				});
			}
		);
	}
}
