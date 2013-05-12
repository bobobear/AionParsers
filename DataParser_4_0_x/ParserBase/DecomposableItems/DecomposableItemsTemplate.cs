namespace Jamie.DecomposableItems
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "decomposable_items", Namespace = "", IsNullable = false)]
	public partial class DecomposableItemsTemplates {
		[XmlElement("decomposable", Form = XmlSchemaForm.Unqualified)]
		public List<DecomposableItemTemplate> decomposable_items;

		List<int> itemIdList = null;

		internal void CreateIndex() {
			if (this.decomposable_items == null)
				return;

			itemIdList = new List<int>();

			foreach (var decomposable in decomposable_items) {
				if (!itemIdList.Contains(decomposable.item_id)) {
					itemIdList.Add(decomposable.item_id);
				}
			}
		}

		public Boolean isDecomposable(int itemId) {
			if (itemIdList.Contains(itemId)) {
				return true;
			}
			return false;
		}
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class DecomposableItemTemplate {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int item_id;

		[XmlElement("items", Form = XmlSchemaForm.Unqualified)]
		public List<DecomposableItems> items;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class DecomposableItems {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int chance;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int minlevel;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int maxlevel;

		[XmlElement("item", Form = XmlSchemaForm.Unqualified)]
		public List<DecomposableItem> item;

		[XmlElement("random_item", Form = XmlSchemaForm.Unqualified)]
		public List<RandomDecomposableItem> random_item;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class DecomposableItem {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int id;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int count;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int rnd_min;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int rnd_max;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string race;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class RandomDecomposableItem {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string type;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int count;
	}
}
