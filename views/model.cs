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
        public {{ model | pascalCase }} {{ model | pascalCase }}(string _id = null)
        {
            return new {{ model | pascalCase }}(this, _id);
        }
        {% endfor %}
    }
}