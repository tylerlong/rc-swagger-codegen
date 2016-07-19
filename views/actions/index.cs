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


{% macro list_action(action) -%}
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