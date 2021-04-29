using Xunit;
using paautoauctionapp.Shared.Utils;

namespace paautoauctionapp.SharedTests
{
    public class MileageBoundsTests
    {

        [Fact]
        public void ReturnsExpectedId()
        {

            Assert.Equal(MileageBounds.GetMileageBoundId(50000), 5);

        }

    }
}
