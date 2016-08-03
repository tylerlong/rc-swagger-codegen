{% import 'endpoint.cs' as endpoint %}


{% macro action(action, segment) %}
    {% if segment != 'profile-image' %}
        {% if action.queryParams() == null %}
            public Task<PutResponse> Put(object requestBody)
            {
                return RC.Put<PutResponse>({{ endpoint.endpoint(action) }}, requestBody, null);
            }
            {% if segment != 'extension' %}
            public Task<PutResponse> Put(PutRequest requestBody)
            {
                return Put(requestBody as object);
            }
            {% endif %}
        {% else %}
            public Task<PutResponse> Put(object requestBody, object queryParams)
            {
                return RC.Put<PutResponse>({{ endpoint.endpoint(action) }}, requestBody, queryParams);
            }
            {% if segment != 'extension' %}
            public Task<PutResponse> Put(PutRequest requestBody, object queryParams)
            {
                return Put(requestBody as object, queryParams);
            }
            {% endif %}
            public Task<PutResponse> Put(object requestBody, PutQueryParams queryParams = null)
            {
                return Put(requestBody, queryParams as object);
            }
            {% if segment != 'extension' %}
            public Task<PutResponse> Put(PutRequest requestBody, PutQueryParams queryParams = null)
            {
                return Put(requestBody as object, queryParams as object);
            }
            {% endif %}

            {{ action.queryModel('cs', 'PutQueryParams') }}
        {% endif %}

        {% if segment != 'extension' %}
            {{ action.requestModel('cs', 'PutRequest') }}
        {% endif %}
        {{ action.responseModel('cs', 'PutResponse') }}
    {% endif %}
{% endmacro %}