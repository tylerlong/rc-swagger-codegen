{% macro model_route(model, hasId) -%}
{% if hasId %}
public {{ model | pascalCase }} {{ model | pascalCase }}(string _id = {% if model == 'account' or model == 'extension' %}"~"{% else %}null{% endif %})
{
    return new {{ model | pascalCase }}(this, _id);
}
{% else %}
public {{ model | pascalCase }} {{ model | pascalCase }}()
{
    return new {{ model | pascalCase }}(this, null);
}
{% endif %}
{%- endmacro %}