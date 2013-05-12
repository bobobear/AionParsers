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

namespace Jamie.ToyPet
{
    class Program
    {
        static string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        static void Main(string[] args) {

		  Utility.WriteExeDetails();
            Console.WriteLine("Loading item strings...");
            Utility.LoadItemStrings(root);

            Console.WriteLine("Loading toypet strings...");
            Utility.LoadPetStrings(root);

		  Console.WriteLine("Loading items...");
            Utility.LoadItems(root);

		  Console.WriteLine("Loading toypets...");
		  Utility.LoadToyPets(root);

		  Console.WriteLine("Loading toypet warehouses...");
		  Utility.LoadToyPetWarehouses(root);

		  var pets = Utility.ToyPetIndex.Toypets.Where(n => n.id != 0);

		  ToyPets outputFile = new ToyPets();
            outputFile.pet = new List<ToyPetTemplate>();

		  foreach (var pet in pets) {
			  var template = new ToyPetTemplate();

			  template.id = pet.id;
			  template.name = Utility.PetStringIndex.GetString(Regex.Replace(Utility.ItemStringIndex.GetString(pet.desc), @"[^\u0030-\u007A\u0020]", string.Empty));
			  template.nameid = Utility.PetStringIndex[pet.desc];
			  template.condition_reward = Utility.ItemIndex.GetItem(pet.pet_condition_reward).id;

			  if (pet.func_type1 != null) {
				  if (template.petFunction == null) template.petFunction = new List<petFunction>();
				  petFunction function = new petFunction();
				  function.type = getPetFuntionType(pet.func_type1);
				  if (function.type == functionType.WAREHOUSE) function.slots = Utility.ToyPetWarehouseIndex.GetToyPetWarehouse(pet.func_type_name1).warehouse_slot_count;
				  template.petFunction.Add(function);
			  }

			  if (pet.func_type2 != null) {
				  if (template.petFunction == null) template.petFunction = new List<petFunction>();
				  petFunction function = new petFunction();
				  function.type = getPetFuntionType(pet.func_type2);
				  if (function.type == functionType.WAREHOUSE) function.slots = Utility.ToyPetWarehouseIndex.GetToyPetWarehouse(pet.func_type_name2).warehouse_slot_count;
				  template.petFunction.Add(function);
			  }

			  if (template.petStats == null) template.petStats = new petStats();

			  template.petStats.reaction = pet.combat_reaction == null ? null : pet.combat_reaction.ToUpper();
			  template.petStats.run_speed = pet.art_org_speed_normal_run;
			  template.petStats.walk_speed = pet.art_org_move_speed_normal_walk;
			  template.petStats.height = (float)(pet.scale / 100);
			  template.petStats.altitude = pet.altitude;

			  outputFile.pet.Add(template);
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
				  using (var fs = new FileStream(Path.Combine(outputPath, "pets.xml"),
										   FileMode.Create, FileAccess.Write))
				  using (var writer = XmlWriter.Create(fs, settings)) {
					  XmlSerializer ser = new XmlSerializer(typeof(ToyPets));
					  ser.Serialize(writer, outputFile);
				  }
			  }
			  catch (Exception ex) {
				  Debug.Print(ex.ToString());
			  }
        }

	   static functionType getPetFuntionType(string func) {
		   switch (func.ToUpper()) {
			   case "DOPING": return functionType.DOPING;
			   case "WAREHOUSE": return functionType.WAREHOUSE;
			   case "LOOTING": return functionType.LOOTING;
			   case "FEEDING": return functionType.FOOD;
			   default: return functionType.NONE;
		   }
	   }
    }
}
