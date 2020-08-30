using DnDBeyond.Models;
using DnDBeyond.Services.Implementations;
using FluentAssertions;
using Xunit;

namespace DnDBeyond_tests
{
    public class HealServiceTest
    {
        [Fact]
        public void NoTempAndMaxCurrent_shouldNotChangeCurrent()
        {
            var expectedCharacter = MakeCharacter(20, 20, 0);

            var healService = new HealService();
            var actualCharacter = MakeCharacter(20, 20, 0);
            healService.HealCharacter(actualCharacter, 10);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndHealLessThanDeficit_shouldNotHealToMax()
        {
            var expectedCharacter = MakeCharacter(20, 15, 0);

            var healService = new HealService();
            var actualCharacter = MakeCharacter(20, 10, 0);
            healService.HealCharacter(actualCharacter, 5);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndHealMoreThanDeficit_shouldHealToMax()
        {
            var expectedCharacter = MakeCharacter(20, 20, 0);

            var healService = new HealService();
            var actualCharacter = MakeCharacter(20, 10, 0);
            healService.HealCharacter(actualCharacter, 15);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void SomeTempAndMaxCurrent_shouldNotChangeCurrentOrTemp()
        {
            var expectedCharacter = MakeCharacter(20, 20, 20);

            var healService = new HealService();
            var actualCharacter = MakeCharacter(20, 20, 20);
            healService.HealCharacter(actualCharacter, 10);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void SomeTempAndCurrentDeficit_shouldHealCurrentAndNotChangeTemp()
        {
            var expectedCharacter = MakeCharacter(20, 20, 20);

            var healService = new HealService();
            var actualCharacter = MakeCharacter(20, 10, 20);
            healService.HealCharacter(actualCharacter, 15);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        private Character MakeCharacter(int max, int current, int temp)
        {
            return new Character
            {
                MaxHitPoints = max,
                CurrentHitPoints = current,
                TemporaryHitPoints = temp
            };
        }
    }
}
