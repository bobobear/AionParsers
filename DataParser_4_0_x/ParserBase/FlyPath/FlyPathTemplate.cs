namespace Jamie.FlyPath
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "flypath_template", Namespace = "", IsNullable = false)]
	public partial class FlyPathTemplates {
		[XmlElement("flypath_location", Form = XmlSchemaForm.Unqualified)]
		public List<FlyPathLocation> flypath_locations;
	}

	[Serializable]
	public partial class FlyPathLocation {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal sx;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal sy;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal sz;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int sworld;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal ex;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal ey;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal ez;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int eworld;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public decimal time;
	}
}
