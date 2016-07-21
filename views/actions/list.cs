{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if action.queryParams() == null %}
        public Task<ListResponse> List()
        {
            return RC.Get<ListResponse>({{ endpoint.endpoint(action) }}, null);
        }
    {% else %}
        public Task<ListResponse> List(object queryParams)
        {
            return RC.Get<ListResponse>({{ endpoint.endpoint(action) }}, queryParams);
        }
        public Task<ListResponse> List(ListQueryParams queryParams = null)
        {
            return List(queryParams as object);
        }

        {{ action.queryModel('cs', 'ListQueryParams') }}
    {% endif %}

    {{ action.responseModel('cs', 'ListResponse') }}
{% endmacro %}