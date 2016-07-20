{% import 'endpoint.cs' as endpoint -%}

{% macro action(action) -%}
{% if action.queryParams() == null %}
public Task<GetResponse> Get()
{
    return RC.Get<GetResponse>({{ endpoint.endpoint(action) }}, null);
}
{% else %}
public Task<GetResponse> Get(GetQueryParams queryParams = null)
{
    return RC.Get<GetResponse>({{ endpoint.endpoint(action) }}, queryParams);
}
{{ action.queryModel('cs', 'GetQueryParams') }}
{% endif %}
{{ action.responseModel('cs', 'GetResponse') }}
{%- endmacro %}