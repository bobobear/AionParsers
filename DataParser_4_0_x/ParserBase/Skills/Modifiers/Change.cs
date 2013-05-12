using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Jamie.ParserBase;

namespace Jamie.Skills
{
    [Serializable]
    public partial class Change
    {
        [XmlAttribute]
        [DefaultValue(modifiersenum.NONE)]
        public modifiersenum stat;

        [XmlAttribute]
        public StatFunc func;

        [XmlAttribute]
        [DefaultValue(0)]
        public int delta;

        [XmlAttribute]
        public int value;

        [XmlAttribute]
        [DefaultValue(false)]
        public bool @unchecked;

        [XmlElement]
        [DefaultValue(null)]
        public Conditions conditions;

        public Change()
        {
            this.@unchecked = false;
        }
    }

    [Serializable]
    public enum modifiersenum
    {
        [EntryValue("None")]
        NONE = 0,
        [EntryValue("ArAll")]
        ABNORMAL_RESISTANCE_ALL,
        [EntryValue("Dex")]
        ACCURACY,
        [EntryValue("Agi")]
        AGILITY,
        [EntryValue("AGILITY_DUPE")]
        AGILITY_DUPE,
        [EntryValue("AllPara")]
        ALLPARA,
        [EntryValue("ALLRESIST")]
        ALLRESIST,
        [EntryValue("AttackRange")]
        ATTACK_RANGE,
        [EntryValue("AttackDelay")]
        ATTACK_SPEED,
        [EntryValue("BLEED_RESISTANCE")]
        BLEED_RESISTANCE,
        [EntryValue("BLIND_RESISTANCE")]
        BLIND_RESISTANCE,
        [EntryValue("Block")]
        BLOCK,
        [EntryValue("BoostCastingTime")]
        BOOST_CASTING_TIME,
        [EntryValue("BoostSkillCastingTime")]
        BOOST_CASTING_TIME_HEAL,
        [EntryValue("BOOST_HATE")]
        BOOST_HATE,
        [EntryValue("MagicalSkillBoost")]
        BOOST_MAGICAL_SKILL,
        [EntryValue("MantraRangeBoost")]
        BOOST_MANTRA_RANGE,
        [EntryValue("CHARM_RESISTANCE")]
        CHARM_RESISTANCE,
        [EntryValue("Concentration")]
        CONCENTRATION,
        [EntryValue("CONFUSE_RESISTANCE")]
        CONFUSE_RESISTANCE,
        [EntryValue("CURSE_RESISTANCE")]
        CURSE_RESISTANCE,
        [EntryValue("DAMAGE_REDUCE")]
        DAMAGE_REDUCE,
        [EntryValue("DISEASE_RESISTANCE")]
        DISEASE_RESISTANCE,
        [EntryValue("ElementalDefendEarth")]
        EARTH_RESISTANCE,
        [EntryValue("ElementalDefendDark")]
        ELEMENTAL_RESISTANCE_DARK,
        [EntryValue("ElementalDefendLight")]
        ELEMENTAL_RESISTANCE_LIGHT,
        [EntryValue("ErAir")]
        ERAIR,
        [EntryValue("ErEarth")]
        EREARTH,
        [EntryValue("ErFire")]
        ERFIRE,
        [EntryValue("ErWater")]
        ERWATER,
        [EntryValue("Dodge")]
        EVASION,
        [EntryValue("FEAR_RESISTANCE")]
        FEAR_RESISTANCE,
        [EntryValue("ElementalDefendFire")]
        FIRE_RESISTANCE,
        [EntryValue("FlySpeed")]
        FLY_SPEED,
        [EntryValue("MaxFP")]
        FLY_TIME,
        [EntryValue("HealBoost")]
        HEAL_BOOST,
        [EntryValue("HealSkillBoost")]
        HEAL_SKILL_BOOST,
        [EntryValue("Vit")]
        HEALTH,
        [EntryValue("HIT_COUNT")]
        HIT_COUNT,
        [EntryValue("IS_MAGICAL_ATTACK")]
        IS_MAGICAL_ATTACK,
        [EntryValue("KnoWil")]
        KNOWIL,
        [EntryValue("Kno")]
        KNOWLEDGE,
        [EntryValue("MagicalHitAccuracy")]
        MAGICAL_ACCURACY,
        [EntryValue("MagicalAttack")]
        MAGICAL_ATTACK,
        [EntryValue("MagicalCritical")]
        MAGICAL_CRITICAL,
        [EntryValue("MagicalCriticalDamageReduce")]
        MAGICAL_CRITICAL_DAMAGE_REDUCE,
        [EntryValue("MagicalCriticalReduceRate")]
        MAGICAL_CRITICAL_RESIST,
        [EntryValue("MagicalDefend")]
        MAGICAL_DEFEND,
        [EntryValue("MagicalResist")]
        MAGICAL_RESIST,
        [EntryValue("magicalskillboostresist")]
        MAGICALSKILLBOOST_RESIST,
        [EntryValue("MAIN_HAND_ACCURACY")]
        MAIN_HAND_ACCURACY,
        [EntryValue("MAIN_HAND_CRITICAL")]
        MAIN_HAND_CRITICAL,
        [EntryValue("MAIN_HAND_POWER")]
        MAIN_HAND_POWER,
        [EntryValue("MAX_DAMAGES")]
        MAX_DAMAGES,
        [EntryValue("MAXDP")]
        MAXDP,
        [EntryValue("MaxHP")]
        MAXHP,
        [EntryValue("MAXHP_DUPE")]
        MAXHP_DUPE,
        [EntryValue("MaxMP")]
        MAXMP,
        [EntryValue("MIN_DAMAGES")]
        MIN_DAMAGES,
        [EntryValue("OFF_HAND_ACCURACY")]
        OFF_HAND_ACCURACY,
        [EntryValue("OFF_HAND_CRITICAL")]
        OFF_HAND_CRITICAL,
        [EntryValue("OFF_HAND_POWER")]
        OFF_HAND_POWER,
        [EntryValue("openareial_arp")]
        BOOST_OPENAREIAL,
        [EntryValue("OpenAerial")]
        OPENAREIAL_RESISTANCE,
        [EntryValue("PARALYZE_RESISTANCE")]
        PARALYZE_RESISTANCE,
        [EntryValue("Parry")]
        PARRY,
        [EntryValue("PERIFICATION_RESISTANCE")]
        PERIFICATION_RESISTANCE,
        [EntryValue("HitAccuracy")]
        PHYSICAL_ACCURACY,
        [EntryValue("PhyAttack")]
        PHYSICAL_ATTACK,
        [EntryValue("Critical")]
        PHYSICAL_CRITICAL,
        [EntryValue("PhysicalCriticalDamageReduce")]
        PHYSICAL_CRITICAL_DAMAGE_REDUCE,
        [EntryValue("PhysicalCriticalReduceRate")]
        PHYSICAL_CRITICAL_RESIST,
        [EntryValue("PhysicalDefend")]
        PHYSICAL_DEFENSE,
        [EntryValue("POISON_RESISTANCE")]
        POISON_RESISTANCE,
        [EntryValue("Str")]
        POWER,
        [EntryValue("PVPAttackRatio")]
        PVP_ATTACK_RATIO,
        [EntryValue("PVPDefendRatio")]
        PVP_DEFEND_RATIO,
        [EntryValue("FPRegen")]
        REGEN_FP,
        [EntryValue("HPRegen")]
        REGEN_HP,
        [EntryValue("MPRegen")]
        REGEN_MP,
        [EntryValue("ArRoot")]
        ROOT_RESISTANCE,
        [EntryValue("SILENCE_RESISTANCE")]
        SILENCE_RESISTANCE,
        [EntryValue("ArSleep")]
        SLEEP_RESISTANCE,
        [EntryValue("SLOW_RESISTANCE")]
        SLOW_RESISTANCE,
        [EntryValue("ArSnare")]
        SNARE_RESISTANCE,
        [EntryValue("Speed")]
        SPEED,
        [EntryValue("Spin_Arp")]
        BOOST_SPIN,
        [EntryValue("ArSpin")]
        SPIN_RESISTANCE,
        [EntryValue("Stagger_Arp")]
        STAGGER_RESISTANCE_PENETRATION,
        [EntryValue("ArStagger")]
        STAGGER_RESISTANCE,
        [EntryValue("Stumble_Arp")]
        STUMBLE_RESISTANCE_PENETRATION,
        [EntryValue("ArStumble")]
        STUMBLE_RESISTANCE,
        [EntryValue("Stun_Arp")]
        STUN_RESISTANCE_PENETRATION,
        [EntryValue("ArStun")]
        STUN_RESISTANCE,
        [EntryValue("STUNLIKE_RESISTANCE")]
        STUNLIKE_RESISTANCE,
        [EntryValue("arDeform")]
        DEFORM_RESISTANCE,
        [EntryValue("ElementalDefendWater")]
        WATER_RESISTANCE,
        [EntryValue("Wil")]
        WILL,
        [EntryValue("ElementalDefendAir")]
        WIND_RESISTANCE,
        [EntryValue("arParalyze")]//4.0 UNK
        AR_PARALYZE,
        [EntryValue("arSilence")]//4.0 UNK
        AR_SILENCE,
        [EntryValue("arFear")]//4.0 UNK
        AR_FEAR,
        [EntryValue("arPulled")]//4.0 UNK
        AR_PULLED,
        [EntryValue("BoostChargeTime")]//4.0 UNK
        BOOST_CHARGE_TIME,
        [EntryValue("SkillCooltimeReset")]//4.0 UNK
        SKILL_COOLTIME_RESET,
        [EntryValue("SummonHouseGate")]//4.0 UNK
        SUMMON_HOUSE_GATE,
        [EntryValue("SummonFunctionalNPC")]//4.0 UNK
        SUMMON_FUNCTIONAL_NPC,
        [EntryValue("RideRobot")]//4.0 UNK
        RIDE_ROBOT,
        [EntryValue("CaseHeal")]//4.0 UNK
        CASE_HEAL,
        [EntryValue("Escape")]//4.0 UNK
        ESCAPE,
        [EntryValue("Flyoff")]//4.0 UNK
        FLYOFF,
        [EntryValue("ProcVPHeal_Instant")]//4.0 UNK
        PROC_VPHEAL_INSTANT
    }

    [Serializable]
    public enum StatFunc
    {
        NONE = 0,
        ADD,
        PERCENT,
        REPLACE,
    }
}
