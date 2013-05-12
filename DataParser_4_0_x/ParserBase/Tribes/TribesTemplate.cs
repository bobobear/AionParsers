namespace Jamie.Tribes
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "tribe_relations", Namespace = "", IsNullable = false)]
	public partial class TribeRelationsTemplate {
		[XmlElement("tribe", Form = XmlSchemaForm.Unqualified)]
		public List<TribeTemplate> tribes;
	}

	[Serializable]
	public partial class TribeTemplate {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlAttribute("base", Form = XmlSchemaForm.Unqualified)]
		public string base_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TribeType aggro;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TribeType friend;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TribeType hostile;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TribeType support;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TribeType neutral;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TribeType none;
	}

	[Serializable]
	public partial class TribeType {
		[XmlText]
		public string text;
	}
}
