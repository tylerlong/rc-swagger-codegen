import * as path from 'path';
import * as fs from 'fs';
import { isValidSegment } from './util';


const swaggerPath: string = path.join(__dirname, './swagger/advanced.json');


const swagger: any = JSON.parse(fs.readFileSync(swaggerPath, 'utf8'));


const paths: string[] = Object.keys(swagger.paths);


const tokenss: string[][] = paths.map(path => path.split('/').filter(token => isValidSegment(token)));


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


const hasIds = new Map<string, boolean>();
const pathsStr = paths.join('\n');
for (const segment of segments) {
    hasIds.set(segment, false);
    const hasIdRegex = new RegExp(`/${segment}/(?:\{[^{}/]+\}|v1\.0)(?:/|$)`, 'm');
    if (hasIdRegex.test(pathsStr)) {
        hasIds.set(segment, true);
    }
}


export { swagger, paths, segments, routes, hasIds };