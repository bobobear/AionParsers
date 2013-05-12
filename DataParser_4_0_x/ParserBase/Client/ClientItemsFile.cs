namespace Jamie.ParserBase {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using Jamie.Items;
	using Jamie.Skills;
	using Jamie.Quests;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "client_items", Namespace = "", IsNullable = false)]
	public class ClientItemsFile {
		[XmlElement("client_item", Form = XmlSchemaForm.Unqualified)]
		public List<Item> ItemList;

		Dictionary<string, Item> keyToItem = null;
		ILookup<string, Item> descToItems = null;
		Dictionary<int, Item> idToItem = null;

		internal void CreateIndex() {
			if (this.ItemList == null)
				return;
			keyToItem = this.ItemList.ToDictionary(i => i.name, i => i, StringComparer.InvariantCultureIgnoreCase);
			descToItems = this.ItemList.ToLookup(i => i.desc, i => i, StringComparer.InvariantCultureIgnoreCase);
			idToItem = this.ItemList.ToDictionary(i => i.id, i => i);
		}

		public string this[string itemKey] {
			get {
				itemKey = itemKey.Trim().ToLower();
				if (keyToItem == null || !keyToItem.ContainsKey(itemKey))
					return itemKey;
				return keyToItem[itemKey].Description;
			}
		}

		public Item GetItem(string itemKey) {
			itemKey = itemKey.Trim().ToLower();
			if (keyToItem == null || !keyToItem.ContainsKey(itemKey)) {
				return null;
			}
			return keyToItem[itemKey];
		}

		public IEnumerable<Item> GetItemsByDescription(string itemDesc) {
			itemDesc = itemDesc.Trim().ToLower();
			if (descToItems == null || !descToItems.Contains(itemDesc)) {
				return Enumerable.Empty<Item>();
			}
			return descToItems[itemDesc];
		}

		public Item GetItem(int itemId) {
			if (!idToItem.ContainsKey(itemId)) {
				return null;
			}
			return idToItem[itemId];
		}
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Item {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int id;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string desc;

		[XmlIgnore]
		public string Description {
			get {
				if (desc == null)
					return desc;
				var s = Utility.StringIndex.GetStringDescription(desc.ToUpper());
				if (s == null)
					return desc;
				return s.body;
			}
		}

		[XmlIgnore]
		public int Count { get; set; }

		public Item Clone() {
			return (Item)this.MemberwiseClone();
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string desc_long;

		[XmlElement(ElementName = "weapon_type", Form = XmlSchemaForm.Unqualified)]
		public WeaponTypes WeaponType;

		[XmlElement(ElementName = "item_type", Form = XmlSchemaForm.Unqualified)]
		public ItemTypes ItemType;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string custom_part_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string mesh;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool mesh_change;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string material;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string attack_fx;
        
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short dmg_decal;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string item_fx;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string combat_item_fx;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string icon_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string blade_fx;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string trail_tex;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string equip_bone;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string combat_equip_bone;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public long price;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int extra_inventory;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public long trade_in_price;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public long trade_in_abyss_point;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public long abyss_point;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int max_stack_count;

		[XmlElement(ElementName = "equipment_slots", Form = XmlSchemaForm.Unqualified)]
		public EquipmentSlots EquipmentSlots;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int min_damage;
        
        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int max_hp;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int physical_critical_reduce_rate;
        
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int max_damage;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int str;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int agi;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int kno;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int hit_accuracy;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int critical;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int parry;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int magical_skill_boost;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int magical_skill_boost_resist;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int magical_hit_accuracy;

		[XmlElement(ElementName = "attack_type", Form = XmlSchemaForm.Unqualified)]
		public AttackTypes AttackType;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int attack_delay;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short hit_count;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public float attack_gap;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public float attack_range;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public float basic_length;

		[XmlElement(ElementName = "quality", Form = XmlSchemaForm.Unqualified)]
		public ItemQualities Quality;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int level;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool lore;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_exchange;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_sell_to_npc;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_deposit_to_character_warehouse;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_deposit_to_account_warehouse;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_deposit_to_guild_warehouse;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool breakable;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool soul_bind;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool remove_when_logout;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string gender_permitted;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int warrior;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int scout;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int mage;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cleric;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int engineer;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int artist;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int fighter;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int knight;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int assassin;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ranger;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int wizard;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int elementalist;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int chanter;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int priest;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int gunner;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int bard;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int rider;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int option_slot_value;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int special_slot_value;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short option_slot_bonus;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string robot_name;
        
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr3;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr4;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr5;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr6;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr7;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr8;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr9;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr10;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr11;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr12;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string random_option_set;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int reidentify_count;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string stat_enchant_type1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int stat_enchant_value1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string stat_enchant_type2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int stat_enchant_value2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string stat_enchant_type3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int stat_enchant_value3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string stat_enchant_type4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int stat_enchant_value4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string stat_enchant_type5;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int stat_enchant_value5;
        
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string f2p_pack_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int f2p_pack_available;

		[XmlElement(ElementName = "bonus_apply", Form = XmlSchemaForm.Unqualified)]
		public Bonuses BonusApply;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public XmlBool default_prohibit;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool no_enchant;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int max_enchant_value;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int max_enchant_bonus;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_proc_enchant;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_composite_weapon;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool cannot_changeskin;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ui_sound_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool can_split;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool item_drop_permitted;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int charge_way;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string boost_str_desc;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string boost_material;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int boost_material_num;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int usable_rank_min;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int usable_rank_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int bm_restrict_category;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string disposable_trade_item;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int disposable_trade_item_count;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0 todo
        public string target_item_category;
        
        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public XmlBool can_ap_extraction;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int ap_extraction_rate;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public XmlBool can_polish;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int polish_burn_on_attack;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int polish_burn_on_defend;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int quest_label;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string polish_set_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int booster_wing;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string race_permitted;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string reset_instance_coolt_sync_id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public bool ride_usable;
        
        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string exp_extraction_cost;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string exp_extraction_reward;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string char_bm_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0 todo
        public int char_bm_available_duration;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0 todo enum
        public string bonus_addexp_item;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0 todo enum
        public string megaphone_rgb;

        
		[XmlIgnore]
		[DefaultValue(ItemRace.PC_ALL)]
		public ItemRace race {
			get {
				if (String.IsNullOrEmpty(race_permitted))
					return ItemRace.PC_ALL;
				string[] races = race_permitted.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (races.Length > 1)
					return ItemRace.PC_ALL;
				else if (races[0] == "pc_light")
					return ItemRace.ELYOS;
				else
					return ItemRace.ASMODIANS;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string abyss_item;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int abyss_item_count;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool confirm_to_delete_cash_item;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ammo_bone;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ammo_fx;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ammo_speed;

		[XmlElement(ElementName = "armor_type", Form = XmlSchemaForm.Unqualified)]
		public ArmorTypes ArmorType;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int dodge;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int magical_resist;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int physical_defend;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int magical_defend;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool can_dye;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string default_color_m;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string default_color_f;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string visual_slot;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int fx_mesh;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short extract_skin_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int block;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int damage_reduce;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int reduce_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int gathering_point;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int require_shard;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string gain_skill1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int gain_level1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string require_skill1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int require_skill1_lv;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string require_skill2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int require_skill2_lv;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string stigma_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string gain_skill2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int gain_level2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string tool_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string motion_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string combineskill;

        [XmlElement(ElementName = "category", Form = XmlSchemaForm.Unqualified)]
		public ItemCategories Category;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string use_fx;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string use_fx_bone;

		[XmlElement(ElementName = "activation_mode", Form = XmlSchemaForm.Unqualified)]
		public ActivationModes ActivationMode;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int activation_count;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int breakdown;

		[XmlElement(ElementName = "activate_target", Form = XmlSchemaForm.Unqualified)]
		public ActivateTargets ActivateTarget;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int use_delay_type_id;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int use_delay;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string craft_recipe_info;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int quest;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string activation_skill;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short activation_level;

		[XmlElement(Form = XmlSchemaForm.Unqualified, IsNullable = true)]
		public Nullable<bool> disassembly_item;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int disassembly_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string assembly_item;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string housing_change_size;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string use_notice;

		[XmlElement(Form = XmlSchemaForm.Unqualified, IsNullable = true)]
		public Nullable<bool> trial_user_can_vendor_buy;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int func_pet_dur_minute;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ride_data_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int warrior_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int scout_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int mage_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int cleric_max;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int engineer_max;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public int artist_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int fighter_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int knight_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int assassin_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ranger_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int wizard_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int elementalist_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int chanter_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int priest_max;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
		public int gunner_max;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
		public int bard_max;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
		public int rider_max;
        
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int unit_sell_count;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int casting_delay;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string return_alias;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int return_worldid;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string area_to_use;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ownership_world;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string stat_enchant_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int stat_enchant_value;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string proc_enchant_skill;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int proc_enchant_skill_level;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int proc_enchant_effect_occur_prob;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string desc_proc;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string proc_fx;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int proc_enchant_effect_occur_left_prob;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int weapon_boost_value;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string dyeing_color;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string skill_to_learn;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int cash_social;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int cash_item;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int cash_title;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string coupon_item;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int coupon_item_count;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int cash_available_minute;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int temporary_exchange_time;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int expire_time;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short inven_warehouse_max_extendlevel;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string cosmetic_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string doc_bg;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string toy_pet_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified, IsNullable = true)]
		public Nullable<int> equip_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string difficulty;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public float scale;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int package_permitted;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int sub_enchant_material_many;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public EnchantEffect sub_enchant_material_effect_type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int sub_enchant_material_effect;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int target_item_level_min;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int target_item_level_max;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string extra_currency_item;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int extra_currency_item_count;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public ItemTag tag;

		public int GetMask() {
			int mask = 0;
			if (lore != null && lore.Value == XmlBoolTypes.True)
				mask |= 1;
			if (can_exchange != null && can_exchange.Value == XmlBoolTypes.True)
				mask |= 2;
			if (can_sell_to_npc != null && can_sell_to_npc.Value == XmlBoolTypes.True)
				mask |= 4;
			if (can_deposit_to_character_warehouse != null && can_deposit_to_character_warehouse.Value == XmlBoolTypes.True)
				mask |= 8;
			if (can_deposit_to_account_warehouse != null && can_deposit_to_account_warehouse.Value == XmlBoolTypes.True)
				mask |= 16;
			if (can_deposit_to_guild_warehouse != null && can_deposit_to_guild_warehouse.Value == XmlBoolTypes.True)
				mask |= 32;
			if (breakable != null && breakable.Value == XmlBoolTypes.True)
				mask |= 64;
			if (soul_bind != null && soul_bind.Value == XmlBoolTypes.True)
				mask |= 128;
			if (remove_when_logout != null && remove_when_logout.Value == XmlBoolTypes.True)
				mask |= 256;
			if (no_enchant != null && no_enchant.Value == XmlBoolTypes.True)
				mask |= 512;
			if (can_proc_enchant != null && can_proc_enchant.Value == XmlBoolTypes.True)
				mask |= 1024;
			if (can_composite_weapon != null && can_composite_weapon.Value == XmlBoolTypes.True)
				mask |= 2048;
			if (!cannot_changeskin)
				mask |= 4096;
			if (can_split != null && can_split.Value == XmlBoolTypes.True)
				mask |= 8192;
			if (item_drop_permitted != null && item_drop_permitted.Value == XmlBoolTypes.True)
				mask |= 16384;
			if (can_dye)
				mask |= 32768;
			return mask;
		}

		[XmlIgnore]
		public int MinLevel {
			get {
				int min = level;
				if (warrior > 0)
					min = Math.Min(min, warrior);
				if (fighter > 0)
					min = Math.Min(min, fighter);
				if (knight > 0)
					min = Math.Min(min, knight);
				if (scout > 0)
					min = Math.Min(min, scout);
				if (assassin > 0)
					min = Math.Min(min, assassin);
				if (ranger > 0)
					min = Math.Min(min, ranger);
				if (mage > 0)
					min = Math.Min(min, mage);
				if (wizard > 0)
					min = Math.Min(min, wizard);
				if (elementalist > 0)
					min = Math.Min(min, elementalist);
				if (cleric > 0)
					min = Math.Min(min, cleric);
				if (priest > 0)
					min = Math.Min(min, priest);
				if (chanter > 0)
					min = Math.Min(min, chanter);
				if (min == 1)
					return level;
				return min;
			}
		}

		[XmlIgnore]
		public int MaxLevel {
			get {
				int max = level;
				if (warrior > 0)
					max = Math.Max(max, warrior_max);
				if (fighter > 0)
					max = Math.Max(max, fighter_max);
				if (knight > 0)
					max = Math.Max(max, knight_max);
				if (scout > 0)
					max = Math.Max(max, scout_max);
				if (assassin > 0)
					max = Math.Max(max, assassin_max);
				if (ranger > 0)
					max = Math.Max(max, ranger_max);
				if (mage > 0)
					max = Math.Max(max, mage_max);
				if (wizard > 0)
					max = Math.Max(max, wizard_max);
				if (elementalist > 0)
					max = Math.Max(max, elementalist_max);
				if (cleric > 0)
					max = Math.Max(max, cleric_max);
				if (priest > 0)
					max = Math.Max(max, priest_max);
				if (chanter > 0)
					max = Math.Max(max, chanter_max);
				return max;
			}
		}

		public string GetRestrictions() {
			string[] restricts = new string[12];
			restricts[0] = warrior.ToString();
			restricts[1] = fighter.ToString();
			restricts[2] = knight.ToString();
			restricts[3] = scout.ToString();
			restricts[4] = assassin.ToString();
			restricts[5] = ranger.ToString();
			restricts[6] = mage.ToString();
			restricts[7] = wizard.ToString();
			restricts[8] = elementalist.ToString();
			restricts[9] = cleric.ToString();
			restricts[10] = priest.ToString();
			restricts[11] = chanter.ToString();
			return String.Join(",", restricts);
		}

		public string GetMaxRestrictions() {
			string[] restricts = new string[12];
			restricts[0] = warrior_max.ToString();
			restricts[1] = fighter_max.ToString();
			restricts[2] = knight_max.ToString();
			restricts[3] = scout_max.ToString();
			restricts[4] = assassin_max.ToString();
			restricts[5] = ranger_max.ToString();
			restricts[6] = mage_max.ToString();
			restricts[7] = wizard_max.ToString();
			restricts[8] = elementalist_max.ToString();
			restricts[9] = cleric_max.ToString();
			restricts[10] = priest_max.ToString();
			restricts[11] = chanter_max.ToString();
			return String.Join(",", restricts);
		}

		public List<Modifier> GetModifiers(SkillMap map) {
			List<Modifier> list = new List<Modifier>();

			SkillMapEntry entry = null;
            if (this.hit_accuracy > 0)
            {
                entry = map.AllMappings["hit_accuracy"];
                list.Add(GetModifier(entry, this.hit_accuracy));
            }
            if (this.magical_hit_accuracy > 0)
            {
                entry = map.AllMappings["magical_hit_accuracy"];
                list.Add(GetModifier(entry, this.magical_hit_accuracy));
            }
            if (this.magical_skill_boost > 0)
            {
                entry = map.AllMappings["magical_skill_boost"];
                list.Add(GetModifier(entry, this.magical_skill_boost));
            }
            if (this.parry > 0)
            {
                entry = map.AllMappings["parry"];
                list.Add(GetModifier(entry, this.parry));
            }
			if (this.dodge > 0) {
				entry = map.AllMappings["dodge"];
				list.Add(GetModifier(entry, this.dodge));
			}
			if (this.block > 0) {
				entry = map.AllMappings["block"];
				list.Add(GetModifier(entry, this.block));
			}
            if (this.critical > 0)
            {
                entry = map.AllMappings["critical"];
                list.Add(GetModifier(entry, this.critical));
            }
            if (this.min_damage > 0)
            {
                entry = map.AllMappings["min_damage"];
                list.Add(GetModifier(entry, this.min_damage));
            }
            if (this.max_damage > 0)
            {
                entry = map.AllMappings["max_damage"];
                list.Add(GetModifier(entry, this.max_damage));
            }
			if (this.damage_reduce > 0) {
				entry = map.AllMappings["damage_reduce"];
				list.Add(GetModifier(entry, this.damage_reduce));
			}
			if (this.magical_resist > 0) {
				entry = map.AllMappings["magical_resist"];
				list.Add(GetModifier(entry, this.magical_resist));
			}
			if (this.physical_defend > 0) {
				entry = map.AllMappings["physical_defend"];
				list.Add(GetModifier(entry, this.physical_defend));
			}
			if (!String.IsNullOrEmpty(this.stat_enchant_type) && this.stat_enchant_value > 0) {
				entry = map.AllMappings[this.stat_enchant_type];
				Modifier modifier = GetModifier(entry, this.stat_enchant_value);
				modifier.bonus = true;
				list.Add(modifier);
			}

			var utility = Utility<Item>.Instance;
			List<string> bonusAttrs = new List<string>();
			utility.Export(this, "bonus_attr", bonusAttrs);

			var clientBonuses = from bonus in bonusAttrs
							where bonus.Length > 1
							let name = Utility.GetAttributeName(bonus)
							let value = Utility.GetAttributeValue(bonus)
							select new { Name = name, Value = value };

			foreach (var bonus in clientBonuses) {
				string name = bonus.Name.ToLower();
				if (!map.AllMappings.ContainsKey(name)) {
					Debug.Print("Missing bonus attribute mapping: {0}", name);
					continue;
				}
				entry = map.AllMappings[name];
				Modifier modifier = GetModifier(entry, bonus.Value);
				modifier.bonus = true;
				list.Add(modifier);
			}
			// zp add charge modifiers a
			List<string> bonusAttrsChargeA = new List<string>();
			utility.Export(this, "bonus_attr_a", bonusAttrsChargeA);

			var clientBonusesChargeA = from bonus in bonusAttrsChargeA
							where bonus.Length > 1
							let name = Utility.GetAttributeName(bonus)
							let value = Utility.GetAttributeValue(bonus)
							select new { Name = name, Value = value };

			foreach (var bonus in clientBonusesChargeA) {
				string name = bonus.Name.ToLower();
				if (!map.AllMappings.ContainsKey(name)) {
					Debug.Print("Missing bonus attribute mapping: {0}", name);
					continue;
				}
				entry = map.AllMappings[name];
				Modifier modifier = GetModifier(entry, bonus.Value);
				modifier.bonus = true;

				modifier.conditions = new ModifierConditions();
				modifier.conditions.charge = new Charge();
				modifier.conditions.charge.level = 1;

				list.Add(modifier);
			}

			// zp add charge modifiers b
			List<string> bonusAttrsChargeB = new List<string>();
			utility.Export(this, "bonus_attr_b", bonusAttrsChargeB);

			var clientBonusesChargeB = from bonus in bonusAttrsChargeB
								  where bonus.Length > 1
								  let name = Utility.GetAttributeName(bonus)
								  let value = Utility.GetAttributeValue(bonus)
								  select new { Name = name, Value = value };

			foreach (var bonus in clientBonusesChargeB) {
				string name = bonus.Name.ToLower();
				if (!map.AllMappings.ContainsKey(name)) {
					Debug.Print("Missing bonus attribute mapping: {0}", name);
					continue;
				}
				entry = map.AllMappings[name];
				Modifier modifier = GetModifier(entry, bonus.Value);
				modifier.bonus = true;

				modifier.conditions = new ModifierConditions();
				modifier.conditions.charge = new Charge();
				modifier.conditions.charge.level = 2;

				list.Add(modifier);
			}

            if (this.attack_delay > 0)
            {
                entry = map.AllMappings["attack_delay"];
                list.Add(GetModifier(entry, this.attack_delay));
            }
            if (this.attack_range > 0)
            {
                entry = map.AllMappings["attack_range"];
                list.Add(GetModifier(entry, (int)(this.attack_range * 1000)));
            }
            if (this.hit_count > 0)
            {
                entry = map.AllMappings["hit_count"];
                list.Add(GetModifier(entry, this.hit_count));
            }

            if (this.min_damage > 0 || this.max_damage > 0)
            {
                var meanMod = new MeanModifier(this.min_damage, this.max_damage);
                meanMod.name = modifiersenum.PHYSICAL_ATTACK;
                list.Add(meanMod);
            }

			if (list.Count == 0)
				return null;
			return list;
		}

		public WeaponStats GetWeaponStats() {
			WeaponStats wstats = new WeaponStats();
			int listCount = 0;

			if (this.min_damage > 0) {
				wstats.minDamage = this.min_damage;
				listCount++;
			}

			if (this.max_damage > 0) {
				wstats.maxDamage = this.max_damage;
				listCount++;
			}

			if (this.critical > 0) {
				wstats.physicalCritical = this.critical;
				listCount++;
			}

			if (this.hit_accuracy > 0) {
				wstats.physicalAccuracy = this.hit_accuracy;
				listCount++;
			}

			if (this.parry > 0) {
				wstats.parry = this.parry;
				listCount++;
			}

			if (this.magical_hit_accuracy > 0) {
				wstats.magicalAccuracy = this.magical_hit_accuracy;
				listCount++;
			}

			if (this.magical_skill_boost > 0) {
				wstats.boostMagicalSkill = this.magical_skill_boost;
				listCount++;
			}

			if (this.attack_range > 0) {
				wstats.attackRange = (int)(this.attack_range * 1000);
				listCount++;
			}

			if (this.hit_count > 0) {
				wstats.hitCount = this.hit_count;
				listCount++;
			}

			if (this.reduce_max > 0) {
				wstats.reduceMax = this.reduce_max;
				listCount++;
			}

			if (listCount > 0) {
				switch (this.WeaponType) {
					case WeaponTypes.OneHandDagger:
						wstats.attackSpeed = 1200;
						break;
					case WeaponTypes.OneHandSword:
						wstats.attackSpeed = 1400;
						break;
					case WeaponTypes.OneHandMace:
						wstats.attackSpeed = 1500;
						break;
					case WeaponTypes.TwoHandStaff:
						wstats.attackSpeed = 2000;
						break;
					case WeaponTypes.TwoHandBook:
						wstats.attackSpeed = 2200;
						break;
					case WeaponTypes.TwoHandOrb:
						wstats.attackSpeed = 2200;
						break;
					case WeaponTypes.TwoHandSword:
						wstats.attackSpeed = 2400;
						break;
					case WeaponTypes.TwoHandPolearm:
						wstats.attackSpeed = 2800;
						break;
					case WeaponTypes.Bow:
						wstats.attackSpeed = 1200;
						break;
					default:
						wstats.attackSpeed = 2000;
						break;
				}
			}

			if (listCount == 0)
				return null;
			return wstats;
		}

		public List<Modifier> GetChargeModifiersA(SkillMap map) {
			List<Modifier> list = new List<Modifier>();
			SkillMapEntry entry = null;
			var utility = Utility<Item>.Instance;
			List<string> bonusAttrs = new List<string>();
			utility.Export(this, "bonus_attr_a", bonusAttrs);

			var clientBonuses = from bonus in bonusAttrs
							let name = Utility.GetAttributeName(bonus)
							let value = Utility.GetAttributeValue(bonus)
							select new { Name = name, Value = value };

			foreach (var bonus in clientBonuses) {
				string name = bonus.Name.ToLower();
				if (!map.AllMappings.ContainsKey(name)) {
					Debug.Print("Missing charge bonus attribute mapping: {0}", name);
					continue;
				}
				entry = map.AllMappings[name];
				Modifier modifier = GetModifier(entry, bonus.Value);
				modifier.bonus = true;
				list.Add(modifier);
			}

			if (list.Count == 0)
				return null;
			return list;
		}

		public List<Modifier> GetChargeModifiersB(SkillMap map) {
			List<Modifier> list = new List<Modifier>();
			SkillMapEntry entry = null;
			var utility = Utility<Item>.Instance;
			List<string> bonusAttrs = new List<string>();
			utility.Export(this, "bonus_attr_b", bonusAttrs);

			var clientBonuses = from bonus in bonusAttrs
							let name = Utility.GetAttributeName(bonus)
							let value = Utility.GetAttributeValue(bonus)
							select new { Name = name, Value = value };

			foreach (var bonus in clientBonuses) {
				string name = bonus.Name.ToLower();
				if (!map.AllMappings.ContainsKey(name)) {
					Debug.Print("Missing charge bonus attribute mapping: {0}", name);
					continue;
				}
				entry = map.AllMappings[name];
				Modifier modifier = GetModifier(entry, bonus.Value);
				modifier.bonus = true;
				list.Add(modifier);
			}

			if (list.Count == 0)
				return null;
			return list;
		}


		Modifier GetModifier(SkillMapEntry entry, int value) {
			var modifier = (Modifier)Activator.CreateInstance(entry.ModifierType);
			if (modifier is AddModifier) {
				AddModifier addMod = (AddModifier)modifier;
				addMod.value = entry.IsNegative ? -value : value;
				addMod.name = (modifiersenum)Enum.Parse(typeof(modifiersenum), entry.to);
				addMod.nameSpecified = true;
				addMod.valueSpecified = true;
			}
			else if (modifier is SubModifier) {
				SubModifier subMod = (SubModifier)modifier;
				subMod.value = entry.IsNegative ? -value : value;
				subMod.name = (modifiersenum)Enum.Parse(typeof(modifiersenum), entry.to);
				subMod.nameSpecified = true;
				subMod.valueSpecified = true;
			}
			else if (modifier is RateModifier) {
				RateModifier rateMod = (RateModifier)modifier;
				rateMod.value = entry.IsNegative ? -value : value;
				rateMod.name = (modifiersenum)Enum.Parse(typeof(modifiersenum), entry.to);
				rateMod.nameSpecified = true;
				rateMod.valueSpecified = true;
			}
			else if (modifier is SetModifier) {
				SetModifier setMod = (SetModifier)modifier;
				setMod.value = entry.IsNegative ? -value : value;
				setMod.name = (modifiersenum)Enum.Parse(typeof(modifiersenum), entry.to);
				setMod.nameSpecified = true;
				setMod.valueSpecified = true;
			}
			return modifier;
		}

		#region version 2.5 added

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public long price_per_use; // only test item

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string desc_craftman;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int charge_level;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int charge_capacity;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public decimal charge_price1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public decimal charge_price2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int burn_on_attack;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int burn_on_defend;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_a1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_a2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_a3;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_a4;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_b1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_b2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_b3;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bonus_attr_b4;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool cannot_extraction;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool cannot_matter_option;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool cannot_matter_enchant;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int guild_level_permitted;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool doping_pet_useable;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool use_emblem;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string activate_target_race;

		[XmlIgnore]
		public Race ActivateRace {
			get {
				return (Race)Enum.Parse(typeof(Race), activate_target_race, true);
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string summon_housing_object;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public XmlBool drop_each_member;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string custom_idle_anim;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string custom_run_anim;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string custom_rest_anim;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string custom_jump_anim;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]//4.0
        public string custom_shop_anim;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int anim_expire_time;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string init_coolt_instance;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string func_pet_name;

		#endregion

		[XmlArray(ElementName = "trade_in_item_list", Form = XmlSchemaForm.Unqualified)]
		[XmlArrayItem("data", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public List<TradeItem> tradeItems;

        [XmlArray(ElementName = "data", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem("data", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<TradeItem> tradeItems2;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum ItemTypes {
		normal = 0,
        nomal,
		abyss,
		draconic,
		devanion,
		legend
	}

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum ItemTag
    {
        none = 0,
        all_pet_feed,
        christmas,
        daily,
        deva,
        devaday,
        [XmlEnum("fanta;japen")]
        fanta_japen,
        final_mission,
        final_misson = final_mission,
        Foolsday,
        goldendeva,
        goods,
        halloween,
        infraexpand,
        july_pcbang,
        july_promotion,
        kaspa,
        kisk,
        lehpa,
        lottopet,
        miss_a,
        npc,
        NShopOpen,
        optionprob,
        pet,
        petcard,
        petseller,
        [XmlEnum("pre-summer")]
        pre_summer,
        repeatquest,
        season1,
        second_genefation,
        taiwan,
        test,
        v19promotion,
        weapon_idcromede_01,
        worldcup,
        wondergirls,
        // 2.1 tags
        [XmlEnum("web-shop-1st")]
        web_shop_1st,
        [XmlEnum("web-shop-2nd")]
        web_shop_2nd,
        [XmlEnum("web-shop-2st")]
        web_shop_2st = web_shop_2nd,
        theme_9,
        sept_theme,
        spet_theme = sept_theme,
        lounge,
        [XmlEnum("lounge-shop")]
        lounge_shop,
        // 2.5
        idraksha,
        IDRaksha = idraksha,
        firstday,
        fristday = firstday,
        valentineday,
        // 2.6
        [XmlEnum("web-shop-3rd")]
        web_shop_3rd,
        lounge_2nd,
        aion_olympics,
        FoolsDay,
        Olympics,
        april_sale,
        Nshop,
        xpboost,
        drboost,
        price,
        job_drop,
        foolsday,

        //2.7
        [XmlEnum("Ver2.7")]
        Ver2_7,
        june_theme,
        summer_theme,
        pcbangboomup,
        infra,
        socialcard_promotion,
        wedding,
        arena_pvp,
        pcbang_boomup,
        sale_enchant,
        timetraveler,
        PCBang,
        item_f2p_Test,
        item_f2p,

        // 3.0
        test_u1_60,
        test_u1_58,
        test_u1_56,
        test_e1_60,
        test_e1_58,
        test_e1_56,
        [XmlEnum("Ver3.0")]
        Ver3_0,
        GF_Cash_Changeskin,
        nc_allstar,
        test_60,
        [XmlEnum("3.0version")]
        _3_0version,
        [XmlEnum("3.1version")]
        _3_1version,
        orphe,
        birth,
        [XmlEnum("3.0 BM")]
        _3_0_BM,
        Unknown,
        Birth_Item,
        [XmlEnum("3.0BM")]
        _3_0BM,
        june_promotion,
        timesale,
        pcbang,

        // 3.5
        test_u1_61,
        test_u1_65,
        test_e1_65,
        Ninja_Cash_Changeskin,
        Cash_Changeskin_31,
        test_65,
        lord,
        [XmlEnum("해외 3.5")]
        Ver3_5,
        [XmlEnum("3.1 해외")]
        Ver3_1,
        cash_ninja,
        orphe_new,
        stigma_summon,

        //4.0
        test_m1_60,
        test_m1_56,
        christmas_2012,
        [XmlEnum("4.0update")]
        ver4_0_update,
        [XmlEnum("3.5version")]
        ver3_5,
        maid,
        diving,
        iu,
        APboost,
        GatherPointBoost,
        CombinePointBoost,
        octoberfest,
        junEvent,
        item_innova_trial,
        item_f2P,
        item_tw_trial,
        ADDEXP,
        [XmlEnum("3.5 BM ADDEXP")]
        _3_5BM_ADDEXP
        
       
    }

	[Serializable]
	[Flags]
	[XmlType(AnonymousType = true)]
	public enum EquipmentSlots {
		none = 0,
		main = 1,
		sub = 2,
		main_or_sub = 3,
		head = 4,
		torso = 8,
		glove = 16,
		foot = 32,
		right_or_left_ear = 192,
		right_or_left_finger = 768,
		neck = 1024,
		shoulder = 2048,
		leg = 4096,
		right_or_left_battery = 24576,
		wing = 32768,
		waist = 65536
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum ExpireDuration {
		[XmlEnum(Name = "0")]
		NONE = 0,
		[XmlEnum(Name = "1")]
		ONE_MIN = 1,
		[XmlEnum(Name = "3")]
		THREE_MIN = 3,
		[XmlEnum(Name = "5")]
		FIVE_MIN = 5,
		[XmlEnum(Name = "10")]
		TEN_MIN = 10,
		[XmlEnum(Name = "30")]
		THIRTY_MIN = 30,
		[XmlEnum(Name = "60")]
		HOUR = 60,
		[XmlEnum(Name = "61")]
		HOUR1 = 61,
		[XmlEnum(Name = "81")]
		HOUR20 = 81,
		[XmlEnum(Name = "120")]
		TWO_HOURS = 120,
		[XmlEnum(Name = "720")]
		TWELVE_HOURS = 720,
		[XmlEnum(Name = "1440")]
		ONE_DAY = 1440,
		[XmlEnum(Name = "1441")]
		ONE_DAY1 = 1441,
		[XmlEnum(Name = "4321")]
		THREE_DAYS = 4321,
		[XmlEnum(Name = "10080")]
		SEVEN_DAYS = 10080,
		[XmlEnum(Name = "10081")]
		SEVEN_DAYS2 = 10081,
		[XmlEnum(Name = "14400")]
		TEN_DAYS = 14400,
		[XmlEnum(Name = "21600")]
		FOURTEEN_DAYS = 21600,
		[XmlEnum(Name = "20161")]
		FOURTEEN_DAYS1 = 20161,
		[XmlEnum(Name = "21601")]
		FIFTEEN_DAYS = 21601,
		[XmlEnum(Name = "43200")]
		MONTH = 43200,
		[XmlEnum(Name = "43201")]
		MONTH2 = 43201,
		[XmlEnum(Name = "129601")]
		THREE_MONTHS = 129601,
		[XmlEnum(Name = "259200")]
		SIX_MONTHS = 259200,
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum AttackTypes {
		none = 0,
		physical,
		magical_fire,
		magical_water
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum ItemQualities {
		common = 0,
		rare,
		unique,
		legend,
		mythic,
		epic,
		junk,
		Rare = rare,
		Legend = legend,
		Unique = unique
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum Bonuses {
		none = 0,
		equip,
		inventory,
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum ArmorTypes {
		none = 0,
		robe,
		clothes,
		leather,
		plate,
		no_armor,
		chain
	}

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum WeaponTypes
    {
        None = 0,
        [XmlEnum("1h_dagger")]
        OneHandDagger = 1,
        [XmlEnum("1h_gun")]
        OneHandGun,
        [XmlEnum("1h_mace")]
        OneHandMace = 2,
        [XmlEnum("1h_sword")]
        OneHandSword = 3,
        [XmlEnum("1h_toolhoe")]
        OneHandToolhoe = 4,
        [XmlEnum("2h_book")]
        TwoHandBook = 5,
        [XmlEnum("2h_cannon")]
        TwoHandCannon,
        [XmlEnum("2h_harp")]
        TwoHandHarp,
        [XmlEnum("2h_keyblade")]
        TwoHandKeyblade,
        [XmlEnum("2h_keyhammer")]
        TwoHandKeyhammer,
        [XmlEnum("2h_orb")]
        TwoHandOrb = 6,
        [XmlEnum("2h_polearm")]
        TwoHandPolearm = 7,
        [XmlEnum("2h_staff")]
        TwoHandStaff = 8,
        [XmlEnum("2h_sword")]
        TwoHandSword = 9,
        [XmlEnum("2h_toolpick")]
        TwoHandToolpick = 10,
        [XmlEnum("2h_toolrod")]
        TwoHandToolrod = 11,
        [XmlEnum("bow")]
        Bow = 12,
        [XmlEnum("noweapon")]
        NoWeapon = 13,
    }

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum ItemCategories {
		none = 0,
		armor_craft,
		cooking,
		weapon_craft,
		handiwork,
		alchemy,
		carpentry,
		tailoring,
		leatherwork,
		harvest,

		// 3.0
		menuisier,
		skillboost_cooking,
		skillboost_armor_craft,
		skillboost_weapon_craft,
		skillboost_handiwork,
		skillboost_alchemy,
		skillboost_tailoring,
		skillboost_menuisier,
		[XmlEnum("3.0version")]
		_3_0version

	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum ActivateTargets {
		none = 0,
		standalone,
		target,
        mymento
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum ActivationModes {
		None = 0,
		Combat,
		combat = Combat,
		Both,
		both = Both
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public enum EnchantEffect {
		none = 0,
		cash_option_prob,
		Probability
	}


	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class TradeItem {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string trade_in_item;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int trade_in_item_count;
	}
}
