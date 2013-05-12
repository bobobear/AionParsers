namespace Jamie.Skills
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using Jamie.ParserBase;

	[XmlInclude(typeof(TargetRaceDamageModifier))]
	[XmlInclude(typeof(FlyingDamageModifier))]
	[XmlInclude(typeof(NonFlyingDamageModifier))]
	[XmlInclude(typeof(AbnormalDamageModifier))]
	[XmlInclude(typeof(FrontDamageModifier))]
	[XmlInclude(typeof(BackDamageModifier))]
	[Serializable]
	public abstract class ActionModifier
	{
	}

	[Serializable]
	public class TargetRaceDamageModifier : ActionModifier
	{
		[XmlAttribute]
		public int value;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;

		[XmlAttribute]
		public SkillTargetRace race;
	}

	[Serializable]
	public enum SkillTargetRace
	{
		PC_ALL = 0,
		ELYOS,
		ASMODIANS,
		LYCAN,
		CONSTRUCT,
		CARRIER,
		DRAKAN,
		LIZARDMAN,
		TELEPORTER,
		NAGA,
		BROWNIE,
		KRALL,
		SHULACK,
		PC_LIGHT_CASTLE_DOOR,
		PC_DARK_CASTLE_DOOR,
		DRAGON_CASTLE_DOOR,
		GCHIEF_LIGHT,
		GCHIEF_DARK,
		DRAGON,
		OUTSIDER,
		RATMAN,
		UNDEAD,
		BEAST,
		MAGICALMONSTER,
		ELEMENTAL,
        LIVINGWATER,
		TRICODARK,
        SIEGEDRAKAN
	}

	[Serializable]
	public partial class FrontDamageModifier : ActionModifier
	{
		[XmlAttribute]
		public int value;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;
	}

	[Serializable]
	public partial class BackDamageModifier : ActionModifier
	{
		[XmlAttribute]
		public int value;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;
	}

	[Serializable]
	public partial class FlyingDamageModifier : ActionModifier
	{
		[XmlAttribute]
		public int value;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;
	}

	[Serializable]
	public partial class NonFlyingDamageModifier : ActionModifier
	{
		[XmlAttribute]
		public int value;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;
	}

	[Serializable]
	public partial class AbnormalDamageModifier : ActionModifier
	{
		[XmlAttribute]
		public int value;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;

		[XmlAttribute]
		public TargetState state;
	}

	[Serializable]
	public partial class ActionModifiers
	{
		[XmlElement("backdamage", Form = XmlSchemaForm.Unqualified)]
		public BackDamageModifier backdamage;

		[XmlElement("frontdamage", Form = XmlSchemaForm.Unqualified)]
		public FrontDamageModifier frontdamage;

		[XmlElement("flyingdamage", Form = XmlSchemaForm.Unqualified)]
		public FlyingDamageModifier flyingdamage;

		[XmlElement("nonflyingdamage", Form = XmlSchemaForm.Unqualified)]
		public NonFlyingDamageModifier nonflyingdamage;

		[XmlElement("abnormaldamage", Form = XmlSchemaForm.Unqualified)]
		public List<AbnormalDamageModifier> abnormaldamage;

		[XmlElement("targetrace", Form = XmlSchemaForm.Unqualified)]
		public List<TargetRaceDamageModifier> targetrace;

		public bool Present {
			get {
				return backdamage != null || frontdamage != null || flyingdamage != null ||
					   nonflyingdamage != null || abnormaldamage != null || targetrace != null;
			}
		}
	}

	[Serializable]
    [Flags]
	public enum TargetState :long
	{
        NONE = 0,
		[EntryValue("_back")]
		BACK = 1,
		[EntryValue("_flying")]
		FLYING = 1<<1,
		[EntryValue("_front")]
		FRONT = 1<<2,
		[EntryValue("_nflying")]
		NON_FLYING = 1<<3,
		[EntryValue("bleed")]
		BLEED = 1<<4,
		[EntryValue("blind")]
		BLIND = 1<<5,
		[EntryValue("curse")]
		CURSE = 1<<6,
		[EntryValue("deform")]
		DEFORM = 1<<7,
		[EntryValue("fear")]
		FEAR = 1<<8,
		[EntryValue("openaerial")]
		OPENAERIAL = 1<<9,
		[EntryValue("paralyze")]
		PARALYZE = 1<<10,
		[EntryValue("poison")]
		POISON = 1<<11,
		[EntryValue("snare")]
		SNARE = 1<<12,
		[EntryValue("stumble")]
        STUMBLE = 1<<13,
		[EntryValue("stun")]
        STUN = 1<<14,
        [EntryValue("bind")]
        BIND = 1<<15,
        [EntryValue("stagger")]
        STAGGER = 1<<16,
        [EntryValue("spin")]
        SPIN = 1<<17,
        [EntryValue("Root")]
        Root = 1<<18,
        [EntryValue("_class_ranger")]
        CLASS_RANGER = 1<<19,
        [EntryValue("_class_assassin")]
        CLASS_ASSASSIN = 1<<20,
        [EntryValue("_class_priest")]
        CLASS_PRIEST = 1<<21,
        [EntryValue("_class_chanter")]
        CLASS_CHANTER = 1<<22,
        [EntryValue("_class_wizard")]
        CLASS_WARRIOR = 1<<23,
        [EntryValue("_class_scout")]
        SCOUT = 1<<24,
        [EntryValue("_class_mage")]
        MAGE = 1<<25,
        [EntryValue("_class_cleric")]
        CLERIC = 1<<26,
        [EntryValue("_class_engineer")]
        ENGINEER = 1<<27,
        [EntryValue("_class_artist")]
        ARTIST = 1<<28,
        [EntryValue("_class_fighter")]
        FIGHTER = 1<<29,
        [EntryValue("_class_knight")]
        KNIGHT = 1<<30,
        [EntryValue("_class_elementallist")]
        ELEMENTALLIST = 1<<31,
        [EntryValue("_class_gunner")]
        GUNNER = 1<<32,
        [EntryValue("_class_bard")]
        BARD = 1<<33,
        [EntryValue("sleep")]
        SLEEP = 1<<34,
        [EntryValue("disease")]
        DISEASE = (1<<35)

	}
}
