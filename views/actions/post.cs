{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if segment != 'profile-image' and segment != 'end' %}
        {% if action.queryParams() == null %}
            {% if segment != 'fax' %}
                public Task<PostResponse> Post(object requestBody)
                {
                    return RC.Post<PostResponse>({{ endpoint.endpoint(action) }}, requestBody, null);
                }
                public Task<PostResponse> Post(PostRequest requestBody)
                {
                    return Post(requestBody as object);
                }
            {% endif %}
        {% else %}
            {% if action.requestBody() == null %}
                public Task<PostResponse> Post(object queryParams)
                {
                    return RC.Post<PostResponse>({{ endpoint.endpoint(action) }}, null, queryParams);
                }
                public Task<PostResponse> Post(PostQueryParams queryParams)
                {
                    return Post(queryParams as object);
                }
            {% else %}
                public Task<PostResponse> Post(object requestBody, object queryParams)
                {
                    return RC.Post<PostResponse>({{ endpoint.endpoint(action) }}, requestBody, queryParams);
                }
                public Task<PostResponse> Post(PostRequest requestBody, object queryParams)
                {
                    return Post(requestBody as object, queryParams);
                }
                public Task<PostResponse> Post(object requestBody, PostQueryParams queryParams = null)
                {
                    return Post(requestBody, queryParams as object);
                }
                public Task<PostResponse> Post(PostRequest requestBody, PostQueryParams queryParams = null)
                {
                    return Post(requestBody as object, queryParams as object);
                }
            {% endif %}
            {{ action.queryModel('cs', 'PostQueryParams') }}
        {% endif %}

        {{ action.requestModel('cs', 'PostRequest') }}
        {% if action.responseBody() != null %}
            {{ action.responseModel('cs', 'PostResponse') }}
        {% else %}
            public class PostResponse { }
        {% endif %}
    {% endif %}
{% endmacro %}