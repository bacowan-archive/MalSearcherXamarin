using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MalSearcher.Model.MalProxy
{
    public static class XmlUtils
    {
        private static string INVALID_ELEMENT_EXCETPION = "The given xml document either had zero or multiple instances of the '{0}' element. There should be exactly one instance of this element";

        public static T ParseSingleNode<T>(XElement rootElement, string elementName)
        {
            try
            {
                return (T)Convert.ChangeType(ParseSingleNode(rootElement, elementName).Value, typeof(T));
            }
            catch (Exception e) when ( e is InvalidCastException || e is FormatException)
            {
                return default(T);
            }
        }

        public static XElement ParseSingleNode(XElement rootElement, string elementName)
        {
            try
            {
                return ParseMultipleNodes(rootElement, elementName).Single();
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(String.Format(INVALID_ELEMENT_EXCETPION, elementName), e);
            }
        }

        public static IEnumerable<XElement> ParseMultipleNodes(XElement rootElement, string elementName)
        {
            return (from e1 in rootElement.Descendants(elementName)
                    select e1);
        }

        public static DateTime? Datetime(string dt)
        {
            string[] ymd = dt.Split('-');
            if (ymd.Length != 3) return null;
            try
            {
                return new DateTime(Int32.Parse(ymd[0]), Int32.Parse(ymd[1]), Int32.Parse(ymd[2]));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
