import { segments, routes, hasIds } from './swagger';
import * as nunjucks from 'nunjucks';
import * as fs from 'fs';
import { actions } from './action';
import * as _ from 'lodash';


const pascalCase = (str: string): string => {
    return _.upperFirst(_.camelCase(str));
}

const env = nunjucks.configure('views', {
    trimBlocks: true,
    lstripBlocks: true,
});
env.addFilter('pascalCase', pascalCase);

const generateModel = (segment) => {
    const models = Array.from(routes.get(segment) || []);
    const result = env.render("model.cs", { segment, models, hasIds, actions });
    fs.writeFileSync(`csharp/src/RC/Generated/${pascalCase(segment)}.cs`, result);
}

for (const segment of segments) {
    generateModel(segment);
}