import { swagger, paths, segments, routes, actions } from './swagger';
import * as nunjucks from 'nunjucks';
import { pascalCase } from 'change-case';
import * as fs from 'fs';


const env = nunjucks.configure('views');
env.addFilter('pascalCase', pascalCase);

const generateModel = (segment) => {
    const models = Array.from(routes.get(segment) || []);
    const methods = Array.from(actions.get(segment) || []);
    const result = env.render("model.cs", { segment, models, methods });
    fs.writeFileSync(`csharp/src/RC/Generated/${pascalCase(segment)}.cs`, result);
}

// for (const segment of segments) {
//     const models = Array.from(routes.get(segment) || []);
//     const methods = Array.from(actions.get(segment) || []);
//     const result = env.render("model.cs", { segment, models, methods });
//     console.log(result);
// }

generateModel('restapi');
