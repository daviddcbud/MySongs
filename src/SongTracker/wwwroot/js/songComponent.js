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
var SongComponent = (function () {
    function SongComponent(_http, _errorHandler) {
        this._http = _http;
        this._errorHandler = _errorHandler;
        this.onSave = new core_1.EventEmitter();
        this.model = {};
    }
    SongComponent.prototype.getSong = function () {
        return this._http.get('/api/Song?id=' + this.id).map(function (res) { return res.json(); });
    };
    SongComponent.prototype.getTags = function () {
        return this._http.get('/api/Categories').map(function (res) { return res.json(); });
    };
    SongComponent.prototype.newLink = function () {
        var cat;
        cat = {};
        cat.link = '';
        cat.description = '';
        this.model.songLinks.push(cat);
    };
    SongComponent.prototype.newCategory = function () {
        var cat;
        cat = {};
        cat.categoryId = 0;
        this.model.songTags.push(cat);
    };
    SongComponent.prototype.deleteCategory = function (cat) {
        cat.isDeleted = true;
    };
    SongComponent.prototype.deleteLink = function (link) {
        link.isDeleted = true;
    };
    SongComponent.prototype.load = function (id) {
        var _this = this;
        this.id = id;
        this.loading = true;
        this.getSong().subscribe(function (x) {
            _this.loading = false;
            _this.model = x;
        }, function (error) {
            _this.loading = false;
            _this._errorHandler.handleError(error);
        });
    };
    SongComponent.prototype.ngOnInit = function () {
        var _this = this;
        console.log('init song');
        this.getTags().subscribe(function (x) {
            _this.tags = x;
        }, function (error) {
            _this._errorHandler.handleError(error);
        });
    };
    SongComponent.prototype.save = function () {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        var _this = this;
        this.loading = true;
        var json = JSON.stringify(this.model);
        console.log(json);
        _this._http.post('/api/SaveSong', json, { headers: headers }).map(function (r) { return r.json(); }).subscribe(function (x) {
            _this.loading = false;
            _this.onSave.emit(x);
        }, function (error) {
            _this._errorHandler.handleError(error);
        });
    };
    __decorate([
        core_1.Output(), 
        __metadata('design:type', core_1.EventEmitter)
    ], SongComponent.prototype, "onSave", void 0);
    SongComponent = __decorate([
        core_1.Component({
            templateUrl: '/views/song.html?v=1.0',
            selector: 'song',
            directives: [autocompleteComponent_1.AutoCompleteComponent]
        }), 
        __metadata('design:paramtypes', [http_1.Http, errorHandlerService_1.ErrorHandlerService])
    ], SongComponent);
    return SongComponent;
}());
exports.SongComponent = SongComponent;
//# sourceMappingURL=songComponent.js.map