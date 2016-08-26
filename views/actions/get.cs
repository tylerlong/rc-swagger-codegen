{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if segment != 'content' and segment != 'profile-image' and not (segment == 'service-info' and action.parent == 'account') %}
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