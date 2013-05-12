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

namespace Jamie.World {
	class Program {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		// Absolute path to \Aion Client\Levels
		static string path = @"R:\VShell\SFTP\3.0.0.0_Unpacked\Levels\";

		static void Main(string[] args) {

			Utility.WriteExeDetails();
			Console.WriteLine("Loading level strings...");
			Utility.LoadLevelStrings(root);

			Console.WriteLine("Loading world ids...");
			Utility.LoadWorldId(root);

			Console.WriteLine("Loading zone maps...");
			Utility.LoadZoneMaps(root);

			Console.WriteLine("Loading Instance Cooltimes...");
			Utility.LoadInstanceCooltimeDataFile(root);

			var worlds = Utility.WorldIdIndex.worlds.Where(n => n.id != 0);

			WorldMapsTemplate outputFile = new WorldMapsTemplate();
			outputFile.world_maps = new List<Map>();

			// Build Enumeration output for WorldMapType.java in gameserver ;)
			StringBuilder worldMapType = new StringBuilder();

			// Build Enumeration output for ClientLevelMap.cs
			StringBuilder clientLevelMap = new StringBuilder();

			foreach (var worldmap in worlds) {
				var template = new Map();

				template.id = worldmap.id;

				template.map = worldmap.name;
				clientLevelMap.Append(" {\"" + worldmap.name.ToLower() + "\", " + worldmap.id.ToString() + " }, " + Environment.NewLine);


				template.name = Utility.LevelStringIndex.GetString("STR_ZONE_NAME_" + worldmap.name).Replace("STR_ZONE_NAME_", String.Empty);
				template.twin_count = worldmap.twin_count;
				template.prison = Convert.ToBoolean(worldmap.prison);
				template.except_buff = Convert.ToBoolean(worldmap.except_buff);
				template.instance = Utility.InstanceCooltimesIndex[worldmap.name] > 0 ? true : false;

				// Load Zone Map and get level size
				ZoneMap zone = Utility.ZoneMapIndex.getZoneMap(worldmap.id);
				template.world_type = zone != null ? zone.getWorldTypeFromString() : WorldType.NONE;
				template.world_size = zone != null ? zone.world_width : 3072;
				if (template.world_size == 0) template.world_size = 3072; // additional check to overide any defaults in client xml
				//if (zone != null) template.world_size = zone.world_width;

				// Other map / zone data must me loaded from individual level / mission files.
				try {
					// If no level data exists, most likely test level thats missing data, ignore
					// without level data the level can not be properly loaded in retail client
					if (!File.Exists(path + worldmap.name.ToLower() + @"\leveldata.xml")) {
						continue;
					}

					Console.WriteLine("Loading Level Data: " + worldmap.name);
					Utility.LoadLevelDataFile(path + worldmap.name.ToLower() + @"\leveldata.xml");

					template.water_level = (int) Utility.LevelDataFile.level_info.WaterLevel;

					Mission mission = Utility.LevelDataFile.Missions.getMission("Mission0");
					if (mission != null) {
						template.fly = mission.LevelOption.Fly.Fly_Whole_Level == 2 ? true : false;
					}

				}
				catch (Exception ex) {
					Debug.Print("Error Processing Level Data File: '{0}'", ex.InnerException);
				}

				// build enumeration for template
				worldMapType.Append(template.map + @"(" + template.id.ToString() + @"), // " + template.name + Environment.NewLine);

				outputFile.world_maps.Add(template);
			}

				string outputPath = Path.Combine(root, @".\output");
				if (!Directory.Exists(outputPath))
					Directory.CreateDirectory(outputPath);

				// save enumertion for gameserver world type
				using (StreamWriter outfile = new StreamWriter(outputPath + @"\WorldMapType.txt")) {
					outfile.Write(worldMapType);
				}

				// save enumertion for parser client level mapping
				using (StreamWriter outfile = new StreamWriter(outputPath + @"\ClientLevelMap.txt")) {
					outfile.Write(clientLevelMap);
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
					using (var fs = new FileStream(Path.Combine(outputPath, "world_maps.xml"), FileMode.Create, FileAccess.Write))
					using (var writer = XmlWriter.Create(fs, settings)) {
						XmlSerializer ser = new XmlSerializer(typeof(WorldMapsTemplate));
						ser.Serialize(writer, outputFile);
					}
				}
				catch (Exception ex) {
					Debug.Print(ex.ToString());
				}

				Console.Write("Parse Zone Data? (y/n) ");
				var answer = Console.ReadLine();
				if (answer.Trim() == "y") {
					ZoneParse zoneParser = new ZoneParse();
					zoneParser.parseZones(outputFile);
				}
		}
	}
}
