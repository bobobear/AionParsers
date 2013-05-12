namespace Jamie.NpcTeleporter
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "npc_teleporter", Namespace = "", IsNullable = false)]
	public partial class NpcTeleporterTemplates {
		[XmlElement("teleporter_template", Form = XmlSchemaForm.Unqualified)]
		public List<TeleporterTemplate> teleporter_templates;
	}

	[Serializable]
	public partial class TeleporterTemplate {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int npc_id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int teleportId;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Locations locations;
	}

	[Serializable]
	public partial class Locations {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<TeleLocation> telelocation;

	}

	[Serializable]
	public partial class TeleLocation {
		[XmlIgnore]
		public string description;

		[XmlIgnore]
		public string name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int loc_id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int teleportid;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int price;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int pricePvp;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int required_quest;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public TeleporterType type;
	}

	public enum TeleporterType {
		REGULAR,
		FLIGHT
	}
}
