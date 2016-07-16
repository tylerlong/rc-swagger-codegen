{% import "macros.cs" as macros -%}

namespace RingCentral
{
    public partial class {{ segment | pascalCase }} : Model
    {
        internal {{ segment | pascalCase }}(Model parent, string _id = null) : base(parent, _id) { }

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
    }
}