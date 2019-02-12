using System.Collections.Generic;

namespace Frame.Core
{
    public class FrameConfig
    {
        public List<Frameitem> FrameItemList { get; set; }
    }

    public class Frameitem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    /*
     XML转对象需要设置标签

    [XmlRoot("FrameConfig")]
    public class FrameConfig
    {
        [XmlElement("FrameItem")]
        public List<Frameitem> FrameItemList { get; set; }
    }

    [XmlType("FrameItem")]
    public class Frameitem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }
    */


}
