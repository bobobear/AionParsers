namespace Jamie.Npcs
{
	using System;
    using System.Linq;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using Jamie.ParserBase;
	using Jamie.Quests;

	[Serializable]
	[XmlRoot("npc_templates", Namespace = "", IsNullable = false)]
	public partial class NpcTemplates
	{
		[XmlElement("import")]
		public TemplateImportPart[] import;

		[XmlElementAttribute("npc_template", Form = XmlSchemaForm.Unqualified)]
		public List<NpcTemplate> NpcList;

        [XmlIgnore]
        Dictionary<int, NpcTemplate> idToTemplate = null;

        [XmlIgnore]
        Dictionary<int, string> idToTitle = null;

        [XmlIgnore]
        ILookup<string, int> titleToIds = null;

        internal void CreateIndex() {
            if (this.NpcList == null)
                return;

            idToTemplate = new Dictionary<int, NpcTemplate>();
            List<KeyValuePair<string, int>> temp = null;

            foreach (var npc in this.NpcList) {
                if (!idToTemplate.ContainsKey(npc.npc_id))
                    idToTemplate.Add(npc.npc_id, npc);
                if (Utility.StringIndex == null)
                    continue;
                if (idToTitle == null)
                    idToTitle = new Dictionary<int, string>();

                if (!idToTitle.ContainsKey(npc.npc_id)) {
                    var descr = Utility.StringIndex.GetStringDescription(npc.title_id);
                    if (descr != null) {
                        string title = descr.body.Trim('<', '>');
                        idToTitle.Add(npc.npc_id, title);
                        string[] titleWords = title.Split(' ', '-');
                        var lookup = titleWords.Select(w => new KeyValuePair<string, int>(w, npc.npc_id));
                        if (temp == null) {
                            temp = lookup.ToList();
                        } else {
                            temp.AddRange(lookup);
                        }
                    }
                }
            }
            if (temp != null)
                titleToIds = temp.ToLookup(a => a.Key, a => a.Value, StringComparer.InvariantCultureIgnoreCase);
        }

        public NpcTemplate this[int npcId] {
            get {
                if (idToTemplate == null || !idToTemplate.ContainsKey(npcId))
                    return null;
                return idToTemplate[npcId];
            }
        }

        public string GetTitle(int npcId) {
            return idToTitle == null || !idToTitle.ContainsKey(npcId) ? String.Empty : idToTitle[npcId];
        }

        public IEnumerable<int> GetNpcsFromTitleKey(string keyword) {
            keyword = keyword.ToLower();
            if (titleToIds.Contains(keyword))
                return titleToIds[keyword];
            return Enumerable.Empty<int>();
        }
	}

	[Serializable]
	public partial class NpcTemplate
	{
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public NpcStatsTemplate stats;

		[XmlArray(Form = XmlSchemaForm.Unqualified)]
		[XmlArrayItem("item", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public List<int> equipment;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public KiskStats kisk_stats;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public BoundRadius bound_radius;

		[XmlAttribute]
		public int npc_id;

		[XmlAttribute]
		public sbyte level;

		[XmlAttribute]
		public int name_id;

		[XmlAttribute]
		[DefaultValue(0)]
		public int title_id;

		[XmlAttribute]
		[DefaultValue("")]
		public string name;

		[XmlAttribute]
		[DefaultValue(0)]
		public decimal height;

		[XmlAttribute]
		[DefaultValue(0)]
		public int talking_distance;

		[XmlAttribute]
		[DefaultValue(0)]
		public int ammo_speed;

		[XmlAttribute]
		public NpcType npc_type;

		[XmlAttribute]
		public NpcRank rank;

		[XmlAttribute]
		[DefaultValue(0)]
        public int stare_range;

		[XmlAttribute]
		//[DefaultValue("")]
		public string tribe;

        [XmlAttribute]
        public string ai;

		[XmlAttribute]
		public string race;

		[XmlAttribute]
		public int hpgauge;

		[XmlAttribute]
		[DefaultValue(0)]
		public int arank;

        [XmlAttribute]
        [DefaultValue(0)]
        public int arange;

        [XmlAttribute]
        [DefaultValue(0)]
        public int arate;

		[XmlAttribute]
		[DefaultValue(0)]
		public int adelay;
	
		[XmlAttribute]
		[DefaultValue(0)]
		public int state;

        [XmlAttribute]
        [DefaultValue(0)]
        public int talk_delay;//af

      //  [XmlAttribute]
       // [DefaultValue("")]
       // public string rating;//al no

      //  [XmlAttribute]
       // [DefaultValue("")]
       // public string abyss_type;//al no

       // [XmlAttribute]
       // [DefaultValue("GENERAL")]
      //  public string type;

		public NpcTemplate() {
			this.title_id = 0;
			this.name = "";
			this.height = 0;
			this.talking_distance = 0;
            this.stare_range = 0;
		}
	}

	[XmlInclude(typeof(NpcStatsTemplate))]
	[Serializable]
	public abstract class StatsTemplate
	{
		[XmlAttribute]
		//[DefaultValue(0)]
		public int maxHp;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int maxMp;

		[XmlIgnoreAttribute()]
		public bool maxMpSpecified;

		[XmlAttribute]
		//[DefaultValue(0)]
		public decimal walk_speed;

		[XmlAttribute]
		//[DefaultValue(0)]
		public decimal run_speed;

		[XmlAttribute]
		//[DefaultValue(0)]
		public float fly_speed;

		[XmlAttribute]
		//[DefaultValue(0)]
		public float attack_speed;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int evasion;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int block;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int parry;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int main_hand_attack;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int main_hand_accuracy;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int main_hand_crit_rate;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int magic_accuracy;
	}

	[Serializable]
	public sealed class NpcStatsTemplate : StatsTemplate
	{
		[XmlAttribute]
		//[DefaultValue(0)]
		public decimal run_speed_fight;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int crit;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int pdef;

        //[XmlAttribute]
        //[DefaultValue(0)]
        //public int mresist;

        [XmlAttribute]
        //[DefaultValue(0)]
        public int mdef;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int maxXp;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int accuracy;

		[XmlAttribute]
		//[DefaultValue(0)]
		public int power;
	}

	[Serializable]
	public sealed class KiskStats
	{
		[XmlAttribute]
		public int resurrects;

		[XmlAttribute]
		public int members;

		[XmlAttribute]
		public int usemask;
	}

	[Serializable]
	public sealed class BoundRadius {
		[XmlAttribute]
		public decimal front;

		[XmlAttribute]
		public decimal side;

		[XmlAttribute]
		public decimal upper;
	}

	[Serializable]
	public enum NpcRank
	{
		NORMAL,
		ELITE,
		JUNK,
		HERO,
		LEGENDARY,
		DISCIPLINED,
		NOVICE,
		SEASONED,
		EXPERT,
		VETERAN,
		MASTER
	}

	[Serializable]
	public enum NpcType
	{
		NON_ATTACKABLE = 0,
		ATTACKABLE,
		AGGRESSIVE,
		POSTBOX,
		RESURRECT,
		USEITEM,
		PORTAL,
		ARTIFACT,
		ARTIFACT_PROTECTOR,
        CHEST,
        //af
        GUARD_INSTANCE,		
	    ASSEMBLED_NPC,
	    INVUL,
	    INVULNERABLE,
	    
	}
}
