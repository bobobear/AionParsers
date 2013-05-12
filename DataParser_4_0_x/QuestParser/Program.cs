using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Jamie.ParserBase;
using Jamie.ParserBase.Skills;

namespace Jamie.Quests {
	class Program {
		static readonly string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

		static void Main(string[] args) {

			Utility.WriteExeDetails();
            Utility.LoadStrings(root);
            Utility.LoadItems(root);
			Utility.LoadQuestFile(root);

			var newStuff = Utility.QuestIndex.QuestList.Where(n => n.nodes.Count > 0)
													   .SelectMany(n => n.nodes);
			var distinctNew = newStuff.Select(n => n.Name).Distinct();

			if (distinctNew.Any()) {
				Console.WriteLine("New elements found in XML which were not coded in. Continue?");
				Console.WriteLine("Elements are:");
				foreach (var nodeName in distinctNew)
					Console.WriteLine("      {0}", nodeName);
				Console.Write("(Y/N): ");
				var input = Console.ReadKey(true);
				if (Char.ToLower(input.KeyChar) == 'n')
					return;
			}

		

			Utility.LoadClientNpcs(root);	
	
			Utility.LoadTitles(root);
		
			Utility.LoadNpcFactions(root);

			Console.Write("Parsing... ");
			int top = Console.CursorTop;
			int left = Console.CursorLeft;

			var utility = Utility<Quest>.Instance;
			List<QuestOur> ourList = new List<QuestOur>();
			foreach (Quest quest in Utility.QuestIndex.QuestList) {
				//testing
				//if (quest.Description == null) continue;

				// disable check, using new check for testing
				// not enabled 임시 in korean
				if (quest.Description != null) {
					if (quest.Description.body == "Temporary" || quest.Description.body == "임시")
						continue;
				}

				// Only Exclude Temporary and min level == 99 (Disabled Quests)
				//if (Utility.QuestStringIndex.GetString(quest.desc) == "Temporary" || quest.minlevel_permitted == 99) continue;

				/* disable check
				if (quest.minlevel_permitted != 99 && quest.minlevel_permitted > 60 &&
					quest.minlevel_permitted != 65)
					continue;
				*/

				QuestOur q = new QuestOur();
				q.id = quest.id;

				Console.SetCursorPosition(left, top);
				Console.Write("Q" + q.id);

				q.max_repeat_count = quest.max_repeat_count;

				// Assign to category
				q.category = quest.category1.ToUpper();

				/* disabled, Get Id's from string index instead
				q.nameId = (quest.Description.id * 2 + 1);
				if (q.nameId > 0)
					q.nameIdSpecified = true;
				*/

				q.nameId = Utility.StringIndex[quest.desc];
				q.nameIdSpecified = true;

				// Get Mentor Faction from Client Data
				if (quest.npcfaction_name != null) q.npcfaction_id = Utility.NpcFactionIndex[quest.npcfaction_name];

				// set mentor type
				if (quest.category2 != null) if (quest.category2.Contains("Mentee")) q.mentor_type = "MENTE";
				if (quest.category2 != null) if (quest.category2.Contains("Mentor")) q.mentor_type = "MENTOR";

                if (quest.race_permitted == string.Empty)
                {
                    q.race_permitted = Race.PC_ALL;
                    q.race_permittedSpecified = true;
                }
                else if (quest.race_permitted.Split(' ').Count() > 1) {
					q.race_permitted = Race.PC_ALL;
					q.race_permittedSpecified = true;
				}
				else {
					q.race_permitted = (Race)Enum.Parse(typeof(Race), quest.race_permitted.ToUpper());
					q.race_permittedSpecified = true;
				}

				q.name = quest.Description.body.TrimEnd();
				q.minlevel_permitted = quest.minlevel_permitted;
				if (quest.minlevel_permitted < quest.maxlevel_permitted)
					q.maxlevel_permitted = quest.maxlevel_permitted;

                q.playtime_hour = quest.playtime_hour;

				if(quest.quest_repeat_cycle!= null)
					q.repeat_cycle = quest.quest_repeat_cycle.ToUpper();
				
				
				q.cannot_giveup = quest.cannot_giveup;
				q.cannot_share = quest.cannot_share;

				string[] classes = quest.class_permitted.ToUpper().Split(' ');
				string classesParse = String.Join(",", classes);
				Class classEnum = (Class)Enum.Parse(typeof(Class), classesParse);
				if ((classEnum & Class.ALL) != Class.ALL) {
					q.class_permitted = ((ClassOur)classEnum).ToString().Replace(",", String.Empty);
					q.class_permittedSpecified = true;
				}
				
				string race = quest.race_permitted.ToLower();
				if (race == "pc_light") {
					q.race_permitted = Race.ELYOS;
					q.race_permittedSpecified = true;
				} else if (race == "pc_dark") {
					q.race_permitted = Race.ASMODIANS;
					q.race_permittedSpecified = true;
				}
				

				string gender = quest.gender_permitted.ToLower();
				if (gender != "all") {
					q.gender_permittedSpecified = true;
					if (gender == "male")
						q.gender_permitted = Gender.MALE;
					else
						q.gender_permitted = Gender.FEMALE;
				}

				var drops = new List<QuestDrop>();
				utility.Export(quest, "drop_item_", drops);
				drops = drops.Where(d => d.item_id != 0 && d.chance > 0).ToList();
				if (drops.Count > 0) {
					// check if multiple NCS
					q.QuestDrops = new List<QuestDrop>();
					foreach (var drop in drops) {
						if (drop.npc_id > 0) {
							drop.collecting_step = quest.collect_progress;
							q.QuestDrops.Add(drop);
						}
						else {
							if (drop.npcIds == null /*&& drop.npc_faction != null*/) {
								// NOT defined in 1.9 but in 2.0 - skip them
								// q.QuestDrops.Add(drop);
								continue;
							}
							foreach (var npcName in drop.npcIds) {
								QuestDrop newDrop = (QuestDrop)drop.Clone();
								int id = Utility.ClientNpcIndex[npcName];
								if (id != -1) {
									newDrop.npc_id = id;
									newDrop.collecting_step = quest.collect_progress;
									q.QuestDrops.Add(newDrop);
								}
							}
						}
					}
				}

				q.QuestWorkItems = new List<QuestItemsOur>();
				utility.Export(quest, "quest_work_item", q.QuestWorkItems);
				q.QuestWorkItems = q.QuestWorkItems.Where(d => d.count > 0).ToList();
				if (q.QuestWorkItems.Count == 0)
					q.QuestWorkItems = null;
				else
					q.QuestWorkItemsSpecified = true;

				if (quest.assassin_selectable_reward != null) {
					q.AssassinSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.assassin_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.assassin_selectable_item);
						if (our != null)
							q.AssassinSelectableRewards.Add(our);
					}
				}

