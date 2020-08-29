using dndbeyond.Models;
using dndbeyond.Services.Implementations;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace dndbeyond_tests
{
    public class DamageServiceTest
    {
        [Fact]
        public void SanityCheck()
        {
            var thisCharacter = MakeCharacter(50, 50, 35);
            var thatCharacter = MakeCharacter(50, 50, 0);
            var datCharacter = MakeCharacter(50, 50, 0);

            thisCharacter.Should().NotBeEquivalentTo(thatCharacter);
            thatCharacter.Should().BeEquivalentTo(datCharacter);
        }

        [Fact]
        public void NoTempAndMoreCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, 10, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 0);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndEqualCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, 0, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 10, 0);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndLessCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, -5, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 5, 0);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndNoCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, -10, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 0, 0);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void MoreTempAndSomeCurrent_shouldLowerTemp()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, 20, 10);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 20);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void EqualTempAndSomeCurrent_shouldLowerTemp()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, 20, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void LessTempAndSomeCurrent_shouldLowerTempAndCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, 15, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 5);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void LessTempAndLessCurrent_shouldLowerTemp()
        {
            var expectedDamageDone = 15;
            var expectedCharacter = MakeCharacter(20, -5, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 5, 5);
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 15, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Immune_shouldNotLowerAny()
        {
            var expectedDamageDone = 0;
            var expectedCharacter = MakeCharacter(20, 20, 10);
            addImmunity(expectedCharacter, "fire");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10); 
            addImmunity(actualCharacter, "fire");
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, "fire");

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Resistant_shouldLowerHalf()
        {
            var expectedDamageDone = 5;
            var expectedCharacter = MakeCharacter(20, 20, 5);
            addResistance(expectedCharacter, "fire");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10);
            addResistance(actualCharacter, "fire");
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, "fire");

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Resistant_shouldRoundDown()
        {
            var expectedDamageDone = 4;
            var expectedCharacter = MakeCharacter(20, 20, 6);
            addResistance(expectedCharacter, "fire");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10);
            addResistance(actualCharacter, "fire");
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 9, "fire");

            actualDamageDone.Should().Be(expectedDamageDone);
            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void DifferentResistance_shouldNotBeModified()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = MakeCharacter(20, 20, 10);
            addResistance(expectedCharacter, "poison");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 0);
            addResistance(actualCharacter, "poison");
            var actualDamageDone = damageService.DamageCharacter(actualCharacter, 10, "fire");

            actualDamageDone.Should().Be(expectedDamageDone);
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

        private void addResistance(Character character, string resistance)
        {
            if(character.Defenses == null)
            {
                character.Defenses = new List<CharacterDefense>();
            }

            character.Defenses.Add(new CharacterDefense() { Type = resistance, Defense = DamageService.RESISTANCE });
        }

        private void addImmunity(Character character, string immunity)
        {
            if (character.Defenses == null)
            {
                character.Defenses = new List<CharacterDefense>();
            }

            character.Defenses.Add(new CharacterDefense() { Type = immunity, Defense = DamageService.IMMUNITY });
        }
    }
}
