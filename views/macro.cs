{% macro field(fld, cls) -%}
public {% if fld.type == cls.name %}_{% endif %}{{ fld.type | csharp_type }} {{ fld.name | csharp_name }};
{%- endmacro %}