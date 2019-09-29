import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

const options = {
	headers: new HttpHeaders({
		'Content-Type': 'application/json'
	}),
	body: {
		id: 1,
		name: 'test'
	}
};

@Injectable({
	providedIn: 'root'
})
export class CategoryService {
	constructor(private http: HttpClient) {}

	private extractData(res: Response) {
		let body = res;
		return body || {};
	}

	public get(): Observable<any> {
		return this.http.get(environment.urlApi + 'category/get').pipe(map(this.extractData));
	}

	public post(category: any): any {
		return this.http.post(environment.urlApi + 'category/create', category);
	}

	public patch(category: any): any {
		return this.http.patch(environment.urlApi + 'category/edit', category);
	}

	public delete(category: any): any {
		options.body = category;
		return this.http.delete(environment.urlApi + 'category/delete', options);
	}

	public getProductCategories(productId: any): any {
		return this.http
			.get(environment.urlApi + 'category/getProduct?productId=' + productId)
			.pipe(map(this.extractData));
	}

	public getParentCategories(): any {
		return this.http.get(environment.urlApi + 'category/getParentCategories').pipe(map(this.extractData));
	}
}
