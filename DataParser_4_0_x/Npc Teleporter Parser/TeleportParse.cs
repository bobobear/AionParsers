using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Jamie.ParserBase;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using GenericParsing;
using Jamie.Npcs;

namespace Jamie.NpcTeleporter {
	class TeleportParse {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		// Absolute path to \Aion Client\Levels
		static string path = @"R:\VShell\SFTP\3.0.0.0_Unpacked\Levels\";
		static string current_mission_map = "none";

		public void parseTeleportLocations(NpcTeleporterTemplates npc_teleporters) {
			Console.WriteLine("Starting Teleport Parse ...");

			Console.WriteLine("Loading level strings ...");
			Utility.LoadLevelStrings(root);

			Console.WriteLine("Loading original teleport_location.xml ...");
			Utility.LoadOriginalTeleporterLocationTemplate(root);

			//Unused for now
			//Console.WriteLine("Loading source_sphere.csv ...");
			//DataSet source_sphere = loadExcelData();

			TeleporterLocationTemplates outputFile = new TeleporterLocationTemplates();
			outputFile.teleloc_templates = new List<TeleporterLocationTemplate>();

			// Create IEnumerable<NpcTeleporterTemplate> of passed templates
			var teleporters = npc_teleporters.teleporter_templates.Where(n => n.teleportId != 0);

			// Create List of loca_id that have already been processed
			List<int> processed = new List<int>();

			// Parse location for each template
			foreach (var teleport in teleporters) {
				var locations = teleport.locations.telelocation.Where(n => n.loc_id != 0);

				foreach (var location in locations) {
					TeleporterLocationTemplate template = new TeleporterLocationTemplate();

					// Skip if location already added
					if (processed.Contains(location.loc_id)) continue;

					// Get Old Template if it exists
					TeleporterLocationTemplate original_template = Utility.OriginalTeleporterLocationTemplate[location.loc_id];

					// Use original info if it exists in teleport_location.xml until more definitive info on x,y,z
					if (original_template != null) {
						template.loc_id = original_template.loc_id;
						template.mapid = original_template.mapid;
						template.name = original_template.name;
						template.posX = original_template.posX;
						template.posY = original_template.posY;
						template.posZ = original_template.posZ;
						template.heading = original_template.heading;
						template.type = location.type == TeleporterType.REGULAR ? "REGULAR" : "FLIGHT";
					}
					else {
						template.loc_id = location.loc_id;
						//template.mapid = getMapIdFromString(Utility.AirlineIndex[teleport.teleportId]);
						template.mapid = getMapIdFromString(Utility.AirportIndex[location.loc_id]);
						template.name = location.description;
						template.type = location.type == TeleporterType.REGULAR ? "REGULAR" : "FLIGHT";

						// Some trickery to get spawn point from mission file
						if (template.type != "FLIGHT") {
							string[] split = location.name.Split('_');

							// Special cases where map name is shortened
							if (split[0].StartsWith("HLFP")) split[0] = "HLFP";
							if (split[0].StartsWith("HDFP")) split[0] = "HDFP";
							switch (split[0]) {
								case "HLFP": split[0] = "housing_lf_personal"; break;
								case "HDFP": split[0] = "housing_df_personal"; break;
							}

							Point point = getPointFromLevelData(split[0], location.name);
							
							template.posX = point.x;
							template.posY = point.y;
							template.posZ = point.z;
							template.heading = point.heading;

							if (template.posX == 1) {
								template.posX = 1;
							}

						}
					}

					if (!processed.Contains(template.loc_id) && template.loc_id != 0) {
						processed.Add(template.loc_id);
						outputFile.teleloc_templates.Add(template);
					}
				}
			}

			// Reorder Templates
			outputFile.teleloc_templates = outputFile.teleloc_templates.OrderBy(n => n.loc_id).ToList<TeleporterLocationTemplate>();
	
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

			using (var fs = new FileStream(Path.Combine(outputPath, "teleport_location.xml"), FileMode.Create, FileAccess.Write))
			using (var writer = XmlWriter.Create(fs, settings)) {
				XmlSerializer ser = new XmlSerializer(typeof(TeleporterLocationTemplates));
				ser.Serialize(writer, outputFile);
			}
		}

		private int getMapIdFromString(string airport_name) {
			string[] split = airport_name.Split('_');

			// Special cases where map name is shortened
			if (split[0].StartsWith("HLFP")) split[0] = "HLFP";
			if (split[0].StartsWith("HDFP")) split[0] = "HDFP";
			switch (split[0]) {
				case "HLFP": split[0] = "housing_lf_personal"; break;
				case "HDFP": split[0] = "housing_df_personal"; break;
			}

			if (ClientLevelMap.mapToId.ContainsKey(split[0])) {
				return ClientLevelMap.mapToId[split[0]];
			}
			else {
				return -1;
			}
		}

		// Unused for Now, could be useful in teh future
		static public DataSet loadExcelData() {
			// Setup the DataSet
			DataSet ds = new DataSet();


			// Use Generic Txt Parser to load source_sphere.csv to a dataset. 
			using (GenericParserAdapter parser = new GenericParserAdapter()) {
				parser.SetDataSource(Path.Combine(root, @".\data\source_sphere.csv"));

				parser.ColumnDelimiter = ',';
				parser.FirstRowHasHeader = true;
				parser.TextQualifier = '\"';

				ds = parser.GetDataSet();
			}

			ds.Tables[0].TableName = "data";

			return ds;

		}

		static public Point getPointFromLevelData(string mapname, string airline_name) {
			// Some overides, Special points
			switch (airline_name) {
				case "HLFPlevel_Airport_Level": return getPoint(1265.7296m, 1837.7058m, 97.3919m, 64);
				case "HDFPlevel_Airport_Level": return getPoint(1067.3567m, 1540.0339m, 97.875m, 113);
			}

			// Lets try to get points from Mission file
			Point p = new Point();
			string file = path + mapname + @"\mission_mission0.xml";

			if(File.Exists(file)){
				if (current_mission_map != mapname) {
					Console.WriteLine("Loading mission file for {0} ...", mapname);
					Utility.LoadMissionFile(path + mapname + @"\mission_mission0.xml");
					current_mission_map = mapname;
				}

				// Try to find a match for our ourline in mission file
				var objects = Utility.MissionFile.Objects.Object.Where(n => n.Name == airline_name);

				foreach (var game_object in objects) {
					if (game_object.Name == airline_name) {
						string pos = game_object.Pos;
						string[] split = pos.Split(',');
						p.x = Convert.ToDecimal(split[0]);
						p.y = Convert.ToDecimal(split[1]);
						p.z = Convert.ToDecimal(split[2]);
						p.heading = 1; // Now where is the heading?
					}
				}
			}

			return p;
		}

		public class Point {
			public decimal x = 1;
			public decimal y = 1;
			public decimal z = 1;
			public int heading = 1;
		}

		static public Point getPoint(decimal x, decimal y, decimal z, int heading) {
			Point point = new Point();
			point.x = x;
			point.y = y;
			point.z = z;
			point.heading = heading;
			return point;
		}
	}
}
