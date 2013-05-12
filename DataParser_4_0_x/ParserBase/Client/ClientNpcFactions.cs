namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "npcfactions", Namespace = "", IsNullable = false)]
    public partial class ClientNpcFactionsFile
    {
	   [XmlElement("npcfaction", Form = XmlSchemaForm.Unqualified)]
		public List<ClientNpcFaction> npcfactions;


	   Dictionary<string, int> nameToId = null;
	   Dictionary<int, string> idToName = null;

	   internal void CreateIndex() {
		   if (this.npcfactions == null)
			   return;

		   nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
		   idToName = new Dictionary<int, string>();

		   foreach (ClientNpcFaction faction in this.npcfactions) {
			   if (!nameToId.ContainsKey(faction.name)) {
				   nameToId.Add(faction.name, faction.id);
			   }
			   else {
				   Debug.Print("String with the name {0} already exists; id = {1}", faction.name, faction.id);
			   }
			   if (!idToName.ContainsKey(faction.id)) {
				   idToName.Add(faction.id, faction.name);
			   }
			   else {
				   Debug.Print("String with the name {0} already exists; id = {1}", faction.name, faction.id);
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

	   public string this[int id] {
		   get {
			   if (idToName == null || id == 0 || !idToName.ContainsKey(id))
				   return null;
			   return idToName[id];
		   }
	   }

	   public List<ClientNpcFaction> getClientNpcFactions() {
		   return npcfactions;
	   }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class ClientNpcFaction
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc;

    	[XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string category;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int minlevel_permitted;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int is_mentor_faction;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int auto_join;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int auto_quit;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string combineskill;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int combine_skillpoint;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string race_permitted;
    }
}
