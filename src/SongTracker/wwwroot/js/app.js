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
var router_1 = require("angular2/router");
var errorHandlerService_1 = require("./shared/errorHandlerService");
var searchComponent_1 = require("./searchComponent");
var categoryComponent_1 = require("./categoryComponent");
var playListComponent_1 = require("./playListComponent");
var AppComponent = (function () {
    function AppComponent(_router, _errorHandler) {
        this._router = _router;
        this._errorHandler = _errorHandler;
    }
    AppComponent.prototype.goToRoute = function (route, params) {
        $('.navbar-collapse').removeClass('in');
        $('.navbar-collapse').removeClass('open');
        this._router.navigate([route, params]);
    };
    AppComponent.prototype.ngOnInit = function () {
        this.message = 'go';
    };
    AppComponent = __decorate([
        router_1.RouteConfig([
            {
                path: '/search', name: 'SearchComponent', component: searchComponent_1.SearchComponent
            },
            {
                path: '/categories', name: 'CategoryComponent', component: categoryComponent_1.CategoryComponent
            },
            {
                path: '/playLists', name: 'PlayListComponent', component: playListComponent_1.PlayListComponent, useAsDefault: true
            }
        ]),
        core_1.Component({
            selector: 'main-app',
            templateUrl: '/views/index.html?v=1.0',
            directives: [router_1.ROUTER_DIRECTIVES]
        }), 
        __metadata('design:paramtypes', [router_1.Router, errorHandlerService_1.ErrorHandlerService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.js.map