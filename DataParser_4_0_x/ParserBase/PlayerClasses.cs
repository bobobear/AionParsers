namespace Jamie.ParserBase
{
	using System;
	using System.Xml.Serialization;

	[Serializable]
    [Flags]
    public enum Class
    {
        NONE = 0,
        WARRIOR = 1,
        SCOUT = 2,
        MAGE = 4,
        CLERIC = 8,
        ENGINEER = 16,
        ARTIST = 32,
        GLADIATOR = 64,
        [XmlEnum("GLADIATOR")]
        FIGHTER = 64,
        TEMPLAR = 128,
        [XmlEnum("TEMPLAR")]
        KNIGHT = 128,
        ASSASSIN = 256,
        RANGER = 512,
        SORCERER = 1024,
        [XmlEnum("SORCERER")]
        WIZARD = 1024,
        SPIRIT_MASTER = 2048,
        [XmlEnum("SPIRIT_MASTER")]
        ELEMENTALLIST = 2048,
        CHANTER = 4096,
        PRIEST = 8192,
        GUNNER = 16384,
        BARD = 32768,
        RIDER = 65536,
        ALL = WARRIOR | SCOUT | MAGE | CLERIC | ENGINEER | ARTIST | FIGHTER | KNIGHT | ASSASSIN | RANGER | WIZARD | ELEMENTALLIST | CHANTER | PRIEST | GUNNER | BARD | RIDER
    }

    [Serializable]
    [Flags]
    public enum ClassOur
    {
        NONE = 0,
        WARRIOR = 1,
        SCOUT = 2,
        MAGE = 4,
        CLERIC = 8,
        ENGINEER = 16,
        ARTIST = 32,
        FIGHTER = 64,
        KNIGHT = 128,
        ASSASSIN = 256,
        RANGER = 512,
        WIZARD = 1024,
        ELEMENTALLIST = 2048,
        CHANTER = 4096,
        PRIEST = 8192,
        GUNNER = 16384,
        BARD = 32768,
        RIDER = 65536,
        ALL = WARRIOR | SCOUT | MAGE | CLERIC | ENGINEER | ARTIST | FIGHTER | KNIGHT | ASSASSIN | RANGER | WIZARD | ELEMENTALLIST | CHANTER | PRIEST | GUNNER | BARD | RIDER
    }

    [Serializable]
    public enum skillPlayerClass
    {
        ALL = 0,
        WARRIOR,
        SCOUT,
        MAGE,
        CLERIC,
        ENGINEER,
        ARTIST,
        FIGHTER,
        KNIGHT,
        ASSASSIN,
        RANGER,
        WIZARD,
        ELEMENTALLIST,
        CHANTER,
        PRIEST,
        GUNNER,
        BARD,
        RIDER
     }
}
