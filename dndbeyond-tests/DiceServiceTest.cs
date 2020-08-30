using dndbeyond.Services.Implementations;
using FluentAssertions;
using Xunit;

namespace dndbeyond_tests
{
    public class DiceServiceTest
    {

        [Fact]
        public void GetDieAverage_shouldWork()
        {
            var diceService = new DiceService();

            diceService.GetDieAverage(6).Should().Be(4);
            diceService.GetDieAverage(8).Should().Be(5);
            diceService.GetDieAverage(10).Should().Be(6);
            diceService.GetDieAverage(12).Should().Be(7);

            // maybe you're playing with some bizarre house rules
            diceService.GetDieAverage(7).Should().Be(4);
        }

    }
}
