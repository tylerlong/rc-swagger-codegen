{% import 'endpoint.cs' as endpoint -%}

{% macro action(action) -%}
{% if action.queryParams() == null %}
public Task<PutResponse> Put(PutRequest requestBody)
{
    return RC.Put<PutResponse>({{ endpoint.endpoint(action) }}, requestBody, null);
}
{% else %}
public Task<PutResponse> Put(PutRequest requestBody, PutQueryParams queryParams = null)
{
    return RC.Put<PutResponse>({{ endpoint.endpoint(action) }}, requestBody, queryParams);
}
{{ action.queryModel('cs', 'PutQueryParams') }}
{% endif %}
{{ action.requestModel('cs', 'PutRequest') }}
{{ action.responseModel('cs', 'PutResponse') }}
{%- endmacro %}