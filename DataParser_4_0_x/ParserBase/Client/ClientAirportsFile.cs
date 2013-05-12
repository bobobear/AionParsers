namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "client_airports", Namespace = "", IsNullable = false)]
    public partial class ClientAirportsFile
    {
	   [XmlElement("client_airport", Form = XmlSchemaForm.Unqualified)]
		public List<ClientAirport> client_airports;

	   Dictionary<string, int> nameToId = null;
	   Dictionary<int, string> idToName = null;

	   internal void CreateIndex() {
		   if (this.client_airports == null)
			   return;

		   nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
		   idToName = new Dictionary<int, string>();

		   foreach (ClientAirport airport in this.client_airports) {
			   if (!nameToId.ContainsKey(airport.name)) {
				   nameToId.Add(airport.name, airport.id);
			   }
			   else {
				   Debug.Print("String with the name {0} already exists; id = {1}", airport.name, airport.id);
			   }
			   if (!idToName.ContainsKey(airport.id)) {
				   idToName.Add(airport.id, airport.name);
			   }
			   else {
				   Debug.Print("String with the name {0} already exists; id = {1}", airport.name, airport.id);
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
			   if (nameToId == null || id == 0 || !idToName.ContainsKey(id))
				   return null;
			   return idToName[id];
		   }
	   }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
	public partial class ClientAirport
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public int ui_map_pos_x;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public int ui_map_pos_y;
    }
}
