namespace Jamie.ParserBase {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "client_instance_cooltimes", Namespace = "", IsNullable = false)]
	public partial class ClientInstanceCooltimesFile {
		[XmlElement("client_instance_cooltime", Form = XmlSchemaForm.Unqualified)]
		public List<ClientInstanceCooltime> client_instance_cooltimes;

		Dictionary<string, int> nameToId = null;
		Dictionary<int, string> idToName = null;
		Dictionary<int, ClientInstanceCooltime> idToInstanceCooldown = null;

		internal void CreateIndex() {
			if (this.client_instance_cooltimes == null)
				return;

			nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
			idToName = new Dictionary<int, string>();
			idToInstanceCooldown = this.client_instance_cooltimes.ToDictionary(i => i.id, i => i);

			foreach (ClientInstanceCooltime client_instance_cooltime in client_instance_cooltimes) {
				if (!nameToId.ContainsKey(client_instance_cooltime.name)) {
					nameToId.Add(client_instance_cooltime.name, client_instance_cooltime.id);
				}
				else {
					Debug.Print("String with the name {0} already exists; id = {1}", client_instance_cooltime.name.ToString(), client_instance_cooltime.id);
				}
				if (!idToName.ContainsKey(client_instance_cooltime.id)) {
					idToName.Add(client_instance_cooltime.id, client_instance_cooltime.name);
				}
				else {
					Debug.Print("String with the name {0} already exists; id = {1}", client_instance_cooltime.name.ToString(), client_instance_cooltime.id);
				}
			}
		}

		public ClientInstanceCooltime getClientInstanceCooltime(int instanceId) {
			if (!idToInstanceCooldown.ContainsKey(instanceId)) {
				return null;
			}
			return idToInstanceCooldown[instanceId];
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
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class ClientInstanceCooltime {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int id;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string desc;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ent_cool_time;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int trial_ent_cool_time;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string race;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int max_member_light;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int max_member_dark;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int enter_min_level_light;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int enter_max_level_light;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int enter_min_level_dark;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int enter_max_level_dark;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string can_enter_mentor;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string resurrection_alias_1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string resurrection_alias_2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string exit_world_1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string exit_alias_1;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string exit_world_2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string exit_alias_2;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string bm_restrict_category;
	}
}
