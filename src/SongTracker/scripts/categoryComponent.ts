import {Component} from "angular2/core";
import {OnInit} from "angular2/core";
import {AutoCompleteComponent} from "./shared/autocompleteComponent"
import {FocusService} from "./shared/focusService"
import {Router, RouteParams} from "angular2/router"
import {Http, Headers} from "angular2/http"
import 'rxjs/Rx'
import {ErrorHandlerService} from "./shared/errorHandlerService"

@Component({
    templateUrl: '/views/category.html?v=1.3',
    selector: 'category',
    directives: [AutoCompleteComponent]
})

export class CategoryComponent implements OnInit {
    model: any;
    categories: any[];
    loading: boolean;
    catToEdit: any;
    constructor(private _http: Http, private _errorHandler: ErrorHandlerService) {
        this.model = {};
        this.categories = []; 
    }

    
    getTags() {
        return this._http.get('/api/Categories').map(res => res.json());
    }
    load() {
        this.loading = true;
        this.getTags().subscribe(x => {
            this.loading = false;
            this.categories = x;
            this.loading = false;
            this.catToEdit = null;
        },
            error => {
                this.loading = false;
                this._errorHandler.handleError(error)
            });
    }
    ngOnInit() {
        this.load();  
    }
    edit(cat) {
        var copy: any;
        copy = { };
        copy.id = cat.id;
        copy.name = cat.name;
        this.catToEdit = copy;
    }
    addNew() {
        this.catToEdit = {};
        this.catToEdit.id = 0;
        this.catToEdit.name = '';
    }
    delete(cat) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        var _this = this;
        this.loading = true;
        var json = JSON.stringify(cat);
        console.log(json);
        _this._http.post('/api/CategoryDelete', json,
            { headers: headers }).map(r => r.json()).subscribe(x => {
                _this.loading = true;
                _this.load();
            },
            error => {
                _this._errorHandler.handleError(error);
                _this.loading = false;
            });

    }
    save() {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        var _this = this;
        this.loading = true;
        var json = JSON.stringify(this.catToEdit);
        console.log(json);
        _this._http.post('/api/Category', json,
            { headers: headers }).map(r => r.json()).subscribe(x => {
                _this.loading = true;
                _this.load();
            },
            error => {
                _this._errorHandler.handleError(error);
                _this.loading = false;
            });

    }

}
