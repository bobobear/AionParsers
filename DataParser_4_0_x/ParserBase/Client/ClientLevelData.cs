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
	[XmlRoot(ElementName = "LevelData", Namespace = "", IsNullable = false)]
	public partial class ClientLevelDataFile {
		[XmlElement("LevelInfo", Form = XmlSchemaForm.Unqualified)]
		public LevelInfo level_info;

		[XmlElement("Missions", Form = XmlSchemaForm.Unqualified)]
		public Missions Missions;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class LevelInfo {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string Name;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public float WaterLevel;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Missions {
		[XmlElement("Mission", Form = XmlSchemaForm.Unqualified)]
		public List<Mission> LevelMissions;


		Dictionary<string, Mission> nameToMission = null;
		internal void CreateIndex() {
			if (this.LevelMissions == null)
				return;

			nameToMission = this.LevelMissions.ToDictionary(i => i.Name, i => i);
		}

		public Mission getMission(string mission_name) {
			if (!nameToMission.ContainsKey(mission_name)) {
				return null;
			}
			return nameToMission[mission_name];
		}

	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Mission {
		[XmlAttribute("Name", Form = XmlSchemaForm.Unqualified)]
		public string Name;

		[XmlElement("LevelOption", Form = XmlSchemaForm.Unqualified)]
		public LevelOption LevelOption;

		[XmlElement("flying_zones", Form = XmlSchemaForm.Unqualified)]
		public FlyingZones flying_zones;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class LevelOption {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Fly Fly;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Ride Ride;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class FlyingZones {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public List<FlyingZone> flying_zone;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public string name;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class FlyingZone {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public PointsInfo points_info;
	}


	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Fly {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int Fly_Whole_Level;

		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int MaxHeight;
	}

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Ride {
		[XmlAttribute(Form = XmlSchemaForm.Unqualified)]
		public int Ride_Whole_Level;
	}
}
