{% macro endpoint(action) -%}
Endpoint({% if action.hasId %}true{% else %}false{% endif %})
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