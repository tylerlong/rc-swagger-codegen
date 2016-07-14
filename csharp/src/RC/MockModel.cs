namespace RingCentral
{
    public class MockModel : Model
    {
        public MockModel(RC rc) : base(null)
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

        private RC rc;
        protected override RC RC
        {
            get
            {
                return rc;
            }
        }
    }
}