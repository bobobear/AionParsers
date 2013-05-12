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

namespace Jamie.World {
	class ZoneParse {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		// Absolute path to \Aion Client\Levels
		static string path = @"R:\VShell\SFTP\3.0.0.0_Unpacked\Levels\";
		static string world_path = @"R:\VShell\SFTP\3.0.0.0_Unpacked\Data\world\";
		private int dupecounter = 2;
		private List<string> zoneList = new List<string>();
		private List<string> zoneDescription = new List<string>();

		// Build Enumeration for zones.xsd ;)
		StringBuilder zonesEnumeration = new StringBuilder();

		public void parseZones(WorldMapsTemplate world_maps) {
			Console.Write("Starting Zone Parse ...");

			Console.WriteLine("Loading level strings...");
			Utility.LoadLevelStrings(root);

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

			ZoneMapsTemplate outputFile = new ZoneMapsTemplate();
			outputFile.zones = new List<Zone>();

			// Create IEnumerable<Map> of passed templates
			var worlds = world_maps.world_maps.Where(n => n.id != 0);

			// Parse zones for each template
			foreach (var world in worlds) {

				// Other map / zone data must me loaded from individual level / mission files.
				try {
					Console.WriteLine("Loading Zone Data: " + world.id.ToString());
					Utility.LoadZoneDataFile(world_path + @"client_world_" + world.map.ToLower() + @".xml");

					// Get List of zones in data file
					var subzones = Utility.ZoneDataFile.subzones != null ? Utility.ZoneDataFile.subzones.subzone.Where(n => n.points_info.points.data.Count() > 0) : new List<SubZone>();
					var iuzones = Utility.ZoneDataFile.attributes != null ? Utility.ZoneDataFile.attributes.item_use_area.Where(n => n.points_info.points.data.Count() > 0) : new List<SubZone>();
					var artifactzones = Utility.ZoneDataFile.artifact_result_areas != null ? Utility.ZoneDataFile.artifact_result_areas.artifact_result_area.Where(n => n.points_info.points.data.Count() > 0) : new List<SubZone>();
					var fortzones = Utility.ZoneDataFile.abyss_castle_areas != null ? Utility.ZoneDataFile.abyss_castle_areas.abyss_castle_area.Where(n => n.points_info.points.data.Count() > 0) : new List<SubZone>();
					var limitzones = Utility.ZoneDataFile.limitareas != null ? Utility.ZoneDataFile.limitareas.limitarea.Where(n => n.points_info.points.data.Count() > 0) : new List<SubZone>();
					var pvpzones = Utility.ZoneDataFile.attributes != null ? Utility.ZoneDataFile.attributes.pvpzone.Where(n => n.points_info.points.data.Count() > 0) : new List<SubZone>();

					foreach (var subzone in subzones) {
						outputFile.zones.Add(getZoneTemplate(subzone, world, "SUB"));
					}

					foreach (var subzone in iuzones) {
						outputFile.zones.Add(getZoneTemplate(subzone, world, "ITEM_USE"));
					}

					foreach (var subzone in artifactzones) {
						outputFile.zones.Add(getZoneTemplate(subzone, world, "ARTIFACT"));
					}

					foreach (var subzone in fortzones) {
						outputFile.zones.Add(getZoneTemplate(subzone, world, "FORT"));
					}

					foreach (var subzone in limitzones) {
						outputFile.zones.Add(getZoneTemplate(subzone, world, "LIMIT"));
					}

					foreach (var subzone in pvpzones) {
						outputFile.zones.Add(getZoneTemplate(subzone, world, "PVP"));
					}

					// Get fLy zones from level data
					List<Zone> flyzones = getFlyZoneTemplates(world);
					if (flyzones.Count > 0) {
						outputFile.zones.AddRange(flyzones);
					}

					using (var fs = new FileStream(Path.Combine(outputPath, @"zones_" + world.id.ToString() + @".xml"), FileMode.Create, FileAccess.Write))
					using (var writer = XmlWriter.Create(fs, settings)) {
						XmlSerializer ser = new XmlSerializer(typeof(ZoneMapsTemplate));
						ser.Serialize(writer, outputFile);
					}

					// Clear zones for next world
					outputFile.zones = new List<Zone>();
				}
				catch (Exception ex) {
					Debug.Print(ex.ToString());
				}
			}

			// build zone enumeration file
			foreach (string zone in zoneList) {
				zonesEnumeration.Append("<xs:enumeration value=\"" + zone.ToUpper() + "\"/>" + Environment.NewLine);
			}

			StringBuilder zoneCrossReference = new StringBuilder();
			foreach (string description in zoneDescription) {
				zoneCrossReference.Append(description + Environment.NewLine);
			}

			// save enumertion for gameserver world type
			using (StreamWriter outfile = new StreamWriter(outputPath + @"\ZoneEnumeration.txt")) {
				outfile.Write(zonesEnumeration);
			}

			// save description cross reference
			using (StreamWriter outfile = new StreamWriter(outputPath + @"\ZoneDescriptionReference.txt")) {
				outfile.Write(zoneCrossReference);
			}
		}

