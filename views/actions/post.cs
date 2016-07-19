{% macro endpoint(action) -%}
Endpoint({% if action.hasId %}true{% else %}false{% endif %})
{%- endmacro %}


{% macro post_action(action) -%}
{% if action.queryParams() == null %}
public Task<PostResponse> Post(PostRequest requestBody)
{
    return RC.Post<PostResponse>({{ endpoint(action) }}, requestBody, null);
}
{% else %}
public Task<PostResponse> Post(PostRequest requestBody, PostQueryParams queryParams = null)
{
    return RC.Post<PostResponse>({{ endpoint(action) }}, requestBody, queryParams);
}
{{ action.queryModel('cs', 'PostQueryParams') }}
{% endif %}
{{ action.requestModel('cs', 'PostRequest') }}
{{ action.responseModel('cs', 'PostResponse') }}
{%- endmacro %}