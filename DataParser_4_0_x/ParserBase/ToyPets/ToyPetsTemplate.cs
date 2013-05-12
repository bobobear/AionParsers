namespace Jamie.ToyPet
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "pets", Namespace = "", IsNullable = false)]
	public partial class ToyPets {
		[XmlElement("pet", Form = XmlSchemaForm.Unqualified)]
		public List<ToyPetTemplate> pet;
	}

	[Serializable]
	public partial class ToyPetTemplate {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int nameid;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int condition_reward;

		[XmlElement("petfunction", Form = XmlSchemaForm.Unqualified)]
		public List<petFunction> petFunction;

		[XmlElement("petstats", Form = XmlSchemaForm.Unqualified)]
		public petStats petStats;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class petFunction {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public functionType type;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public int slots;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class petStats {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(null)]
		public string reaction;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public float run_speed;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public float walk_speed;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public float height;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public float altitude;
	}

	[Serializable]
	public enum functionType {
		FOOD,
		DOPING,
		WAREHOUSE,
		LOOTING,
		NONE
	}


}
