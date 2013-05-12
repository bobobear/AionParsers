using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Diagnostics;
using Jamie.ParserBase;
using Jamie.Npcs;
using GenericParsing;

namespace Jamie.Spawns {
	class Program {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		static string sfiles = @"R:\SVN\Elite Game Servers 3.0\egs_al3\AL-Game\data\static_data\spawns\Npcs\";
		static string zfiles = @"R:\SVN\Elite Game Servers 3.0\DataTools\DataParsers\bin\Debug\output\zfiles\";

		static void Main(string[] args) {

			Utility.WriteExeDetails();

			// Lets give some meaningful names to npcs
			Console.WriteLine("Loading npc strings ...");
			Utility.LoadNpcStrings(root);

			Console.WriteLine("Loading monster strings ...");
			Utility.LoadMonsterStrings(root);

			Console.WriteLine("Loading level strings ...");
			Utility.LoadLevelStrings(root);

			Console.WriteLine("Loading client npcs ...");
			Utility.LoadClientNpcs(root);

			Dictionary<string, string> mapsToProcess = new Dictionary<string, string>();
			Dictionary<string, string> spawnfiles = new Dictionary<string, string>();

			Console.WriteLine("Building index of zfiles to process...");

			// Build index of zfix files to process
			string[] filePaths = Directory.GetFiles(zfiles);
			foreach (string file in filePaths) {
				string[] split = file.Remove(0,zfiles.Length).Split('_');
				if(ClientLevelMap.isMap(split[0])){
					if(!mapsToProcess.ContainsKey(split[0])){
						mapsToProcess.Add(split[0], file);
					}
				}
			}

			Console.WriteLine("Building index of spawnfiles...");

			// Build index to spawnfiles by mapid
			filePaths = Directory.GetFiles(sfiles);
			foreach (string file in filePaths) {
				string[] split = file.Remove(0,sfiles.Length).Split('_');
				if(!spawnfiles.ContainsKey(split[0]) && mapsToProcess.ContainsKey(split[0])){
					spawnfiles.Add(file, split[0]);
				}
			}

			foreach (var mapId in mapsToProcess) {
				// load zfile to dataset
				DataSet data = loadTxtToDataset(mapId.Value, "spawns");

				// modifier for filenames
				string modifier = "";

				// loop through matching spawn files
				foreach(var s in spawnfiles){
					if (s.Value == mapId.Key) {

						int iMapId = 0;
						Int32.TryParse(s.Value, out iMapId);

						// load original spawn file
						Utility.LoadOriginalSpawnsFile(s.Key);


						modifier = modifier + "_" + Utility.LevelStringIndex.GetString("STR_ZONE_NAME_" + ClientLevelMap.getStringFromId(iMapId));
						if(s.Key.Contains("Monster")) modifier = modifier + "_Monster";
						if(s.Key.Contains("Gathering")) modifier = modifier + "_Gathering";
						
						// Copy original spawn file to new outputfile
						SpawnsFile outputFile = Utility.OriginalSpawnsFile;

						outputFile.spawn_map.map_id = iMapId;

						// Lets find spawns that need updating
						foreach (var group in outputFile.spawn_map.Spawns) {

							// Some overrides
							// TODO: Figure out why the spawn of npcid's with no npc template in client data? Special NPC, Static Object, Assembled npc?
							if (group.npc_id == 1000000 || group.npc_id == 1000001) continue;

							string desc = Utility.ClientNpcIndex[group.npc_id].desc;
							group.name = Utility.NpcStringIndex.GetString(desc);
							if (group.name == desc) group.name = Utility.MonsterStringIndex.GetString(desc);

							foreach (var spawn in group.@object) {
								DataRow[] foundRows;
								foundRows = data.Tables["spawns"].Select("x='" + spawn.x + "' AND y='" + spawn.y + "'");

								// Foudn spawn that needs updating
								if (foundRows.Count<DataRow>() > 0) {
									spawn.z = Convert.ToDecimal(foundRows[0][3].ToString());
								}
							}
						}

						outputFile.spawn_map.Spawns = outputFile.spawn_map.Spawns.OrderBy(sp => sp.name).ToArray();

						string outputPath = Path.Combine(root, @"output");
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
							using (var fs = new FileStream(Path.Combine(outputPath,
													 String.Format("{0}.xml",
													 mapId.Key + modifier)),
													 FileMode.Create, FileAccess.Write))
							using (var writer = XmlWriter.Create(fs, settings)) {
								XmlSerializer ser = new XmlSerializer(typeof(SpawnsFile));
								ser.Serialize(writer, outputFile);
							}
						}
						catch (Exception ex) {
							Debug.Print(ex.ToString());
						}

						modifier = "";
						// Done Udpating Spawn file, next >>
					}
				}
			}

			// Done
		}

		static public DataSet loadTxtToDataset(string path, string name) {
			// Setup the DataSet
			DataSet ds = new DataSet();

			// Use Generic Txt Parser to load csv to dataset
			using (GenericParserAdapter parser = new GenericParserAdapter()) {
				parser.SetDataSource(path);

				parser.ColumnDelimiter = ',';
				parser.FirstRowHasHeader = true;
				parser.TextQualifier = '\"';

				ds = parser.GetDataSet();
			}

			ds.Tables[0].TableName = name;

			return ds;

		}
	}
}
