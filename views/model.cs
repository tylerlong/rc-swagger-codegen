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
    }
}