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
var songComponent_1 = require("./songComponent");
var AddSongsComponent = (function () {
    function AddSongsComponent(_http, _errorHandler) {
        this._http = _http;
        this._errorHandler = _errorHandler;
        this.onSave = new core_1.EventEmitter();
        this.model = {};
        this.searchOptions = [];
        this.searchOptions.push({ id: 0, name: 'Category' });
        this.searchOptions.push({ id: 1, name: 'Song Name' });
        this.model.searchOption = 0;
        this.categoryId = 0;
        this.model.title = '';
        this.results = [];
    }
    AddSongsComponent.prototype.songSaved = function (song) {
        this.search();
        var modal = $('#songModal');
        modal.modal('hide');
    };
    AddSongsComponent.prototype.add = function (song) {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        var vm;
        vm = {};
        vm.playListId = this.playListId;
        vm.songId = song.id;
        var _this = this;
        this.loading = true;
        var json = JSON.stringify(vm);
        _this._http.post('/api/AddSongToPlayList', json, { headers: headers }).map(function (r) { return r.json(); }).subscribe(function (x) {
            _this.loading = false;
            _this.onSave.emit(song);
            song.added = true;
        }, function (error) {
            _this._errorHandler.handleError(error);
            _this.loading = false;
        });
    };
    AddSongsComponent.prototype.newSong = function () {
        this.songToEdit = {};
        this.songToEdit.id = 0;
        this.songToEdit.name = '';
        this.songToEdit.artist = '';
        this.songToEdit.songLinks = [];
        this.songToEdit.songTags = [];
        this._songComponent.model = this.songToEdit;
        this._songComponent.newCategory();
        var modal = $('#songModal');
        modal.modal('show');
    };
    AddSongsComponent.prototype.editSong = function (song) {
        this.songToEdit = song;
        this._songComponent.load(song.id);
        var modal = $('#songModal');
        modal.modal('show');
    };
    Object.defineProperty(AddSongsComponent.prototype, "categoryId", {
        get: function () {
            return this.model.categoryId;
        },
        set: function (value) {
            this.model.categoryId = value;
            this.search();
        },
        enumerable: true,
        configurable: true
    });
    AddSongsComponent.prototype.tagChosen = function () {
    };
    AddSongsComponent.prototype.onTagSelected = function ($event) {
    };
    AddSongsComponent.prototype.getTags = function () {
        return this._http.get('/api/tags').map(function (res) { return res.json(); });
    };
    AddSongsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.loading = true;
        this.getTags().subscribe(function (x) {
            _this.loading = false;
            _this.tags = x;
            _this.loading = false;
        }, function (error) {
            _this.loading = false;
            _this._errorHandler.handleError(error);
        });
    };
    AddSongsComponent.prototype.search = function () {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        var _this = this;
        this.searching = true;
        var json = JSON.stringify(this.model);
        console.log(json);
        _this._http.post('/api/Search', json, { headers: headers }).map(function (r) { return r.json(); }).subscribe(function (x) {
            _this.searching = false;
            _this.results = x;
        }, function (error) {
            _this._errorHandler.handleError(error);
            _this.searching = false;
        });
    };
    __decorate([
        core_1.ViewChild(songComponent_1.SongComponent), 
        __metadata('design:type', songComponent_1.SongComponent)
    ], AddSongsComponent.prototype, "_songComponent", void 0);
    __decorate([
        core_1.Output(), 
        __metadata('design:type', core_1.EventEmitter)
    ], AddSongsComponent.prototype, "onSave", void 0);
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Object)
    ], AddSongsComponent.prototype, "playListId", void 0);
    AddSongsComponent = __decorate([
        core_1.Component({
            templateUrl: '/views/addsongs.html?v=1.4',
            selector: 'addsongs',
            directives: [autocompleteComponent_1.AutoCompleteComponent, songComponent_1.SongComponent]
        }), 
        __metadata('design:paramtypes', [http_1.Http, errorHandlerService_1.ErrorHandlerService])
    ], AddSongsComponent);
    return AddSongsComponent;
}());
exports.AddSongsComponent = AddSongsComponent;
//# sourceMappingURL=addSongsComponent.js.map