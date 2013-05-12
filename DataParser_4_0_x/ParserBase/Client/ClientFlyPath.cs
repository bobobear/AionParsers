namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "path_db", Namespace = "", IsNullable = false)]
    public partial class ClientFlyPathFile
    {
	   [XmlElement("path_group", Form = XmlSchemaForm.Unqualified)]
        public List<PathGroup> path_groups;

	   public List<PathGroup> getPathGroups() {
		   return path_groups;
	   }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class PathGroup
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int group_id;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool fixed_Camera;

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public FlyPath path;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public Wind wind;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public Start start;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public End end;

	   [XmlElement(Form = XmlSchemaForm.Unqualified)]
	   public decimal fly_time;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class FlyPath {
	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int id;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string file;

    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class Start {
	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public decimal x;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public decimal y;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public decimal z;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string world;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class End {
	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public decimal x;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public decimal y;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public decimal z;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string world;
    }


    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class Wind {
	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public int id;

	    [XmlElement(Form = XmlSchemaForm.Unqualified)]
	    public string file;
    }
}
