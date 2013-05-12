namespace Jamie.ParserBase
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "world_id", Namespace = "", IsNullable = false)]
    public partial class ClientWorldId
    {
		[XmlElement("data", Form = XmlSchemaForm.Unqualified)]
		public List<WorldMap> worlds;

        Dictionary<string, int> nameToId = null;

        internal void CreateIndex() {
		   if (this.worlds == null)
                return;

            nameToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

		  foreach (WorldMap world in worlds) {
			  if (!nameToId.ContainsKey(world.name)) {
				  nameToId.Add(world.name, world.id);
                } else {
				  Debug.Print("String with the name {0} already exists; id = {1}", world.ToString(), world.id);
                }
            }
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
    public partial class WorldMap
{
	   [XmlText]
	   public string name;

        [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
        public int id;

	   [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
	   public int twin_count;

	   [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
	   public int max_user;

	   [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
	   public string prison;

	   [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
	   public string except_buff;
    }
}
