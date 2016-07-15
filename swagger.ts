import * as path from 'path';
import * as fs from 'fs';
import { isValidSegment, isListAction } from './util';


const swaggerPath: string = path.join(__dirname, './swagger/advanced.json');


const swagger = JSON.parse(fs.readFileSync(swaggerPath, 'utf8'));


const paths: string[] = Object.keys(swagger.paths);


const tokenss = paths.map(path => path.split('/').filter(token => isValidSegment(token)));


const segments = new Set<string>(tokenss.reduce((prev, current) => prev.concat(current), []));


const routes = new Map<string, Set<string>>();
for (const segment of segments) {
    routes.set(segment, new Set<string>());
}
for (const tokens of tokenss) {
    for (let i = 1; i < tokens.length; i++) {
        routes.get(tokens[i - 1]).add(tokens[i]);
    }
}


const actions = new Map<string, Set<string>>();
for (const path of Object.keys(swagger.paths)) {
    const segment = path.split('/').reverse().filter(item => isValidSegment(item))[0];
    if (!actions.has(segment)) {
        actions.set(segment, new Set<string>());
    }
    const methods = Object.keys(swagger.paths[path]).filter(m => ['get', 'post', 'put', 'delete'].indexOf(m) != -1);
    for (let method of methods) {
        if (method == 'get') {
            if (isListAction(path, swagger.paths[path])) {
                method = 'list';
            }
        }
        actions.get(segment).add(method);
    }
}


const hasIds = new Map<string, boolean>();
const pathsStr = paths.join('\n');
for (const segment of segments) {
    hasIds.set(segment, false);
    const hasIdRegex = new RegExp(`/${segment}/(?:\{[^{}/]+\}|v1\.0)(?:/|$)`, 'm');
    if (hasIdRegex.test(pathsStr)) {
        hasIds.set(segment, true);
    }
}


export { swagger, paths, segments, routes, actions, hasIds };