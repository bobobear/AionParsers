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

namespace Jamie.Tribes {
	class Program {
		static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

		static void Main(string[] args) {

			Utility.WriteExeDetails();

			Console.WriteLine("Loading Tribes ...");
			Utility.LoadTribes(root);

			var tribes = Utility.TribesIndex.client_tribes.Where(n => n.name != null);

			TribeRelationsTemplate outputFile = new TribeRelationsTemplate();
			outputFile.tribes = new List<TribeTemplate>();

			// Lets build a tribe reference file for the xsd
			List<string> tribe_names = new List<string>();

			foreach (var tribe in tribes) {
				var template = new TribeTemplate();

				template.name = tribe.name.ToUpper();
				if (tribe.base_tribe != null) template.base_name = tribe.base_tribe.text.ToUpper();
				if (tribe.aggressive != null) { template.aggro = new TribeType(); template.aggro.text = tribe.aggressive.text.ToUpper().Replace(',', ' '); }
				if (tribe.friendly != null) { template.friend = new TribeType(); template.friend.text = tribe.friendly.text.ToUpper().Replace(',', ' '); }
				if (tribe.hostile != null) { template.hostile = new TribeType(); template.hostile.text = tribe.hostile.text.ToUpper().Replace(',', ' '); }
				if (tribe.neutral != null) { template.neutral = new TribeType(); template.neutral.text = tribe.neutral.text.ToUpper().Replace(',', ' '); }
				if (tribe.support != null) { template.support = new TribeType(); template.support.text = tribe.support.text.ToUpper().Replace(',', ' '); }
				if (tribe.none != null) { template.none = new TribeType(); template.none.text = tribe.none.text.ToUpper().Replace(',', ' '); }

				// Build our list of tribes for xsd
				if (template.name != null && !tribe_names.Contains(template.name)) tribe_names.Add(template.name);

				outputFile.tribes.Add(template);
			}

			outputFile.tribes = outputFile.tribes.OrderBy(tribe => tribe.name).ToList<TribeTemplate>();

			string outputPath = Path.Combine(root, @".\output");
			if (!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			// build enumeration for xsd
			StringBuilder tribe_output = new StringBuilder();
			tribe_names.Sort();
			foreach (var name in tribe_names) {
				tribe_output.Append(/*<xs:enumeration value=\"" + */name + "," + Environment.NewLine);
			}
			// save enumertion for tribes
			using (StreamWriter outfile = new StreamWriter(outputPath + @"\TribesEnumeration.txt")) {
				outfile.Write(tribe_output);
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
				using (var fs = new FileStream(Path.Combine(outputPath, "tribe_relations.xml"),
										 FileMode.Create, FileAccess.Write))
				using (var writer = XmlWriter.Create(fs, settings)) {
					XmlSerializer ser = new XmlSerializer(typeof(TribeRelationsTemplate));
					ser.Serialize(writer, outputFile);
				}
			}
			catch (Exception ex) {
				Debug.Print(ex.ToString());
			}
		}
	}
}
