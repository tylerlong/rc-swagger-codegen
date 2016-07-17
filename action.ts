import { isValidSegment } from './util';
import { swagger, paths } from './swagger';


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

    public queryParams() : any {
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


const actions = new Map<string, Action[]>();
for(const path of paths) {
    const methods = Object.keys(swagger.paths[path]).filter(m => ['get', 'post', 'put', 'delete'].indexOf(m) != -1);
    for(const method of methods) {
        const action = new Action(path, method);
        const segment = action.segment;
        if (!actions.has(segment)) {
            actions.set(segment, new Array<Action>());
        }
        actions.get(segment).push(action);
    }
}


export { Action, actions };