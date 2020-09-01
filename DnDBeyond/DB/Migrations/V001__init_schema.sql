CREATE TABLE characters (
    Id BIGINT PRIMARY KEY,
    Name text,
    Level integer,
    MaxHitPoints integer,
    CurrentHitPoints integer,
    TemporaryHitPoints integer,
    HitPointsMethod text
);
CREATE TABLE classes (
    Id BIGINT PRIMARY KEY,
    CharacterId BIGINT,
    Name text,
    Level integer,
    HitDiceValue integer,
    CONSTRAINT fk_character_class FOREIGN KEY(CharacterId) REFERENCES characters(Id)
);
CREATE TABLE defenses (
    Id BIGINT PRIMARY KEY,
    CharacterId BIGINT,
    Type text,
    Defense text,
    CONSTRAINT fk_character_defense FOREIGN KEY(CharacterId) REFERENCES characters(Id)
);
CREATE TABLE stats (
    Id BIGINT PRIMARY KEY,
    CharacterId BIGINT,
    Strength text,
    Dexterity text,
    Constitution text,
    Intelligence text,
    Wisdom text,
    Charisma text,
    CONSTRAINT fk_character_stats FOREIGN KEY(CharacterId) REFERENCES characters(Id)
);
CREATE TABLE modifiers (
    Id BIGINT PRIMARY KEY,
    AffectedObject text,
    AffectedValue text,
    Value integer
);
CREATE TABLE items (
    Id BIGINT PRIMARY KEY,
    CharacterId BIGINT,
    ModifierId BIGINT,
    Name text,
    CONSTRAINT fk_character_item FOREIGN KEY(CharacterId) REFERENCES characters(Id),
    CONSTRAINT fk_modifier_item FOREIGN KEY(ModifierId) REFERENCES modifiers(Id)
);