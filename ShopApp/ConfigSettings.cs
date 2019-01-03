using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ShopApp
{
    public class ConfigSettings
    {
        private ConfigSettings()
        {
        }


        public string DocAnPath;

        public static string ReadSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
        public static void WriteSetting(string key, string value)
        {
            var doc = loadConfigDocument();


            var node = doc.SelectSingleNode("//appSettings");

            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found in config file.");
            }
            try
            {
                var elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    elem.SetAttribute("value", value);
                }
                else
                {
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }
        }

        public static void RemoveSetting(string key)
        {
            var doc = loadConfigDocument();


            var node = doc.SelectSingleNode("//appSettings");

            try
            {
                if (node == null)
                {
                    throw new InvalidOperationException("appSettings section not found in config file.");
                }
                else
                {
                    node.RemoveChild(node.SelectSingleNode(string.Format("//add[@key='{0}']", key)));
                    doc.Save(getConfigFilePath());
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception(string.Format("The key {0} does not exist.", key), e);
            }
        }

        private static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        private static string getConfigFilePath()
        {
            return Assembly.GetExecutingAssembly().Location + ".config";
        }
    }
}
