using System;
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
            var thisCharacter = new Character
            {
                Name = "Eressil",
                MaxHitPoints = 50,
                TemporaryHitPoints = 35
            };

            var thatCharacter = new Character
            {
                Name = "Rhone",
                MaxHitPoints = 50,
                TemporaryHitPoints = 0
            };

            thisCharacter.Should().NotBeEquivalentTo(thatCharacter);
        }

        [Fact]
        public void NoTempAndMoreCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 10,
                TemporaryHitPoints = 0
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 0
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndEqualCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 0,
                TemporaryHitPoints = 0
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 10,
                TemporaryHitPoints = 0
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndLessCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = -5,
                TemporaryHitPoints = 0
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 5,
                TemporaryHitPoints = 0
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void NoTempAndNoCurrent_shouldLowerCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = -10,
                TemporaryHitPoints = 0
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 0,
                TemporaryHitPoints = 0
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void MoreTempAndSomeCurrent_shouldLowerTemp()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 10
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 20
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void EqualTempAndSomeCurrent_shouldLowerTemp()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 0
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 10
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void LessTempAndSomeCurrent_shouldLowerTempAndCurrent()
        {
            var expectedDamageDone = 10;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 15,
                TemporaryHitPoints = 0
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 5
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void LessTempAndLessCurrent_shouldLowerTemp()
        {
            var expectedDamageDone = 15;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = -5,
                TemporaryHitPoints = 0
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 5,
                TemporaryHitPoints = 5
            };

            var actualDamageDone = damageService.DamageCharacter(character, 15, null);

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Immune_shouldNotLowerAny()
        {
            var expectedDamageDone = 0;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 10,
                Defenses = new List<CharacterDefense>()
                {
                    new CharacterDefense() { Type = "fire", Defense = DamageService.IMMUNITY }
                }
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 10,
                Defenses = new List<CharacterDefense>()
                {
                    new CharacterDefense() { Type = "fire", Defense = DamageService.IMMUNITY }
                }
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, "fire");

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Resistant_shouldLowerHalf()
        {
            var expectedDamageDone = 5;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 5,
                Defenses = new List<CharacterDefense>()
                {
                    new CharacterDefense() { Type = "fire", Defense = DamageService.RESISTANCE }
                }
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 10,
                Defenses = new List<CharacterDefense>()
                {
                    new CharacterDefense() { Type = "fire", Defense = DamageService.RESISTANCE }
                }
            };

            var actualDamageDone = damageService.DamageCharacter(character, 10, "fire");

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

        [Fact]
        public void Resistant_shouldRoundDown()
        {
            var expectedDamageDone = 4;
            var expectedCharacter = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 6,
                Defenses = new List<CharacterDefense>()
                {
                    new CharacterDefense() { Type = "fire", Defense = DamageService.RESISTANCE }
                }
            };

            var damageService = new DamageService();
            var character = new Character
            {
                MaxHitPoints = 20,
                CurrentHitPoints = 20,
                TemporaryHitPoints = 10,
                Defenses = new List<CharacterDefense>()
                {
                    new CharacterDefense() { Type = "fire", Defense = DamageService.RESISTANCE }
                }
            };

            var actualDamageDone = damageService.DamageCharacter(character, 9, "fire");

            actualDamageDone.Should().Be(expectedDamageDone);
            character.Should().BeEquivalentTo(expectedCharacter);
        }

    }
}
