"use strict";
///<reference path="../node_modules/angular2/typings/browser.d.ts"/>
var browser_1 = require("angular2/platform/browser");
var app_1 = require("./app");
var router_1 = require("angular2/router");
var http_1 = require("angular2/http");
var errorHandler_service_1 = require("./shared/errorHandler.service");
browser_1.bootstrap(app_1.AppComponent, [http_1.HTTP_PROVIDERS, router_1.ROUTER_PROVIDERS, errorHandler_service_1.ErrorHandlerService]);
//# sourceMappingURL=boot.js.map