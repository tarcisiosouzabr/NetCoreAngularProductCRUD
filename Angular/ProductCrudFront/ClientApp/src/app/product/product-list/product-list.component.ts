import { Component, OnInit } from '@angular/core';
import { ProductServiceService } from 'src/app/services/product-service.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-product-list',
	templateUrl: './product-list.component.html',
	styleUrls: [ './product-list.component.css' ]
})
export class ProductListComponent implements OnInit {
	displayedColumns: string[] = [ 'position', 'name', 'weight', 'symbol', 'action' ];
	dataSource;

	constructor(private productService: ProductServiceService, private _snackBar: MatSnackBar) {}

	ngOnInit() {
		this.getProduct();
	}

	getProduct() {
		this.productService.get().subscribe(
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

	delete(product: any) {
		this.productService.delete(product).subscribe(
			(res) => {
				this.getProduct();
				this._snackBar.open('O Produto ' + product.name + ' foi excluído com sucesso!', 'Ok', {
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
