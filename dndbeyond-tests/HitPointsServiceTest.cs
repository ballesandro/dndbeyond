using dndbeyond.Models;
using dndbeyond.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace dndbeyond_tests
{
    public class HitPointsServiceTest
    {

        [Fact]
        public void CalculateConModNoItemsStat10_shouldBe0()
        {
            var expected = 0;

            var hitPointsService = new HitPointsService();
            var character = MakeCharacter(10);
            var actual = hitPointsService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModNoItemsLowStat_shouldBeNegative()
        {
            var expected = -1;

            var hitPointsService = new HitPointsService();
            var character = MakeCharacter(8);
            var actual = hitPointsService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModNoItemsLowStatWithRounding_shouldBeNegative()
        {
            var expected = -1;

            var hitPointsService = new HitPointsService();
            var character = MakeCharacter(9);
            var actual = hitPointsService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModNoItemsHighStat_shouldBePositive()
        {
            var expected = 2;

            var hitPointsService = new HitPointsService();
            var character = MakeCharacter(14);
            var actual = hitPointsService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateConModNoItemsHighStatWithRounding_shouldBePositive()
        {
            var expected = 2;

            var hitPointsService = new HitPointsService();
            var character = MakeCharacter(13);
            var actual = hitPointsService.CalculateConMod(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void GetDieAverage_shouldBeCool()
        {
            var hitPointsService = new HitPointsService();

            hitPointsService.getDieAverage(6).Should().Be(4);
            hitPointsService.getDieAverage(8).Should().Be(5);
            hitPointsService.getDieAverage(10).Should().Be(6);
            hitPointsService.getDieAverage(12).Should().Be(7);

            // maybe you're playing with some bizarre house rules
            hitPointsService.getDieAverage(7).Should().Be(4);
        }

        [Fact]
        public void CalculateMaxHitPointsAverageWithOneClass_shouldBeCool()
        {
            // con mod = 2, average hit die = 6
            // (2 * 5) + (6 * 5)
            var expected = 40;

            var hitPointsService = new HitPointsService();
            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 5);
            var actual = hitPointsService.CalculateMaxHitPointsAverage(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateMaxHitPointsAverageWithMultipleClasses_shouldBeCool()
        {
            // con mod = 2, average hit die = 6 & 7.
            // (2 * 5) + (6 * 1) + (7 * 4)
            var expected = 44; 

            var hitPointsService = new HitPointsService();
            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 1);
            AddClass(character, 12, 4);
            var actual = hitPointsService.CalculateMaxHitPointsAverage(character);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithOneClassForMinRolls_shouldBeMin()
        {
            // con mod = 2
            // min: (2 * 5) + (1 * 5) = 15
            var expectedMin = 15;

            var mockDiceService = new Mock<IDiceService>();
            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns(1);

            var hitPointsService = new HitPointsService(null, null, mockDiceService.Object, null);

            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 5);
            var actual = hitPointsService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMin);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithMultipleClassesForMinRolls_shouldBeMin()
        {
            // con mod = 2
            // min: (2 * 5) + (1 * 1) + (1 * 4) = 15
            var expectedMin = 15;

            var mockDiceService = new Mock<IDiceService>();
            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns(1);

            var hitPointsService = new HitPointsService(null, null, mockDiceService.Object, null);
            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 1);
            AddClass(character, 12, 4);
            var actual = hitPointsService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMin);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithOneClassForMaxRolls_shouldBeMax()
        {
            // con mod = 2
            // min: (2 * 5) + (10 * 5) = 60
            var expectedMax = 60;

            var mockDiceService = new Mock<IDiceService>();
            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns<int>(roll => roll);

            var hitPointsService = new HitPointsService(null, null, mockDiceService.Object, null);
            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 5);
            var actual = hitPointsService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMax);
        }

        [Fact]
        public void CalculateMaxHitPointsRandomWithMultipleClasses_shouldBeMax()
        {
            // con mod = 2
            // max: (2 * 5) + (10 * 1) + (12 * 4) = 68
            var expectedMax = 68;

            var mockDiceService = new Mock<IDiceService>();
            mockDiceService.Setup(die => die.Roll(It.IsAny<int>()))
                .Returns<int>(roll => roll);

            var hitPointsService = new HitPointsService(null, null, mockDiceService.Object, null);
            var character = MakeCharacter(5, 13);
            AddClass(character, 10, 1);
            AddClass(character, 12, 4);
            var actual = hitPointsService.CalculateMaxHitPointsRandom(character);

            actual.Should().Be(expectedMax);
        }

        private Character MakeCharacter(int level, int constitution)
        {
            var character = MakeCharacter(constitution);
            character.Level = level;
            return character;
        }

        private Character MakeCharacter(int constitution)
        {
            return new Character()
            {
                Stats = new CharacterStats()
                {
                    Constitution = constitution
                }
            };
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
