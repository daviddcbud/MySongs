declare var $: any;
import {Component} from "angular2/core";
import {OnInit, ViewChild} from "angular2/core";
import {AutoCompleteComponent} from "./shared/autocompleteComponent"
import {FocusService} from "./shared/focusService"
import {Router, RouteParams} from "angular2/router"
import {Http, Headers} from "angular2/http"
import 'rxjs/Rx'
import {ErrorHandlerService} from "./shared/errorHandlerService"
import {AddSongsComponent} from "./addSongsComponent"
@Component({
    templateUrl: '/views/playLists.html?v=1.4',
    selector: 'playList',
    directives: [AutoCompleteComponent, AddSongsComponent]
})

export class PlayListComponent implements OnInit {
    @ViewChild(AddSongsComponent)
    private _addSongsComponent: AddSongsComponent;
    playLists: any[];
    loading: boolean;
    _playListId: any;
    playList: any;
    playListtoEdit: any;
    songs: any[];
    newMode: boolean;
    editingPlaylist: any;
    addingSongs: boolean;
    addSongs() {
         
        this.addingSongs = true;
    }
    doneAdding() {
        this.addingSongs = false;
    }
    songsAdded(song) {
        this.loadPlayList();
    }

    newPlayList() {
        this.editingPlaylist = {};
        
        this.editingPlaylist.id = 0;
        this.editingPlaylist.description = '';
        
    }
    get playListId() {
        return this._playListId;
    }
    set playListId(value) {

        this._playListId= value;
        this.loadPlayList();
        
    }
    loadPlayList() {
        if (this._playListId == 0) return;
        this.loading = true;
        this._http.get('/api/PlayList?id=' + this._playListId).map(res => res.json())
            .subscribe(x => {
                this.loading = false;
                this.playList = x;
                this.loading = false;

            },
            error => {
                this.loading = false;
                this._errorHandler.handleError(error)
            });
    }
    constructor(private _http: Http, private _errorHandler: ErrorHandlerService) {
        
        this.playLists = [];
        this.songs = [];
    }


    getData() {
        
        return this._http.get('/api/PlayLists').map(res => res.json());
    }
    load() {
        this.loading = true;
        this.getData().subscribe(x => {
            this.loading = false;
            this.playLists = x;
            this.loading = false;
            if (this.playLists.length > 0) {
                if (this.editingPlaylist) {
                    this.playListId = this.editingPlaylist.id;
                }
                else {
                    this.playListId = this.playLists[0].id;
                }
            }
            this.editingPlaylist = null;
            
        },
            error => {
                this.loading = false;
                this._errorHandler.handleError(error)
            });
    }
    ngOnInit() {
        this.load();
    }
    removeSong(song) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        var _this = this;
        this.loading = true;
        var json = JSON.stringify(song);
        
        _this._http.post('/api/RemoveSong', json,
            { headers: headers }).map(r => r.json()).subscribe(x => {
                _this.loading = false;
                _this.playList.songs.splice(_this.playList.songs.indexOf(song), 1);

            },
            error => {
                _this._errorHandler.handleError(error);
                _this.loading = true;
            });


    }
    save() {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        var _this = this;
        this.loading = true;
        var json = JSON.stringify(this.editingPlaylist );
        
        _this._http.post('/api/SavePlayList', json,
            { headers: headers }).map(r => r.json()).subscribe(x => {
                _this.editingPlaylist.id = x;
                _this.loading = true;
                _this.load();
                
            },
            error => {
                _this._errorHandler.handleError(error);
                _this.loading = true;
            });
    }

}
