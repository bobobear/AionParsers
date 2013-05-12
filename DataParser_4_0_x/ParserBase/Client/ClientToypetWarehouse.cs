namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "client_toypet_warehouses", Namespace = "", IsNullable = false)]
	public partial class ClientToyPetWarehouses {
		[XmlElement("client_toypet_warehouse", Form = XmlSchemaForm.Unqualified)]
		public List<ToyPetWarehouse> ToyPetWarehouses;

		Dictionary<string, int> nameToId = null;
		Dictionary<string, ToyPetWarehouse> nameToToyPetWarehouse = null;

		internal void CreateIndex() {
			if (this.ToyPetWarehouses == null)
				return;

			nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
			nameToToyPetWarehouse = this.ToyPetWarehouses.ToDictionary(i => i.name, i => i, StringComparer.InvariantCultureIgnoreCase);

			foreach (ToyPetWarehouse toypetwarehouse in this.ToyPetWarehouses) {
				if (!nameToId.ContainsKey(toypetwarehouse.name)) {
					nameToId.Add(toypetwarehouse.name, toypetwarehouse.id);
				}
				else {
					Debug.Print("String with the name {0} already exists; id = {1}", toypetwarehouse.name, toypetwarehouse.id);
				}
			}
		}

		public ToyPetWarehouse GetToyPetWarehouse(string name) {
			name = name.Trim().ToLower();
			if (name == null || !nameToToyPetWarehouse.ContainsKey(name)) {
				return null;
			}
			return nameToToyPetWarehouse[name];
		}

		public int this[string stringId] {
			get {
				if (nameToId == null || String.IsNullOrEmpty(stringId) || !nameToId.ContainsKey(stringId))
					return -1;
				return nameToId[stringId];
			}
		}
	}

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ToyPetWarehouse
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public string warehouse_slot_type;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public int warehouse_slot_count;
    }
}
