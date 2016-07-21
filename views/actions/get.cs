{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if segment != 'service-info' and segment != 'content' and segment != 'profile-image' %}
        {% if action.queryParams() == null %}
            public Task<GetResponse> Get()
            {
                return RC.Get<GetResponse>({{ endpoint.endpoint(action) }}, null);
            }
        {% else %}
            public Task<GetResponse> Get(object queryParams)
            {
                return RC.Get<GetResponse>({{ endpoint.endpoint(action) }}, queryParams);
            }
            public Task<GetResponse> Get(GetQueryParams queryParams = null)
            {
                return Get(queryParams as object);
            }

            {{ action.queryModel('cs', 'GetQueryParams') }}
        {% endif %}

        {{ action.responseModel('cs', 'GetResponse') }}
    {% endif %}
{% endmacro %}