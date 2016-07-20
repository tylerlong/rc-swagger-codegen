{% import "macros.cs" as macros -%}

{% if actions.has(segment) %}
using System.Threading.Tasks;

{% endif %}
namespace RingCentral
{
    public partial class {{ segment | pascalCase }} : Model
    {
        {{ macros.model_constructor(segment, hasIds.get(segment)) | ltrim }}

        protected override string PathSegment
        {
            get
            {
                return "{{ segment }}";
            }
        }

        {% for model in models %}
        {{ macros.model_route(model, hasIds.get(model)) | ltrim | indent(8, false) }}
        {% endfor %}

        {% if actions.has(segment) %}
        {% for action in actions.get(segment) %}
        {% if action.name == 'get' %}
        {{ macros.action(action, segment) | indent(8, false) }}
        {% endif %}
        {% if action.name == 'delete' %}
        {{ macros.action(action, segment) | indent(8, false) }}
        {% endif %}
        {% if action.name == 'put' %}
        {{ macros.action(action, segment) | indent(8, false) }}
        {% endif %}
        {% if action.name == 'list' %}
        {{ macros.action(action, segment) | indent(8, false) }}
        {% endif %}
        {% if action.name == 'post' %}
        {{ macros.action(action, segment) | indent(8, false) }}
        {% endif %}
        {% endfor %}
        {% endif %}
    }
}