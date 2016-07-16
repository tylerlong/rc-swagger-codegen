{% import "macros.cs" as macros -%}

namespace RingCentral
{
    public partial class {{ segment | pascalCase }} : Model
    {
        {{ macros.model_constructor(segment, hasIds.get(segment)) }}

        protected override string PathSegment
        {
            get
            {
                return "{{ segment }}";
            }
        }

        {% for model in models %}
        {{ macros.model_route(model, hasIds.get(model)) | indent(8, false) }}
        {% endfor %}
    }
}