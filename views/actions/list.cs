{% macro endpoint(action) -%}
Endpoint({% if action.hasId %}true{% else %}false{% endif %})
{%- endmacro %}


{% macro action(action) -%}
{% if action.queryParams() == null %}
public Task<ListResponse> List()
{
    return RC.Get<ListResponse>({{ endpoint(action) }}, null);
}
{% else %}
public Task<ListResponse> List(ListQueryParams queryParams = null)
{
    return RC.Get<ListResponse>({{ endpoint(action) }}, queryParams);
}
{{ action.queryModel('cs', 'ListQueryParams') }}
{% endif %}
{{ action.responseModel('cs', 'ListResponse') }}
{%- endmacro %}