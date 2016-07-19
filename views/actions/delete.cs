{% macro endpoint(action) -%}
Endpoint({% if action.hasId %}true{% else %}false{% endif %})
{%- endmacro %}


{% macro delete_action(action) -%}
{% if action.queryParams() == null %}
public Task<System.Net.Http.HttpResponseMessage> Delete()
{
    return RC.Delete({{ endpoint(action) }}, null);
}
{% else %}
public Task<System.Net.Http.HttpResponseMessage> Delete(DeleteQueryParams queryParams = null)
{
    return RC.Delete({{ endpoint(action) }}, queryParams);
}
{{ action.queryModel('cs', 'DeleteQueryParams') }}
{% endif %}
{%- endmacro %}