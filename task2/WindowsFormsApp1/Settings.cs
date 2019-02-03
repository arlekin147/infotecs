//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Xml;
using System;
//using System.IO;

namespace WindowsFormsApp1
{
    public static class Settings
    {
        static Settings()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Settings.path);
            XmlElement root = doc.DocumentElement;
            XmlNodeList settings = root.ChildNodes;
            foreach(XmlNode setting in settings)
            {
                Console.WriteLine("set" + setting.Name);
                switch (setting.Name)
                {
                    case "Frequency":
                        {
                            try
                            {
                                var attr = setting.Attributes.GetNamedItem("value");
                                string[] splited = attr.Value.Split(':');
                                /*Frequency += Int32.Parse(splited[0]) * 1000 * 60;
                                Frequency += Int32.Parse(splited[1]) * 1000;*/
                                Frequency = Int32.Parse(attr.Value);
                                Console.WriteLine(Frequency + "F");
                                
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        }
                    case "IsShowTitle":
                        {
                            if(setting.Attributes.GetNamedItem("value").Value == "True")
                            {
                                IsShowTitle = true;
                            }
                            break;
                        }
                    case "IsShowDescription":
                        {
                            if (setting.Attributes.GetNamedItem("value").Value == "True")
                            {
                                IsShowDescription = true;
                            }
                            break;
                        }
                    case "IsShowPubDate":
                        {
                            if (setting.Attributes.GetNamedItem("value").Value == "True")
                            {
                                IsShowPubDate = true;
                            }
                            break;
                        }
                }
            }

            



        }

        public static void save()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Settings.path);
            XmlElement root = doc.DocumentElement;
            XmlNodeList settings = root.ChildNodes;
            foreach (XmlNode setting in settings)
            {
                switch (setting.Name)
                {
                    case "Frequency":
                        {
                            var attr = setting.Attributes.GetNamedItem("value");
                            attr.Value = "" + Frequency;
                            break;
                        }
                    case "IsShowTitle":
                        {
                           
                            setting.Attributes.GetNamedItem("value").Value = IsShowTitle.ToString();
                            break;
                        }
                    case "IsShowDescription":
                        {
                            setting.Attributes.GetNamedItem("value").Value = IsShowDescription.ToString();
                            break;
                        }
                    case "IsShowPubDate":
                        {
                            setting.Attributes.GetNamedItem("value").Value = IsShowPubDate.ToString();
                            break;
                        }
                }
            }
            doc.Save(Settings.path);
        }

        
        private static string path = "settings.xml";
        public static int Frequency { set; get; } // MM:SS
        public static bool IsShowTitle { set; get; }
        public static bool IsShowDescription { set; get; }
        public static bool IsShowPubDate { set; get; }
    }
  
}
