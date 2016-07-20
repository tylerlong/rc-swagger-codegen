{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if action.queryParams() == null %}
        public Task<System.Net.Http.HttpResponseMessage> Delete()
        {
            return RC.Delete({{ endpoint.endpoint(action) }}, null);
        }
    {% else %}
        public Task<System.Net.Http.HttpResponseMessage> Delete(DeleteQueryParams queryParams = null)
        {
            return RC.Delete({{ endpoint.endpoint(action) }}, queryParams);
        }

        {{ action.queryModel('cs', 'DeleteQueryParams') }}
    {% endif %}
{% endmacro %}