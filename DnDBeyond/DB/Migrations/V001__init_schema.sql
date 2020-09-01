CREATE TABLE characters (
    "Id" SERIAL PRIMARY KEY,
    "Name" text,
    "Level" integer,
    "MaxHitPoints" integer,
    "CurrentHitPoints" integer,
    "TemporaryHitPoints" integer,
    "HitPointsMethod" integer
);
CREATE TABLE classes (
    "Id" SERIAL PRIMARY KEY,
    "CharacterId" BIGINT,
    "Name" text,
    "ClassLevel" integer,
    "HitDiceValue" integer,
    CONSTRAINT fk_character_class FOREIGN KEY("CharacterId") REFERENCES characters("Id")
);
CREATE TABLE defenses (
    "Id" SERIAL PRIMARY KEY,
    "CharacterId" BIGINT,
    "Type" text,
    "Defense" integer,
    CONSTRAINT fk_character_defense FOREIGN KEY("CharacterId") REFERENCES characters("Id")
);
CREATE TABLE stats (
    "Id" SERIAL PRIMARY KEY,
    "CharacterId" BIGINT,
    "Strength" integer,
    "Dexterity" integer,
    "Constitution" integer,
    "Intelligence" integer,
    "Wisdom" integer,
    "Charisma" integer,
    CONSTRAINT fk_character_stats FOREIGN KEY("CharacterId") REFERENCES characters("Id")
);
CREATE TABLE modifiers (
    "Id" SERIAL PRIMARY KEY,
    "AffectedObject" text,
    "AffectedValue" text,
    "Value" integer
);
CREATE TABLE items (
    "Id" SERIAL PRIMARY KEY,
    "CharacterId" BIGINT,
    "ModifierId" BIGINT,
    "Name" text,
    CONSTRAINT fk_character_item FOREIGN KEY("CharacterId") REFERENCES characters("Id"),
    CONSTRAINT fk_modifier_item FOREIGN KEY("ModifierId") REFERENCES modifiers("Id")
);