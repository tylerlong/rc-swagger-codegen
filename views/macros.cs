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

{% import "actions/index.cs" as actions -%}

{% macro action(action) -%}
{% if action.name == 'get' %}
{{ actions.get_action(action) }}
{% elif action.name == 'list' %}
{{ actions.list_action(action) }}
{% elif action.name == 'post' %}
{{ actions.post_action(action) }}
{% elif action.name == 'put' %}
{{ actions.put_action(action) }}
{% elif action.name == 'delete' %}
{{ actions.delete_action(action) }}
{% endif %}
{%- endmacro %}