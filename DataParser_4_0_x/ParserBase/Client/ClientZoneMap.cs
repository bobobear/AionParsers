namespace Jamie.ParserBase {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using Jamie.World;

	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot(ElementName = "zonemaps", Namespace = "", IsNullable = false)]
	public partial class ClientZoneMaps {
		[XmlElement("zonemap", Form = XmlSchemaForm.Unqualified)]
		public List<ZoneMap> zone_maps;

		Dictionary<string, int> nameToId = null;
		Dictionary<int, string> idToName = null;
		Dictionary<int, ZoneMap> idToZoneMap = null;

		internal void CreateIndex() {
			if (this.zone_maps == null)
				return;

			nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
			idToName = new Dictionary<int, string>();
			idToZoneMap = this.zone_maps.ToDictionary(i => i.id, i => i);

			foreach (ZoneMap zone in zone_maps) {
				if (!nameToId.ContainsKey(zone.name)) {
					nameToId.Add(zone.name, zone.id);
				}
				else {
					Debug.Print("String with the name {0} already exists; id = {1}", zone.name.ToString(), zone.id);
				}
				if (!idToName.ContainsKey(zone.id)) {
					idToName.Add(zone.id, zone.name);
				}
				else {
					Debug.Print("String with the name {0} already exists; id = {1}", zone.name.ToString(), zone.id);
				}
			}
		}

		public ZoneMap getZoneMap(int mapid) {
			if (!idToZoneMap.ContainsKey(mapid)) {
				return null;
			}
			return idToZoneMap[mapid];
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
	public class ZoneMap {
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int id;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string name;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int world_width;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int world_height;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int offset_x;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int offset_y;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int map_width;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int map_height;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public MapCategory map_category;

		public WorldType getWorldTypeFromString() {
			switch (map_category.ToString().ToUpper()) {
				case "LIGHT": return WorldType.ELYSEA;
				case "DARK": return WorldType.ASMODAE;
				case "DRAGON": return WorldType.BALAUREA;
				case "ABYSS": return WorldType.ABYSS;
				default: return WorldType.NONE;
			}
		}
	}

	public enum MapCategory {
		none = 0,
		light,
		dark,
		dragon,
		abyss
	}
}
