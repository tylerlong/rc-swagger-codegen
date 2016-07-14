using Flurl;

namespace RingCentral
{
    public abstract partial class Model
    {
        public string _id;
        protected abstract string PathSegment { get; }
        protected Model parent;

        protected Model(Model parent, string _id = null)
        {
            this.parent = parent;
            this._id = _id;
        }

        protected virtual string Endpoint
        {
            get
            {
                var url = parent.Endpoint.AppendPathSegment(PathSegment);
                if (_id != null)
                {
                    url = url.AppendPathSegment(_id);
                }
                return url.Path;
            }
        }

        protected virtual RestClient RC
        {
            get
            {
                return parent.RC;
            }
        }
    }
}