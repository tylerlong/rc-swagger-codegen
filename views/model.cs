{% import "macros.cs" as macros %}


{% if actions.has(segment) %}
using System.Threading.Tasks;
{% endif %}

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
        {{ macros.model_route(model, hasIds.get(model)) }}
        {% endfor %}

        {% if actions.has(segment) %}
            {% for action in actions.get(segment) %}
                {{ macros.action(action, segment) }}
            {% endfor %}
        {% endif %}
    }
}