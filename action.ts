import { isValidSegment } from './util';
import { swagger, paths } from './swagger';
import * as _ from 'lodash';
import { renderModel } from './new';
import { pascalCase } from 'change-case';


class Action {
    path: string;
    segment: string;
    parent: string;
    hasId: boolean;
    method: string;
    name: string;

    constructor(path: string, method: string) {
        this.path = path;
        this.method = method;
        this.name = this.getName();
        this.segment = path.split('/').reverse().find((item) => isValidSegment(item));
        this.parent = path.split('/').reverse().filter((item) => isValidSegment(item))[1];
        this.hasId = !isValidSegment(path.split('/').reverse()[0]);
    }

    private getName(): string {
        if (this.method == 'get') {
            if (this.path == '/restapi') {
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

    public queryParams(): any {
        let parameters: Array<any> = swagger.paths[this.path][this.method].parameters;
        if (parameters == undefined) {
            return null;
        }
        parameters = parameters.filter(item => item.in == 'query');
        if (parameters.length == 0) {
            return null;
        }
        const result = { properties: {} };
        for (const parameter of parameters) {
            result.properties[parameter.name] = parameter;
        }
        return result;
    }
    public queryModel(language: string, name: string): string {
        return renderModel(name, this.queryParams());
    }

    public requestBody(): any {
        const parameters: any = swagger.paths[this.path][this.method].parameters;
        if (parameters == undefined) {
            return null;
        }
        const bodyParam = parameters.find((item) => item.name == 'body');
        if (bodyParam == undefined) {
            return null;
        }
        return bodyParam.schema;
    }
    public requestModel(language: string, name: string): string {
        return renderModel(name, this.requestBody());
    }

    public responseBody(): any {
        const schema = swagger.paths[this.path][this.method].responses.default.schema;
        if (schema == undefined) {
            return null;
        }
        return schema;
    }
    public responseModel(language: string, name: string): string {
        return renderModel(pascalCase(name), this.responseBody());
    }

    public equals(other: Action): boolean {
        return (this.segment == other.segment && this.hasId == other.hasId
            && this.method == other.method && this.name == other.name);
    }
}


const actions = new Map<string, Action[]>();
for (const path of paths) {
    const methods = Object.keys(swagger.paths[path]).filter(m => ['get', 'post', 'put', 'delete'].indexOf(m) != -1);
    for (const method of methods) {
        const action = new Action(path, method);
        const segment = action.segment;
        if (!actions.has(segment)) {
            actions.set(segment, new Array<Action>());
        }
        if (actions.get(segment).find(item => item.equals(action)) == undefined) {
            actions.get(segment).push(action);
        }
    }
}


export { Action, actions };