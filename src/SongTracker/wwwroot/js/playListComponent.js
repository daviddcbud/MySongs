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
var core_2 = require("angular2/core");
var autocompleteComponent_1 = require("./shared/autocompleteComponent");
var http_1 = require("angular2/http");
require('rxjs/Rx');
var errorHandlerService_1 = require("./shared/errorHandlerService");
var addSongsComponent_1 = require("./addSongsComponent");
var PlayListComponent = (function () {
    function PlayListComponent(_http, _errorHandler) {
        this._http = _http;
        this._errorHandler = _errorHandler;
        this.playLists = [];
        this.songs = [];
    }
    PlayListComponent.prototype.addSongs = function () {
        this.addingSongs = true;
    };
    PlayListComponent.prototype.doneAdding = function () {
        this.addingSongs = false;
    };
    PlayListComponent.prototype.newSongAdded = function (song) {
        this._addSongsComponent.add(song);
    };
    PlayListComponent.prototype.songsAdded = function (song) {
        this.loadPlayList();
    };
    PlayListComponent.prototype.newPlayList = function () {
        this.editingPlaylist = {};
        this.editingPlaylist.id = 0;
        this.editingPlaylist.description = '';
    };
    Object.defineProperty(PlayListComponent.prototype, "playListId", {
        get: function () {
            return this._playListId;
        },
        set: function (value) {
            this._playListId = value;
            this.loadPlayList();
        },
        enumerable: true,
        configurable: true
    });
    PlayListComponent.prototype.loadPlayList = function () {
        var _this = this;
        if (this._playListId == 0)
            return;
        this.loading = true;
        this._http.get('/api/PlayList?id=' + this._playListId).map(function (res) { return res.json(); })
            .subscribe(function (x) {
            _this.loading = false;
            _this.playList = x;
            _this.loading = false;
        }, function (error) {
            _this.loading = false;
            _this._errorHandler.handleError(error);
        });
    };
    PlayListComponent.prototype.getData = function () {
        return this._http.get('/api/PlayLists').map(function (res) { return res.json(); });
    };
    PlayListComponent.prototype.load = function () {
        var _this = this;
        this.loading = true;
        this.getData().subscribe(function (x) {
            _this.loading = false;
            _this.playLists = x;
            _this.loading = false;
            if (_this.playLists.length > 0) {
                if (_this.editingPlaylist) {
                    _this.playListId = _this.editingPlaylist.id;
                }
                else {
                    _this.playListId = _this.playLists[0].id;
                }
            }
            _this.editingPlaylist = null;
        }, function (error) {
            _this.loading = false;
            _this._errorHandler.handleError(error);
        });
    };
    PlayListComponent.prototype.ngOnInit = function () {
        this.load();
    };
    PlayListComponent.prototype.removeSong = function (song) {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        var _this = this;
        this.loading = true;
        var json = JSON.stringify(song);
        _this._http.post('/api/RemoveSong', json, { headers: headers }).map(function (r) { return r.json(); }).subscribe(function (x) {
            _this.loading = false;
            _this.playList.songs.splice(_this.playList.songs.indexOf(song), 1);
        }, function (error) {
            _this._errorHandler.handleError(error);
            _this.loading = true;
        });
    };
    PlayListComponent.prototype.save = function () {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        var _this = this;
        this.loading = true;
        var json = JSON.stringify(this.editingPlaylist);
        _this._http.post('/api/SavePlayList', json, { headers: headers }).map(function (r) { return r.json(); }).subscribe(function (x) {
            _this.editingPlaylist.id = x;
            _this.loading = true;
            _this.load();
        }, function (error) {
            _this._errorHandler.handleError(error);
            _this.loading = true;
        });
    };
    __decorate([
        core_2.ViewChild(addSongsComponent_1.AddSongsComponent), 
        __metadata('design:type', addSongsComponent_1.AddSongsComponent)
    ], PlayListComponent.prototype, "_addSongsComponent", void 0);
    PlayListComponent = __decorate([
        core_1.Component({
            templateUrl: '/views/playLists.html?v=1.8',
            selector: 'playList',
            directives: [autocompleteComponent_1.AutoCompleteComponent, addSongsComponent_1.AddSongsComponent]
        }), 
        __metadata('design:paramtypes', [http_1.Http, errorHandlerService_1.ErrorHandlerService])
    ], PlayListComponent);
    return PlayListComponent;
}());
exports.PlayListComponent = PlayListComponent;
//# sourceMappingURL=playListComponent.js.map