using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ZoneEdit
{
    /*ZoneEdit seems to return odd payloads for success and errors
     * We need to convert that to something usable
     * 
    <SUCCESS CODE = "201" TEXT="no update required for foo.com to 10.10.175.5" ZONE="foo.com">
    <SUCCESS CODE = "200" TEXT="foo.com updated to 10.10.175.5" ZONE="foo.com">

    <ERROR CODE = "702" PARAM="600" TEXT="Minimum 600 seconds between requests" ZONE="foo.com"/>
    */

    #region ZoneEditResponses Class
    public class ZoneEditResponses : List<ZoneEditResponse>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
    #endregion

    #region ZoneEditResponse Class
    public class ZoneEditResponse
    {
        [XmlAttribute("CODE")]
        public int Code { get; set; }
        [XmlAttribute("TEXT")]
        public string Text { get; set; }
        [XmlAttribute("ZONE")]
        public string Zone { get; set; }


        public static ZoneEditResponses GetResponse(string xmlString)
        {
            ZoneEditResponses responses = new ZoneEditResponses();
           // List<ZoneEditResponse> responses = new List<ZoneEditResponse>();
            if (xmlString.IndexOf("ERROR")> -1)
            {
                string el = $"{xmlString}</ERROR>";
                ZoneEditResponse response = DeserializeZoneEditResponse(el,"ERROR");
                responses.Message = response.Text;
                responses.Add(response);
            }
            else
            {
                string pattern = "<SUCCESS[^>]*>";
                MatchCollection matches = Regex.Matches(xmlString, pattern, RegexOptions.Singleline);
                responses.Success = true;
                foreach (Match match in matches)
                {
                    string el = $"{match.Value}</SUCCESS>";
                    ZoneEditResponse response = DeserializeZoneEditResponse(el, "SUCCESS");
                    if (response.Code == 201)
                        responses.Message = response.Text;
                    responses.Add(response);
                }
            }
            return responses;
        }

        public static ZoneEditResponse DeserializeZoneEditResponse(string xmlString, string elementName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ZoneEditResponse), new XmlRootAttribute(elementName));
                using (StringReader stringReader = new StringReader(xmlString))
                {
                    return (ZoneEditResponse)serializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during deserialization.
                Console.WriteLine("Error deserializing XML: " + ex.Message);
                return null;
            }
        }

    }
    #endregion
}
