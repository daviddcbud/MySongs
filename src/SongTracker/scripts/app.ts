declare var $: any;
import {Component} from "angular2/core";
import {RouteConfig, Router, ROUTER_DIRECTIVES} from "angular2/router";
import {OnInit} from "angular2/core"
import {ErrorHandlerService} from "./shared/errorHandlerService"
import {SearchComponent} from "./searchComponent"
import {CategoryComponent} from "./categoryComponent"
import {PlayListComponent} from "./playListComponent"
@RouteConfig([
  
    {
        path: '/search', name: 'SearchComponent', component: SearchComponent, useAsDefault: true
    },
    {
        path: '/categories', name: 'CategoryComponent', component: CategoryComponent
    }
    ,
    {
        path: '/playLists', name: 'PlayListComponent', component: PlayListComponent
    }
])

@Component({
    selector: 'main-app',
    templateUrl: '/views/index.html?v=1.0',
    directives: [ROUTER_DIRECTIVES]
})
export class AppComponent implements OnInit {

    message:string
    constructor(private _router: Router, private _errorHandler: ErrorHandlerService) {
    }
    goToRoute(route, params) {
        $('.navbar-collapse').removeClass('in');
        $('.navbar-collapse').removeClass('open');
        this._router.navigate([route,params]);
    }
     
    ngOnInit() {
        this.message = 'go';
    }
    

}
