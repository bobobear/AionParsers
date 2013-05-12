namespace Jamie.Skills
{
	using System;
	using System.Collections.Generic;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using System.ComponentModel;

	[XmlInclude(typeof(ArrowCheckCondition))]
	[XmlInclude(typeof(TargetFlyingCondition))]
	[XmlInclude(typeof(TargetCondition))]
	[XmlInclude(typeof(SelfCondition))]
	[XmlInclude(typeof(PlayerMovedCondition))]
	[XmlInclude(typeof(DpCondition))]
	[XmlInclude(typeof(HpCondition))]
	[XmlInclude(typeof(MpCondition))]
	[XmlInclude(typeof(WeaponCondition))]
	[XmlInclude(typeof(ChainCondition))]
	[XmlInclude(typeof(ArmorCondition))]
	[XmlInclude(typeof(OnFlyCondition))]
	[XmlInclude(typeof(CombatCheckCondition))]
	[XmlInclude(typeof(AbnormalCondition))]

	[Serializable]
	public abstract partial class Condition
	{
	}

	[Serializable]
	public partial class ArrowCheckCondition : Condition
	{
	}

	[Serializable]
	public partial class TargetFlyingCondition : Condition
	{
		public TargetFlyingCondition() { }

		public TargetFlyingCondition(FlyRestriction value) {
			this.restriction = value;
		}

		[XmlAttribute]
		[DefaultValue(FlyRestriction.NONE)]
		public FlyRestriction restriction;
	}

	[Serializable]
	public partial class TargetCondition : Condition {
		public TargetCondition() { }

		public TargetCondition(TargetAttribute value) {
			this.value = value;
		}

		[XmlAttribute]
		[DefaultValue(TargetAttribute.NONE)]
		public TargetAttribute value;
	}

	[Serializable]
	public partial class SelfCondition : Condition
	{
		public SelfCondition() { }

		public SelfCondition(FlyRestriction value) {
			this.restriction = value;
		}

		[XmlAttribute]
		[DefaultValue(FlyRestriction.NONE)]
		public FlyRestriction restriction;
	}

	[Serializable]
	public enum TargetAttribute
	{
		NONE = 0,
		SELF = 1,
		NPC = 2,
		PC = 3,
		ALL = 4,
	}

	[Serializable]
	public partial class PlayerMovedCondition : Condition
	{
		public PlayerMovedCondition() { }

		public PlayerMovedCondition(bool allow) {
			this.allow = allow;
		}

		[XmlAttribute]
		public bool allow;
	}

	[Serializable]
	public partial class DpCondition : Condition
	{
		public DpCondition() { }

		public DpCondition(int value) {
			this.value = value;
		}

		[XmlAttribute]
		public int value;
	}

	[Serializable]
	public partial class HpCondition : Condition
	{
		public HpCondition() { }

		public HpCondition(int value, int delta) {
			this.value = value;
			this.delta = delta;
		}

		[XmlAttribute]
		public int value;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;
	}

	[Serializable]
	public partial class MpCondition : Condition
	{
		public MpCondition() { }

		public MpCondition(int value, int delta) {
			this.value = value;
			this.delta = delta;
		}

		[XmlAttribute]
		public int value;

		[XmlAttribute]
		public int delta;
	}

	[Serializable]
	public partial class WeaponCondition : Condition {
		public WeaponCondition() { }

		public WeaponCondition(string weapon) {
			this.weapon = weapon;
		}

		[XmlAttribute]
		public string weapon;
	}

	[Serializable]
	public partial class ChainCondition : Condition {
		[XmlAttribute]
		public string category;

		[XmlAttribute]
		[DefaultValue(null)]
		public string precategory;

		[XmlAttribute]
		[DefaultValue(0)]
		public int time;
	}

	[Serializable]
	public partial class ArmorCondition : Condition {
		public ArmorCondition() { }

		public ArmorCondition(string armor) {
			this.armor = armor;
		}

		[XmlAttribute]
		public string armor;
	}

	[Serializable]
	public partial class OnFlyCondition : Condition {
		public OnFlyCondition() { }
	}

	[Serializable]
	public partial class CombatCheckCondition : Condition {
		public CombatCheckCondition() { }
	}

	[Serializable]
	public partial class NoFlyCondition : Condition {
		public NoFlyCondition() { }
	}

	[Serializable]
	public partial class AbnormalCondition : Condition {
		public AbnormalCondition() { }

		public AbnormalCondition(TargetState value) {
			this.value = value;
		}

		[XmlAttribute]
		public TargetState value;
	}

	[Serializable]
	public partial class Conditions
	{
		[XmlElement("arrowcheck", Form = XmlSchemaForm.Unqualified, Type = typeof(ArrowCheckCondition))]
		[XmlElement("abnormal", Form = XmlSchemaForm.Unqualified, Type = typeof(AbnormalCondition))]
		[XmlElement("targetflying", Form = XmlSchemaForm.Unqualified, Type = typeof(TargetFlyingCondition))]
		[XmlElement("mp", Form = XmlSchemaForm.Unqualified, Type = typeof(MpCondition))]
		[XmlElement("hp", Form = XmlSchemaForm.Unqualified, Type = typeof(HpCondition))]
		[XmlElement("dp", Form = XmlSchemaForm.Unqualified, Type = typeof(DpCondition))]
		[XmlElement("playermove", Form = XmlSchemaForm.Unqualified, Type = typeof(PlayerMovedCondition))]
		[XmlElement("selfflying", Form = XmlSchemaForm.Unqualified, Type = typeof(SelfCondition))]
		[XmlElement("armor", Form = XmlSchemaForm.Unqualified, Type = typeof(ArmorCondition))]
		[XmlElement("weapon", Form = XmlSchemaForm.Unqualified, Type = typeof(WeaponCondition))]
		[XmlElement("chain", Form = XmlSchemaForm.Unqualified, Type = typeof(ChainCondition))]
		[XmlElement("onfly", Form = XmlSchemaForm.Unqualified, Type = typeof(OnFlyCondition))]
		[XmlElement("noflying", Form = XmlSchemaForm.Unqualified, Type = typeof(NoFlyCondition))]
		[XmlElement("combatcheck", Form = XmlSchemaForm.Unqualified, Type = typeof(CombatCheckCondition))]
		[XmlElement("target", Form = XmlSchemaForm.Unqualified, Type = typeof(TargetCondition))]
		public List<Condition> ConditionList;
	}
}
