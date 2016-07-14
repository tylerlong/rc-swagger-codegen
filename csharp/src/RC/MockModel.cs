namespace RingCentral
{
    public class MockModel : Model
    {
        public MockModel(RestClient rc) : base(null)
        {
            this.rc = rc;
        }

        protected override string PathSegment
        {
            get
            {
                return "";
            }
        }

        protected override string Endpoint
        {
            get
            {
                return "";
            }
        }

        private RestClient rc;
        protected override RestClient RC
        {
            get
            {
                return rc;
            }
        }
    }
}