import { swagger, segments, routes, hasIds } from './swagger';
import * as nunjucks from 'nunjucks';
import * as fs from 'fs';
import { actions } from './action';
import * as _ from 'lodash';
import { renderModel } from './render';


const pascalCase = (str: string): string => {
    return _.upperFirst(_.camelCase(str));
}

const env = nunjucks.configure('views', {
    autoescape: false,
    trimBlocks: true,
    lstripBlocks: true,
});
env.addFilter('pascalCase', pascalCase);

const format = (str: string): string => {
    let indent = 0;
    let result = '';
    for(const line of str.replace(/\s+$/mg, '').replace(/\n{2,}/g, '\n').split('\n').map(item => item.trim())) {
        if (line == '}') {
            indent -= 4;
        }
        result += _.repeat(' ', indent) + line + '\n';
        if (line == '{') {
            indent += 4;
        }
    }
    return result;
}

const generateModel = (segment) => {
    const models = Array.from(routes.get(segment) || []);
    let result = env.render("model.cs", { segment, models, hasIds, actions });
    result = format(result);
    fs.writeFileSync(`csharp/src/RC/Generated/${pascalCase(segment)}.cs`, result);
}

for (const segment of segments) {
    generateModel(segment);
}

// generate definitions
let temp = '';
for (let key of Object.keys(swagger.definitions)) {
  temp += format(renderModel(key)) + '\n\n';
}
fs.writeFileSync('csharp/src/RC/Generated/Definitions.cs', temp);
