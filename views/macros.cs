{% macro model_constructor(segment, hasId) -%}
    {% if hasId %}
        internal {{ segment | pascalCase }}(Model parent, string _id = null) : base(parent, _id) { }
    {% else %}
        internal {{ segment | pascalCase }}(Model parent) : base(parent, null) { }
    {% endif %}
{%- endmacro %}


{% macro model_route(model, hasId) -%}
    {% if hasId %}
        public {{ model | pascalCase }} {{ model | pascalCase }}(string _id = {% if model == 'account' or model == 'extension' %}"~"{% else %}null{% endif %})
        {
            return new {{ model | pascalCase }}(this, _id);
        }
    {% else %}
        public {{ model | pascalCase }} {{ model | pascalCase }}()
        {
            return new {{ model | pascalCase }}(this);
        }
    {% endif %}
{%- endmacro %}



{% macro action(action) -%}
    {% import 'actions/' + action.name + '.cs' as temp %}
    {{ temp['action'](action) }}
{%- endmacro %}