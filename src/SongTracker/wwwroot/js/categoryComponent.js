"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("angular2/core");
var autocompleteComponent_1 = require("./shared/autocompleteComponent");
var http_1 = require("angular2/http");
require('rxjs/Rx');
var errorHandlerService_1 = require("./shared/errorHandlerService");
var CategoryComponent = (function () {
    function CategoryComponent(_http, _errorHandler) {
        this._http = _http;
        this._errorHandler = _errorHandler;
        this.model = {};
        this.categories = [];
    }
    CategoryComponent.prototype.getTags = function () {
        return this._http.get('/api/Categories').map(function (res) { return res.json(); });
    };
    CategoryComponent.prototype.load = function () {
        var _this = this;
        this.loading = true;
        this.getTags().subscribe(function (x) {
            _this.loading = false;
            _this.categories = x;
            _this.loading = false;
            _this.catToEdit = null;
        }, function (error) {
            _this.loading = false;
            _this._errorHandler.handleError(error);
        });
    };
    CategoryComponent.prototype.ngOnInit = function () {
        this.load();
    };
    CategoryComponent.prototype.edit = function (cat) {
        var copy;
        copy = {};
        copy.id = cat.id;
        copy.name = cat.name;
        this.catToEdit = copy;
    };
    CategoryComponent.prototype.addNew = function () {
        this.catToEdit = {};
        this.catToEdit.id = 0;
        this.catToEdit.name = '';
    };
    CategoryComponent.prototype.delete = function (cat) {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        var _this = this;
        this.loading = true;
        var json = JSON.stringify(cat);
        console.log(json);
        _this._http.post('/api/CategoryDelete', json, { headers: headers }).map(function (r) { return r.json(); }).subscribe(function (x) {
            _this.loading = true;
            _this.load();
        }, function (error) {
            _this._errorHandler.handleError(error);
            _this.loading = false;
        });
    };
    CategoryComponent.prototype.save = function () {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        var _this = this;
        this.loading = true;
        var json = JSON.stringify(this.catToEdit);
        console.log(json);
        _this._http.post('/api/Category', json, { headers: headers }).map(function (r) { return r.json(); }).subscribe(function (x) {
            _this.loading = true;
            _this.load();
        }, function (error) {
            _this._errorHandler.handleError(error);
            _this.loading = false;
        });
    };
    CategoryComponent = __decorate([
        core_1.Component({
            templateUrl: '/views/category.html?v=1.3',
            selector: 'category',
            directives: [autocompleteComponent_1.AutoCompleteComponent]
        }), 
        __metadata('design:paramtypes', [http_1.Http, errorHandlerService_1.ErrorHandlerService])
    ], CategoryComponent);
    return CategoryComponent;
}());
exports.CategoryComponent = CategoryComponent;
//# sourceMappingURL=categoryComponent.js.map