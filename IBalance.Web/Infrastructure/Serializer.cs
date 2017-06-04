using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace IBalance.Web.Infrastructure
{
    public class Serializer
    {
        private string _pathToFile;

        public Serializer(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public string PathToFile
        {
            get
            {
                return _pathToFile;
            }
        }

        public void SerializeObject<T>(T objectToSerialize)
        {
            XmlSerializer xsSubmit = new XmlSerializer(objectToSerialize.GetType());
            using (StreamWriter stringWriter = new StreamWriter(_pathToFile))
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    xsSubmit.Serialize(writer, objectToSerialize);
                }
            }
        }

        //public static string SerializeObject<T>(this T toSerialize)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

        //    using (StringWriter textWriter = new StringWriter())
        //    {
        //        xmlSerializer.Serialize(textWriter, toSerialize);
        //        return textWriter.ToString();
        //    }
        //}
    }
}