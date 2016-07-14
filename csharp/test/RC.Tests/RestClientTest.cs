using Xunit;
using RingCentral;

namespace RringCentral.Test
{
    public class RestClientTest
    {
        [Fact]
        public void PassingTest()
        {
            var rc = new RestClient("", "", "");
            var restapi = rc.Restapi();
            Assert.NotNull(restapi);
        }
    }
}