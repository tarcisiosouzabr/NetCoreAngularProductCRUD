import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-category-list',
	templateUrl: './category-list.component.html',
	styleUrls: [ './category-list.component.css' ]
})
export class CategoryListComponent implements OnInit {
	displayedColumns: string[] = [ 'id', 'name', 'action' ];
	dataSource;
	constructor(private categoryService: CategoryService, private _snackBar: MatSnackBar) {}

	ngOnInit() {
		this.getProduct();
	}

	getProduct() {
		this.categoryService.get().subscribe(
			(res) => {
				this.dataSource = res;
			},
			(error) => {
				this._snackBar.open('Erro ao consultar produtos!', 'Ok', {
					duration: 2000
				});
			}
		);
	}

	delete(category: any) {
		this.categoryService.delete(category).subscribe(
			(res) => {
				this.getProduct();
				this._snackBar.open('A categoria ' + category.name + ' foi excluído com sucesso!', 'Ok', {
					duration: 3000
				});
			},
			(error) => {
				this._snackBar.open('Erro ao excluir produto!', 'Ok', {
					duration: 3000
				});
			}
		);
	}
}
