using dndbeyond.Models;
using dndbeyond.Services;
using dndbeyond.Services.Implementations;
using FluentAssertions;
using Moq;
using Xunit;

namespace dndbeyond_tests
{
    public class HitPointsServiceTest
    {
        private readonly HitPointsService hpService;
        private readonly Mock<IDiceService> mockDiceService;

        public HitPointsServiceTest()
        {
            mockDiceService = new Mock<DiceService>().As<IDiceService>();
            mockDiceService.Setup(mock => mock.GetDieAverage(It.IsAny<int>())).CallBase();

            hpService = new HitPointsService(null, mockDiceService.Object, null, null);
        }

        [Fact]
        public void CalculateConModNoItemsStat10_shouldBe0()
        {
            var expected = 0;

            var character = MakeCharacter(1, 10);
            var actual = hpService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModNoItemsLowStat_shouldBeNegative()
        {
            var expected = -1;

            var character = MakeCharacter(1, 8);
            var actual = hpService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModNoItemsLowStatWithRounding_shouldBeNegative()
        {
            var expected = -1;

            var character = MakeCharacter(1, 9);
            var actual = hpService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModNoItemsHighStat_shouldBePositive()
        {
            var expected = 2;

            var character = MakeCharacter(1, 14);
            var actual = hpService.CalculateConMod(character);

            actual.Should().Be(expected);
        }


        [Fact]
        public void CalculateConModNoItemsHighStatWithRounding_shouldBePositive()
        {
            var expected = 2;

            var character = MakeCharacter(1, 13);
            var actual = hpService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModWithItems_shouldWork()
        {
            var expected = 5;

            var character = MakeCharacter(1, 14);
            character.Items.Add(new Item()
            {
                Modifier = new Modifier()
                {
                    AffectedObject = "stats",
                    AffectedValue = "constitution",
                    Value = 5
                }
            });
            character.Items.Add(new Item()
            {
                Modifier = new Modifier()
                {
                    AffectedObject = "stats",
                    AffectedValue = "charisma",
                    Value = 5
                }
            });
            var actual = hpService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateMaxHitPointsAverageWithOneClass_shouldWork()
        {
            // con mod = 2, average hit die = 6
            // (2 * 5) + (6 * 5)
            var expected = 40;

            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 5);
            var actual = hpService.CalculateMaxHitPointsAverage(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateMaxHitPointsAverageWithMultipleClasses_shouldBeAverage()
        {
            // con mod = 2, average hit die = 6 & 7.
            // (2 * 5) + (6 * 1) + (7 * 4)
            var expected = 44; 

            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 1);
            AddClass(character, 12, 4);
            var actual = hpService.CalculateMaxHitPointsAverage(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithOneClassForMinRolls_shouldBeMin()
        {
            // con mod = 2
            // min: (2 * 5) + (1 * 5) = 15
            var expectedMin = 15;

            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns(1);

            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 5);
            var actual = hpService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMin);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithMultipleClassesForMinRolls_shouldBeMin()
        {
            // con mod = 2
            // min: (2 * 5) + (1 * 1) + (1 * 4) = 15
            var expectedMin = 15;

            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns(1);

            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 1);
            AddClass(character, 12, 4);
            var actual = hpService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMin);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithOneClassForMaxRolls_shouldBeMax()
        {
            // con mod = 2
            // min: (2 * 5) + (10 * 5) = 60
            var expectedMax = 60;

            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns<int>(roll => roll);

            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 5);
            var actual = hpService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMax);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithMultipleClasses_shouldBeMax()
        {
            // con mod = 2
            // max: (2 * 5) + (10 * 1) + (12 * 4) = 68
            var expectedMax = 68;

            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns<int>(roll => roll);

            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 1);
            AddClass(character, 12, 4);
            var actual = hpService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMax);
        }

        private Character MakeCharacter(int level, int constitution)
        {
            var character = new Character()
            {
                Level = level,
                Stats = new CharacterStats()
            {
                    Constitution = constitution
                }
            };
            return character;
        }

        private void AddClass(Character character, int hitDiceValue, int classLevel)
        {
            character.Classes.Add(new CharacterClass()
            {
                HitDiceValue = hitDiceValue,
                ClassLevel = classLevel
            });
        }
    }
}
