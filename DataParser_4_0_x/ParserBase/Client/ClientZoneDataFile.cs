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
	[XmlRoot(ElementName = "clientzones", Namespace = "", IsNullable = false)]
	public partial class ClientZoneDataFile {
		[XmlElement("subzones", Form = XmlSchemaForm.Unqualified)]
		public SubZones subzones;

		[XmlElement("attributes", Form = XmlSchemaForm.Unqualified)]
		public AttributeZones attributes;

		[XmlElement("artifact_result_areas", Form = XmlSchemaForm.Unqualified)]
		public ArtifactResultAreas artifact_result_areas;

		[XmlElement("abyss_castle_areas", Form = XmlSchemaForm.Unqualified)]
		public AbyssCastleAreas abyss_castle_areas;

		[XmlElement("limitareas", Form = XmlSchemaForm.Unqualified)]
		public LimitAreas limitareas;

		[XmlElement("fortress_warfare_areas", Form = XmlSchemaForm.Unqualified)]
		public FortressWarfareAreas fortress_warfare_areas;

		[XmlElement("artifact_infos", Form = XmlSchemaForm.Unqualified)]
		public ArtifactInfos artifact_infos;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class SubZones {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<SubZone> subzone;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class AttributeZones {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<SubZone> item_use_area;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<SubZone> pvpzone;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class ArtifactResultAreas {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<SubZone> artifact_result_area;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class AbyssCastleAreas {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<SubZone> abyss_castle_area;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class LimitAreas {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<SubZone> limitarea;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class FortressWarfareAreas {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<SubZone> fortress_warfare_area;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class SubZone {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string norecall;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string noride;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int priority;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int abyss_id;

		[XmlElement("string", Form = XmlSchemaForm.Unqualified)]
		public string desc;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string breath_area;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string power_area;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ui_map;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string fatigue_korea;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public PointsInfo points_info;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class PointsInfo {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string type;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Points points;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public float bottom;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public float top;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Points {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<Data> data;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int points_size;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Data {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public decimal x;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public decimal y;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class ArtifactInfos {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<ArtifactInfo> artifact_info;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class ArtifactInfo {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string artifact_name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string artifact_result_area1;
	}
}
