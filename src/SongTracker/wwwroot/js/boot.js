"use strict";
///<reference path="../node_modules/angular2/typings/browser.d.ts"/>
var browser_1 = require("angular2/platform/browser");
var app_1 = require("./app");
var router_1 = require("angular2/router");
var http_1 = require("angular2/http");
var errorHandlerService_1 = require("./shared/errorHandlerService");
var focusService_1 = require("./shared/focusService");
browser_1.bootstrap(app_1.AppComponent, [http_1.HTTP_PROVIDERS, router_1.ROUTER_PROVIDERS, errorHandlerService_1.ErrorHandlerService, focusService_1.FocusService]);
//# sourceMappingURL=boot.js.map