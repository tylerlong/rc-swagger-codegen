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

    private getSampleValue(type: string): any {
        switch (type) {
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

    private getSampleProps(props: any): any {
        const result = {};
        for (const prop of Object.keys(props)) {
            if (props[prop]['$ref'] != undefined) {
                result[prop] = this.getSampleSchema(props[prop]);
            } else {
                const type = props[prop].type;
                if (type == 'array') {
                    result[prop] = [this.getSampleSchema(props[prop].items)];
                } else {
                    result[prop] = this.getSampleValue(type);
                }
            }
        }
        return result;
    }

    private getSampleSchema(schema: any) {
        if (schema.properties != undefined) {
            return this.getSampleProps(schema.properties);
        }
        if (schema['$ref'] != undefined) {
            const refTokens = schema['$ref'].split('/');
            return this.getSampleProps(swagger[refTokens[1]][refTokens[2]].properties);
        }
        if (schema.enum != undefined) {
            return schema.enum.map(item => this.getSampleSchema(item));
        }
        if (schema.type != undefined) {
            return this.getSampleValue(schema.type);
        }
        throw `unexpected schema: ${schema}`;
    }

    private json2model(json: string, language: string, name: string): string {
        if (json == null) {
            return null;
        }
        const { render } = require(`json2model/controllers/${language}`);
        return render(name, json, false);
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
        const result = {};
        for (const parameter of parameters) {
            result[parameter.name] = this.getSampleValue(parameter.type);
        }
        return result;
    }
    public queryModel(language: string, name: string): string {
        return this.json2model(this.queryParams(), language, name);
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
        return this.getSampleSchema(bodyParam.schema);
    }
    public requestModel(language: string, name: string): string {
        return this.json2model(this.requestBody(), language, name);
    }

    public responseBody(): any {
        const schema = swagger.paths[this.path][this.method].responses.default.schema;
        if (schema == undefined) {
            return null;
        }
        return this.getSampleSchema(schema);
    }
    public responseModel(language: string, name: string): string {
        return this.json2model(this.responseBody(), language, name);
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
        actions.get(segment).push(action);
    }
}


export { Action, actions };