using Xunit;
using RingCentral;

namespace RringCentral.Test
{
    public class ModelTest
    {
        [Fact]
        public void MockTest()
        {
            var rc = new RestClient("", "");
            Assert.NotNull(rc);
            var restapi = rc.Restapi();
            Assert.Equal(rc.server + "/restapi/v1.0", restapi.Url());
            Assert.Equal(rc.server + "/restapi", restapi.Url(false));
        }
    }
}