namespace Jamie.World
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "world_maps", Namespace = "", IsNullable = false)]
	public partial class WorldMapsTemplate {
		[XmlElement("map", Form = XmlSchemaForm.Unqualified)]
		public List<Map> world_maps;
	}

	[Serializable]
	public partial class Map {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string map;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int max_user;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int twin_count;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool prison;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool instance;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int water_level;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int death_level;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(WorldType.NONE)]
		public WorldType world_type;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int world_size;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool fly;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool except_buff;

		[XmlElement("ai_info", Form = XmlSchemaForm.Unqualified)]
		public AiInfo ai_info;
	}

	[Serializable]
	public partial class AiInfo {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int chase_target;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int chase_home;
	}

	[Serializable]
	public enum WorldType {
		ASMODAE,
		ELYSEA,
		BALAUREA,
		ABYSS,
		NONE
	}


}