		public Zone getZoneTemplate(SubZone subzone, Map world, string zone_type) {
			var template = new Zone();

			template.mapid = world.id;
			template.priority = subzone.priority;
			template.zone_type = zone_type;
			template.area_type = subzone.points_info.type.ToUpper();

			string desc = Utility.LevelStringIndex.GetString(subzone.desc);
			if (desc.StartsWith("_") || desc == "") desc = subzone.name + desc;
			if (zone_type == "ITEM_USE") {
				template.name = getFormattedZoneName(desc, 0);
			}
			else {
				template.name = getFormattedZoneName(desc, world.id);
			}
			//template.description = (Utility.LevelStringIndex.GetString(subzone.desc) + "_" + world.id.ToString()).ToUpper();
			zoneDescription.Add(template.name + " // " + subzone.name + " : " + world.id.ToString());

			if (subzone.norecall != null) template.norecall = subzone.norecall.ToUpper() == "TRUE" ? true : false;
			if (subzone.noride != null) template.noride = subzone.noride.ToUpper() == "TRUE" ? true : false;


			if (zone_type == "ARTIFACT") {

				// make list of siege artifacts that affect this area
				var artifact_infos = Utility.ZoneDataFile.artifact_infos != null ? Utility.ZoneDataFile.artifact_infos.artifact_info.Where(n => n != null) : new List<ArtifactInfo>();
				List<int> siegeId = new List<int>();
				foreach (var artifact in artifact_infos) {
					if (artifact.artifact_result_area1 == subzone.name) {
						siegeId.Add(Convert.ToInt32(artifact.artifact_name.Split('_')[1].Substring(0, 4)));
					}

				}
				if (siegeId.Count > 0) template.siege_id = siegeId;
			}
			else {
				template.siege_id = null;
			}
			if (zone_type == "FORT") {
				List<int> abyssid = new List<int>();
				abyssid.Add(subzone.abyss_id);
				template.siege_id = abyssid;
			}

			template.points = new Points();
			template.points.top = subzone.points_info.top;
			template.points.bottom = subzone.points_info.bottom;
			template.points.point = new List<Point>();

			IEnumerable points = subzone.points_info.points.data as IEnumerable;
			foreach (ParserBase.Data point in points) {
				Point newpoint = new Point();
				newpoint.x = Math.Round(point.x, 4);
				newpoint.y = Math.Round(point.y, 4);
				template.points.point.Add(newpoint);
			}

			return template;
		}

		public List<Zone> getFlyZoneTemplates(Map world) {
			List<Zone> templates = new List<Zone>();
			Zone template = new Zone();

			// Other map / zone data must me loaded from individual level / mission files.
			try {
				// If no level data exists, most likely test level thats missing data, ignore
				// without level data the level can not be properly loaded in retail client
				if (!File.Exists(path + world.map.ToLower() + @"\leveldata.xml")) {
					return templates;
				}

				Console.WriteLine("Loading Level Mission Data: " + world.map);
				Utility.LoadMissionFile(path + world.map.ToLower() + @"\mission_mission0.xml");

				// If no flying templates return the empty list
				if (Utility.MissionFile.CommonShapeList.flying_zones.flying_zone == null) {
					return templates;
				}

				IEnumerable flyzones = Utility.MissionFile.CommonShapeList.flying_zones.flying_zone.Where(n => n != null);

				foreach (MissionCommonShapeListFlying_zonesFlying_zone_zone flyzone in flyzones) {
					template.mapid = world.id;
					template.zone_type = "FLY";

					template.name = getFormattedZoneName(flyzone.name, 0);

					template.description = stripCharacters(flyzone.name);
					zoneDescription.Add(template.name + " // " + world.id.ToString() + " : " + template.description);

					template.area_type = flyzone.points_info.type.ToUpper();

					template.points = new Points();
					template.points.top = flyzone.points_info.top;
					template.points.bottom = flyzone.points_info.bottom;
					template.points.point = new List<Point>();

					var points = flyzone.points_info.points.data.Where(n => n != null);

					foreach (points_infoPointsData point in points) {
						Point newpoint = new Point();
						newpoint.x = Math.Round(point.x, 4);
						newpoint.y = Math.Round(point.y, 4);
						template.points.point.Add(newpoint);
					}

					templates.Add(template);
				}
			}
			catch (Exception ex) {
				Debug.Print("Error Processing Level Data File: '{0}'", ex.InnerException);
			}

			return templates;
		}

		public string getFormattedZoneName(string name, int mapid) {
			// some trickery to avoid duplicate zone names and avoid names that start with a number
			if(Char.IsNumber(name[0])) name = "Z_" + name;

			string zonename = name.Replace(' ', '_').Replace("\'", String.Empty).Replace(".", "_").Replace(",", "_").Replace("(", "_").Replace(")", String.Empty).Replace("-", "_");
			string tempzonename = mapid != 0 ? zonename + "_" + mapid.ToString() : zonename;
			if (!zoneList.Contains(tempzonename)) {
				zonename = mapid != 0 ? zonename + "_" + mapid.ToString() : zonename;
				zoneList.Add(zonename);
			}
			else {
				string newzonename;
				string tempnewzonename;
				do {
					newzonename = zonename + "_" + dupecounter.ToString();
					tempnewzonename = mapid != 0 ? newzonename + "_" + mapid.ToString() : newzonename;
					if (!zoneList.Contains(tempnewzonename)) {
						newzonename = mapid != 0 ? newzonename + "_" + mapid.ToString() : newzonename;
						zoneList.Add(newzonename);
						zonename = newzonename;
					}
					else {
						dupecounter++;
					}
				} while (!zoneList.Contains(newzonename));
			}
			return zonename.ToUpper();
		}

		public string stripCharacters(string s) {
			return s.Replace(' ', '_').Replace("\'", String.Empty).Replace(".", "_").Replace(",", "_").Replace("(", "_").Replace(")", String.Empty);
		}
	}
}
