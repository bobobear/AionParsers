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

namespace Jamie.SkillTree
{
    class Program
    {
        static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        static void Main(string[] args) {

		  Utility.WriteExeDetails();

            Console.WriteLine("Loading skill strings...");
            Utility.LoadSkillStrings(root);

		  Console.WriteLine("Loading skills...");
            Utility.LoadSkills(root);

		  Console.WriteLine("Loading skill learns...");
		  Utility.LoadSkillLearns(root);

		  var skill_learns = Utility.SkillLearnIndex.SkillList.Where(n => n.id != 0);

		  SkillTreeTemplates outputFile = new SkillTreeTemplates();
            outputFile.skills = new List<SkillTreeTemplate>();

		  foreach (var skill in skill_learns) {
			  var template = new SkillTreeTemplate();

			  template.minLevel = skill.pc_level;
			  template.race = skill.skillRace.ToString();
			  template.stigma = skill.stigma_display > 0 ? true : false;
			  template.autolearn = (bool) skill.autolearn;
			  template.name = Utility.SkillStringIndex.GetString(Utility.SkillIndex[skill.skill].desc);
			  template.skillLevel = skill.skill_level;
			  template.skillId = Utility.SkillIndex[skill.skill].id;
			  template.classId = skill.classId;

			  outputFile.skills.Add(template);
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
				  using (var fs = new FileStream(Path.Combine(outputPath, "skill_tree.xml"), FileMode.Create, FileAccess.Write))
				  using (var writer = XmlWriter.Create(fs, settings)) {
					  XmlSerializer ser = new XmlSerializer(typeof(SkillTreeTemplates));
					  ser.Serialize(writer, outputFile);
				  }
			  }
			  catch (Exception ex) {
				  Debug.Print(ex.ToString());
			  }
        }
    }
}
