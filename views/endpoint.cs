{% macro endpoint(action) -%}
    Endpoint({% if action.hasId %}true{% else %}false{% endif %})
{%- endmacro %}