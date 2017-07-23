using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utilities
{
    /// <summary>
    /// xml帮助类
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        ///  Parse xml from xml string
        /// </summary>
        /// <typeparam name="ITEM"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ITEM Parse<ITEM>(string config)
        {
            XmlReader reader = XmlReader.Create(new StringReader(config));

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ITEM));
                return (ITEM)serializer.Deserialize(reader);
            }
            catch //todo: exception handling
            {
                throw;
            }
        }

        /// <summary>
        /// Parse xml from file path
        /// </summary>
        /// <typeparam name="ITEM"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <remarks>It is caller's responsibility to catch exception</remarks>
        public static ITEM ParseFromFile<ITEM>(string filePath)
        {
            using (Stream fstream = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ITEM));
                return (ITEM)serializer.Deserialize(fstream);
            }
        }


        /// <summary>
        /// Serialize object to xml string
        /// </summary>
        /// <typeparam name="ITEM"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <remarks>It is caller's responsibility to catch exception</remarks>
        public static string ToXmlString<ITEM>(ITEM item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ITEM));
            MemoryStream memStream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = Encoding.UTF8;
            using (XmlWriter writer = XmlWriter.Create(memStream, setting))
            {
                serializer.Serialize(writer, item);
                writer.Flush();
                writer.Close();
                memStream.Position = 0;
                using (StreamReader reader = new StreamReader(memStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Serialize object to xml string
        /// </summary>
        /// <typeparam name="ITEM"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <remarks>It is caller's responsibility to catch exception</remarks>
        public static string ToXmlStringNoDeclaration<ITEM>(ITEM item)
        {
            //Create our own namespaces for the output
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //Add an empty namespace and empty value
            ns.Add("", "");

            XmlSerializer serializer = new XmlSerializer(typeof(ITEM));
            MemoryStream memStream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();

            setting.OmitXmlDeclaration = true;

            setting.Encoding = Encoding.UTF8;
            using (XmlWriter writer = XmlWriter.Create(memStream, setting))
            {
                serializer.Serialize(writer, item, ns);
                writer.Flush();
                writer.Close();
                memStream.Position = 0;
                using (StreamReader reader = new StreamReader(memStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Serialize an object to xml file
        /// </summary>
        /// <typeparam name="ITEM"></typeparam>
        /// <param name="item"></param>
        /// <param name="filePath"></param>
        /// <remarks>It is caller's responsibility to catch exception</remarks>
        public static void ToXmlFile<ITEM>(ITEM item, string filePath)
        {
            using (FileStream fstream = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ITEM));
                serializer.Serialize(fstream, item);
            }
        }
    }
}
