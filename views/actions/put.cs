{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if segment != 'profile-image' and segment != 'extension' %}
        {% if action.queryParams() == null %}
            public Task<PutResponse> Put(PutRequest requestBody)
            {
                return RC.Put<PutResponse>({{ endpoint.endpoint(action) }}, requestBody, null);
            }
        {% else %}
            public Task<PutResponse> Put(PutRequest requestBody, object queryParams)
            {
                return RC.Put<PutResponse>({{ endpoint.endpoint(action) }}, requestBody, queryParams);
            }
            public Task<PutResponse> Put(PutRequest requestBody, PutQueryParams queryParams = null)
            {
                return Put(requestBody, queryParams as object);
            }

            {{ action.queryModel('cs', 'PutQueryParams') }}
        {% endif %}

        {{ action.requestModel('cs', 'PutRequest') }}
        {{ action.responseModel('cs', 'PutResponse') }}
    {% endif %}
{% endmacro %}