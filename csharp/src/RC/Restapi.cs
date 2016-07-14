namespace RingCentral
{
    public class Restapi : Model
    {
        internal Restapi(Model parent, string _id = null) : base(parent, _id) { }

        protected override string PathSegment
        {
            get
            {
                return "restapi";
            }
        }
    }
}