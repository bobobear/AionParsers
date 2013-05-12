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

namespace Jamie.NpcTeleporter {
	class Program {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

		static void Main(string[] args) {
			Utility.WriteExeDetails();

			Console.WriteLine("Loading npc strings ...");
			Utility.LoadNpcStrings(root);

			Console.WriteLine("Loading monster strings ...");
			Utility.LoadMonsterStrings(root);

			Console.WriteLine("Loading level strings ...");
			Utility.LoadLevelStrings(root);

			Console.WriteLine("Loading client airline ...");
			Utility.LoadClientAirline(root);

			Console.WriteLine("Loading client airports ...");
			Utility.LoadClientAirports(root);

			Console.WriteLine("Loading client npcs ...");
			Utility.LoadClientNpcs(root);

			var teleporter_templates = Utility.AirlineIndex.getClientAirlines();

			NpcTeleporterTemplates outputFile = new NpcTeleporterTemplates();
			outputFile.teleporter_templates = new List<TeleporterTemplate>();

			foreach (var teleporter in teleporter_templates) {
				var template = new TeleporterTemplate();

				// Some client_airline's do not have teleporter data, Skip
				if (teleporter.airline_list == null) continue;

				// Get Npc object that handles teleporter
				Npc npc = Utility.ClientNpcIndex.getTeleportNpcFromString(teleporter.name);
				// Some Special In zone teleporters in main cities do not have airport crossreference
				if (npc == null) npc = getSpecialNpc(teleporter.name);

				template.npc_id = npc != null ? npc.id : -1;
				template.name = npc != null ? Utility.NpcStringIndex.GetString(npc.desc) != npc.desc ? Utility.NpcStringIndex.GetString(npc.desc) : Utility.MonsterStringIndex.GetString(npc.desc) : "Missing Npc Data";
				template.teleportId = teleporter.id;

				// Create New Element and List of Teleport Locations
				template.locations = new Locations();
				template.locations.telelocation = new List<TeleLocation>();

				// Add Data from Client for Each Location
				foreach (var location in teleporter.airline_list.getAirlineData()) {
					TeleLocation telelocation = new TeleLocation();
					telelocation.loc_id = Utility.AirportIndex[location.airport_name];
					telelocation.description = Utility.LevelStringIndex.GetString("STR_" + location.airport_name);
					telelocation.name = location.airport_name; // Ignored Field used as reference when parsing Teleport Locations
					telelocation.price = location.fee;
					telelocation.pricePvp = location.pvpon_fee;
					telelocation.required_quest = location.required_quest;
					telelocation.type = location.flight_path_group_id == 0 ? TeleporterType.REGULAR : TeleporterType.FLIGHT;
					if (telelocation.type == TeleporterType.FLIGHT) {
						telelocation.teleportid = (location.flight_path_group_id * 1000) + 1;
					}


					template.locations.telelocation.Add(telelocation);
				}

				outputFile.teleporter_templates.Add(template);
			}

			Console.WriteLine("Writing npc_teleporter.xml ...");

			// Reorder Templates
			outputFile.teleporter_templates = outputFile.teleporter_templates.OrderBy(n => n.teleportId).ToList<TeleporterTemplate>();

			string outputPath = Path.Combine(root, @".\output");
			if (!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			var settings = new XmlWriterSettings() {
				CheckCharacters = false,
				CloseOutput = false,
				Indent = true,
				IndentChars = "\t",
				NewLineChars = "\n",
				Encoding = new UTF8Encoding(false)
			};

			try {
				using (var fs = new FileStream(Path.Combine(outputPath, "npc_teleporter.xml"),
										 FileMode.Create, FileAccess.Write))
				using (var writer = XmlWriter.Create(fs, settings)) {
					XmlSerializer ser = new XmlSerializer(typeof(NpcTeleporterTemplates));
					ser.Serialize(writer, outputFile);
				}
			}
			catch (Exception ex) {
				Debug.Print(ex.ToString());
			}

			Console.WriteLine("Writing npc_teleporter.xml Done!");

			Console.WriteLine("Parse Teleport Location Data? (y/n) ");
			var answer = Console.ReadLine();
			if (answer.Trim() == "y") {
				TeleportParse teleportParser = new TeleportParse();
				teleportParser.parseTeleportLocations(outputFile);
			}
		}

		static public Npc getSpecialNpc(string name) {
			switch (name) {
				case "LC1_Airport_ZONE_Entrance": return Utility.ClientNpcIndex[730265];
				case "LC2_Airport_ZONE_Entrance": return Utility.ClientNpcIndex[730266];
				case "DC1_Airport_ZONE_Entrance": return Utility.ClientNpcIndex[730268];
				case "DC2_Airport_ZONE_Entrance": return Utility.ClientNpcIndex[730269];
			}
			return null;
		}
	}
}
