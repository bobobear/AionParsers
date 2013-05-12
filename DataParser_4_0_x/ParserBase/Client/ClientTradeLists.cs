namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "client_npc_trade_in_lists", Namespace = "", IsNullable = false)]
    public partial class ClientTradelistsFile
    {
	   [XmlElement("client_npc_trade_in_list", Form = XmlSchemaForm.Unqualified)]
        public List<ClientTradeList> GoodLists;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class ClientTradeList
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string name;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string desc;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        [DefaultValue(0)]
        public int use_category;

        [XmlArray(ElementName = "goods_list", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem("data", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<TradeListData> data;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class TradeListData
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string item;
    }
}
