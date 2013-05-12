namespace Jamie.Skills {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Reflection;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using Jamie.ParserBase;

	[Serializable]
	public partial class SubEffect {
		[XmlAttribute]
		[DefaultValue(0)]
		public int chance;

		[XmlAttribute]
		public int skill_id;
	}

	[XmlInclude(typeof(AlwaysBlockEffect))]
	[XmlInclude(typeof(AlwaysDodgeEffect))]
	[XmlInclude(typeof(AlwaysParryEffect))]
	[XmlInclude(typeof(AlwaysResistEffect))]
	[XmlInclude(typeof(ArmorMasteryEffect))]
	[XmlInclude(typeof(AuraEffect))]
	[XmlInclude(typeof(BackDashEffect))]
	[XmlInclude(typeof(BindEffect))]
	[XmlInclude(typeof(BleedEffect))]
	[XmlInclude(typeof(BlindEffect))]
	[XmlInclude(typeof(BoostHateEffect))]
	[XmlInclude(typeof(BoostHealEffect))]
	[XmlInclude(typeof(BoostSkillCastingTimeEffect))]
	[XmlInclude(typeof(BufEffect))]
	[XmlInclude(typeof(BuffBindEffect))]
	[XmlInclude(typeof(BuffSilenceEffect))]
	[XmlInclude(typeof(BuffSleepEffect))]
	[XmlInclude(typeof(BuffStunEffect))]
	[XmlInclude(typeof(CarveSignetEffect))]
	[XmlInclude(typeof(ChangeMpConsumptionEffect))]
	[XmlInclude(typeof(CloseAerialEffect))]
	[XmlInclude(typeof(ConfuseEffect))]
	[XmlInclude(typeof(CurseEffect))]
	[XmlInclude(typeof(DashEffect))]
	[XmlInclude(typeof(DeboostHealEffect))]
	[XmlInclude(typeof(DeformEffect))]
	[XmlInclude(typeof(DelayDamageEffect))]
	[XmlInclude(typeof(DelayedFPAttackInstantEffect))]
	[XmlInclude(typeof(DiseaseEffect))]
	[XmlInclude(typeof(DispelBuffCounterAtkEffect))]
	[XmlInclude(typeof(DispelBuffEffect))]
	[XmlInclude(typeof(DispelDebuffEffect))]
	[XmlInclude(typeof(DispelDebuffMentalEffect))]
	[XmlInclude(typeof(DispelDebuffPhysicalEffect))]
	[XmlInclude(typeof(DispelEffect))]
	[XmlInclude(typeof(DpHealEffect))]
	[XmlInclude(typeof(DpHealInstantEffect))]
	[XmlInclude(typeof(EvadeEffect))]
	[XmlInclude(typeof(ExtendAuraRangeEffect))]
	[XmlInclude(typeof(FearEffect))]
	[XmlInclude(typeof(FpAttackEffect))]
	[XmlInclude(typeof(FpAttackInstantEffect))]
	[XmlInclude(typeof(FpHealEffect))]
	[XmlInclude(typeof(FpHealInstantEffect))]
	[XmlInclude(typeof(HealCastorOnAttackedEffect))]
	[XmlInclude(typeof(HealOverTimeEffect))]
	[XmlInclude(typeof(HealInstantEffect))]
	[XmlInclude(typeof(HideEffect))]
	[XmlInclude(typeof(HostileUpEffect))]
	//[XmlInclude(typeof(HpUseOverTimeEffect))]
	[XmlInclude(typeof(InvulnerableWingEffect))]
	[XmlInclude(typeof(ItemHealDpEffect))]
	[XmlInclude(typeof(ItemHealEffect))]
	[XmlInclude(typeof(ItemHealFpEffect))]
	[XmlInclude(typeof(ItemHealMpEffect))]
	[XmlInclude(typeof(MagicCounterAtkEffect))]
	[XmlInclude(typeof(MoveBehindEffect))]
	[XmlInclude(typeof(MpAttackEffect))]
	[XmlInclude(typeof(MpAttackInstantEffect))]
	[XmlInclude(typeof(MpHealEffect))]
	[XmlInclude(typeof(MpHealInstantEffect))]
	//[XmlInclude(typeof(MpUseOverTimeEffect))]
	[XmlInclude(typeof(NoFlyEffect))]
	[XmlInclude(typeof(OneTimeBoostHealEffect))]
	[XmlInclude(typeof(OneTimeBoostSkillAttackEffect))]
	[XmlInclude(typeof(OneTimeBoostSkillCriticalEffect))]
	[XmlInclude(typeof(OpenAerialEffect))]
	[XmlInclude(typeof(ParalyzeEffect))]
	[XmlInclude(typeof(PetOrderUseUltraSkillEffect))]
	[XmlInclude(typeof(PoisonEffect))]
	[XmlInclude(typeof(PolymorphEffect))]
	[XmlInclude(typeof(ProcAtkInstantEffect))]
	[XmlInclude(typeof(ProtectEffect))]
	[XmlInclude(typeof(ProvokerEffect))]
	[XmlInclude(typeof(PulledEffect))]
	[XmlInclude(typeof(RandomMoveLocEffect))]
	[XmlInclude(typeof(RebirthEffect))]
	[XmlInclude(typeof(RecallInstantEffect))]
	[XmlInclude(typeof(ReflectorEffect))]
	[XmlInclude(typeof(ResurrectBaseEffect))]
	[XmlInclude(typeof(ResurrectEffect))]
	[XmlInclude(typeof(ResurrectPositionalEffect))]
	[XmlInclude(typeof(ReturnEffect))]
	[XmlInclude(typeof(ReturnPointEffect))]
	[XmlInclude(typeof(RootEffect))]
	[XmlInclude(typeof(SearchEffect))]
	[XmlInclude(typeof(ShieldEffect))]
	[XmlInclude(typeof(ShieldMasteryEffect))]
	[XmlInclude(typeof(SignetBurstEffect))]
	[XmlInclude(typeof(SignetEffect))]
	[XmlInclude(typeof(SilenceEffect))]
	[XmlInclude(typeof(SkillAtkDrainInstantEffect))]
	[XmlInclude(typeof(SkillAttackInstantEffect))]
	[XmlInclude(typeof(SkillLauncherEffect))]
	[XmlInclude(typeof(SimpleRootEffect))]
	[XmlInclude(typeof(SleepEffect))]
	[XmlInclude(typeof(SlowEffect))]
	[XmlInclude(typeof(SnareEffect))]
	[XmlInclude(typeof(SpellAttackEffect))]
	[XmlInclude(typeof(SpellAtkDrainEffect))]
	[XmlInclude(typeof(SpellAtkDrainInstantEffect))]
	[XmlInclude(typeof(SpellAttackInstantEffect))]
	[XmlInclude(typeof(SpinEffect))]
	[XmlInclude(typeof(StaggerEffect))]
	[XmlInclude(typeof(StatboostEffect))]
	[XmlInclude(typeof(StatdownEffect))]
	[XmlInclude(typeof(StatupEffect))]
	[XmlInclude(typeof(StumbleEffect))]
	[XmlInclude(typeof(StunEffect))]
	[XmlInclude(typeof(SummonEffect))]
	[XmlInclude(typeof(SummonGroupGateEffect))]
	[XmlInclude(typeof(SummonHomingEffect))]
	[XmlInclude(typeof(SummonServantEffect))]
	[XmlInclude(typeof(SummonSkillAreaEffect))]
	[XmlInclude(typeof(SummonTotemEffect))]
	[XmlInclude(typeof(SummonTrapEffect))]
	[XmlInclude(typeof(SwitchHostileEffect))]
	[XmlInclude(typeof(SwitchHpMpEffect))]
	[XmlInclude(typeof(ShapeChangeEffect))]
	[XmlInclude(typeof(WeaponDualEffect))]
	[XmlInclude(typeof(WeaponMasteryEffect))]
	[XmlInclude(typeof(WeaponStatboostEffect))]
	[XmlInclude(typeof(WeaponStatupEffect))]
	[XmlInclude(typeof(XPBoostEffect))]
	[XmlInclude(typeof(CondSkillLauncherEffect))]
	[Serializable]
	public abstract partial class Effect : IDynamicImport<ClientEffect> {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SubEffect subeffect;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public ActionModifiers modifiers;

		[XmlElement("change", Form = XmlSchemaForm.Unqualified)]
		public ChangeList change;

		[XmlAttribute]
		[DefaultValue(0)]
		public int duration;

		[XmlAttribute]
		[DefaultValue(0)]
		public int checktime;

		[XmlAttribute]
		[DefaultValue(0)]
		public int effectid;

		[XmlAttribute]
		public int e;

		[XmlAttribute]
		[DefaultValue(0)]
		public int basiclvl;

		[XmlAttribute]
		[DefaultValue(SkillElement.NONE)]
		public SkillElement element;

		[XmlAttribute]
		[DefaultValue(HopType.NONE)]
		public HopType hoptype;

		[XmlAttribute]
		[DefaultValue(0)]
		public int hopb;

		[XmlAttribute]
		[DefaultValue(0)]
		public int hopa;

		[XmlAttribute]
		[DefaultValue(0)]
		public int randomtime;

		[XmlAttribute("accmod1")]
		[DefaultValue(0)]
		public int acc_mod1;

		[XmlAttribute("accmod2")]
		[DefaultValue(0)]
		public int acc_mod2;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool noresist;

		[XmlAttribute]
		[DefaultValue(0)]
		public int rng;

		[XmlAttribute]
		[DefaultValue(0)]
		public decimal preeffects_mask_prob;

		[XmlAttribute]
		[DefaultValue("0")]
		public string preeffects_mask;

		[XmlIgnore]
		public bool preeffects_mask_probSpecified;
		/*
		[XmlAttribute]
		[DefaultValue(1)]
		public decimal critical_prob;

		[XmlAttribute]
		[DefaultValue(TargetState.NONE)]
		public TargetState cond_effect;
		*/

		[XmlElement]
		[DefaultValue(null)]
		public Conditions conditions;

		#region IDynamicImport<ClientEffect> Members

		public virtual void Import(ClientEffect importObject, IEnumerable<FieldInfo> getters) {
			effectid = importObject.effectid;
			e = importObject.e;
			basiclvl = importObject.basiclv;
			hoptype = importObject.hop_type;
			hopa = importObject.hop_a;
			hopb = importObject.hop_b;
			if (importObject.remain[1].HasValue && importObject.remain[1].Value != 0)
				duration = importObject.remain[1].Value;
			if (importObject.remain[0].HasValue)
				duration += importObject.remain[0].Value;
			randomtime = importObject.randomtime;
			noresist = importObject.noresist;

			if (importObject.accuracy_modifiers != null && importObject.accuracy_modifiers[0] != null)
				acc_mod1 = importObject.accuracy_modifiers[0].Value;

			if (importObject.accuracy_modifiers != null && importObject.accuracy_modifiers[1] != null)
				acc_mod2 = importObject.accuracy_modifiers[1].Value;

			if (this is OverTimeEffect || this is SpellAtkDrainEffect || this is DiseaseEffect)
				checktime = importObject.checktime;

			if (importObject.reserved[9] != null) {
				try {
					element = (SkillElement)(Enum.Parse(typeof(SkillElement), importObject.reserved[9].Trim(), true));
					if (!Enum.IsDefined(typeof(SkillElement), element))
						element = SkillElement.NONE;
				}
				catch {
					Debug.Print("R9: {0}", importObject.reserved[9]);
				}
			}

			if (importObject.cond_preeffect != null) {
				this.preeffects_mask = importObject.cond_preeffect.Substring(1, importObject.cond_preeffect.Length -1);
				if (importObject.cond_preeffect_prob[1] != null)
					this.preeffects_mask_prob = importObject.cond_preeffect_prob[1].Value;
				else
					this.preeffects_mask_prob = 1;
			}

			if (this.preeffects_mask_prob != 1 && e > 1 && this.preeffects_mask_prob != 100)
				this.preeffects_mask_probSpecified = true;

			/* Moved to Conditions List Below
			if (importObject.critical_prob_modifiers[1] != null) {
				this.critical_prob = importObject.critical_prob_modifiers[1].Value;
			}
			else {
				this.critical_prob = 1;
			}

			if (!String.IsNullOrEmpty(importObject.cond_status)) {
				NamedEnum<TargetState> v = new NamedEnum<TargetState>(importObject.cond_status);
				TargetState state = v;
				this.cond_effect = state;
			}
			*/

			if (!String.IsNullOrEmpty(importObject.cond_status)) {
				NamedEnum<TargetState> v = new NamedEnum<TargetState>(importObject.cond_status);
				TargetState state = v;

				if (this.conditions == null)
					this.conditions = new Conditions();
				if (this.conditions.ConditionList == null)
					this.conditions.ConditionList = new List<Condition>();

				switch (state) {
					case TargetState.NON_FLYING: this.conditions.ConditionList.Add(new NoFlyCondition()); break;
					case TargetState.FLYING: this.conditions.ConditionList.Add(new TargetFlyingCondition()); break;
					default: this.conditions.ConditionList.Add(new AbnormalCondition(state)); break;
				}
			}
		}

		#endregion

		public void Import(ClientEffect importObject, Type effectClass,
						   int addValuePos, int addDeltaPos,
						   int percValuePos, int percDeltaPos, int percentCheckPos,
						   Stat overrideStat) {
			if (this.change == null)
				this.change = new ChangeList();

			Stat usedStat = overrideStat == Stat.None ? importObject.ChangeStat : overrideStat;

			List<Stat> stats = new List<Stat>();
			if (usedStat == Stat.ElementalDefendAll || usedStat == Stat.AllResist)
				stats.AddRange(new Stat[] { Stat.ElementalDefendWater, Stat.ElementalDefendAir, 
											Stat.ElementalDefendFire, Stat.ElementalDefendEarth });
			else if (usedStat == Stat.AllSpeed)
				stats.AddRange(new Stat[] { Stat.Speed, Stat.FlySpeed });
			else if (usedStat == Stat.PMAttack)
				stats.AddRange(new Stat[] { Stat.PhyAttack, Stat.MagicalAttack });
			else if (usedStat == Stat.PMDefend)
				stats.AddRange(new Stat[] { Stat.PhysicalDefend, Stat.MagicalResist });
			else if (usedStat == Stat.ActiveDefend)
				stats.AddRange(new Stat[] { Stat.Dodge, Stat.Parry, Stat.Block });
			else if (usedStat == Stat.ArStunLike)
				stats.AddRange(new Stat[] { Stat.ArStun, Stat.ArStumble, Stat.ArStagger, Stat.ArSpin, Stat.OpenAerial });
			else
				stats.Add(usedStat);

			foreach (Stat stat in stats) {
				var change = new Change();
				change.stat = new NamedEnum<modifiersenum>(stat.ToString());

				if (change.stat == modifiersenum.NONE) continue;

				int isPercent = 0;
				if (importObject.reserved[percentCheckPos] != null) {
					isPercent = Int32.Parse(importObject.reserved[percentCheckPos].Trim());
					bool overideFunction = false;
					int valueIdx;
					int deltaIdx;

					// Speed should always be evaluated as % even though description may not show it.
					if (stat == Stat.Speed || stat == Stat.FlySpeed) {
						overideFunction = true;
					}
					
					if (isPercent == 1 || overideFunction == true) {
						change.func = StatFunc.PERCENT;
						valueIdx = percValuePos;
						deltaIdx = percDeltaPos;
					}
					else {
						change.func = StatFunc.ADD;
						valueIdx = addValuePos;
						deltaIdx = addDeltaPos;
					}
					// zer0patches : Need to update positions maybe?
					if (importObject.reserved[valueIdx] != null) {
						change.value = Int32.Parse(importObject.reserved[valueIdx].Trim());
					}
					else {
						change.value = 1;
					}
					if (importObject.reserved[deltaIdx] != null)
						change.delta = Int32.Parse(importObject.reserved[deltaIdx].Trim());
				}
				else {
					if (importObject.reserved[addValuePos] != null) {
						change.func = StatFunc.ADD;
						change.value = Int32.Parse(importObject.reserved[addValuePos].Trim());
						if (importObject.reserved[addDeltaPos] != null)
							change.delta = Int32.Parse(importObject.reserved[addDeltaPos].Trim());
					}
					if (change.value == 0 && change.delta == 0) {
						change.func = StatFunc.PERCENT;

						// zer0patches : Need to update positions maybe?
						if (importObject.reserved[percValuePos] != null) {
							change.value = Int32.Parse(importObject.reserved[percValuePos].Trim());
						}
						else {
							change.value = 1;
						}

						if (importObject.reserved[percDeltaPos] != null)
							change.delta = Int32.Parse(importObject.reserved[percDeltaPos].Trim());
					}
				}

				if (stat.ToString().EndsWith("Delay") || effectClass.Equals(typeof(CurseEffect))) {
					change.value = -change.value;
					//if (effectClass.Equals(typeof(SlowEffect)))
					change.delta = -change.delta;
				}

				if ((stat == Stat.Speed || stat == Stat.FlySpeed) && change.func == StatFunc.ADD) {
					//change.value *= 100;
				}

				if (stat == Stat.AttackRange && change.func == StatFunc.ADD)
					change.value *= 1000;

				if (importObject.type == EffectType.WeaponStatBoost || importObject.type == EffectType.WeaponStatUp) {
					if(change.conditions == null)
						change.conditions = new Conditions();
					if(change.conditions.ConditionList == null)
						change.conditions.ConditionList = new List<Condition>();
					WeaponCondition condition = new WeaponCondition();

					// Make some corrections to maintain server compatibility with exisitng enumerations
					string required_weapons = importObject.reserved[4].ToUpper().Trim();
					if (required_weapons.Contains("2H_SWORD")) required_weapons = required_weapons.Replace("2H_SWORD", "SWORD_2H");

					condition.weapon = required_weapons;
					change.conditions.ConditionList.Add(condition);
				}

				if (importObject.reserved[8] != null && importObject.reserved[8].Trim() == "1") {
					Condition flycondition = new OnFlyCondition();
					if (change.conditions == null)
						change.conditions = new Conditions();
					if (change.conditions.ConditionList == null)
						change.conditions.ConditionList = new List<Condition>();
					change.conditions.ConditionList.Add(flycondition);
				}

				this.change.Add(change);
			}
		}
	}

	[Serializable]
	public enum HopType {
		NONE = 0,
		DAMAGE,
		SKILLLV,
	}

	[Serializable]
	public enum SkillElement {
		NONE = 0,
		FIRE,
		WIND,
		AIR = WIND,
		WATER,
		EARTH,
		LIGHT,
		DARK,
	}
}
