import { swagger } from './swagger';


import * as nunjucks from 'nunjucks';
const env = nunjucks.configure('views/definitions', {
  autoescape: false,
  trimBlocks: true,
  lstripBlocks: true,
});

import * as _ from 'lodash';


const getType = (value) => {
  if (!value.type) {
    return _.last<string>(value['$ref'].split('/')).replace(/\./g, '_');
  }
  let type: string = value.type;
  if (type == 'array') {
    return (value.items.type || _.last<string>(value.items['$ref'].split('/')).replace(/\./g, '_')) + '[]';
  }
  if (type == 'integer') {
    if(value.format == 'int64') {
      return 'long?';
    }
    return 'int?';
  }
  if (type == 'boolean') {
    return 'bool?'
  }
  return type;
}

const getName = (value) => {
  if (_.includes(['operator', 'default', 'ref'], value)) {
    return '@' + value;
  }
  return value;
}

const getFields = (obj) => Object.keys(obj).map((key) => ({
  name: getName(key),
  type: getType(obj[key]),
  description: obj[key].description,
}));
const renderModel = (name, model = null) => {
  if (name.endsWith('[]')) {
    name = name.substr(0, name.length - 2);
  }
  if (model == null) {
    model = swagger.definitions[name];
  }
  let properties = model.properties
  if(properties == undefined) {
    properties = swagger.definitions[_.last<string>(model['$ref'].split('/'))].properties
  }
  return env.render('Model.cs', { name: name.replace(/\./g, '_'), fields: getFields(properties).map(f => renderField(f)) });
}
const renderField = (field) => {
  let result = env.render('Field.cs', { field: field });
  return result;
}


export { renderModel };