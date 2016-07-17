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


{% macro endpoint(action) -%}
Endpoint({% if action.hasId %}true{% else %}false{% endif %})
{%- endmacro %}


{% macro action(action) -%}
{% if action.name == 'get' %}
{{ get_action(action) }}
{% elif action.name == 'list' %}
{{ list_action(action) }}
{% elif action.name == 'post' %}
{{ post_action(action) }}
{% elif action.name == 'put' %}
{{ put_action(action) }}
{% elif action.name == 'delete' %}
{{ delete_action(action) }}
{% endif %}
{%- endmacro %}


{% macro get_action(action) -%}
{% if action.queryParams() == null %}
public Task<GetResponse> Get()
{
    return RC.Get<GetResponse>({{ endpoint(action) }}, null);
}
{% else %}
public Task<GetResponse> Get(GetQueryParams queryParams = null)
{
    return RC.Get<GetResponse>({{ endpoint(action) }}, queryParams);
}
{{ action.queryModel('cs', 'GetQueryParams') }}
{% endif %}
{{ action.responseModel('cs', 'GetResponse') }}
{%- endmacro %}


{% macro list_action(action) -%}
list()
{%- endmacro %}
{% macro post_action(action) -%}
post()
{%- endmacro %}


{% macro put_action(action) -%}
{% if action.queryParams() == null %}
public Task<PutResponse> Put(PutRequest requestBody)
{
    return RC.Put<PutResponse>({{ endpoint(action) }}, requestBody, null);
}
{% else %}
public Task<PutResponse> Put(PutRequest requestBody, PutQueryParams queryParams = null)
{
    return RC.Put<PutResponse>({{ endpoint(action) }}, requestBody, queryParams);
}
{{ action.queryModel('cs', 'PutQueryParams') }}
{% endif %}
{{ action.requestModel('cs', 'PutRequest') }}
{{ action.responseModel('cs', 'PutResponse') }}
{%- endmacro %}


{% macro delete_action(action) -%}
{% if action.queryParams() == null %}
public void Delete()
{
    RC.Delete({{ endpoint(action) }}, null);
}
{% else %}
public void Delete(DeleteQueryParams queryParams = null)
{
    RC.Delete({{ endpoint(action) }}, queryParams);
}
{{ action.queryModel('cs', 'DeleteQueryParams') }}
{% endif %}
{%- endmacro %}