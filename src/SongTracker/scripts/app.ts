import {Component} from "angular2/core";
import {RouteConfig, Router, ROUTER_DIRECTIVES} from "angular2/router";
import {OnInit} from "angular2/core"
import {ErrorHandlerService} from "./shared/errorHandler.service"


@Component({
    selector: 'main-app',
    templateUrl: '/views/index.html?v=1.8',
    directives: [ROUTER_DIRECTIVES]
})
export class AppComponent implements OnInit {

    message:string
    constructor(private _errorHandler: ErrorHandlerService) {
    }

     
    ngOnInit() {
        this.message = 'go';
    }
    

}
