using Xunit;

namespace RringCentral.Test
{
    public class RestClientTest
    {
        [Fact]
        public void PassingTest()
        {
            var rc = new RingCentral.RC("", "", "");
            var restapi = rc.Restapi();
            Assert.NotNull(restapi);
        }
    }
}