				if (quest.chanter_selectable_reward != null) {
					q.ChanterSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.chanter_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.chanter_selectable_item);
						if (our != null)
							q.ChanterSelectableRewards.Add(our);
					}
				}

				if (quest.elementalist_selectable_reward != null) {
					q.ElementalistSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.elementalist_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.elementalist_selectable_item);
						if (our != null)
							q.ElementalistSelectableRewards.Add(our);
					}
				}

				if (quest.fighter_selectable_reward != null) {
					q.FighterSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.fighter_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.fighter_selectable_item);
						if (our != null)
							q.FighterSelectableRewards.Add(our);
					}
				}

				if (quest.knight_selectable_reward != null) {
					q.KnightSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.knight_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.knight_selectable_item);
						if (our != null)
							q.KnightSelectableRewards.Add(our);
					}
				}

				if (quest.priest_selectable_reward != null) {
					q.PriestSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.priest_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.priest_selectable_item);
						if (our != null)
							q.PriestSelectableRewards.Add(our);
					}
				}

				if (quest.ranger_selectable_reward != null) {
					q.RangerSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.ranger_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.ranger_selectable_item);
						if (our != null)
							q.RangerSelectableRewards.Add(our);
					}
				}

				if (quest.wizard_selectable_reward != null) {
					q.WizardSelectableRewards = new List<QuestItemsOur>();
					foreach (var r in quest.wizard_selectable_reward) {
						QuestItemsOur our = GetRewardItem(r.wizard_selectable_item);
						if (our != null)
							q.WizardSelectableRewards.Add(our);
					}
				}

                if (quest.bard_selectable_reward != null)
                {
                    q.Bard_selectablRewards = new List<QuestItemsOur>();
                    foreach (var r in quest.bard_selectable_reward)
                    {
                        QuestItemsOur our = GetRewardItem(r.bard_selectable_item);
                        if (our != null)
                            q.Bard_selectablRewards.Add(our);
                    }
                }

                if (quest.gunner_selectable_reward != null)
                {
                    q.Gunner_selectableRewards = new List<QuestItemsOur>();
                    foreach (var r in quest.gunner_selectable_reward)
                    {
                        QuestItemsOur our = GetRewardItem(r.gunner_selectable_item);
                        if (our != null)
                            q.Gunner_selectableRewards.Add(our);
                    }
                }

				q.CollectItems = new List<CollectItem>();
				utility.Export(quest, "collect_item", q.CollectItems);
				q.CollectItems = q.CollectItems.Where(d => d.count > 0).ToList();
				if (q.CollectItems.Count == 0)
					q.CollectItems = null;
				else
					q.CollectItemsSpecified = true;

				q.combine_skillpoint = quest.combine_skillpoint;
				if (q.combine_skillpoint > 0)
					q.combine_skillpointSpecified = true;

				if (quest.combineskill != CombineSkillType.any) {
					q.combineskill = (int)quest.combineskill;
					q.combineskillSpecified = true;
				}

				var questConds = GetConditions(quest, "finished_quest_cond");
				if (questConds.Count > 0) {
					if (q.start_conditions == null) q.start_conditions = new QuestStartCondition();
					q.start_conditions.finishedQuestSteps = new List<QuestStep>();
					for (int i = 0; i < questConds.Count; i++) {
						q.start_conditions.finishedQuestSteps.Add(questConds[i].questSteps[0]);
					}
				}
				questConds = GetConditions(quest, "unfinished_quest_cond");
				if (questConds.Count > 0) {
					if (q.start_conditions == null) q.start_conditions = new QuestStartCondition();
					q.start_conditions.unfinishedQuestSteps = new List<QuestStep>();
					for (int i = 0; i < questConds.Count; i++) {
						q.start_conditions.unfinishedQuestSteps.Add(questConds[i].questSteps[0]);
					}
				}
				questConds = GetConditions(quest, "acquired_quest_cond");
				if (questConds.Count > 0) {
					if (q.start_conditions == null) q.start_conditions = new QuestStartCondition();
					q.start_conditions.acquiredQuestSteps = new List<QuestStep>();
					for (int i = 0; i < questConds.Count; i++) {
						q.start_conditions.acquiredQuestSteps.Add(questConds[i].questSteps[0]);
					}
				}
				questConds = GetConditions(quest, "noacquired_quest_cond");
				if (questConds.Count > 0) {
					if (q.start_conditions == null) q.start_conditions = new QuestStartCondition();
					q.start_conditions.notAcquiredQuestSteps = new List<QuestStep>();
					for (int i = 0; i < questConds.Count; i++) {
						q.start_conditions.notAcquiredQuestSteps.Add(questConds[i].questSteps[0]);
					}
				}
				questConds = GetConditions(quest, "equiped_item_name");
				if (questConds.Count > 0) {
					if (q.start_conditions == null) q.start_conditions = new QuestStartCondition();
					q.start_conditions.equippedQuestSteps = new List<QuestStep>();
					for (int i = 0; i < questConds.Count; i++) {
						q.start_conditions.equippedQuestSteps.Add(questConds[i].questSteps[0]);
					}
				}

				q.use_class_reward = quest.use_class_reward == 1 ? true : false;

				var rewards = new List<Rewards>();

				utility.Export(quest, "reward_exp", rewards);
				utility.Export(quest, "reward_gold", rewards);
                utility.Export(quest, "reward_score", rewards);
				utility.Export(quest, "reward_abyss_point", rewards);
				utility.Export(quest, "reward_title", rewards);
				utility.Export(quest, "reward_extend_inventory", rewards);
				utility.Export(quest, "reward_extend_stigma", rewards);
				utility.Export(quest, "reward_item", rewards);
				utility.Export(quest, "selectable_reward_item", rewards);

				rewards = rewards.Where(r => r.BasicRewards != null || r.SelectableRewards != null ||
											 r.exp > 0 || r.gold > 0 || r.reward_abyss_point > 0 ||
											 r.title > 0).ToList();
				if (rewards.Count > 0)
					q.Rewards = rewards;

				Rewards extRewards = null;

				if (quest.reward_gold_ext != 0) {
					extRewards = new Rewards();
					extRewards.gold = quest.reward_gold_ext;
					extRewards.goldSpecified = true;
				}

                if (quest.reward_score_ext != 0)
                {
                    extRewards = new Rewards();
                    extRewards.score = quest.reward_score_ext;
                }

				if (quest.reward_title_ext != null) {
					int titleId = Utility.TitleIndex[quest.reward_title_ext];
					if (titleId > 0) {
						if (extRewards == null)
							extRewards = new Rewards();
						extRewards.title = titleId;
					}
				}

				rewards = new List<Rewards>();
				utility.Export(quest, "reward_item_ext_", rewards);
				rewards = rewards.Where(r => r.BasicRewards != null).ToList();
				if (rewards.Count > 0) {
					var items = rewards.SelectMany(r => r.BasicRewards);
					if (extRewards == null)
						extRewards = new Rewards();
					extRewards.BasicRewards = new List<QuestItemsOur>();
					extRewards.BasicRewards.AddRange(items);
				}

				rewards = new List<Rewards>();
				utility.Export(quest, "selectable_reward_item_ext_", rewards);
				rewards = rewards.Where(r => r.SelectableRewards != null).ToList();
				if (rewards.Count > 0) {
					var items = rewards.SelectMany(r => r.SelectableRewards);
					if (extRewards == null)
						extRewards = new Rewards();
					extRewards.SelectableRewards = new List<QuestItemsOur>();
					extRewards.SelectableRewards.AddRange(items);
				}

				if (extRewards != null)
					q.ExtRewards = new List<Rewards>() { extRewards };

				// Lets Add Item Bonuses
				var bonuses = new List<Rewards>();
				utility.Export(quest, "reward_item", bonuses);
				bonuses = rewards.Where(r => r.BonusRewards != null).ToList();
				if (bonuses.Count > 0) {
					foreach (var bonus in bonuses[0].BonusRewards) {
						if (bonus.name.Contains("Quest_L_matter_option") || bonus.name.Contains("Quest_D_matter_option")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[4].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "MANASTONE"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_L_medicine") || bonus.name.Contains("Quest_D_medicine")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "MEDICINE"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_D_food") || bonus.name.Contains("Quest_D_food")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "FOOD"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_D_fortress") || bonus.name.Contains("Quest_D_fortress")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "FORTRESS"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_L_Goods") || bonus.name.Contains("Quest_D_Goods")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "GOODS"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_A_BranchLunarEvent")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "LUNAR"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_L_magical") || bonus.name.Contains("Quest_D_magical")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "MAGICAL"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_D_material")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "MATERIAL"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_L_medal") || bonus.name.Contains("Quest_D_medal")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "MEDAL"; // bonus type

							string quantity = split[split.Count() - 1];
							int number;
							bool result = Int32.TryParse(quantity, out number);
							if (result) q.bonus.quantity = number;
						}
						if (bonus.name.Contains("Quest_L_Christmas") || bonus.name.Contains("Quest_D_Christmas")) {
							q.bonus = new Bonus();
							q.bonus.type = "MOVIE"; // bonus type
						}
						if (bonus.name.Contains("Quest_L_Recipe")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "RECIPE"; // bonus type
						}
						if (bonus.name.Contains("Quest_L_redeem") || bonus.name.Contains("Quest_D_redeem")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "REDEEM"; // bonus type
						}
						if (bonus.name.Contains("Quest_L_BranchRiftEvent") || bonus.name.Contains("Quest_D_BranchRiftEvent")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "RIFT"; // bonus type
						}
						if (bonus.name.Contains("Quest_L_task") || bonus.name.Contains("Quest_D_task")) {
							q.bonus.skill = q.combineskill;
							q.bonus.type = "TASK"; // bonus type
						}
						if (bonus.name.Contains("Quest_A_BranchWinterEvent")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "WINTER"; // bonus type
						}
						if (bonus.name.Contains("Quest_L_boss") || bonus.name.Contains("Quest_D_boss")) {
							string[] split = bonus.name.Split('_');
							q.bonus = new Bonus();
							q.bonus.level = Int32.Parse(split[3].Substring(0, 2)); // Get Level Part of Random Bonus
							q.bonus.type = "BOSS"; // bonus type
						}
					}
				}

				ourList.Add(q);
				Thread.Sleep(1);
			}

			quest_data fileData = new quest_data();
			fileData.Quests = ourList;

			var settings = new XmlWriterSettings() {
				CheckCharacters = false,
				CloseOutput = false,
				Encoding = new UTF8Encoding(false),
				Indent = true,
				IndentChars = "\t",
			};

			string outputPath = Path.Combine(root, @".\output\");
			if (!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			try {
				using (var fs = new FileStream(Path.Combine(outputPath, "quest_data.xml"),
											   FileMode.Create, FileAccess.Write))
				using (var writer = XmlWriter.Create(fs, settings)) {
					XmlSerializer ser = new XmlSerializer(typeof(quest_data));
					ser.Serialize(writer, fileData);
				}
			}
			catch (Exception ex) {
				Debug.Print(ex.ToString());
			}

			Console.Clear();
			Console.WriteLine("Done.");
			Console.ReadKey();
			Environment.Exit(0);
		}

		static QuestItemsOur GetRewardItem(string data) {
			string[] parts = data.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			int itemCount = 0;
			if (parts.Length > 1) {
				itemCount = Int32.Parse(parts[1]);
			}
			else {
				itemCount = 1;
			}
			int id = 0;
			Item item = Utility.ItemIndex.GetItem(parts[0]);
			if (item != null) {
				id = item.id;
			}
			if (id == 0)
				return null;
			return new QuestItemsOur() { count = itemCount, item_id = id, item_idSpecified = true };
		}

		static List<QuestItemsOur> GetSelectableRewards(Quest quest, string name) {
			List<QuestItemsOur> list = new List<QuestItemsOur>(1);
			Utility<Quest>.Instance.Export(quest, name, list);
			list = list.Where(r => r.count > 0).ToList();
			if (list.Count == 0)
				list = null;
			return list;
		}

		static List<QuestStartCondition> GetConditions(Quest quest, string name) {
			List<String> conds = new List<string>();
			Utility<Quest>.Instance.Export(quest, name, conds);
			List<QuestStartCondition> startConditions = new List<QuestStartCondition>();
			if (conds.Count > 0) {
				foreach (string cond in conds) {
					var finalConds = new Dictionary<int, QuestStep>();
					var condition = new QuestStartCondition();
					condition.questSteps = new List<QuestStep>();

					if (name == "equiped_item_name") {
						int itemId = Utility.ItemIndex.GetItem(cond).id;
						QuestStep qs = new QuestStep(0, 0, itemId);
						if (finalConds.ContainsKey(itemId)) {
							QuestStep qsOld = finalConds[itemId];
						}
						else {
							finalConds.Add(itemId, qs);
						}
					}
					else {
						string[] parseString = cond.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
						foreach (string c in parseString) {
							string[] condData = c.Split('_');
							string questIdStep = (condData.Length > 1 ? condData[1] : condData[0]).TrimStart('q', 'Q');
							condData = questIdStep.Split(':');
							int questId = Int32.Parse(condData[0]);
							int rewardNo = 0;
							if (condData.Length > 1) rewardNo = Int32.Parse(condData[1]);
							QuestStep qs = new QuestStep(questId, rewardNo, 0);
							if (finalConds.ContainsKey(questId)) {
								QuestStep qsOld = finalConds[questId];
							}
							else {
								finalConds.Add(questId, qs);
							}
						}
					}

					condition.questSteps = finalConds.OrderBy(p => p.Key).Select(p => p.Value).ToList();
					startConditions.Add(condition);
				}
			}
			return startConditions;
		}
	}
}
