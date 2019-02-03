using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Task2
{
    class RssReader
    {

        public Items[] articles; // создаем массив элементов item канала

        public bool getNewArticles(string fileSource)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileSource);

                XmlNodeList nodeList;
                XmlNode root = doc.DocumentElement;
                articles = new Items[root.SelectNodes("channel/item").Count];
                nodeList = root.ChildNodes;
                int count = 0;

                foreach (XmlNode chanel in nodeList)
                {
                    foreach (XmlNode chanel_item in chanel)
                    {

                        if (chanel_item.Name == "item")
                        {
                            XmlNodeList itemsList = chanel_item.ChildNodes;
                            articles[count] = new Items();

                            foreach (XmlNode item in itemsList)
                            {
                                if (item.Name == "title")
                                {
                                    articles[count].title = item.InnerText;
                                }
                                if (item.Name == "link")
                                {
                                    articles[count].link = item.InnerText;
                                }
                                if (item.Name == "description")
                                {
                                    articles[count].description = item.InnerText;
                                }
                                if (item.Name == "pubDate")
                                {
                                    articles[count].pubDate = item.InnerText;
                                }
                            }
                            count += 1;
                        }


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
