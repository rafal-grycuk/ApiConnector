using System.IO;
using System.Xml.Serialization;

namespace ApiConnector.Net.HttpRest.Utilities
{
    public static class Extensions
    {
        public static T XmlDeserialize<T>(this string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            T t = (T)xs.Deserialize(new StringReader(xml));
            return t;

        }
    }
}
