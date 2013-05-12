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

namespace Jamie.FlyPath {
	class Program {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

		static void Main(string[] args) {
			Utility.WriteExeDetails();

			Console.WriteLine("Loading world ids...");
			Utility.LoadWorldId(root);

			Console.WriteLine("Loading client flypaths ...");
			Utility.LoadClientFlyPaths(root);

			var locations = Utility.FlyPathIndex.getPathGroups();

			FlyPathTemplates outputFile = new FlyPathTemplates();
			outputFile.flypath_locations = new List<FlyPathLocation>();

			foreach (var location in locations) {
				var template = new FlyPathLocation();

				template.id = location.group_id;
				template.sx = location.start.x;
				template.sy = location.start.y;
				template.sz = location.start.z;
				template.sworld = Utility.WorldIdIndex[location.start.world];
				template.ex = location.end.x;
				template.ey = location.end.y;
				template.ez = location.end.z;
				template.eworld = Utility.WorldIdIndex[location.end.world];
				template.time = Math.Round(location.fly_time, 1);

				outputFile.flypath_locations.Add(template);
			}

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
				using (var fs = new FileStream(Path.Combine(outputPath, "flypath_template.xml"),
										 FileMode.Create, FileAccess.Write))
				using (var writer = XmlWriter.Create(fs, settings)) {
					XmlSerializer ser = new XmlSerializer(typeof(FlyPathTemplates));
					ser.Serialize(writer, outputFile);
				}
			}
			catch (Exception ex) {
				Debug.Print(ex.ToString());
			}
		}
	}
}
