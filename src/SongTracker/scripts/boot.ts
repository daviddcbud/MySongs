///<reference path="../node_modules/angular2/typings/browser.d.ts"/>
import {bootstrap} from "angular2/platform/browser";
import {AppComponent} from "./app";
import {ROUTER_PROVIDERS} from "angular2/router";
import {HTTP_PROVIDERS} from "angular2/http";
import {ErrorHandlerService} from "./shared/errorHandlerService";
import {FocusService} from "./shared/focusService"
bootstrap(AppComponent, [HTTP_PROVIDERS, ROUTER_PROVIDERS, ErrorHandlerService, FocusService]);
