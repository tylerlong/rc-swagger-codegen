using Xunit;

namespace RingCentral.Test
{
    [Collection("RestClient collection")]
    public class ContactTest
    {
        private RestClient rc;
        public ContactTest(RestClientFixture fixture)
        {
            rc = fixture.rc;
        }

        [Fact]
        public void ContactList()
        {
            var list = rc.Restapi().Account().Extension().AddressBook().Contact().List().Result;
            Assert.NotNull(list);
            Assert.Equal(1, list.paging.page);
        }
    }
}