{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if action.queryParams() == null %}
        public Task<System.Net.Http.HttpResponseMessage> Delete()
        {
            return RC.Delete({{ endpoint.endpoint(action) }}, null);
        }
    {% else %}
        public Task<System.Net.Http.HttpResponseMessage> Delete(object queryParams)
        {
            return RC.Delete({{ endpoint.endpoint(action) }}, queryParams);
        }
        public Task<System.Net.Http.HttpResponseMessage> Delete(DeleteQueryParams queryParams = null)
        {
            return Delete(queryParams as object);
        }

        {{ action.queryModel('cs', 'DeleteQueryParams') }}
    {% endif %}
{% endmacro %}