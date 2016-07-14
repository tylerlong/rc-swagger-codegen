using Xunit;
using RingCentral;

namespace RringCentral.Test
{
    public class DictionaryTest
    {
        [Fact]
        public void MockTest()
        {
            var rc = new RestClient("", "", "");
            var dictionary = rc.Restapi().Dictionary();
            Assert.NotNull(dictionary);
            var location = dictionary.Location();
            Assert.NotNull(location);
            var state = dictionary.State();
            Assert.NotNull(state);
        }
    }
}