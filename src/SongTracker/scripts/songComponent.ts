import {Component, Input, Output, EventEmitter} from "angular2/core";
import {OnInit} from "angular2/core";
import {AutoCompleteComponent} from "./shared/autocompleteComponent"
import {FocusService} from "./shared/focusService"
import {Router, RouteParams} from "angular2/router"
import {Http, Headers} from "angular2/http"
import 'rxjs/Rx'
import {ErrorHandlerService} from "./shared/errorHandlerService"

@Component({
    templateUrl: '/views/song.html?v=1.1',
    selector: 'song',
    directives: [AutoCompleteComponent]
})

export class SongComponent implements OnInit {
    model: any;
    id: any;
    loading: boolean;
    @Output() onSave: EventEmitter<any>=new EventEmitter<any>();
    tags: any[];
    constructor(private _http: Http, private _errorHandler: ErrorHandlerService) {
        this.model = {};
     }

     
    getSong() {
        return this._http.get('/api/Song?id=' + this.id).map(res => res.json());
    }
    getTags() {
         
        return this._http.get('/api/Categories').map(res => res.json());
    }
    newLink() {
        var cat: any;
        cat = {};
        cat.isDeleted = false;
        cat.link = '';
        cat.id = 0;
        cat.description = '';
        this.model.songLinks.push(cat);
    }
    newCategory() {
        var cat: any;
        cat = {};
        cat.id = 0;
        cat.isDeleted = false;
        cat.categoryId = 0;
        this.model.songTags.push(cat);
         
    }
    deleteCategory(cat) {
        cat.isDeleted = true;
    }
    deleteLink(link) {
        link.isDeleted = true;
    }
    load(id) {
        this.id = id;
        this.loading = true;
        this.getSong().subscribe(x => {
            this.loading = false;
            this.model = x;
            this.onSave.emit(this.id);
        },
            error => {
                this.loading = false;
                this._errorHandler.handleError(error)
            });
    }
    ngOnInit() {
        console.log('init song');
        this.getTags().subscribe(x => {
            
            this.tags = x;
        },
            error => {
            
                this._errorHandler.handleError(error)
            });
    }
      
    save() {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        var _this = this;
        this.loading = true;
        var json = JSON.stringify(this.model);
        console.log(json);
        _this._http.post('/api/SaveSong', json,
            { headers: headers }).map(r => r.json()).subscribe(x => {
                _this.loading = false;
                _this.onSave.emit(x);
            },
            error => {
                _this._errorHandler.handleError(error);
                _this.loading = false;
            });

    }

}
