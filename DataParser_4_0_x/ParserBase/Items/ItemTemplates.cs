namespace Jamie.Items {
	using System;
	using System.ComponentModel;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using Jamie.Skills;
	using Jamie.ParserBase;
	using Jamie.Pets;

	[Serializable]
	public class ItemFeed {
		[XmlAttribute]
		public FoodType type;

		[XmlAttribute]
		[DefaultValue(0)]
		public int level;
	}

	[Serializable]
	public class RequireSkill {
		[XmlElement("skillId", Form = XmlSchemaForm.Unqualified)]
		public int[] skillId;

		[XmlAttribute]
		public int skilllvl;

		[XmlIgnore]
		public bool skilllvlSpecified;
	}

	[Serializable]
	public class Stigma {
		[XmlElement("require_skill", Form = XmlSchemaForm.Unqualified)]
		public RequireSkill[] require_skill;

		[XmlAttribute]
		public int shard;

		[XmlIgnore]
		public bool shardSpecified;

		[XmlAttribute]
		public int skilllvl;

		[XmlIgnore]
		public bool skilllvlSpecified;

		[XmlAttribute]
		public int skillid;

		[XmlIgnore]
		public bool skillidSpecified;
	}

	[Serializable]
	public class Godstone {
		[XmlAttribute]
		public int probability;

		[XmlIgnore]
		public bool probabilitySpecified;

		[XmlAttribute]
		[DefaultValue(0)]
		public int probabilityleft;

		[XmlAttribute]
		public int skilllvl;

		[XmlIgnore]
		public bool skilllvlSpecified;

		[XmlAttribute]
		public int skillid;

		[XmlIgnore]
		public bool skillidSpecified;
	}

	[Serializable]
	public class TradeItemList {
		[XmlElement("tradein_item", Form = XmlSchemaForm.Unqualified)]
		public TradeItem[] tradeItems;
	}

	[Serializable]
	public partial class WeaponStats {
		[XmlAttribute("min_damage", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int minDamage;

		[XmlAttribute("max_damage", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int maxDamage;

		[XmlAttribute("attack_speed", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int attackSpeed;

		[XmlAttribute("physical_critical", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int physicalCritical;

		[XmlAttribute("physical_accuracy", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int physicalAccuracy;

		[XmlAttribute("parry", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int parry;

		[XmlAttribute("magical_accuracy", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int magicalAccuracy;

		[XmlAttribute("boost_magical_skill", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int boostMagicalSkill;

		[XmlAttribute("attack_range", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int attackRange;

		[XmlAttribute("hit_count", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int hitCount;

		[XmlAttribute("reduce_max", Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int reduceMax;
	}

	[Serializable]
	public class TradeItem {
		[XmlAttribute]
		public int id;

		[XmlAttribute]
		public int price;
	}

	[XmlInclude(typeof(ToyPetSpawnAction))]
	[XmlInclude(typeof(CraftLearnAction))]
	[XmlInclude(typeof(QuestStartAction))]
	[XmlInclude(typeof(DyeAction))]
	[XmlInclude(typeof(RideAction))]
	[XmlInclude(typeof(ExtractAction))]
	[XmlInclude(typeof(EnchantItemAction))]
	[XmlInclude(typeof(SkillUseAction))]
	[XmlInclude(typeof(SkillLearnAction))]
	[XmlInclude(typeof(SplitAction))]
	[XmlInclude(typeof(ReadAction))]
	[XmlInclude(typeof(EmotionAction))]
	[XmlInclude(typeof(TitleAction))]
	[XmlInclude(typeof(TicketAction))]
	[XmlInclude(typeof(CosmeticAction))]
	[XmlInclude(typeof(ChargeAction))]

	[Serializable]
	public abstract partial class AbstractItemAction {
	}

	[Serializable]
	public class ToyPetSpawnAction : AbstractItemAction {
		[XmlAttribute]
		public int npcid;
		[XmlIgnore]
		public bool npcidSpecified;
	}

	[Serializable]
	public class CraftLearnAction : AbstractItemAction {
		[XmlAttribute]
		public int recipeid;

		[XmlIgnore]
		public bool recipeidSpecified;
	}

	[Serializable]
	public class QuestStartAction : AbstractItemAction {
		[XmlAttribute]
		public int questid;

		[XmlIgnore]
		public bool questidSpecified;
	}

	[Serializable]
	public class DyeAction : AbstractItemAction {
		[XmlAttribute]
		public string color;
	}

	[Serializable]
	public class ChargeAction : AbstractItemAction {
		[XmlAttribute]
		public int capacity;
		/*
		[XmlAttribute]
		public bool isPack;
		 */
	}

	[Serializable]
	public class ExtractAction : AbstractItemAction {
	}

	[Serializable]
	public class EnchantItemAction : AbstractItemAction {
		/*
		[XmlAttribute]
		public int count;
		*/
	}

	[Serializable]
	public class SkillUseAction : AbstractItemAction {
		public SkillUseAction() { }

		public SkillUseAction(int skillId, int level) {
			this.level = level;
			this.skillid = skillId;
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int level;

		[XmlAttribute]
		[DefaultValue(0)]
		public int skillid;
	}

	[Serializable]
	public class SkillLearnAction : AbstractItemAction {
		[XmlAttribute]
		public int skillid;

		[XmlIgnore]
		public bool skillidSpecified;

		[XmlAttribute]
		public skillPlayerClass @class;

		[XmlIgnore]
		public bool classSpecified;

		[XmlAttribute]
		public int level;

		[XmlIgnore]
		public bool levelSpecified;
		/*
		[XmlAttribute]
		public skillRace race;

		[XmlIgnore]
		public bool raceSpecified;
		 */
	}

	[Serializable]
	public class SplitAction : AbstractItemAction {
	}

	[Serializable]
	public class RideAction : AbstractItemAction {
		[XmlAttribute]
		[DefaultValue(0)]
		public int rideId;
	}

	[Serializable]
	public class ReadAction : AbstractItemAction {
	}

	[Serializable]
	public class MotionAction : AbstractItemAction {
		[XmlAttribute]
		[DefaultValue(0)]
		public int idle;

		[XmlAttribute]
		[DefaultValue(0)]
		public int jump;

		[XmlAttribute]
		[DefaultValue(0)]
		public int rest;

		[XmlAttribute]
		[DefaultValue(0)]
		public int run;

		[XmlAttribute]
		[DefaultValue(0)]
		public int minutes;
	}

	[Serializable]
	public class EmotionAction : AbstractItemAction {
		[XmlAttribute]
		public int emotionid;

		[XmlAttribute]
		[DefaultValue(0)]
		public int minutes;
	}

	[Serializable]
	public class TitleAction : AbstractItemAction {
		[XmlAttribute]
		public int titleid;

		[XmlAttribute]
		[DefaultValue(0)]
		public int minutes;
	}

	[Serializable]
	public class TicketAction : AbstractItemAction {
		[XmlAttribute]
		public StorageType storage;

		[XmlAttribute]
		public int level;

		public TicketAction() {
			level = 1;
		}
	}

	[Serializable]
	public class CosmeticAction : AbstractItemAction {
		[XmlAttribute]
		public CosmeticType cosmeticName;
	}

	[Serializable]
	public enum StorageType {
		none = 0,
		CUBE = 1,
		WAREHOUSE = 2,
	}

	[Serializable]
	public enum CosmeticType {
		hair_color,
		face_color,
		lip_color,
		eye_color,
		hair_type,
		face_type,
		voice_type,
		makeup_type,
		tattoo_type,
		preset_name
	}

	[Serializable]
	public enum skillRace {
		ELYOS,
		ASMODIANS,
		PC_ALL,
	}

	[Serializable]
	public class ItemActions {
		[XmlElement("skilllearn", Form = XmlSchemaForm.Unqualified)]
		public SkillLearnAction[] skilllearn;

		[XmlElement("ride", Form = XmlSchemaForm.Unqualified)]
		public RideAction[] ride;

		[XmlElement("skilluse", Form = XmlSchemaForm.Unqualified)]
		public SkillUseAction[] skilluse;

		[XmlElement("enchant", Form = XmlSchemaForm.Unqualified)]
		public EnchantItemAction[] enchant;

		[XmlElement("queststart", Form = XmlSchemaForm.Unqualified)]
		public QuestStartAction[] queststart;

		[XmlElement("dye", Form = XmlSchemaForm.Unqualified)]
		public DyeAction[] dye;

		[XmlElement("charge", Form = XmlSchemaForm.Unqualified)]
		public ChargeAction[] condition;

		[XmlElement("craftlearn", Form = XmlSchemaForm.Unqualified)]
		public CraftLearnAction[] craftlearn;

		[XmlElement("extract", Form = XmlSchemaForm.Unqualified)]
		public ExtractAction[] extract;

		[XmlElement("toypetspawn", Form = XmlSchemaForm.Unqualified)]
		public ToyPetSpawnAction[] toypetspawn;

		[XmlElement("decompose", Form = XmlSchemaForm.Unqualified)]
		public SplitAction[] split;

		[XmlElement("read", Form = XmlSchemaForm.Unqualified)]
		public ReadAction[] read;

		[XmlElement("animation", Form = XmlSchemaForm.Unqualified)]
		public MotionAction[] motion;

		[XmlElement("learnemotion", Form = XmlSchemaForm.Unqualified)]
		public EmotionAction[] emotion;

		[XmlElement("titleadd", Form = XmlSchemaForm.Unqualified)]
		public TitleAction[] title;

		[XmlElement("expandinventory", Form = XmlSchemaForm.Unqualified)]
		public TicketAction[] ticket;

		[XmlElement("changecolor", Form = XmlSchemaForm.Unqualified)]
		public CosmeticAction[] cosmetic;
	}

	[Serializable]
	public class ItemTemplate {
		[XmlElement("modifiers", Form = XmlSchemaForm.Unqualified)]
		public Modifiers[] modifiers;

		//[XmlElement("weapon_stats", Form = XmlSchemaForm.Unqualified)]
		//public WeaponStats weapon_stats;

		[XmlElement("actions", Form = XmlSchemaForm.Unqualified)]
		public ItemActions[] actions;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Godstone godstone;


		[XmlElement("tradein_list", Form = XmlSchemaForm.Unqualified)]
		public TradeItemList tradeList;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Stigma stigma;
/*
		[XmlElement("feed", Form = XmlSchemaForm.Unqualified)]
		public ItemFeed[] feed;
*/
		[XmlAttribute]
		public int id;

		[XmlAttribute]
		[DefaultValue(0)]
		public int func_pet_id;

		[XmlAttribute]
		public int level;

		[XmlIgnore]
		public bool levelSpecified;

		[XmlAttribute]
		[DefaultValue(0)]
		public int m_slots;

		[XmlAttribute]
		[DefaultValue(0)]
		public int m_slots_r;

		[XmlAttribute]
		[DefaultValue(0)]
		public int burn_attack;

		[XmlAttribute]
		[DefaultValue(0)]
		public int burn_defend;

		[XmlAttribute]
		[DefaultValue(0)]
		public int charge_level;

		[XmlAttribute]
		[DefaultValue(0)]
		public int charge_price1;

		[XmlAttribute]
		[DefaultValue(0)]
		public int charge_price2;

		[XmlAttribute]
		public int mask;

		[XmlIgnore]
		public bool maskSpecified;

		[XmlAttribute]
		public weaponType weapon_type;

		[XmlIgnore]
		public bool weapon_typeSpecified;

		[XmlAttribute]
		public armorType armor_type;

		[XmlIgnore]
		public bool armor_typeSpecified;

		[XmlAttribute]
		public int max_stack_count;

		[XmlAttribute]
		public string item_type;

		[XmlAttribute]
        [DefaultValue("item_category")]
		public string category;

		[XmlAttribute]
		public ItemQuality quality;

		[XmlIgnore]
		public bool qualitySpecified;

		[XmlAttribute]
		public int price;

		[XmlIgnore]
		public bool priceSpecified;

		[XmlAttribute]
		[DefaultValue(0)]
		public int ap;

		[XmlAttribute]
		[DefaultValue(0)]
		public int ai;

		[XmlAttribute]
		[DefaultValue(0)]
		public int aic;

		[XmlAttribute]
		[DefaultValue(0)]
		public int ri;

		[XmlAttribute]
		[DefaultValue(0)]
		public int ric;
		/*
		[XmlAttribute]
		[DefaultValue(0)]
		public int ci;

		[XmlAttribute]
		[DefaultValue(0)]
		public int cic;
		*/
		[XmlAttribute]
		public ItemRace race;

		[XmlIgnore]
		[DefaultValue(ItemRace.PC_ALL)]
		public ItemRace origRace;

		[XmlAttribute]
		public bool drop;

		[XmlIgnore]
		public bool dropSpecified;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool dye;

		[XmlAttribute]
		public bool can_proc_enchant;

		[XmlIgnore]
		public bool can_proc_enchantSpecified;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool no_enchant;

		[XmlAttribute]
		[DefaultValue(0)]
		public int option_slot_bonus;

		[XmlAttribute]
		public bool can_fuse;

		[XmlIgnore]
		public bool can_fuseSpecified;

		[XmlAttribute]
		public string restrict;

		[XmlAttribute]
		[DefaultValue("")]
		public string restrict_max;

		[XmlAttribute]
		public int desc;

		[XmlIgnore]
		public bool descSpecified;

		[XmlAttribute]
		[DefaultValue(0)]
		public float attack_gap;

		[XmlAttribute]
		public string attack_type;

		[XmlIgnore]
		public bool dmg_decalSpecified;

		[XmlAttribute]
		public int usedelay;

		[XmlIgnore]
		public bool usedelaySpecified;

		[XmlAttribute]
		public int usedelayid;

		[XmlIgnore]
		public bool usedelayidSpecified;

		[XmlAttribute]
		public int slot;

		[XmlIgnore]
		public bool slotSpecified;

		[XmlAttribute]
		public EquipType equipment_type;

		[XmlIgnore]
		public bool equipment_typeSpecified;

		[XmlAttribute]
		public string return_alias;

		[XmlAttribute]
		public int return_world;

		[XmlIgnore]
		public bool return_worldSpecified;

		[XmlAttribute]
		public int weapon_boost;

		[XmlIgnore]
		public bool weapon_boostSpecified;

		[XmlAttribute]
		[DefaultValue(Gender.ALL)]
		public Gender gender;

		[XmlAttribute]
		public string func_pet_name;

		[XmlAttribute]
		public BonusApplyType bonus_apply;

		[XmlIgnore]
		public bool bonus_applySpecified;

		[XmlAttribute]
		public string name;
		/*
		[XmlAttribute]
		public int quest;

		[XmlIgnore]
		public bool questSpecified;
		*/
		[XmlAttribute("expire_time")]
		[DefaultValue(0)]
		public ExpireDuration expire_time;

		[XmlAttribute("cash_minute")]
		[DefaultValue(0)]
		public ExpireDuration cash_minute;

		[XmlAttribute("temp_exchange_time")]
		[DefaultValue(0)]
		public ExpireDuration temp_exchange_time;
/* zp unused for now
		[XmlAttribute("world_drop")]
		[DefaultValue(false)]
		public bool world_drop;
*/
		public bool HasActions() {
			return actions != null && (actions[0].craftlearn != null ||
			    actions[0].dye != null || actions[0].condition != null ||
			    actions[0].enchant != null ||
			    actions[0].extract != null || actions[0].queststart != null ||
			    actions[0].read != null || actions[0].skilllearn != null ||
			    actions[0].skilluse != null || actions[0].split != null ||
			    actions[0].toypetspawn != null || actions[0].emotion != null ||
			    actions[0].title != null || actions[0].ride != null);
		}
	}

	[Serializable]
	public enum EquipType {
		NONE = 0,
		ARMOR,
		WEAPON,
	}

	[Serializable]
	public enum BonusApplyType {
		EQUIP,
		INVENTORY,
	}

	[Serializable]
	[DefaultValue(ItemRace.PC_ALL)]
	public enum ItemRace {
		PC_ALL = 0,
		ELYOS,
		ASMODIANS,
	}

	[Serializable]
	public enum ItemQuality {
		COMMON,
		RARE,
		UNIQUE,
		LEGEND,
		MYTHIC,
		EPIC,
		JUNK,
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "item_templates", Namespace = "", IsNullable = false)]
	public class ItemTemplates {
		[XmlElement("item_template", Form = XmlSchemaForm.Unqualified)]
		public ItemTemplate[] TemplateList;
	}
}
