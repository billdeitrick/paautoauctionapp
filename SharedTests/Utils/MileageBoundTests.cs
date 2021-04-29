using Xunit;
using paautoauctionapp.Shared.Utils;

namespace paautoauctionapp.SharedTests
{
    public class MileageBoundTests
    {
        [Fact]
        public void ToStringWithUpperAndLower()
        {
            MileageBound mb = new MileageBound(10000, 20000);

            Assert.Equal("10,000-20,000", mb.ToString());
        }

        [Fact]
        public void ToStringLowerOnly()
        {
            MileageBound mb = new MileageBound(20000, null);

            Assert.Equal("More than 20,000", mb.ToString());
        }

        [Fact]
        public void ToStringUpperOnly()
        {
            MileageBound mb = new MileageBound(null, 20000);

            Assert.Equal("Less than 20,000", mb.ToString());
        }
    }
}
