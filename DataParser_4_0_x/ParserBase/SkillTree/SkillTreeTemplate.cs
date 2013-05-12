namespace Jamie.SkillTree
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "skill_tree", Namespace = "", IsNullable = false)]
	public partial class SkillTreeTemplates {
		[XmlElement("skill", Form = XmlSchemaForm.Unqualified)]
		public List<SkillTreeTemplate> skills;
	}

	[Serializable]
	public partial class SkillTreeTemplate {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int minLevel;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string race;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool stigma;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(false)]
		public bool autolearn;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int skillLevel;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int skillId;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string classId;
	}
}
