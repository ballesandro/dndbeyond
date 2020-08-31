using DnDBeyond.Models;
using DnDBeyond.Models.Enum;
using DnDBeyond.Services.Implementations;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace DnDBeyond_tests
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
            var expectedCharacter = MakeCharacter(20, 10, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 0);
            damageService.DamageCharacter(actualCharacter, 10, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndEqualCurrent_shouldLowerCurrent()
        {
            var expectedCharacter = MakeCharacter(20, 0, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 10, 0);
            damageService.DamageCharacter(actualCharacter, 10, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndLessCurrent_shouldLowerCurrent()
        {
            var expectedCharacter = MakeCharacter(20, -5, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 5, 0);
            damageService.DamageCharacter(actualCharacter, 10, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndNoCurrent_shouldLowerCurrent()
        {
            var expectedCharacter = MakeCharacter(20, -10, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 0, 0);
            damageService.DamageCharacter(actualCharacter, 10, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void MoreTempAndSomeCurrent_shouldLowerTemp()
        {
            var expectedCharacter = MakeCharacter(20, 20, 10);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 20);
            damageService.DamageCharacter(actualCharacter, 10, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void EqualTempAndSomeCurrent_shouldLowerTemp()
        {
            var expectedCharacter = MakeCharacter(20, 20, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10);
            damageService.DamageCharacter(actualCharacter, 10, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void LessTempAndSomeCurrent_shouldLowerTempAndCurrent()
        {
            var expectedCharacter = MakeCharacter(20, 15, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 5);
            damageService.DamageCharacter(actualCharacter, 10, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void LessTempAndLessCurrent_shouldLowerTemp()
        {
            var expectedCharacter = MakeCharacter(20, -5, 0);

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 5, 5);
            damageService.DamageCharacter(actualCharacter, 15, null);

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Immune_shouldNotLowerAny()
        {
            var expectedCharacter = MakeCharacter(20, 20, 10);
            AddImmunity(expectedCharacter, "fire");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10); 
            AddImmunity(actualCharacter, "fire");
            damageService.DamageCharacter(actualCharacter, 10, "fire");

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Resistant_shouldLowerHalf()
        {
            var expectedCharacter = MakeCharacter(20, 20, 5);
            AddResistance(expectedCharacter, "fire");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10);
            AddResistance(actualCharacter, "fire");
            damageService.DamageCharacter(actualCharacter, 10, "fire");

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Resistant_shouldRoundDown()
        {
            var expectedCharacter = MakeCharacter(20, 20, 6);
            AddResistance(expectedCharacter, "fire");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10);
            AddResistance(actualCharacter, "fire");
            damageService.DamageCharacter(actualCharacter, 9, "fire");

            actualCharacter.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void DifferentResistance_shouldNotBeModified()
        {
            var expectedCharacter = MakeCharacter(20, 20, 0);
            AddResistance(expectedCharacter, "poison");

            var damageService = new DamageService();
            var actualCharacter = MakeCharacter(20, 20, 10);
            AddResistance(actualCharacter, "poison");
            damageService.DamageCharacter(actualCharacter, 10, "fire");

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

        private void AddResistance(Character character, string resistance)
        {
            if(character.Defenses == null)
            {
                character.Defenses = new List<CharacterDefense>();
            }

            character.Defenses.Add(new CharacterDefense() { Type = resistance, Defense = DefenseDegree.Resistance });
        }

        private void AddImmunity(Character character, string immunity)
        {
            if (character.Defenses == null)
            {
                character.Defenses = new List<CharacterDefense>();
            }

            character.Defenses.Add(new CharacterDefense() { Type = immunity, Defense = DefenseDegree.Immunity });
        }
    }
}
