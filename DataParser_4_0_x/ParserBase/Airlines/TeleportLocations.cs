namespace Jamie.NpcTeleporter
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using System.Diagnostics;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "teleport_location", Namespace = "", IsNullable = false)]
	public partial class TeleporterLocationTemplates {
		[XmlElement("teleloc_template", Form = XmlSchemaForm.Unqualified)]
		public List<TeleporterLocationTemplate> teleloc_templates;

		Dictionary<int, TeleporterLocationTemplate> idToTemplate = null;

		internal void CreateIndex() {
			if (this.teleloc_templates == null)
				return;

			idToTemplate = new Dictionary<int, TeleporterLocationTemplate>();

			foreach (TeleporterLocationTemplate template in this.teleloc_templates) {
				if (!idToTemplate.ContainsKey(template.loc_id)) {
					idToTemplate.Add(template.loc_id, template);
				}
				else {
					Debug.Print("String with the name {0} already exists; id = {1}", template.loc_id, template.name);
				}
			}
		}

		public TeleporterLocationTemplate this[int id] {
			get {
				if (idToTemplate == null || id == 0 || !idToTemplate.ContainsKey(id))
					return null;
				return idToTemplate[id];
			}
		}
	}

	[Serializable]
	public partial class TeleporterLocationTemplate {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int loc_id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int mapid;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public decimal posX;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public decimal posY;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public decimal posZ;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		[DefaultValue(0)]
		public decimal heading;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string type;
	}
}
