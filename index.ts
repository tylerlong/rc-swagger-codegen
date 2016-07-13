import { swagger, paths, segments, routes, actions } from './swagger';
import * as nunjucks from 'nunjucks';

for (const segment of segments) {
    const models = routes[segment];
    const methods = actions[segment];
    const result = nunjucks.renderString("hello world {{ name }}", { name: 'Tyler' });
    console.log(result);
    break;
}