using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiConnector.Net.Test.Utilites
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
