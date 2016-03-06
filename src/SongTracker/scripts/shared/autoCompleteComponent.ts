import {Component, Input, Output, EventEmitter, OnChanges} from "angular2/core";
import {FocusService} from "./focusService"
import {OnInit} from "angular2/core";
@Component({
    templateUrl: '/views/autocomplete.html?v=1.10',
    selector: 'autocomplete',
    styleUrls: ['css/autocomplete.css?v=1.0']

})

export class AutoCompleteComponent implements OnInit, OnChanges {
    @Input() data: any[];
    filteredData: any[];
    @Output() onSelected: EventEmitter<any> = new EventEmitter();
    @Output() onFinished: EventEmitter<any> = new EventEmitter();
    searchText: string;
    @Input() placeholder: string;
    @Input() selectedItem: any;
    @Input() nextControlFocus: string;
    @Input() id: string

    ngOnChanges(changes) {
        if (changes.selectedItem) {
            if (changes.selectedItem.currentValue && changes.selectedItem.currentValue.name) {
                this.searchText = changes.selectedItem.currentValue.name;
            }

        }
    }
    constructor(private _focusService: FocusService) {

    }
    clearSearch() {
        this.searchText = '';
        this.selectedItem = null;
        this.filteredData = [];
        this.onSelected.emit(null);
    }
    keypress(keyCode) {

        if (keyCode == 40) {
            this.selectDown();
        }
        else if (keyCode == 38) {
            this.selectUp();
        }
        else if (keyCode == 13) {
            this.onEnterKey();
        }
        else {
            this.filter();
        }
    }
    selectUp() {
        var index = this.filteredData.indexOf(this.selectedItem);
        if (index != 0) {
            this.selectedItem = this.filteredData[index - 1];
        }
    }
    selectDown() {
        var index = this.filteredData.indexOf(this.selectedItem);
        if (index != this.filteredData.length - 1) {
            this.selectedItem = this.filteredData[index + 1];
        }
        if (index < 0 && this.filteredData.length > 0) {
            this.selectedItem = this.filteredData[0];
        }

    }
    isSelected(item) {
        return item == this.selectedItem;
    }
    onEnterKey() {
        this.select(this.selectedItem);
        if (this.nextControlFocus) {

            this._focusService.focus(this.nextControlFocus);

        }
    }
    ngOnInit() {
        if (this.selectedItem) this.select(this.selectedItem);
    }
    select(item: any) {
        this.selectedItem = item;
        this.onSelected.emit(item);
        this.onFinished.emit(this.selectedItem);
        this.searchText = this.selectedItem.name;
        this.filteredData = [];
    }

    filter() {

        this.onSelected.emit(null);
        this.selectedItem = null;
        this.filteredData = [];
        if (!this.data) return;
        for (var item of this.data) {

            var name = <string>item.name;

            if (name && this.searchText) {

                if (this.searchText.length == 1) {

                    if (name.toLowerCase().indexOf(this.searchText.toLowerCase(), 0) == 0) {
                        this.filteredData.push(item);
                    }

                }
                else {
                    if (name.toLowerCase().indexOf(this.searchText.toLowerCase(), 0) > -1) {
                        this.filteredData.push(item);
                    }
                }
            }
        }
        if (this.filteredData.length > 0) {
            this.selectedItem = this.filteredData[0];
        }

    }

}