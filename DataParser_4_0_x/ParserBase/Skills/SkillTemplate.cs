namespace Jamie.Skills {
	using System;
	using System.Collections.Generic;
	using System.Xml.Serialization;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Reflection;
	using System.Diagnostics;
	using System.Linq;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot("skill_data", Namespace = "", IsNullable = false)]
	public partial class SkillData {
		[XmlElement("skill_template", Form = XmlSchemaForm.Unqualified)]
		public List<SkillTemplate> SkillList = new List<SkillTemplate>();
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class SkillTemplate {

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Properties initproperties;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Conditions startconditions;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Properties setproperties;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Conditions useconditions;

        //[XmlElement(Form = XmlSchemaForm.Unqualified)]
        //public Conditions useequipmentconditions;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Effects effects;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Actions actions;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        [DefaultValue(null)]
        public Motion motion;

        [XmlAttribute]
        [DefaultValue(0)]
        public int penalty_skill_id;

		[XmlAttribute]
		public int skill_id;

		[XmlAttribute]
		public string name;

		[XmlAttribute]
		[DefaultValue(0)]
		public int nameId;

		[XmlAttribute]
		public string stack;

		[XmlAttribute]
		public int lvl;

		[XmlAttribute]
		public skillType skilltype;

		[XmlAttribute]
		public skillSubType skillsubtype;

		[XmlAttribute]
		public TargetSlot tslot;

		[XmlAttribute]
		[DefaultValue(0)]
		public int tslot_level;

		[XmlAttribute]
		public activationAttribute activation;

        [XmlAttribute]
        public int duration;

        [XmlAttribute]
        [DefaultValue(null)]
        public string dispel_category;

        /*
        [XmlAttribute("req_dispel_level")]
        [DefaultValue(0)]
        public int dispel_level;
         */

		[XmlAttribute]
		public int cooldown;

		[XmlAttribute]
		[DefaultValue(0)]
		public int pvp_damage;

		[XmlAttribute]
		[DefaultValue(0)]
		public int pvp_duration;

        // missing self_chain_count
        [XmlAttribute]
        [DefaultValue(0)]
        private int self_chain_count;

        [XmlAttribute]
        [DefaultValue(0)]
        public int chain_skill_prob;

		[XmlAttribute]
		[DefaultValue(0)]
		public int cancel_rate;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool stance;

     
		[XmlAttribute("cooldownId")]
		[DefaultValue(0)]
		public int delay_id;
        

		[XmlAttribute]
		[DefaultValue(0)]
		public int skillset_exception;


        /*
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(null)]
		public PeriodicAction periodicactions;
         */


		public SkillTemplate() {
			this.cooldown = 0;
			this.chain_skill_prob = 0;
			this.cancel_rate = 0;
			this.stance = false;
			this.skillset_exception = 0;
		}

		public SkillTemplate(int skillId, int skillLevel) {
			this.skill_id = skillId;
			this.lvl = skillLevel;
		}
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class Motion {
		[XmlAttribute]
		[DefaultValue(null)]
		public string name;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool instant_skill;

		[XmlAttribute]
		[DefaultValue(0)]
		public int speed;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class PeriodicAction {
		[XmlAttribute]
		[DefaultValue(0)]
		public int checktime;

		[XmlElement]
		[DefaultValue(null)]
		public MpUseAction mpuse;

		[XmlElement]
		[DefaultValue(null)]
		public HpUseAction hpuse;
	}

	[Serializable]
	public enum skillSubType {
		NONE = 0,
		ATTACK = 1,
		CHANT = 2,
		HEAL = 3,
		BUFF = 4,
		DEBUFF = 5,
		SUMMON = 6,
		SUMMONHOMING = 7,
		SUMMONTRAP = 8,
	}

	[Serializable]
	public enum FlyRestriction {
		NONE = 0,
		ALL = 1,
		FLY = 2,
		GROUND = 3
	}

	[Serializable]
	public enum TargetSlot {
		NONE = 0,
		BUFF = 1,
		DEBUFF = 2,
		SPEC = 3,
		SPEC2 = 4,
		BOOST = 5,
		NOSHOW = 6,
		CHANT = 7,
	}

	[Serializable]
	public enum activationAttribute {
		NONE = 0,
		ACTIVE = 1,
		PROVOKED = 2,
		MAINTAIN = 3,
		TOGGLE = 4,
		PASSIVE = 5,
        CHARGE  = 6,
	}

	[Serializable]
	public enum DispelCategory {
		NONE = 0,
		ALL = 1,
		BUFF = 2,
        Debuff =3,
		DEBUFF_MENTAL = 4,
		DEBUFF_PHYSICAL = 5,
		EXTRA = 6,
		NPC_BUFF = 7,
		NPC_DEBUFF_PHYSICAL = 8,
		STUN = 9,
	}
}
