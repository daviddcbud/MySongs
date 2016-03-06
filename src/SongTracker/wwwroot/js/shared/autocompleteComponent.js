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
var focusService_1 = require("./focusService");
var AutoCompleteComponent = (function () {
    function AutoCompleteComponent(_focusService) {
        this._focusService = _focusService;
        this.onSelected = new core_1.EventEmitter();
        this.onFinished = new core_1.EventEmitter();
    }
    AutoCompleteComponent.prototype.ngOnChanges = function (changes) {
        if (changes.selectedItem) {
            if (changes.selectedItem.currentValue && changes.selectedItem.currentValue.name) {
                this.searchText = changes.selectedItem.currentValue.name;
            }
        }
    };
    AutoCompleteComponent.prototype.clearSearch = function () {
        this.searchText = '';
        this.selectedItem = null;
        this.filteredData = [];
        this.onSelected.emit(null);
    };
    AutoCompleteComponent.prototype.keypress = function (keyCode) {
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
    };
    AutoCompleteComponent.prototype.selectUp = function () {
        var index = this.filteredData.indexOf(this.selectedItem);
        if (index != 0) {
            this.selectedItem = this.filteredData[index - 1];
        }
    };
    AutoCompleteComponent.prototype.selectDown = function () {
        var index = this.filteredData.indexOf(this.selectedItem);
        if (index != this.filteredData.length - 1) {
            this.selectedItem = this.filteredData[index + 1];
        }
        if (index < 0 && this.filteredData.length > 0) {
            this.selectedItem = this.filteredData[0];
        }
    };
    AutoCompleteComponent.prototype.isSelected = function (item) {
        return item == this.selectedItem;
    };
    AutoCompleteComponent.prototype.onEnterKey = function () {
        this.select(this.selectedItem);
        if (this.nextControlFocus) {
            this._focusService.focus(this.nextControlFocus);
        }
    };
    AutoCompleteComponent.prototype.ngOnInit = function () {
        if (this.selectedItem)
            this.select(this.selectedItem);
    };
    AutoCompleteComponent.prototype.select = function (item) {
        this.selectedItem = item;
        this.onSelected.emit(item);
        this.onFinished.emit(this.selectedItem);
        this.searchText = this.selectedItem.name;
        this.filteredData = [];
    };
    AutoCompleteComponent.prototype.filter = function () {
        this.onSelected.emit(null);
        this.selectedItem = null;
        this.filteredData = [];
        if (!this.data)
            return;
        for (var _i = 0, _a = this.data; _i < _a.length; _i++) {
            var item = _a[_i];
            var name = item.name;
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
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Array)
    ], AutoCompleteComponent.prototype, "data", void 0);
    __decorate([
        core_1.Output(), 
        __metadata('design:type', core_1.EventEmitter)
    ], AutoCompleteComponent.prototype, "onSelected", void 0);
    __decorate([
        core_1.Output(), 
        __metadata('design:type', core_1.EventEmitter)
    ], AutoCompleteComponent.prototype, "onFinished", void 0);
    __decorate([
        core_1.Input(), 
        __metadata('design:type', String)
    ], AutoCompleteComponent.prototype, "placeholder", void 0);
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Object)
    ], AutoCompleteComponent.prototype, "selectedItem", void 0);
    __decorate([
        core_1.Input(), 
        __metadata('design:type', String)
    ], AutoCompleteComponent.prototype, "nextControlFocus", void 0);
    __decorate([
        core_1.Input(), 
        __metadata('design:type', String)
    ], AutoCompleteComponent.prototype, "id", void 0);
    AutoCompleteComponent = __decorate([
        core_1.Component({
            templateUrl: '/views/autocomplete.html?v=1.10',
            selector: 'autocomplete',
            styleUrls: ['css/autocomplete.css?v=1.0']
        }), 
        __metadata('design:paramtypes', [focusService_1.FocusService])
    ], AutoCompleteComponent);
    return AutoCompleteComponent;
}());
exports.AutoCompleteComponent = AutoCompleteComponent;
//# sourceMappingURL=autocompleteComponent.js.map