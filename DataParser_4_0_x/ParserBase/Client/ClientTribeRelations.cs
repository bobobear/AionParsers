namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "npc_tribe_relations", Namespace = "", IsNullable = false)]
    public partial class ClientTribeRelations
    {
	   [XmlElement("tribe", Form = XmlSchemaForm.Unqualified)]
		public List<ClientTribe> client_tribes;


	   Dictionary<string, ClientTribe> nameToTribe = null;

	   internal void CreateIndex() {
		   if (this.client_tribes == null)
			   return;

		   nameToTribe = new Dictionary<string, ClientTribe>();


		   foreach (ClientTribe tribe in this.client_tribes) {
			   if (!nameToTribe.ContainsKey(tribe.name)) {
				   nameToTribe.Add(tribe.name, tribe);
			   }
			   else {
				   Debug.Print("String with the name {0} already exists; id = {1}", tribe.name);
			   }
		   }
	   }

	   public ClientTribe this[string name] {
		   get {
			   if (nameToTribe == null || String.IsNullOrEmpty(name) || !nameToTribe.ContainsKey(name))
				   return null;
			   return nameToTribe[name];
		   }
	   }

	   public List<ClientTribe> getTribes() {
		   return client_tribes;
	   }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class ClientTribe
    {
        [XmlAttribute("Tribe", Form = XmlSchemaForm.Unqualified)]
        public string name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public TribeType base_tribe;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public TribeType aggressive;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public TribeType support;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public TribeType hostile;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public TribeType friendly;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public TribeType neutral;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public TribeType none;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class TribeType {
	    [XmlText]
	    public string text;

	    public List<string> getTribesFronString() {
		    List<string> names = new List<string>();
 
		    return names;
	    }
    }
}
