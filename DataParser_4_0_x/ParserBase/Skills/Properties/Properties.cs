namespace Jamie.Skills {
	using System;
	using System.ComponentModel;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using System.Collections.Generic;

	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Properties {
        [XmlElement("addweaponrange", Form = XmlSchemaForm.Unqualified)]
        public AddWeaponRangeProperty addweaponrange;

        [XmlElement("firsttarget", Form = XmlSchemaForm.Unqualified)]
        public FirstTargetProperty firsttarget;

        [XmlElement("firsttargetrange", Form = XmlSchemaForm.Unqualified)]
        public FirstTargetRangeProperty firsttargetrange;

        [XmlElement("targetrange", Form = XmlSchemaForm.Unqualified)]
        public TargetRangeProperty target_range;

        [XmlElement("targetrelation", Form = XmlSchemaForm.Unqualified)]
        public TargetRelationProperty targetrelation;

        [XmlElement("targetspecies", Form = XmlSchemaForm.Unqualified)]
        public TargetSpeciesProperty targetspecies;
	}

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(TargetStatusProperty))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(TargetRelationProperty))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(TargetRangeProperty))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FirstTargetRangeProperty))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FirstTargetProperty))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AddWeaponRangeProperty))]
    [System.SerializableAttribute()]
    public abstract partial class Property
    {
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public partial class TargetStatusProperty : Property
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public partial class TargetRelationProperty : Property
    {

        private TargetRelationAttribute valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TargetRelationAttribute value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public enum TargetRelationAttribute
    {

        /// <remarks/>
        NONE,

        /// <remarks/>
        ENEMY,

        /// <remarks/>
        MYPARTY,

        /// <remarks/>
        ALL,

        /// <remarks/>
        FRIEND,
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public partial class TargetSpeciesProperty : Property
    {

        private TargetSpeciesAttribute valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TargetSpeciesAttribute value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public enum TargetSpeciesAttribute
    {

        /// <remarks/>
        NONE,

        /// <remarks/>
        ALL,

        /// <remarks/>
        PC,

        /// <remarks/>
        NPC,
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public partial class TargetRangeProperty : Property
    {

        private int maxcountField;

        private bool maxcountFieldSpecified;

        private int distanceField;

        private bool distanceFieldSpecified;

        private TargetRangeAttribute valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int maxcount
        {
            get
            {
                return this.maxcountField;
            }
            set
            {
                this.maxcountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxcountSpecified
        {
            get
            {
                return this.maxcountFieldSpecified;
            }
            set
            {
                this.maxcountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool distanceSpecified
        {
            get
            {
                return this.distanceFieldSpecified;
            }
            set
            {
                this.distanceFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TargetRangeAttribute value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public enum TargetRangeAttribute
    {

        /// <remarks/>
        NONE,

        /// <remarks/>
        ONLYONE,

        /// <remarks/>
        PARTY,

        /// <remarks/>
        AREA,

        /// <remarks/>
        PARTY_WITHPET,

        /// <remarks/>
        POINT,
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public partial class FirstTargetRangeProperty : Property
    {

        private int valueField;

        private bool valueFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valueSpecified
        {
            get
            {
                return this.valueFieldSpecified;
            }
            set
            {
                this.valueFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public partial class FirstTargetProperty : Property
    {

        private FirstTargetAttribute valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public FirstTargetAttribute value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public enum FirstTargetAttribute
    {

        /// <remarks/>
        NONE,

        /// <remarks/>
        TARGETORME,

        /// <remarks/>
        ME,

        /// <remarks/>
        MYPET,

        /// <remarks/>
        TARGET,

        /// <remarks/>
        PASSIVE,

        /// <remarks/>
        TARGET_MYPARTY_NONVISIBLE,

        /// <remarks/>
        POINT,
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    public partial class AddWeaponRangeProperty : Property
    {
    }
}
