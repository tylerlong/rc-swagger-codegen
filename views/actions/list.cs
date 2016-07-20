{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if action.queryParams() == null %}
        public Task<ListResponse> List()
        {
            return RC.Get<ListResponse>({{ endpoint.endpoint(action) }}, null);
        }
    {% else %}
        public Task<ListResponse> List(ListQueryParams queryParams = null)
        {
            return RC.Get<ListResponse>({{ endpoint.endpoint(action) }}, queryParams);
        }

        {{ action.queryModel('cs', 'ListQueryParams') }}
    {% endif %}

    {{ action.responseModel('cs', 'ListResponse') }}
{% endmacro %}