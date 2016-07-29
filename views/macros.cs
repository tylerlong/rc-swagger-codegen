{% macro model_constructor(segment, hasId) %}
    {% if hasId %}
        internal {{ segment | pascalCase }}(Model parent, string _id = null) : base(parent, _id) { }
    {% else %}
        internal {{ segment | pascalCase }}(Model parent) : base(parent, null) { }
    {% endif %}
{% endmacro %}


{% macro model_route(model, hasId) %}
    {% if hasId %}
        public {{ model | pascalCase }} {{ model | pascalCase }}(ID _id)
        {
            return new {{ model | pascalCase }}(this, _id);
        }
        public {{ model | pascalCase }} {{ model | pascalCase }}()
        {
            return new {{ model | pascalCase }}(this, {% if model == 'account' or model == 'extension' %}"~"{% else %}null{% endif %});
        }
    {% else %}
        public {{ model | pascalCase }} {{ model | pascalCase }}()
        {
            return new {{ model | pascalCase }}(this);
        }
    {% endif %}
{% endmacro %}


{% macro action(action, segment) %}
    {% import 'actions/' + action.name + '.cs' as temp %}
    {{ temp['action'](action, segment) }}
{% endmacro %}