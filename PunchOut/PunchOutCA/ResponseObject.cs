using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace iBuyCL
{
    [XmlRoot(ElementName = "Status")]
    public class Status
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "text")]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "StartPage")]
    public class StartPage
    {
        [XmlElement(ElementName = "URL")]
        public string URL { get; set; }
    }

    [XmlRoot(ElementName = "PunchOutSetupResponse")]
    public class PunchOutSetupResponse
    {
        [XmlElement(ElementName = "StartPage")]
        public StartPage StartPage { get; set; }
    }

    [XmlRoot(ElementName = "Response")]
    public class Response
    {
        [XmlElement(ElementName = "Status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "PunchOutSetupResponse")]
        public PunchOutSetupResponse PunchOutSetupResponse { get; set; }
    }

    [XmlRoot(ElementName = "cXML")]
    public class CXML
    {
        [XmlElement(ElementName = "Response")]
        public Response Response { get; set; }
        [XmlAttribute(AttributeName = "payloadID")]
        public string PayloadID { get; set; }
        [XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }
    }

}
