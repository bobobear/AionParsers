namespace Jamie.ParserBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "skill_base_clients", Namespace = "", IsNullable = false)]
    public partial class ClientSkillFile
    {
        [XmlElement("skill_base_client", Form = XmlSchemaForm.Unqualified)]
        public List<ClientSkill> SkillList = new List<ClientSkill>();

        Dictionary<int, ClientSkill> idToSkill = new Dictionary<int, ClientSkill>();
        Dictionary<string, ClientSkill> nameToSkill = new Dictionary<string, ClientSkill>();

        internal void CreateIndex()
        {
            if (SkillList == null)
                return;
            idToSkill = SkillList.ToDictionary(s => s.id, s => s);
            nameToSkill = SkillList.ToDictionary(s => s.name.ToLower(), s => s);
        }

        public ClientSkill this[string skillId]
        {
            get
            {
                skillId = skillId.Trim().ToLower();
                if (nameToSkill.ContainsKey(skillId))
                    return nameToSkill[skillId];
                return null;
            }
        }
    }

    [Serializable]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class ClientSkill
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Activation activation_attribute;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool add_wpn_range;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string allow_use_form_category;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string ammo_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string ammo_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string ammo_fx_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string ammo_fxc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int ammo_speed;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string aura_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string aura_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string aura_fx_slot;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string auto_attack;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string broadcast_use_message;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cancel_rate;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string cast_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string cast_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string cast_fxc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string cast_voice;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string castcancel_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string castcancel_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int casting_delay;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int chain_category_level;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string chain_category_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int self_chain_count;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int prechain_count;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string chain_category_priority;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int chain_skill_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int chain_skill_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string chain_time;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Stance change_stance;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string clone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string component;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int component_count;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int component_expendable;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_checktime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_checktime_lv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public CostType cost_checktime_parameter;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_dp;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_dp_lv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_end;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_end_lv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public CostType cost_parameter;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_start;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_start_lv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_time;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_toggle;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_toggle_lv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        [DefaultValue(CounterSkill.None)]
        public CounterSkill counter_skill;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int delay_id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int delay_time;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int delay_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc_abnormal;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc_knight;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc_long;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc_long_2nd;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc_prechain_category_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        [DefaultValue(DispellCategory.None)]
        public DispellCategory dispel_category;

        public string getDispellCategory()
        {
            switch (dispel_category)
            {
                case DispellCategory.None: return null;
                case DispellCategory.All: return "ALL";
                case DispellCategory.Buff: return "BUFF";
                case DispellCategory.Debuff: return "DEBUFF";
                case DispellCategory.DebuffMen: return "DEBUFF_MENTAL";
                case DispellCategory.DebuffPhy: return "DEBUFF_PHYSICAL";
                case DispellCategory.Extra: return "EXTRA";
                case DispellCategory.Npc_buff: return "NPC_BUFF";
                case DispellCategory.Npc_DebuffPhy: return "NPC_DEBUFF_PHYSICAL";
                case DispellCategory.Stun: return "STUN";
                default: return null;
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_acc_mod1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_acc_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_basiclv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_checktime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_critical_add_dmg_mod1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_critical_add_dmg_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_critical_prob_mod1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_critical_prob_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_effectid;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect1_hidemsg;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_hop_a;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_hop_b;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_hop_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect1_noresist;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_randomtime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_remain1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_remain2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved_cond1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_reserved_cond1_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_reserved_cond1_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved_cond2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_reserved_cond2_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect1_reserved_cond2_prob2; // 2.5

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved10;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved11;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved12;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved13;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved14;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved15;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved16;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved17;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved18;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved19;


        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved5;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved6;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved7;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved8;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_reserved9;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_target_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect1_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_acc_mod1; // 2.5 added

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_acc_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect2_basiclv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        [DefaultValue(true)]
        public bool effect2_checkforchain;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect2_checktime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_cond_attack_dir;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_attack_dir_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_attack_dir_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_cond_preeffect;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_preeffect_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_preeffect_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_cond_race;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_race_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_race_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_cond_status;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_status_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_cond_status_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_critical_add_dmg_mod1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_critical_add_dmg_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_critical_prob_mod1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_critical_prob_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect2_effectid;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect2_hidemsg;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect2_hop_a;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect2_hop_b;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_hop_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect2_noresist;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect2_randomtime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_remain1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_remain2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved_cond1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_reserved_cond1_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect2_reserved_cond1_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved10;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved11;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved12;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved13;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved14;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved15;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved16;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved17;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved18;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved5;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved6;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved7;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved8;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_reserved9;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_target_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect2_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_acc_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect3_basiclv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect3_checkforchain;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect3_checktime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_cond_attack_dir;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_cond_attack_dir_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_cond_attack_dir_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_cond_preeffect;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_cond_preeffect_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_cond_preeffect_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_cond_race_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_cond_status;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_cond_status_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_cond_status_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_critical_prob_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect3_effectid;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect3_hidemsg;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect3_hop_a;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect3_hop_b;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_hop_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect3_noresist;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect3_randomtime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_remain1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_remain2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved_cond1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_reserved_cond1_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_reserved_cond1_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved_cond2; // 2.5

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_reserved_cond2_prob1; // 2.5

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect3_reserved_cond2_prob2; // 2.5

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved10;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved11;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved12;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved13;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved14;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved15;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved16;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved17;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved18;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved5;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved6;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved7;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved8;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_reserved9;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_target_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect3_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect4_basiclv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect4_checkforchain;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect4_checktime;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_cond_preeffect;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_cond_preeffect_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_cond_preeffect_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_cond_status;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_cond_status_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_cond_status_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect4_critical_prob_mod2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect4_effectid;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect4_hidemsg;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect4_hop_a;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect4_hop_b;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_hop_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool effect4_noresist;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_remain1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_remain2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved_cond1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_reserved_cond1_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_reserved_cond1_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int? effect4_critical_prob_mod1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved10;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved11;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved12;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved13;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved14;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved15;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved16;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved17;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved18;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved5;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved6;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved7;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved8;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_reserved9;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_target_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string effect4_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string fire_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string fire_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string fire_fxc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public FirstTarget first_target;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int first_target_valid_distance;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_camera_shake;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_camera_work;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx_attacker_oriented;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx_ex1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx_ex2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx_ex3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx_ex4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fx_ex5;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_fxc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string hit_target_camera_shake;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public HostileType hostile_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string instant_skill;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string interval_hit_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string interval_hit_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string max_maintain_count;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string max_skill_point;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string motion_mode;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        [DefaultValue(ClientMotion.None)]
        public ClientMotion motion_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool move_casting;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int motion_play_speed;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int remaining_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int decal_dir_angle;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool need_fx; // 2.5

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string cast_camera_work; // 2.5; values: 3 or fx name

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string no_jump_cancel;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string no_remove_at_die;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string no_save_on_logout;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string nonchained_delay_time;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int nouse_combat_state;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int obstacle;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string peace_skill;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string penalty_skill_succ;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string penalty_type_succ;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string pre_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string pre_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string pre_fx_delay;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string prechain_category_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string prechain_skillname;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int pvp_damage_ratio;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int pvp_remain_time_ratio;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string pvp_skill_cancel_bonus;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string reflect_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string remove_at_fly_end;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_2hsword;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_book;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_bow;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_dagger;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_dispel_level;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_dp_lv;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public LeftWeapon required_leftweapon;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_mace;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_orb;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_polearm;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_staff;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool required_sword;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int revision_distance;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public FlyingRestriction self_flying_restriction;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string show_weapon;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public SkillCategory skill_category;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string skillicon_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string splash;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string status_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string status_fx_bone;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string status_fx_slot;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string status_sfx1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string status_shader;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string stc_desc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public SkillSubType sub_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public FlyingRestriction target_flying_restriction;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int target_marker_radius;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int target_maxcount;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public TargetRange target_range;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_range_area_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int target_range_opt1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_range_opt2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_range_opt3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_range_opt4; // 2.5 added

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public RelationRestriction target_relation_restriction;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Slot target_slot;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int target_slot_level;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public SpeciesRestriction target_species_restriction;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool target_stop;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_valid_status1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_valid_status2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_valid_status3;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_valid_status4;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string target_valid_status5;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public SkillType type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string trail_tex;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool ultra_skill;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool ultra_transfer;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        [DefaultValue("")]
        public string use_arrow; // FX_pow or 0

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int use_arrow_count;

        #region Version 2.5 added

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_charge_armor;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int cost_charge_weapon;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int conflict_id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_harp;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_keyblade;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_keyhammer;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_gun;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_cannon;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string charge_set_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int polish_charge_weapon;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string skill_group_name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect3_critical_prob_mod1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int charging_delay;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_cond_preeffect_prob1;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_cond_preeffect_prob12;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int penalty_no_casting_time_succ;

        [XmlElement(Form = XmlSchemaForm.Unqualified)] //hide nohide
        public string self_hide_restriction;

        [XmlElement(Form = XmlSchemaForm.Unqualified)] //hide nohide
        public string target_hide_restriction;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int toggle_timer;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int required_ride_robot; //0 1

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int effect1_cond_preeffect_prob2;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string system_fire_fx;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string system_fire_fx_bone;


        #endregion

        public ClientSkill()
        {
            effect2_checkforchain = true;
            effect3_checkforchain = true;
            effect4_checkforchain = true;
        }
    }

    [Serializable]
    public enum FirstTarget
    {
        None = 0,
        Me = 2,
        ME = Me,
        me = Me,
        MyPet = 3,
        Point = 7,
        Target = 4,
        target = Target,
        Target_MyParty_NonVisible = 6,
        TargetOrMe = 1,
        TargetorMe = TargetOrMe,
    }

    [Serializable]
    public enum TargetRange
    {
        None = 0,
        Area = 3,
        area = Area,
        OnlyOne = 1,
        onlyone = OnlyOne,
        Party = 2,
        party = Party,
        Party_WithPet = 4,
        Point = 5,
    }

    [Serializable]
    public enum SpeciesRestriction
    {
        None = 0,
        Npc = 3,
        NPC = Npc,
        npc = Npc,
        Pc = 2,
        PC = Pc,
        All = 1,
        all = All,
        ALL = All,
    }

    [Serializable]
    public enum RelationRestriction
    {
        None = 0,
        All = 3,
        all = All,
        ALL = All,
        Enemy = 1,
        enemy = Enemy,
        Friend = 4,
        friend = Friend,
        MyParty = 2,
    }

    [Serializable]
    public enum SkillType
    {
        None = 0,
        Physical = 1,
        Magical = 2,
    }

    public enum SkillSubType
    {
        None = 0,
        none = None,
        Attack = 1,
        attack = Attack,
        Chant = 2,
        Heal = 3,
        heal = Heal,
        Buff = 4,
        buff = Buff,
        Debuff = 5,
        debuff = Debuff,
        Summon = 6,
        summon = Summon,
        SummonHoming = 7,
        SummonTrap = 8,
    }

    [Serializable]
    public enum SkillCategory
    {
        [XmlEnum("SKILLCTG_NONE")]
        None = 0,

        [XmlEnum("SKILLCTG_CHAIN_SKILL")]
        ChainSkill,

        [XmlEnum("SKILLCTG_DEATHBLOW")]
        DeathBlow,

        [XmlEnum("SKILLCTG_DISPELL")]
        Dispell,

        [XmlEnum("SKILLCTG_DRAIN")]
        Drain,

        [XmlEnum("SKILLCTG_HEAL")]
        Heal,

        [XmlEnum("SKILLCTG_MENTAL_DEBUFF")]
        MentalDebuff,

        [XmlEnum("SKILLCTG_PHYSICAL_DEBUFF")]
        PhysicalDebuff,

        [XmlEnum("SKILLCTG_REBIRTH")]
        Rebirth
    }

    [Serializable]
    public enum HostileType
    {
        None = 0,
        none = None,
        Direct = 1,
        direct = Direct,
        Indirect = 2,
        indirect = Indirect,
    }

    [Serializable]
    public enum Slot
    {
        None = 0,
        none = None,
        Buff = 1,
        buff = Buff,
        Debuff = 2,
        DeBuff = Debuff,
        debuff = Debuff,
        Special = 3,
        Special2 = 4,
        special2 = Special2,
        Boost = 5,
        NoShow = 6,
        Noshow = NoShow,
        noshow = NoShow,
        Chant = 7,
    }

    [Serializable]
    public enum FlyingRestriction
    {
        None = 0,
        All = 1,
        Fly = 2,
        fly = Fly,
        Ground = 3,
        ground = Ground,
    }

    [Serializable]
    public enum LeftWeapon
    {
        None = 0,
        Shield,
        Dual

    }

    [Serializable]
    public enum CounterSkill
    {
        None = 0,
        Block,
        Dodge,
        dodge = Dodge,
        Parry,
    }

    [Serializable]
    public enum Activation
    {
        None = 0,
        Active = 1,
        Provoked = 2,
        Maintain = 3,
        maintain = Maintain,
        Toggle = 4,
        Passive = 5,
        Charge = 6,
    }

    [Serializable]
    public enum DispellCategory
    {
        None = 0,
        never = None,
        All = 1,
        all = All,
        ALL = All,
        Buff = 2,
        BUFF = Buff,
        buff = Buff,
        Debuff = 3,
        DebuffMen = 4,
        debuffmen = DebuffMen,
        DeBuffMen = DebuffMen,
        DebuffPhy = 5,
        debuffphy = DebuffPhy,
        debuffPhy = DebuffPhy,
        Extra = 6,
        Npc_Buff = 7,
        Npc_buff = Npc_Buff,
        npc_buff = Npc_Buff,
        NPC_Buff = Npc_Buff,
        Npc_DebuffPhy = 8,
        NPC_DebuffPhy = Npc_DebuffPhy,
        Stun = 9,
        stun = Stun,


    }

    [Serializable]
    public enum Stance
    {
        none = 0,
        stance1,
    }

    [Serializable]
    public enum CostType
    {
        NONE = 0,
        MP,
        HP,
        MP_RATIO,
        MP_Ratio = MP_RATIO,
        HP_RATIO,
    }

    [Serializable]
    public enum ClientMotion
    {
        None = 0,
        Angleshot,
        AreaATK,
        AreaATK2,
        AreaATKLF,
        AreaATKLH,
        AreaATKRF,
        AreaATKRH,
        AreaAtk,
        AreaFire,
        AreaFire2,
        AreaFireOD,
        Areaatk,
        Areaatk2,
        Areafire,
        Areafire2,
        AsBurst,
        BACKDASH2,
        BackATK,
        BackAtk,
        BackDash,
        BackDash2,
        BashATK,
        BashATK2,
        BasicShot,
        Buff,
        CHainATK2,
        ChainATK03,
        ChainATK1,
        ChainATK2,
        ChainATK3,
        ChainAtk2,
        Chainatk1,
        Chant,
        CloseAerial,
        CryingInsult,
        CycloneStrike,
        DPATKC,
        DPATKM,
        Debuff,
        DefendRoar,
        DivineATK,
        DownATK,
        DownATK2,
        DownATK3,
        DownShot,
        Downshot,
        DrainEye,
        DrainShot,
        Eatfire,
        ElementalShield,
        FIAreaATK,
        FIAreaATK2,
        FiBuff,
        FiDpatk,
        FrontDash,
        GDAreaATK,
        GDNormalfire,
        HackATK,
        HandBind,
        Handbind,
        HarshPain,
        Heal,
        HealingGrace,
        HellCurse,
        Hellpain,
        HurtATK2,
        JumpingATK,
        JumpingCut,
        KnDpatk,
        Lightning2,
        MaPointfire,
        Mending,
        MovingATK,
        MultiATK,
        NormalFIre,
        NormalFire,
        NormalFire2,
        NormalFire3,
        NormalFire4,
        NormalFireMO,
        Normalfire,
        NormalfireHD,
        NormalfireTH,
        Normalshot,
        OpenAerial,
        PHBuff,
        PHBurst,
        PQuickATK,
        PhBuff,
        PhBurst,
        PhBurst2,
        Phbuff,
        Phburst,
        Phburst1,
        PointFire,
        PointFire2,
        PointFire3,
        PointFire4,
        PointFireLH,
        PointFireRH,
        Pointfire,
        Pointfire2,
        Pointfire3,
        Pointfire4,
        Pointfire5,
        PollutionCut,
        PowerATK,
        PowerATK1,
        PowerATK2,
        PowerATK3,
        PowerAtk,
        PowerShot,
        Powershot,
        PunishingLight,
        PushATK,
        QuickShot,
        RaiseATK,
        RaisingStorm,
        RangeAtk,
        RapidShot,
        Rasummon,
        RestoreLife,
        RobustATK,
        SanctIn,
        SanctOut,
        Sanctuary,
        SanctuaryFire,
        Say,
        SeismicDrain,
        ShadowDrop,
        ShieldATK,
        ShieldATK2,
        ShieldHit,
        ShieldStance,
        Signet1,
        Signet2,
        SnatchATK,
        SnatchAtk,
        SongChange01,
        SongofWeight01,
        SoulSteal,
        StuckATK,
        Summon1,
        Talk,
        ThroughShot,
        Throw,
        ThrowTrap,
        ThunderBlade,
        Tiger5,
        Transform,
        TrapFire,
        TripleSlash,
        TripleSnacher,
        TurnATK,
        TurnATK2,
        TurnATK3,
        UpperATK2,
        VacuumExplosion,
        VitalityDrain,
        WeaponBind,
        WhirlDrain,
        WhirlTornado,
        WindBlade,
        alterfire,
        areaatk,
        areafire,
        asdpatk,
        axe,
        breakATK,
        buff,
        buff3,
        buildbuff,
        chainATK1,
        chainATK2,
        chainatk03,
        chainatk1,
        chainatk2,
        chant,
        chchant1,
        chchant2,
        chchant3,
        chdpatk,
        closeaerial,
        conjfire,
        crossparry,
        deBuff,
        debuff,
        debuff2,
        downatk,
        downshot,
        eatfire,
        elspirit,
        elsummon1,
        elsummon2,
        elsummon3,
        genocidalSword,
        getfire,
        heal,
        heal2,
        hellcurse,
        herb,
        holyATK,
        hurtatk,
        maPointfire2,
        mending,
        mending2,
        mine,
        multiatk,
        normalFire,
        normalfire,
        normalfire2,
        normalfire3,
        normalfire4,
        normalfire5,
        normalshot,
        openaerial,
        phbuff,
        phburst,
        phburst2,
        pointFire,
        pointfire,
        pointfire3,
        pointfire4,
        powerATK,
        poweratk,
        powerkick,
        powershot,
        prHeal1,
        prHeal3,
        prheal1,
        prheal3,
        pushatk3,
        quest_drinkpoint,
        radpatk,
        shieldstance,
        signet2,
        signetexp1,
        signetexp2,
        smashATK,
        spindash,
        stuckATK,
        suction,
        [XmlEnum("throw")]
        _throw,
        tiger1,
        tiger2,
        tiger3,
        tigerbeat,
        trapfire,
        twistatk,
        waveATK,
        /// <summary>
        /// ////////////////////////////////////////////////////////////4.0
        /// </summary>
        StuckATK2,
        ChargingHit,
        ChargingShock,
        BurserkLance,
        BodySmash,
        DragonSlash,
        FrontDash_FlyingSlash,
        FlameLance,
        meteorStrike,
        MagicalFlame,
        DeepSleep,
        BloodySlash,
        EncouragingRoar,
        RapidFire,
        FinalFire,
        SealingArrow,
        BackSlash,
        BackStrike,
        BackBlow,
        StunBurst,
        HolyLight,
        HolyEmpower,
        HolyLightwave,
        ThunderofPunishment,
        TearingStrike,
        HarshThrust,
        ElementalStrike,
        Normalfire2,
        Shockattack01,
        GreatSong01,
        SongBless01,
        Shockattack02,
        Defensebuff01,
        buff02,
        SongDestroy01,
        GreatSong02,
        ColdShock01,
        Shockattack03,
        SongHeal01,
        Songsleep01,
        SongofInjury01,
        SongofRefresh01,
        buff01,
        SongResurrection01,
        Songteleport01,
        GreatSong03,
        SoulShock01,
        SongBless02,
        SongSleep01,
        MentalicShock01,
        Shockattack04,
        SongGravity01,
        SongPowerShock01,
        SongSlow01,
        SongEmergentHeal01,
        SongPeace01,
        MentalicShock02,
        CrazyDance01,
        SongBattle01,
        SongAir01,
        buff1,
        buff2,
        buff4,
        Reload,
        rapidshot4,
        Checkshot,
        FocusShot2,
        LowerShot,
        AimWing,
        Focusshot3,
        FocusShot3,
        Chainshot2,
        rapidshot3,
        rapidshot2,
        StrikeShot2,
        Escape,
        rapidshot1,
        Chainshot1,
        StrikeShot1,
        Chainshot3,
        FocusShot1,
        PointFire1,
        DrainAtk,
        Strikeshot1,
        BackDash_GunFire,
        Chargeshot2,
        ChargeWideShot1,
        Chargeshot1,
        MagicCannon,
        ChargeWideShot2,
        AntiGravityShot,
        AimShot1,
        AimShot2,
        ChargeShot2,
        FallShot,
        ShockChargeShot,
        ChargeWideShot3,
        Chargeshot3,
        FlameShot,
        PR_Root_G3,
        cash_social_diving,
        Teleport,
        EarthQuake,
        Phburst2,
        MultiAtk,
        SpinAtk,
        ChainAtk1,
        StuckAtk,
        sanctuary,
        shapechange,
        BreathL,
        BreathM,
        BreathR,
        DeadlyAtk,
        HurtATK,
        BreathL4s,
        BreathL8s,
        BreathM4s,
        BreathM8s,
        BreathR4s,
        BreathR8s,
        Openariel,
        TwistATK,

    }
}
