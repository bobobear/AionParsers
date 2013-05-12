namespace Jamie.World
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "zones", Namespace = "", IsNullable = false)]
	public partial class ZoneMapsTemplate {
		[XmlElement("zone", Form = XmlSchemaForm.Unqualified)]
		public List<Zone> zones;

		[XmlAttribute("desc", Form = XmlSchemaForm.Unqualified)]
		public string desc;

		[XmlAttribute("map", Form = XmlSchemaForm.Unqualified)]
		public string map;
	}

	[Serializable]
	public partial class Zone {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int mapid;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string description;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string area_type;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string zone_type;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int priority;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(null)]
		public List<int>siege_id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool norecall;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool noride;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Points points;
	}

	[Serializable]
	public partial class Points {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public float bottom;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public float top;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<Point> point;
	}

	[Serializable]
	public partial class Point {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal x;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal y;
	}
}
