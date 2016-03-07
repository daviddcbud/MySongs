declare var $: any;
import {Component,ViewChild} from "angular2/core";
import {OnInit} from "angular2/core";
import {AutoCompleteComponent} from "./shared/autocompleteComponent"
import {FocusService} from "./shared/focusService"
import {Router, RouteParams} from "angular2/router"
import {Http, Headers} from "angular2/http"
import 'rxjs/Rx'
import {ErrorHandlerService} from "./shared/errorHandlerService"
import {SongComponent} from "./songComponent"

@Component({
    templateUrl: '/views/search.html?v=1.4',
    selector: 'search',
    directives: [AutoCompleteComponent, SongComponent]
})

export class SearchComponent implements OnInit {
    @ViewChild(SongComponent)
    private _songComponent: SongComponent;
    model: any;
     
    tags: any[];
    loading: boolean;
    results: any[];
    searching: boolean;
    searchOptions: any[];
    songToEdit: any;
    songSaved(song) {
        this.search();
        
        var modal = <any>$('#songModal');
        modal.modal('hide');
    }
    addSong() {
        this.songToEdit = {};
        this.songToEdit.id = 0;
        this.songToEdit.name = '';
        this.songToEdit.artist = '';
        this.songToEdit.songLinks = [];
        this.songToEdit.songTags = [];
        this._songComponent.model = this.songToEdit;
        this._songComponent.newCategory();
        var modal = <any>$('#songModal');
        modal.modal('show');
    }
    editSong(song) {
        this.songToEdit = song;
        this._songComponent.load(song.id);
        var modal = <any>$('#songModal');
        modal.modal('show');
    }
    get categoryId() {
        return this.model.categoryId;
    }
    set categoryId(value) {

        this.model.categoryId = value;

        this.search();
    }
    constructor(private _http: Http, private _errorHandler: ErrorHandlerService) {
        this.model = {};
        this.searchOptions = [];
        this.searchOptions.push({ id: 0, name: 'Category' });
        this.searchOptions.push({ id: 1, name: 'Song Name' });
        this.model.searchOption = 0;
        this.categoryId = 0;
        this.model.title = '';
        this.results = [];
    }
     
    tagChosen() {
    }
    onTagSelected($event) {
    }
    getTags() {
        return this._http.get('/api/tags').map(res => res.json());
    }
    ngOnInit() {
        this.loading = true;
        this.getTags().subscribe(x => {
            this.loading = false;
            this.tags = x;
            this.loading = false;
        },
            error => {
                this.loading = false;
                this._errorHandler.handleError(error)
            });
    }

    search() {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        var _this = this;
        this.searching = true;
        var json = JSON.stringify(this.model);
        console.log(json);
        _this._http.post('/api/Search', json,
            { headers: headers }).map(r => r.json()).subscribe(x => {
                _this.searching = false;
                _this.results = x;

            },
            error => {
                _this._errorHandler.handleError(error);
                _this.searching = false;
            });

    }

}
