import { swagger, paths, segments, routes, actions } from './swagger';
import * as nunjucks from 'nunjucks';
import { pascalCase } from 'change-case';


const env = nunjucks.configure('views');
env.addFilter('pascalCase', pascalCase);

for (const segment of segments) {
    const models = Array.from(routes.get(segment) || []);
    const methods = Array.from(actions.get(segment) || []);
    const result = env.render("model.cs", { segment, models, methods });
    console.log(result);
}