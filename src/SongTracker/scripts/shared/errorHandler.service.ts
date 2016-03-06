import {Injectable} from "angular2/core";

declare var toastr: any;
@Injectable()
export class ErrorHandlerService {
    handleError(error) {
        var body = error._body;
        var errObject = JSON.parse(body);
        alert(errObject.exceptionMessage);
    }
}