namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "client_airlines", Namespace = "", IsNullable = false)]
    public partial class ClientAirlineFile
    {
	   [XmlElement("client_airline", Form = XmlSchemaForm.Unqualified)]
		public List<ClientAirline> client_airlines;


	   Dictionary<string, int> nameToId = null;
	   Dictionary<int, string> idToName = null;

	   internal void CreateIndex() {
		   if (this.client_airlines == null)
			   return;

		   nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
		   idToName = new Dictionary<int, string>();

		   foreach (ClientAirline airline in this.client_airlines) {
			   if (!nameToId.ContainsKey(airline.name)) {
				   nameToId.Add(airline.name, airline.id);
			   }
			   else {
				   Debug.Print("String with the name {0} already exists; id = {1}", airline.name, airline.id);
			   }
			   if (!idToName.ContainsKey(airline.id)) {
				   idToName.Add(airline.id, airline.name);
			   }
			   else {
				   Debug.Print("String with the name {0} already exists; id = {1}", airline.name, airline.id);
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

	   public List<ClientAirline> getClientAirlines() {
		   return client_airlines;
	   }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class ClientAirline
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int need_confirm;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string airline_world;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string cur_airport_name;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public AirlineList airline_list;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class AirlineList {
	    [XmlElement("data", Form = XmlSchemaForm.Unqualified)]
	    public List<AirlineData> data;

	    public List<AirlineData> getAirlineData() {
		    return data;
	    }
    }
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class AirlineData {
	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string airport_name;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int fee;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int pvpon_fee;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int required_quest;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int flight_path_group_id;
    }
}
