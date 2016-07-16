import { isValidSegment } from './util';
import { swagger } from './swagger';


class Action {
    path: string;
    segment: string;
    hasId: boolean;
    method: string;
    name: string;

    constructor(path: string, method: string) {
        this.path = path;
        this.method = method;
        this.name = this.getName();
        this.segment = path.split('/').reverse().find((item) => isValidSegment(item));
        this.hasId = !isValidSegment(path.split('/').reverse()[0]);
    }

    private getName() : string {
        if(this.method == 'get') {
            if(this.path == '/restapi') {
                return 'list';
            }
            const properties = swagger.paths[this.path].get.responses.default.schema.properties;
            if (properties != undefined && properties.navigation != undefined) {
                return 'list';
            }
            return 'get';
        }
        return this.method;
    }

    private getSampleValue(type: string) : any {
        switch(type) {
            case 'integer':
                return 1;
            case 'string':
                return 's';
            case 'boolean':
                return true;
            case 'number':
                return 3.14;
            default:
                throw `unknown type: ${type}`;
        }
    }

    public queryString() : any {
        const parameters : any = swagger.paths[this.path][this.method].parameters;
        if(parameters == undefined) {
            return null;
        }
        const result = {};
        for(const parameter of parameters) {
            result[parameter.name] = this.getSampleValue(parameter.type);
        }
        return result;
    }

    public requestBody() : any {
        return {};
    }

    public responseBody() : any {
        return {};
    }
}


export { Action };