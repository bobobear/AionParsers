namespace Jamie.Trade
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
	public partial class TradeInGoodsList
	{
		[XmlElement("item", Form = XmlSchemaForm.Unqualified)]
		public List<TradeInGoodsListItem> Items;

		[XmlAttribute]
		public int id;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public partial class TradeInGoodsListItem
	{
		[XmlAttribute]
		public int id;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "tradeingoodslists", Namespace = "", IsNullable = false)]
	public partial class TradeInGoodsLists
	{
		[XmlElement("list", Form = XmlSchemaForm.Unqualified)]
		public List<TradeInGoodsList> list;
	}
}
