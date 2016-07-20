{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if segment != 'profile-image' and segment != 'lookup' and segment != 'revoke' and segment != 'end' %}
        {% if action.queryParams() == null %}
            public Task<PostResponse> Post(PostRequest requestBody)
            {
                return RC.Post<PostResponse>({{ endpoint.endpoint(action) }}, requestBody, null);
            }
        {% else %}
            public Task<PostResponse> Post(PostRequest requestBody, PostQueryParams queryParams = null)
            {
                return RC.Post<PostResponse>({{ endpoint.endpoint(action) }}, requestBody, queryParams);
            }

            {{ action.queryModel('cs', 'PostQueryParams') }}
        {% endif %}

        {{ action.requestModel('cs', 'PostRequest') }}
        {{ action.responseModel('cs', 'PostResponse') }}
    {% endif %}
{% endmacro %}