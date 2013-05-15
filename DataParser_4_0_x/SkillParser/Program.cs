namespace Jamie.Skills
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Jamie.ParserBase;

    class Program
    {
        static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        static Assembly effectsAssembly = Assembly.GetAssembly(typeof(Effect));

        static void Main(string[] args)
        {

            Utility.WriteExeDetails();

            Utility.LoadStrings(root);

            Utility.LoadClientNpcs(root);

            Utility.LoadItems(root);

            Utility.LoadSkills(root);

            Utility.LoadSkillLearns(root);

            Utility.LoadUltraSkills(root);



            string outputPath = Path.Combine(root, @".\output");
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var outputFile = new SkillData();

            var settings = new XmlWriterSettings()
            {
                CheckCharacters = false,
                CloseOutput = false,
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\n",
                Encoding = new UTF8Encoding(false)
            };

            #region Pet UltraSkill parsing

            PetSkillTemplates petTemplates = new PetSkillTemplates();
            List<PetSkill> ultraSkills = new List<PetSkill>();

            foreach (var ultra in Utility.UltraSkillIndex.UltraSkillList)
            {
                PetSkill petSkill = new PetSkill();
                petSkill.skill_id = Utility.SkillIndex[ultra.ultra_skill].id;
                if (String.Compare("Light_Summon_MagmaElemental_G1", ultra.pet_name) == 0)
                    ultra.pet_name = "Dark_Summon_MagmaElemental_G1";
                else if (String.Compare("Dark_Summon_TempestElemental_G1", ultra.pet_name) == 0)
                    ultra.pet_name = "Light_Summon_TempestElemental_G1";
                petSkill.pet_id = Utility.ClientNpcIndex[ultra.pet_name];
                if (petSkill.pet_id == -1)
                {
                    petSkill.missing_pet_id = ultra.pet_name;
                    petSkill.pet_id = 0;
                }
                ClientSkill skill = Utility.SkillIndex[ultra.order_skill];
                if (skill != null)
                    petSkill.order_skill = Utility.SkillIndex[ultra.order_skill].id;
                //else
                //    petSkill.missing_order_skill = ultra.order_skill;
                ultraSkills.Add(petSkill);
            }

            petTemplates.SkillList = ultraSkills.OrderBy(s => s.skill_id).ToList();

            try
            {
                using (var fs = new FileStream(Path.Combine(outputPath, "pet_skills.xml"),
                                         FileMode.Create, FileAccess.Write))
                using (var writer = XmlWriter.Create(fs, settings))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(PetSkillTemplates));
                    ser.Serialize(writer, petTemplates);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }

            #endregion

            /* Old
			var skillsetEx = new Dictionary<int, int>() {
                { 1566, 1564 }, { 1586, 1564 }, { 2210, 1564 }, { 1574, 1564 }, { 1575, 1564 }, { 1576, 1564 },
                { 1596, 1564 }, { 1564, 1564 }, { 1565, 1564 }, { 1590, 1564 },
                { 1554, 1554 }, { 1588, 1554 },
                { 969, 969 }, { 970, 969 }, { 971, 969 }, { 989, 969 }, { 2181, 969 }, { 1220, 969 },
                { 1221, 969 }, { 1222, 969 }, { 1267, 969 }, { 1318, 969 }, { 1319, 969 }, { 1342, 969 },
                { 2183, 969 },
                { 1105, 1105 }, { 1107, 1105 }, { 1108, 1105 }, { 1187, 1105 }, { 1112, 1105 }, { 1186, 1105 }
			};
			*/

            // skillset_exception == client_skills.delay_id (All client skills have delay id but skills on same cooldown/exception list have the same delay id)
            var skillsetEx2 = new Dictionary<int, int>() {
			 {1564, 2}, {1565, 2}, {1566, 2}, {1574, 2}, {1575, 2}, {1586, 2}, {1590, 2}, {1596, 2}, {2210, 2},
			 {1105, 4}, {1107, 4}, {1108, 4}, {1112, 4}, {1186, 4}, {1187, 4},
			 {969, 5}, {970, 5}, {971, 5}, {989, 5}, {1220, 5}, {1221, 5}, {1222, 5}, {1267, 5}, {1318, 5}, {1319, 5}, {1342, 5}, {2181, 5}, {2183, 5},
			 {540, 6}, {1636, 6}, {1637, 6}, {1685, 6}, {1708, 6}, {1781, 6}, {1782, 6}, {1935, 6}, {2008, 6}, {2062, 6}, {2231, 6}, {2232, 6}, {8345, 6},
                {173, 7}, {524, 7},
			 {1554, 8}, {1555, 8}, {1556, 8}, {1587, 8}, {1588, 8},
			 {955, 9}, {956, 9}, {957, 9}, {986, 9}, {1230, 9}, {2159, 9}, {2165, 9}, {2178, 9},  
			 {559, 175}, {582, 175}, {851, 175}, {858, 175}, {948, 175}, 
			 {646, 2005}, {684, 2005}, {693, 2005}, {694, 2005}, {776, 2005}, {777, 2005}, {786, 2005}, {2075, 2005}
			};


            // Limit skills with same delay_id to a max # of occurances
            var skillset_maxoccurs = new Dictionary<int, int>() {
                {646, 2}, {684, 2}, {693, 2}, {776, 2}, {777, 2}, {786, 2}, {2075, 2}
			};

            var delayIdOverrides = new Dictionary<int, int>() {
                {11885, 11885}, {11886, 11885}, {11887, 11885}, {11888, 11885}, {11889, 11885},
                {11890, 11890}, {11891, 11890}, {11892, 11890}, {11893, 11890}, {11894, 11890}
			};

            //  skills which have 0 cast time or cooldown is smaller and which exclude each other
            var delayIdsForSkills = new Dictionary<int, int>() {
				{1178, 1178}, {2148, 1178}, {1177, 1178}, {2147, 1178}
			};

            #region Finding the chainDelayIdsInclude

            //Dictionary<int, int> diffDelays = Utility.SkillIndex.SkillList.ToDictionary(s => s.id, s => s.delay_id);
            //Dictionary<int, bool> diffCount = diffDelays.Values.GroupBy(x => x).ToDictionary(x => x.Key, g => g.Count() > 1);
            /*
            //test lookups
            int skill_id = 2075; //known to have listed exception
            int delay_id = 2005; //delay id associated with client skill

            if (diffCount.ContainsKey(delay_id) && diffCount[delay_id]) {
                int skill_exception_test = diffDelays[skill_id];
            }
            */

            /*
            var diffDelays = Utility.SkillIndex.SkillList.Where(s => s.delay_id > 0 &&
                                                                s.sub_type == SkillSubType.buff &&
                                                                s.chain_category_level > 0)
                                                         .ToLookup(s => s.delay_id, s => s);
            var chainCategories = new Dictionary<string, List<ClientSkill>>();
            
            foreach (var group in diffDelays) {
                var skills = diffDelays[group.Key];
                string[] chains = skills.Select(s => s.chain_category_priority).Distinct().ToArray();
                string categoryName = chains.Where(s => s != null).FirstOrDefault();
                if (categoryName == null) {
                    chains = skills.Select(s => s.prechain_skillname).Distinct().ToArray();
                    categoryName = chains.Where(s => s != null).FirstOrDefault();
                }

                if (categoryName == null)
                    continue;

                if (chainCategories.ContainsKey(categoryName)) {
                    Debug.Print("Different delay id for chain: {0}", categoryName);
                    chainCategories[categoryName].AddRange(skills.ToList());
                } else {
                    chainCategories.Add(categoryName, skills.ToList());
                }
            }

            chainCategories = chainCategories.Where(pair => pair.Value.Count > 1)
                                             .OrderBy(p => p.Value.First().delay_id)
                                             .ToDictionary(pair => pair.Key, pair => pair.Value);

            // StringBuilder sb = new StringBuilder();
            foreach (var pair in chainCategories) {
                // sb.AppendFormat("SkillSet by delay: {0}\r\n", pair.Key);
                foreach (var skill in pair.Value) {
                    // string desc = Utility.StringIndex.GetString(skill.desc);
                    if (skill.casting_delay == 0) {
                        int dur = 0;
                        if (skill.effect1_reserved_cond1 == null ||
                            skill.effect1_reserved_cond1_prob2 == 100 &&
                            skill.effect1_reserved_cond1 == "EveryHit")
                            dur = Math.Max(dur, skill.effect1_remain2);
                        if (skill.effect2_reserved_cond1 == null || 
                            skill.effect2_reserved_cond1_prob2 == 100 &&
                            skill.effect2_reserved_cond1 == "EveryHit")
                            dur = Math.Max(dur, skill.effect2_remain2);
                        if (skill.effect3_reserved_cond1 == null || 
                            skill.effect3_reserved_cond1_prob2 == 100 &&
                            skill.effect3_reserved_cond1 == "EveryHit")
                            dur = Math.Max(dur, skill.effect3_remain2);
                        if (skill.effect4_reserved_cond1 == null || 
                            skill.effect4_reserved_cond1_prob2 == 100 &&
                            skill.effect4_reserved_cond1 == "EveryHit")
                            dur = Math.Max(dur, skill.effect4_remain2);
                        skill.casting_delay = dur;
                    }

                    if (skill.casting_delay == 0 || skill.casting_delay / 1000 > skill.delay_time / 100) {
                        sb.AppendFormat("\tSkill: id={0}; name={1}; delayId={2}; r10={3}/{4}/{5}/{6}; delay={7}; cooldown={8}\r\n",
                                        skill.id, desc,
                                        skill.delay_id, skill.effect1_reserved10, skill.effect2_reserved10,
                                        skill.effect3_reserved10, skill.effect4_reserved10,
                                        skill.casting_delay, skill.delay_time / 100);
                    }
                }
            }
            */

            #endregion

            Dictionary<ClientSkill, List<ClientEffect>> list = new Dictionary<ClientSkill, List<ClientEffect>>();
            Dictionary<string, HashSet<string>> statData = new Dictionary<string, HashSet<string>>();

            const EffectType filterEffect = EffectType.None;
            const Stat filterStat = Stat.None;
            StringBuilder sb = new StringBuilder();

            //for (var filterEffect = EffectType.None + 1;
            //     filterEffect <= EffectType.XPBoost; filterEffect++) {

            foreach (var sk in Utility.SkillIndex.SkillList)
            {
                var template = new SkillTemplate();
                template.skill_id = sk.id;
                template.name = sk.desc;

                // Check Skill Strings for Name ID
                template.nameId = Utility.StringIndex[sk.desc.ToUpper()] * 2 + 1;

                // SKill Name ID is Not in Skill Strings, Check Item Stings
                if (template.nameId == -1)
                {
                    template.nameId = Utility.StringIndex[sk.desc.ToUpper()] * 2 + 1;
                }

                // SKill Name ID is Not in Item Strings, Check Main Strings
                if (template.nameId == -1)
                {
                    template.nameId = Utility.StringIndex[sk.desc.ToUpper()] * 2 + 1;
                }

                // SKill Name ID is Not in General Strings, Check UI Strings
                if (template.nameId == -1)
                {
                    template.nameId = Utility.StringIndex[sk.desc.ToUpper()] * 2 + 1;
                }

                string stack;
                int level = -1;
                if (level == -1)
                    level = Utility.GetSkillLevelFromName(sk.name, out stack);
                //if (level == -1 && !String.IsNullOrEmpty(sk.skillicon_name))
                //    level = Utility.GetSkillLevelFromName(sk.skillicon_name, out stack); 
                if (level == -1)
                    level = 1;
                template.lvl = level;
                level = Utility.GetSkillLevelFromName(sk.desc, out stack);
                template.stack = stack.ToUpper();
                // some overides for stack.
                if (sk.id == 251 || sk.id == 258 || sk.id == 385)
                {
                    template.stack = "SKILL_GL_251_258_385";
                }


                template.skilltype = (skillType)sk.type;
                template.skillsubtype = (skillSubType)sk.sub_type;
                template.tslot = (TargetSlot)sk.target_slot;
                template.tslot_level = sk.target_slot_level;
                template.activation = (activationAttribute)sk.activation_attribute;
                template.cooldown = sk.delay_time / 100;
                template.cancel_rate = sk.cancel_rate;
                if (sk.casting_delay > 0)
                    template.duration = sk.casting_delay;
                template.pvp_duration = sk.pvp_remain_time_ratio;
                template.chain_skill_prob = sk.chain_skill_prob1 == 0 && sk.chain_skill_prob2 != 0 ? sk.chain_skill_prob2 : 0;
                //template.dispel_category = sk.dispel_category.ToString().ToUpper();
                template.dispel_category = sk.getDispellCategory();
                //template.dispel_level = sk.required_dispel_level;
                //template.delay_id = sk.delay_id;

                if (!String.IsNullOrEmpty(sk.penalty_skill_succ))
                {
                    var penaltySkill = Utility.SkillIndex[sk.penalty_skill_succ];
                    if (penaltySkill == null)
                    {
                        Debug.Print("Missing penalty skill: {0}", sk.penalty_skill_succ);
                    }
                    else
                    {
                        template.penalty_skill_id = penaltySkill.id;
                    }
                }

                if (sk.change_stance != Stance.none)
                    template.stance = true;
                template.pvp_damage = sk.pvp_damage_ratio;

                if (sk.first_target != FirstTarget.None)
                {

                    //template.setproperties = new Properties();
                    var properties = new Properties();

                    properties.firsttarget = new FirstTargetProperty();
                    properties.firsttarget.value = (FirstTargetAttribute)sk.first_target;
                    if (sk.first_target_valid_distance > 0)
                    {
                        properties.firsttargetrange = new FirstTargetRangeProperty();
                        properties.firsttargetrange.value = sk.first_target_valid_distance;
                        properties.firsttargetrange.valueSpecified = true;

                    }
                    //else if (sk.first_target == FirstTarget.Target)
                    //	properties.firsttargetrange = new FirstTargetRangeProperty();

                    if (sk.target_range != TargetRange.None)
                    {
                        properties.target_range = new TargetRangeProperty();
                        if (sk.target_range_opt1 != 0)
                        {
                            properties.target_range.distance = sk.target_range_opt1;
                            properties.target_range.distanceSpecified = true;
                        }
                        if (sk.target_maxcount != 0)
                        {
                            properties.target_range.maxcount = sk.target_maxcount;
                            properties.target_range.maxcountSpecified = true;
                        }
                        if (sk.target_range != TargetRange.None)
                        {
                            properties.target_range.value = (TargetRangeAttribute)sk.target_range;
                        }
                    }


                    if (sk.target_relation_restriction != RelationRestriction.None)
                    {
                        properties.targetrelation = new TargetRelationProperty();
                        properties.targetrelation.value = (TargetRelationAttribute)sk.target_relation_restriction;
                    }


                    if (sk.target_species_restriction != SpeciesRestriction.None)
                    {
                        properties.targetspecies = new TargetSpeciesProperty();
                        properties.targetspecies.value = (TargetSpeciesAttribute)sk.target_species_restriction;
                    }


                    var importUtil = Utility<ClientSkill>.Instance;
                    List<string> states = new List<string>();
                    importUtil.Export<string>(sk, "target_valid_status", states);
                    TargetState state = 0;
                    foreach (string s in states)
                    {
                        try
                        {
                            TargetState s1 = (TargetState)Enum.Parse(typeof(TargetState), s, true);
                            state |= s1;
                        }
                        catch (Exception e) { } 
                    }
                    //if (state != TargetState.NONE)
                    //{
                    //    states = states.ConvertAll(s => s.ToUpper());
                    //    List<TargetStatusProperty> statusProperties = states.ConvertAll(s => { TargetStatusProperty t = new TargetStatusProperty(); t.value = s; return t; });
                    //    properties.targetstatus = statusProperties;
                    //}

                    if (sk.add_wpn_range) {
                        template.initproperties = new Properties();
                        template.initproperties.addweaponrange = new AddWeaponRangeProperty();
                    }

                    template.setproperties = properties;
                }

                template.useconditions = new Conditions();
                template.startconditions = new Conditions();
                template.actions = new Actions();

                List<Condition> useList = new List<Condition>();
                List<Condition> useEquipmentConditionsList = new List<Condition>();
                List<Condition> startList = new List<Condition>();
                List<Action> actionList = new List<Action>();

                //TargetFlyingCondition targetflyingcondition = null;
                TargetCondition targetcondition = null;

                /*
                if (sk.target_species_restriction != SpeciesRestriction.None && sk.target_species_restriction != SpeciesRestriction.All)
                {
                    targetcondition = new TargetCondition((FlyingRestriction)sk.target_species_restriction);
                    startList.Add(targetcondition);
                }*/

                /*
                if (sk.required_leftweapon != LeftWeapon.None)
                {
                    useEquipmentConditionsList.Add(new ArmorCondition(sk.required_leftweapon.ToString().ToUpper()));
                }
                 */

                // Periodic Actions
                if (sk.cost_checktime > 0 || sk.cost_toggle > 0)
                {
                    PeriodicAction periodicAction = new PeriodicAction();
                    CostType parameter = sk.cost_checktime_parameter;

                    if (parameter == CostType.NONE)
                        parameter = sk.cost_parameter;

                    if (parameter == CostType.MP || parameter == CostType.MP_RATIO)
                    {
                        var mpUseAction = new MpUseAction();
                        periodicAction.checktime = sk.cost_time;
                        mpUseAction.ratio = parameter == CostType.MP_RATIO;

                        if (sk.cost_checktime_lv > 0 || sk.cost_checktime > 0)
                        {
                            mpUseAction.value = sk.cost_checktime;
                            mpUseAction.delta = sk.cost_checktime_lv;
                        }
                        else if (sk.cost_toggle_lv > 0 || sk.cost_toggle > 0)
                        {
                            mpUseAction.value = sk.cost_toggle;
                            mpUseAction.delta = sk.cost_toggle_lv;
                        }
                        periodicAction.mpuse = mpUseAction;
                    }
                    else if (parameter == CostType.HP || parameter == CostType.HP_RATIO)
                    {
                        var hpUseAction = new HpUseAction();
                        periodicAction.checktime = sk.cost_time;
                        hpUseAction.percent = parameter == CostType.HP_RATIO;

                        if (sk.cost_checktime_lv > 0 || sk.cost_checktime > 0)
                        {
                            hpUseAction.value = sk.cost_checktime;
                            hpUseAction.delta = sk.cost_checktime_lv;
                        }
                        else if (sk.cost_toggle_lv > 0 || sk.cost_toggle > 0)
                        {
                            hpUseAction.value = sk.cost_toggle;
                            hpUseAction.delta = sk.cost_toggle_lv;
                        }

                        periodicAction.hpuse = hpUseAction;
                    }

                    //template.periodicactions = periodicAction;
                }
                else
                {
                    // Non Periodic Actions
                    if (sk.cost_parameter != CostType.NONE && sk.cost_checktime == 0)
                    {
                        CostType parameter = sk.cost_checktime_parameter;

                        if (parameter == CostType.NONE)
                            parameter = sk.cost_parameter;

                        if (parameter == CostType.MP || parameter == CostType.MP_RATIO)
                        {
                            var mpUseAction = new MpUseAction();
                            mpUseAction.ratio = parameter == CostType.MP_RATIO;

                            if (sk.cost_end > 0 || sk.cost_end_lv > 0)
                            {
                                mpUseAction.value = sk.cost_end;
                                mpUseAction.delta = sk.cost_end_lv;
                                actionList.Add(mpUseAction);
                            }
                            else if (sk.cost_toggle_lv > 0 || sk.cost_toggle > 0)
                            {
                                mpUseAction.value = sk.cost_toggle;
                                mpUseAction.delta = sk.cost_toggle_lv;
                                actionList.Add(mpUseAction);
                            }
                        }
                        else if (parameter == CostType.HP || parameter == CostType.HP_RATIO)
                        {
                            var hpUseAction = new HpUseAction();
                            hpUseAction.percent = parameter == CostType.HP_RATIO;

                            if (sk.cost_end > 0 || sk.cost_end_lv > 0)
                            {
                                hpUseAction.value = sk.cost_end;
                                hpUseAction.delta = sk.cost_end_lv;
                                actionList.Add(hpUseAction);
                            }
                            else if (sk.cost_toggle_lv > 0 || sk.cost_toggle > 0)
                            {
                                hpUseAction.value = sk.cost_toggle;
                                hpUseAction.delta = sk.cost_toggle_lv;
                                actionList.Add(hpUseAction);
                            }
                        }
                    }
                }

                if (sk.cost_end != 0)
                {
                    startList.Add(new MpCondition(sk.cost_end, sk.cost_end_lv));
                }

                /*
                if (sk.required_leftweapon != LeftWeapon.None)
                {
                    startList.Add(new ArmorCondition(sk.required_leftweapon.ToString().ToUpper()));
                }*/

                if (sk.use_arrow != null && sk.use_arrow != "0")
                { // 3 arena skills have FX_pow, FX_HIT and weaponbody
                    startList.Add(new ArrowCheckCondition());
                }
                else
                {
                    if (sk.use_arrow_count != 0)
                        startList.Add(new ArrowCheckCondition());
                }

                if (sk.cost_dp > 0)
                {
                    startList.Add(new DpCondition(sk.cost_dp));
                    actionList.Add(new DpUseAction(sk.cost_dp, sk.cost_dp_lv));
                }
                /*
                string required_weapons = getRequiredWeapons(sk);
                if (required_weapons != null)
                {
                    startList.Add(new WeaponCondition(required_weapons));
                }

                // Target Flying Start Condition
                FlyRestriction restriction = (FlyRestriction)sk.target_flying_restriction;
                if (restriction != FlyRestriction.NONE)
                {
                    if (targetflyingcondition == null)
                        startList.Add(new TargetFlyingCondition(restriction));
                    else
                        targetflyingcondition.restriction = restriction;
                }

                if (sk.chain_category_name != null)
                {
                    ChainCondition chain = new ChainCondition();
                    chain.category = sk.chain_category_name.ToUpper();

                    if (sk.prechain_category_name != null) chain.precategory = sk.prechain_category_name.ToUpper();
                    if (sk.chain_time != null) chain.time = Int32.Parse(sk.chain_time);

                    startList.Add(chain);
                }
                 */

                // Self Flying Start Condition
                /*
                restriction = (FlyRestriction)sk.self_flying_restriction;
                if (restriction != FlyRestriction.NONE)
                    startList.Add(new SelfCondition(restriction));

                if (sk.nouse_combat_state == 1)
                {
                    startList.Add(new CombatCheckCondition());
                }
                */

                if (sk.component != null)
                {
                    Item item = Utility.ItemIndex.GetItem(sk.component);
                    if (item == null)
                    {
                        Debug.Print("Missing item for skill {0}", sk.id);
                    }
                    else
                    {
                        actionList.Add(new ItemUseAction(item.id, sk.component_count));
                    }
                }

                if (!sk.move_casting)
                    useList.Add(new PlayerMovedCondition(false));

                #region Fill conditions and actions

                // Use Conditions
                if (useList.Count == 0)
                    template.useconditions = null;
                else
                    template.useconditions.ConditionList = useList;

                // Start Conditions
                if (startList.Count == 0)
                    template.startconditions = null;
                else
                    template.startconditions.ConditionList = startList;


                // Skill Actions
                if (actionList.Count == 0)
                    template.actions = null;
                else
                    template.actions.ActionList = actionList;

                #endregion

                if (skillsetEx2.ContainsKey(sk.id))
                {
                    template.skillset_exception = skillsetEx2[sk.id];
                }

                /*
                if (sk.delay_id != 0)
                    template.skillset_exception = diffCount.ContainsKey(sk.delay_id) && diffCount[sk.delay_id] == true ? diffDelays[sk.id] : 0;
                */


                //if (delayIdsForSkills.ContainsKey(sk.id))
                //{
                //    template.delay_id = delayIdsForSkills[sk.id];
                //}
                //else if (delayIdOverrides.ContainsKey(sk.id))
                //{
                //    template.delay_id = delayIdOverrides[sk.id];
                //}

                string descrStr = sk.desc_long == null ? sk.desc : sk.desc_long;
                var desc = Utility.StringIndex.GetStringDescription(descrStr);
                string desc_2nd;
                if (!String.IsNullOrEmpty(sk.desc_long_2nd))
                    desc_2nd = Utility.StringIndex.GetStringDescription(sk.desc_long_2nd).body;

                if (sk.motion_name != ClientMotion.None)
                {
                    Motion motion = new Motion();
                    motion.name = sk.motion_name != ClientMotion.None ? sk.motion_name.ToString().ToLower() : null;
                    motion.instant_skill = sk.instant_skill != null && sk.instant_skill == "1" ? true : false;
                    motion.speed = sk.motion_play_speed != 0 && sk.motion_play_speed != 100 ? sk.motion_play_speed : 0;
                    template.motion = motion;
                }


                #region Effect processing

                var utility = Utility<ClientSkill>.Instance;
                List<ClientEffect> effects = new List<ClientEffect>();
                utility.Export(sk, "effect", effects);

                var validEffects = effects.Where(e => e.type != EffectType.None).ToArray();
                bool save = validEffects.Where(e => e.type == filterEffect).Any();
                // var validEffects = effects.Where(e => e.changeStat != Stat.None).ToArray();
                // bool save = validEffects.Where(e => e.changeStat == filterStat).Any();

                string text = desc == null ? String.Empty : desc.body;
                int idx = 0;
                var vars = (from v in Utility.GetVarStrings(text)
                            let parts = v.Split('.')
                            let parsed = Int32.TryParse(parts[0].Remove(0, 1), out idx)
                            let name = parsed ? parts[1] : v
                            let var = parsed ? parts[2] : String.Empty
                            select new { Id = idx, Data = new StatData(name, var) })
                          .ToLookup(a => a.Id, a => a);

                if (save)
                {
                    sb.Append("\r\n");
                    sb.AppendFormat("---Skill Id = {0}, Name = '{1}' ---\r\n",
                                 sk.id, Utility.StringIndex.GetString(sk.desc));
                    if (desc == null)
                        sb.AppendFormat("NO DESCRIPTION\r\n");
                    else
                        sb.AppendFormat("{0}: {1}\r\n", desc.name, desc.body);
                    if (sk.desc_abnormal != null)
                    {
                        sb.AppendFormat("ABNORMAL: {0}\r\n", Utility.StringIndex.GetString(sk.desc_abnormal));
                    }
                }

                foreach (var eff in validEffects)
                {
                    EffectClass @class = (EffectClass)eff.type;

                    #region Overrides


                    #endregion

                    Type type = effectsAssembly.GetType(String.Format("{0}.{1}", typeof(Effect).Namespace,
                                                        @class.ToString()));
                    Effect ourEffect = null;

                    if (save)
                        sb.AppendFormat("Data for {0}:\r\n", @class);

                    if (vars.Any())
                    {
                        if (vars.Contains(idx + 1))
                        {
                            foreach (var v in vars[idx + 1])
                            {
                                if (!statData.ContainsKey(v.Data.Name))
                                    statData.Add(v.Data.Name, new HashSet<string>());
                                statData[v.Data.Name].Add(v.Data.Var);
                                if (save)
                                    sb.AppendFormat("\tEffect = {0}, Var = {1}\r\n",
                                                 v.Data.Name, v.Data.Var);
                            }
                        }
                    }
                    if (save)
                    {
                        sb.AppendFormat("\tE{0} Reserved: ", eff.e);
                        using (TextWriter wr = new StringWriter(sb))
                        {
                            ObjectDumper.Write(eff.reserved, wr);
                            sb.Append("\r\n");
                            if (!String.IsNullOrEmpty(eff.reserved_cond1))
                            {
                                sb.AppendFormat("\tCondition = {0}, Prob. = ", eff.reserved_cond1);
                                ObjectDumper.Write(eff.reserved_cond1_prob, wr);
                                sb.Append("\r\n");
                            }
                            if (!String.IsNullOrEmpty(eff.reserved_cond2))
                            {
                                sb.AppendFormat("\tCondition = {0}, Prob. = ", eff.reserved_cond2);
                                ObjectDumper.Write(eff.reserved_cond2_prob, wr);
                                sb.Append("\r\n");
                            }
                        }
                    }

                    if (type == null)
                    {
                        string skillName = Utility.StringIndex.GetString(sk.desc);
                        Debug.Print("Effect {0} not handled (skillId={1}; name='{2}')", @class, sk.id, skillName);
                        continue;
                    }
                    else
                    {
                        ourEffect = (Effect)Activator.CreateInstance(type);
                    }

                    if (template.effects == null)
                    {
                        template.effects = new Effects();/*
						if (sk.skillicon_name != null)
							template.effects.food = sk.skillicon_name.EndsWith("_food"); */
                    }

                    eff.Skill = sk;
                    eff.Template = template;
                    ourEffect.Import(eff, null);

                   template.effects.EffectList.Add(ourEffect);
                }

                #endregion

                //if (template.effects != null && template.effects.EffectList != null) {
                //    if (template.effects.EffectList.Count == 0)
                //        template.effects.EffectList = null;
                //    else if (template.activation != activationAttribute.PASSIVE) {
                //        foreach (var eff in template.effects.EffectList) {
                //            if (eff is DeformEffect || eff is PolymorphEffect || eff is ShapeChangeEffect ||
                //                eff is WeaponDualEffect) {
                //            } else
                //                eff.basiclvl = 0; // not used for active skills
                //        }
                //    }
                //}
                if (template.effects != null && template.effects.EffectList == null)
                    template.effects = null;

                outputFile.SkillList.Add(template);

                if (filterEffect != EffectType.None)
                {
                    string fileName = /*"stat_" + filterStat*/filterEffect.ToString() + ".txt";
                    using (var fs = new FileStream(Path.Combine(outputPath, fileName), FileMode.Create,
                                             FileAccess.Write))
                    {
                        using (TextWriter wr = new StreamWriter(fs))
                        {
                            wr.Write(sb.ToString());
                        }
                    }
                }

                sb.Length = 0;
            }

            try
            {
                using (var fs = new FileStream(Path.Combine(outputPath, "skill_templates.xml"),
                                         FileMode.Create, FileAccess.Write))
                using (var writer = XmlWriter.Create(fs, settings))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(SkillData));
                    ser.UnknownAttribute += new XmlAttributeEventHandler(OnUnknownAttribute);
                    ser.UnknownElement += new XmlElementEventHandler(OnUnknownElement);
                    ser.Serialize(writer, outputFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Process completed");
            Console.Read();
        }

        static void OnUnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            FieldInfo field = e.ObjectBeingDeserialized.GetType().GetField(e.Attr.Name);
            MethodInfo parse = field.FieldType.GetMethod("Parse");
            field.SetValue(e.ObjectBeingDeserialized, parse.Invoke(null, new object[] { e.Attr.Value }));
        }

        static void OnUnknownElement(object sender, XmlElementEventArgs e)
        {
            FieldInfo field = e.ObjectBeingDeserialized.GetType().GetField(e.Element.Name);
            MethodInfo parse = field.FieldType.GetMethod("Parse");
            field.SetValue(e.ObjectBeingDeserialized, parse.Invoke(null, new object[] { e.Element.Value }));
        }

        static string getRequiredWeapons(ClientSkill sk)
        {
            string required = "";
            if (sk.required_2hsword) required += "SWORD_2H" + " ";
            if (sk.required_book) required += "BOOK_2H" + " ";
            if (sk.required_bow) required += "BOW" + " ";
            if (sk.required_dagger) required += "DAGGER_1H" + " ";
            if (sk.required_mace) required += "MACE_1H" + " ";
            if (sk.required_orb) required += "ORB_2H" + " ";
            if (sk.required_polearm) required += "POLEARM_2H" + " ";
            if (sk.required_staff) required += "STAFF_2H" + " ";
            if (sk.required_sword) required += "SWORD_1H" + " ";

            if (required == "")
            {
                return null;
            }

            return required.Trim();
        }
    }
}
