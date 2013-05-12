using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Diagnostics;
using Jamie.ParserBase;
using Jamie.Quests;

namespace Jamie.Npcs {
	class Program {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

		static void Main(string[] args) {

			Utility.WriteExeDetails();

			Utility.LoadStrings(root);
	
			Utility.LoadClientNpcs(root);

            Utility.LoadItems(root);

            Utility.LoadNpcsTemplate(root);

			var client_npcs = Utility.ClientNpcIndex.NpcList.Where(n => n.id != 0);

			NpcTemplates outputFile = new NpcTemplates();
			outputFile.NpcList = new List<NpcTemplate>();

			// Lets get some progress information
			int counter = 0;
			int total_record_count = client_npcs.Count();

			// Lets build a tribe reference file for the xsd
			List<string> tribes = new List<string>();

            List<string> ai_names = new List<string>();

            List<string> npctypes = new List<string>();

			foreach (var client_npc in client_npcs) {
				var template = new NpcTemplate();

                // Get npc data from existing server templates
				NpcTemplate original_template = Utility.OriginalNpcTemplate[client_npc.id];

				template.npc_id = client_npc.id;
                //template.name = "test_4_0";// temp solution //getNpcNameFromClientStrings(client_npc.desc);
				template.name_id = getNpcNameIdFromClientStrings(client_npc.desc); if (template.name_id == -1) Debug.Write("Missing Name ID for NPC ID: " + template.npc_id.ToString());
				//template.type = client_npc.npc_type != null ? client_npc.npc_type.ToUpper() : "GENERAL";//4.0 ok
                template.tribe = client_npc.tribe != null ? client_npc.tribe.ToUpper() : "NONE"; // //4.0 ok
               
                string race_check = client_npc.race_type != null ? client_npc.race_type.ToUpper() : "NONE";//4.0 ok

                if (race_check == "PC_DARK") template.race = "ASMODIANS";
                if (race_check == "PC_LIGHT") template.race = "ELYOS";

                if (race_check == "DARK") template.race = "ASMODIANS";
                if (race_check == "LIGHT") template.race = "ELYOS";
                template.race = race_check;
				template.height = Math.Round((client_npc.scale / 100),2);//4.0 ok
				template.hpgauge = client_npc.hpgauge_level;//4.0 ok
				
				//template.ai = getAiHandlerFromString(client_npc); //test
                template.arange = (int)client_npc.attack_range;//4.0 ok
                template.arate = (int)client_npc.attack_rate;//4.0 ok
                template.adelay = client_npc.attack_delay;//4.0 ok
                template.stare_range = (int)client_npc.stare_distance;//4.0 ok
                template.talking_distance = (int)client_npc.talking_distance;//4.0 ok
                template.title_id = client_npc.npc_title != null ? Utility.StringIndex[client_npc.npc_title] : 0;//4.0 ok
                template.talk_delay = client_npc.talk_delay_time;//4.0 ok

				template.stats = new NpcStatsTemplate();
				template.stats.walk_speed = Math.Round(client_npc.move_speed_normal_walk, 1);
				template.stats.run_speed = Math.Round(client_npc.move_speed_normal_run, 1);
				template.stats.run_speed_fight = Math.Round(client_npc.move_speed_combat_run, 1);

				// Data that can not be parsed from client
				if (original_template != null) {
					template.level = original_template.level;
					template.rank = original_template.rank;

                    template.name = original_template.name;
                    template.ai = original_template.ai; //test
					//template.rating = original_template.rating;
					//template.abyss_type = original_template.abyss_type;
					template.npc_type = original_template.npc_type;
					template.stats.maxHp = original_template.stats.maxHp;
					template.stats.maxXp = original_template.stats.maxXp;
					template.stats.main_hand_attack = original_template.stats.main_hand_attack;
					template.stats.main_hand_accuracy = original_template.stats.main_hand_accuracy;
					template.stats.pdef = original_template.stats.pdef;
					//template.stats.mresist = original_template.stats.mresist;
                    template.stats.mdef = original_template.stats.mdef;
					template.stats.power = original_template.stats.power;
					template.stats.evasion = original_template.stats.evasion;
					template.stats.accuracy = original_template.stats.accuracy;
				}
				else {
					template.level = 55;
					template.rank = NpcRank.VETERAN;
					//template.rating = "NORMAL";
					template.npc_type = getNpcTypeFromCursorType(client_npc.cursor_type.ToString(), client_npc.npc_function_type.ToString(), client_npc.name);
					//template.abyss_type = getAbyssType(); // TODO:
                    template.name = "test_4_0";
                    template.ai = getAiHandlerFromString(client_npc); //test
					template.stats.maxHp = 35000;
					template.stats.maxXp = 1000000;
					template.stats.main_hand_attack = 1000;
					template.stats.main_hand_accuracy = 1000;
					template.stats.pdef = 4000;
					//template.stats.mresist = 1000;
                    template.stats.mdef = 1000;
					template.stats.power = 100;
					template.stats.evasion = 1000;
					template.stats.accuracy = 1000;
				}

				// Not sure what bound radius is for
				if (client_npc.BoundRadius != null) {
					template.bound_radius = new BoundRadius();
					template.bound_radius.front = Math.Round(client_npc.BoundRadius.front, 2);
					template.bound_radius.side = Math.Round(client_npc.BoundRadius.side, 2);
					template.bound_radius.upper = Math.Round(client_npc.BoundRadius.upper, 2);
				}

				// Get visible equipment on npcs
				if (client_npc.VisibleEquipment != null) {
					template.equipment = new List<int>();
					int itemId;
					if (client_npc.VisibleEquipment.head != null)  if((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.head)) != -1) template.equipment.Add(itemId);
					if (client_npc.VisibleEquipment.torso != null) if ((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.torso)) != -1) template.equipment.Add(itemId);
					if (client_npc.VisibleEquipment.leg != null) if ((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.leg)) != -1) template.equipment.Add(itemId);
					if (client_npc.VisibleEquipment.foot != null) if ((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.foot)) != -1) template.equipment.Add(itemId);
					if (client_npc.VisibleEquipment.shoulder != null) if ((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.shoulder)) != -1) template.equipment.Add(itemId);
					if (client_npc.VisibleEquipment.glove != null) if ((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.leg)) != -1) template.equipment.Add(itemId);
					if (client_npc.VisibleEquipment.main != null) if ((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.main)) != -1) template.equipment.Add(itemId);
					if (client_npc.VisibleEquipment.sub != null) if ((itemId = getNpcEquipmentItemId(client_npc.VisibleEquipment.sub)) != -1) template.equipment.Add(itemId);
				}

				// Build a list of templates to output
				outputFile.NpcList.Add(template);

				// Build our list of tribes for xsd
                if (template.tribe != null && /*template.tribe != " " && */!tribes.Contains(template.tribe)) tribes.Add(template.tribe);

                if (client_npc.ai_name != null && /*client_npc.ai_name != " " &&*/ !ai_names.Contains(client_npc.ai_name.ToUpper())) ai_names.Add(client_npc.ai_name.ToUpper());

                if (client_npc.race_type != null && /*client_npc.npc_type != " " &&*/ !npctypes.Contains(client_npc.race_type.ToUpper())) npctypes.Add(client_npc.race_type.ToUpper());
				// A nifty progress indicator
				counter++;
				Console.Write("\rWriting npc_templates.xml : {0}%", (counter*100) / total_record_count);
			}

			string outputPath = Path.Combine(root, @".\output");
			if (!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			// build enumeration for xsd
			StringBuilder tribe = new StringBuilder();
			foreach (var name in tribes) {
				tribe.Append("<xs:enumeration value=\"" + name + "\"/>" + Environment.NewLine);
			}
			// save enumertion for tribes
			using (StreamWriter outfile = new StreamWriter(outputPath + @"\TribesFromNpcs.txt")) {
				outfile.Write(tribe);
			}


            // build enumeration for xsd
            StringBuilder ai_dump = new StringBuilder();
            foreach (var name in ai_names)
            {
                ai_dump.Append("<xs:enumeration value=\"" + name + "\"/>" + Environment.NewLine);
            }
            // save enumertion for tribes
            using (StreamWriter outfile = new StreamWriter(outputPath + @"\Ai_dump FromNpcs.txt"))
            {
                outfile.Write(ai_dump);
            }

            // build enumeration for xsd
            StringBuilder npctypes_dump = new StringBuilder();
            foreach (var name in npctypes)
            {
                npctypes_dump.Append("<xs:enumeration value=\"" + name + "\"/>" + Environment.NewLine);
            }
            // save enumertion for tribes
            using (StreamWriter outfile = new StreamWriter(outputPath + @"\Npctypes_dump FromNpcs.txt"))
            {
                outfile.Write(npctypes_dump);
            }

			var settings = new XmlWriterSettings() {
				CheckCharacters = false,
				CloseOutput = false,
				Indent = true,
				IndentChars = "\t",
				NewLineChars = "\n",
				Encoding = new UTF8Encoding(false)
			};

			try {
				using (var fs = new FileStream(Path.Combine(outputPath, "npc_templates.xml"),
										 FileMode.Create, FileAccess.Write))
				using (var writer = XmlWriter.Create(fs, settings)) {
					XmlSerializer ser = new XmlSerializer(typeof(NpcTemplates));
					ser.Serialize(writer, outputFile);
				}
			}
			catch (Exception ex) {
				Debug.Print(ex.ToString());
			}
		}

		static int getNpcEquipmentItemId(string desc) {
			// Equipment Group is Valid but NPC is not wearing armor on this appendage
			if (desc == null) return -1;

			Item item = Utility.ItemIndex.GetItem(desc);
			return item != null ? item.id : -1;
		}

		static NpcType getNpcTypeFromCursorType(string cursor_type, string npc_function_type, string name) {
			if (name.ToUpper().Contains("TELEPORTER")) return NpcType.PORTAL;
			if (name.ToUpper().Contains("GROUPGATE")) return NpcType.PORTAL;
			if (name.ToUpper().Contains("DRAGONPORTAL")) return NpcType.PORTAL;
			if (name.ToUpper().Contains("_ARTIFACT_")) return NpcType.ARTIFACT;

			switch (name.ToUpper()) {
				case "BINDING_STONE": return NpcType.RESURRECT;
				case "TEST_RESURRECT": return NpcType.RESURRECT;

			}

			if (npc_function_type.ToUpper() != "NONE") {
				switch (npc_function_type.ToUpper()) {
					case "POSTBOX": return NpcType.POSTBOX;
					case "BINDSTONE": return NpcType.RESURRECT;
				}
			}

			switch (cursor_type.ToUpper()) {
				case "NONE": return NpcType.NON_ATTACKABLE;
				case "TALK": return NpcType.NON_ATTACKABLE;
				case "TRADE": return NpcType.NON_ATTACKABLE;
				case "ACTION": return NpcType.USEITEM;
				case "ATTACK": return NpcType.ATTACKABLE;
				default: return NpcType.NON_ATTACKABLE;
			}
		}

		public static string getAiHandlerFromString(Npc client_npc) {

            if (client_npc.tribe != null && client_npc.tribe.ToUpper().Contains("AGGRESSIVE")) return "aggressive";
            
			// Try to determine ai from client npc function field
			// Very Reliable
			if (client_npc.npc_function_type != NpcFunction.None) {
				if (client_npc.npc_function_type == NpcFunction.Teleport) return "general";
				if (client_npc.npc_function_type == NpcFunction.Merchant) return "general";
				if (client_npc.npc_function_type == NpcFunction.Warehouse) return "general";
				if (client_npc.npc_function_type == NpcFunction.Vendor) return "general";
				if (client_npc.npc_function_type == NpcFunction.Bindstone) return "resurrect";
				if (client_npc.npc_function_type == NpcFunction.Postbox) return "postbox";
			}

			// try to determine gameserver ai from client ai_name
			// Somewhat reliable
			if (client_npc.ai_name != null) {
				if (client_npc.ai_name.ToLower().Contains("skillarea")) return "skillarea";
				if (client_npc.ai_name.ToLower().Contains("trap")) return "trap";
				if (client_npc.ai_name.ToLower().Contains("merchant")) return "general";
				if (client_npc.ai_name.ToLower().StartsWith("d2_")) return "aggressive";
                if (client_npc.ai_name.ToLower().StartsWith("nd2_")) return "aggressive";
				// Very Reliable
				switch (client_npc.ai_name.ToLower()) {
					case "summonhoming": return "homing";
					case "summontotem": return "servant";
					case "summoned": return "dummy";
					case "groupgate": return "useitem";
				}
			}

			// try to determine ai from dir
			// Questionable
			if (client_npc.dir != null) {
				if (client_npc.dir.ToLower().Contains("groupgate")) return "groupgate";
				if (client_npc.dir.ToLower().Contains("soulhealer")) return "general";
			}

			// see if general type npc and assign general ai after other check fail
			// Unreliable
			if (client_npc.npc_type != null) {
				if (client_npc.npc_type.ToLower() == "general") return "general";
			}

			// Last resort, return non
            return "general";
		}

		public static string getNpcNameFromClientStrings(string desc) {
			string name = Utility.StringIndex.GetString(desc);

			if (name == desc) name = Utility.StringIndex.GetString(desc);

			return name;
		}

		public static int getNpcNameIdFromClientStrings(string desc) {
			int nameid = Utility.StringIndex[desc];

			if (nameid == -1) nameid = Utility.StringIndex[desc];

			return nameid;
		}

		public static T StringToEnum<T>(string name) {
			return (T)Enum.Parse(typeof(T), name);
		}
	}
}
