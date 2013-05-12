namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "client_toypets", Namespace = "", IsNullable = false)]
    public partial class ClientToyPetsFile
    {
	   [XmlElement("client_toypet", Form = XmlSchemaForm.Unqualified)]
        public List<ToyPet> Toypets;

        Dictionary<string, int> nameToId = null;

        internal void CreateIndex() {
		   if (this.Toypets == null)
                return;

            nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

		  foreach (ToyPet toypet in this.Toypets) {
                if (!nameToId.ContainsKey(toypet.name)) {
				 nameToId.Add(toypet.name, toypet.id);
                } else {
				 Debug.Print("String with the name {0} already exists; id = {1}", toypet.name, toypet.id);
                }
            }
        }

        public int this[string stringId] {
            get {
                if (nameToId == null || String.IsNullOrEmpty(stringId) || !nameToId.ContainsKey(stringId))
                    return -1;
                return nameToId[stringId];
            }
        }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class ToyPet
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string name;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string pet_condition_reward;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string combat_reaction;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string func_type1;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string func_type_name1;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string func_type2;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string func_type_name2;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public float art_org_move_speed_normal_walk;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public float art_org_speed_normal_run;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public int scale;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public float altitude;
    }
}
