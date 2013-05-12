namespace Jamie.Skills {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Serialization;
	using System.Reflection;
	using Jamie.ParserBase;

	[Serializable]
	public enum AttackType {
		ALL = 0,
		ATTACKED = 1,
		PHYSICAL_SKILL = 2,
		MAGICAL_SKILL = 3,
		ATTACK = 4,
	}

	[Serializable]
	public class ShieldEffect : Effect {
		[XmlAttribute]
		public int value;

		[XmlIgnore]
		[DefaultValue(false)]
		public bool valueSpecified;

		[XmlAttribute]
		[DefaultValue(0)]
		public int delta;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool percent;

		[XmlAttribute]
		[DefaultValue(0)]
		public int hitvalue;

		[XmlAttribute]
		[DefaultValue(0)]
		public int hitdelta;

		[XmlAttribute]
		[DefaultValue(SkillTargetRace.PC_ALL)]
		public SkillTargetRace condrace;
		/*
		[XmlAttribute]
		[DefaultValue(null)]
		public HitType attacktype;

		[XmlAttribute]
		public int probability;
		*/
		[XmlAttribute]
		[DefaultValue(null)]
		public string hittype;

		public override void Import(ClientEffect importObject, IEnumerable<FieldInfo> getters) {
			base.Import(importObject, getters);
			percent = importObject.reserved[5] != null && importObject.reserved[5].Trim() == "1";
			if (importObject.cond_race != null)
				condrace = (SkillTargetRace)Enum.Parse(typeof(SkillTargetRace), importObject.cond_race.Trim(),
														true);
/*
			HitType hitType = (HitType)Enum.Parse(typeof(HitType), importObject.reserved_cond1.Trim(), true);
			if (Enum.IsDefined(typeof(HitType), hitType)) {
				attacktype = (AttackType)hitType;
				//probability = importObject.reserved_cond1_prob[1].Value;
			}
*/

			if (importObject.reserved_cond1 != null) {
				string type = importObject.reserved_cond1.Trim().ToUpper();
				if (type != "EVERYHIT") this.hittype = type;
			}

			if (this.GetType().Equals(typeof(ProtectEffect)) ||
				this.GetType().Equals(typeof(ReflectorEffect))) {
				if (importObject.reserved[8] == null) {
					if (importObject.reserved[7] != null) {
						value = Int32.Parse(importObject.reserved[7].Trim());
						if(importObject.reserved[7] != null)
							valueSpecified = true;
					}
					if (importObject.reserved[6] != null)
						delta = Int32.Parse(importObject.reserved[6].Trim());
				}
				else {
					value = Int32.Parse(importObject.reserved[8].Trim());
					if (importObject.reserved[8] != null)
						valueSpecified = true;
					delta = Int32.Parse(importObject.reserved[7].Trim());
				}
			}
			else if (this.GetType() != typeof(ProvokerEffect)) {
				value = Int32.Parse(importObject.reserved[7].Trim());
				if (importObject.reserved[7] != null)
					valueSpecified = true;
                if (importObject.reserved[6] != null)
				    delta = Int32.Parse(importObject.reserved[6].Trim());
				hitvalue = Int32.Parse(importObject.reserved[1].Trim());
                if (importObject.reserved[0] != null)
				    hitdelta = Int32.Parse(importObject.reserved[0].Trim());
			}
		}
	}

	[Serializable]
	public sealed class ProtectEffect : ShieldEffect {
		[XmlAttribute]
		public int radius;

		public override void Import(ClientEffect importObject, IEnumerable<FieldInfo> getters) {
			base.Import(importObject, getters);
			this.radius = Int32.Parse(importObject.reserved[4].Trim());

			// always true for protect effect
			base.percent = true;
		}
	}

	[Serializable]
	public sealed class ReflectorEffect : ShieldEffect {
		[XmlAttribute]
		public int radius;

		public override void Import(ClientEffect importObject, IEnumerable<FieldInfo> getters) {
			base.Import(importObject, getters);
			this.radius = Int32.Parse(importObject.reserved[4].Trim());

			if (importObject.reserved[1] != null)
				hitvalue = Int32.Parse(importObject.reserved[1].Trim());
			if (importObject.reserved[0] != null)
				hitdelta = Int32.Parse(importObject.reserved[0].Trim());
			if (importObject.reserved[7] != null) {
				this.value = Int32.Parse(importObject.reserved[7].Trim());
				this.valueSpecified = true;
			}
		}
	}

	[Serializable]
	public sealed class ProvokerEffect : ShieldEffect {
		[XmlAttribute]
		[DefaultValue(0)]
		public int hittypeprob2;

		[XmlAttribute]
		[DefaultValue(0)]
		public int radius;

		[XmlAttribute]
		public int skill_id;

		[XmlAttribute]
		[DefaultValue(ProvokeTarget.NONE)]
		public ProvokeTarget provoke_target;

		public override void Import(ClientEffect importObject, IEnumerable<FieldInfo> getters) {
			base.Import(importObject, getters);

			string provokerSkill = importObject.reserved[16].Trim();

			try {
				this.skill_id = Utility.SkillIndex[provokerSkill].id;
			}
			catch {
				// TODO: Add Error Handling for unmatched skill and log
				return;
			}

			provoke_target = (ProvokeTarget)Enum.Parse(typeof(ProvokeTarget), importObject.reserved[13], true);

			if (importObject.reserved_cond1 != null) {
				string type = importObject.reserved_cond1.Trim().ToUpper();
				if(type != "EVERYHIT") this.hittype = type;

				/*
				HitType hitType = (HitType)Enum.Parse(typeof(HitType), importObject.reserved_cond1.Trim(), true);
				if (Enum.IsDefined(typeof(HitType), hitType)) {
					base.attacktype = (AttackType)hitType;
					//base.probability = importObject.reserved_cond1_prob[1].Value;
				}
				*/
			}
			if (importObject.reserved_cond1_prob[1] != null) {
				this.hittypeprob2 = importObject.reserved_cond1_prob[1].Value;
			}

			if (importObject.reserved[4] != null) {
				this.radius = Int32.Parse(importObject.reserved[4].Trim());
			}
		}
	}

	[Serializable]
	public enum ProvokeTarget {
		NONE = 0,
		ME,
		OPPONENT,
	}
}
