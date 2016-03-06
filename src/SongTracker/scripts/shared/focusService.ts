import {Http} from "angular2/http";
import {Injectable} from "angular2/core";

@Injectable()
export class FocusService {

    focus(id: string) {
        setTimeout(function () {
            var element = document.getElementById(id);

            if (element) element.focus();
        });
    }
}