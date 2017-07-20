using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XML
{

    class Item
    {
        public string Litle { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
    }
    class Order
    {
        public int id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Brand { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Price { get; set; }
        public Order(int id, Nullable<int> UserId, string Brand, string Article, string Name, Nullable<int> Count, Nullable<int> Price)
        {
            this.id = id;
            this.UserId = UserId;
            this.Brand = Brand;
            this.Article = Article;
            this.Name = Name;
            this.Count = Count;
            this.Price = Price;
        }
        public void method1() { }
        public void method2() { }
        public void method3() { }


    }
    class file
    {
        public string filename { get; set; }
        public string filetype { get; set; }
        public DateTime date { get; set; }
        public file(string filename, string filetype, DateTime date)
        {
            this.filename = filename;
            this.filetype = filetype;
            this.date = date;
        }
    }

    class Program
    {
        public static object XmkDeclaration { get; private set; }

        static void Main(string[] args)
        {


            //string url = "https://habrahabr.ru/rss/interesting/";
            //byte[] data;
            //using (WebClient webClient = new WebClient())
            //    data = webClient.DownloadData(url);

            //string str = Encoding.GetEncoding("UTF-8").GetString(data);
            //XDocument xd = XDocument.Parse(str);
            //Console.WriteLine(str);
            string path = @"C:\Users\СантыбаевХ\Desktop\books_DB";
            string[] files = Directory.GetFiles(path);
            List<file> fileList = new List<file>();
            foreach (var item in files)
            {
                string name = item.Substring(item.LastIndexOf('\\'), (item.LastIndexOf('.') - 1 - item.LastIndexOf('\\')));
                string type = item.Substring(item.LastIndexOf('.'), item.Length - item.LastIndexOf('.'));
                DateTime date = File.GetCreationTime(item);
                fileList.Add(new file(name, type, date));
            }
            xmlWithPrIFile(fileList);

            //List<Order> oreders = new List<Order>();
            //oreders.Add(new Order(1, 1, "brand1", "Article1", "Name1", 1, 1));
            //oreders.Add(new Order(2, 2, "brand2", "Article2", "Name2", 2, 2));
            //oreders.Add(new Order(3, 3, "brand3", "Article3", "Name3", 3, 3));
            //xmlWithPrI(oreders);
            //CreateXmlUsingSysXml(oreders[0]);
            //readXml();
        }
        static void CreatXml()
        {
            XDocument xdoc = new XDocument();

            XElement ordersRoot = new XElement("orders");
            XElement xmlOrders = new XElement("order");

            XAttribute orderAttr = new XAttribute("Id", 161);
            XElement orderElem_UserId = new XElement("UserId", 1127);
            XElement orderElem_Brand = new XElement("Brand", "Delphi");
            XElement orderElem_Article = new XElement("Article", "TC512");
            XElement orderElem_Name = new XElement("Name", "Delphi рычаг");

            xmlOrders.Add(orderAttr);
            xmlOrders.Add(orderElem_UserId);
            xmlOrders.Add(orderElem_Brand);
            xmlOrders.Add(orderElem_Article);
            xmlOrders.Add(orderElem_Name);

            ordersRoot.Add(xmlOrders);
            xdoc.Add(ordersRoot);

            xdoc.Save("orders.xml");
        }
        static void xmlWithPrI(List<Order> order)
        {
            XDocument xdoc = new XDocument();

            XElement ordersRoot = new XElement("orders");
            XElement xmlOrders = new XElement("order");
            // XAttribute orderAttr = new XAttribute();
            MethodInfo[] mInfo = typeof(Order).GetMethods();
            PropertyInfo[] pInfo = typeof(Order).GetProperties();
            foreach (var item in order)
            {
                foreach (var props in mInfo)
                {
                    if (props.CustomAttributes.ToString() == "Count = 0")
                    {
                        XElement el = new XElement(props.Name, item.GetType().GetProperty(props.Name).GetValue(item, null));
                        xmlOrders.Add(el);
                    }
                }
            }
            foreach (var item in order)
            {
                foreach (var props in mInfo)
                {
                    if (props.CustomAttributes.ToString() == "Count = 0")
                    {
                        XAttribute el = new XAttribute(props.Name, item.GetType().GetProperty(props.Name).GetValue(item, null));
                        xmlOrders.Add(el);
                    }
                }
            }


        }
        static void xmlWithPrIFile(List<file> order)
        {
            XDocument xdoc = new XDocument();

            XElement ordersRoot = new XElement("files");
            XElement xmlOrders = new XElement("file");
            // XAttribute orderAttr = new XAttribute();
            MethodInfo[] mInfo = typeof(Order).GetMethods();
            PropertyInfo[] pInfo = typeof(Order).GetProperties();
            foreach (var item in order)
            {
                foreach (var props in mInfo)
                {
                    if (props.CustomAttributes.ToString() == "Count = 0")
                    {
                        XElement el = new XElement(props.Name, item.GetType().GetProperty(props.Name).GetValue(item, null));
                        xmlOrders.Add(el);
                    }
                }
            }
            foreach (var item in order)
            {
                foreach (var props in mInfo)
                {
                    if (props.CustomAttributes.ToString() == "Count = 0")
                    {
                        XAttribute el = new XAttribute(props.Name, item.GetType().GetProperty(props.Name).GetValue(item, null));
                        xmlOrders.Add(el);
                    }
                }
            }


        }
        static void CreateXmlUsingSysXml(Order order/*, ValidationType validationType*/)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(declaration);
            XmlElement rootElement = doc.CreateElement("orders");

            XmlElement orderElement = doc.CreateElement("order");
            orderElement.SetAttribute("Id", order.id.ToString());

            XmlElement orderElement_UserId = doc.CreateElement("UserId");
            orderElement_UserId.InnerText = order.UserId.ToString();
            orderElement_UserId.SetAttribute("UserId", "123456");

            orderElement.AppendChild(orderElement_UserId);
            rootElement.AppendChild(orderElement);
            doc.AppendChild(rootElement);
            doc.Save("name.xml");


        }
        static void readXml()
        {
            //XmlDocument doc = new XmlDocument();
            //doc.Load("https://news.rambler.ru/rss/science/");
            XmlSerializer formatter = new XmlSerializer(typeof(rss));
            using (FileStream fs = new FileStream("rss.xml", FileMode.OpenOrCreate))
            {
                rss newrss = (rss)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine("Канал: {0} --- Версия: {1}", newrss.channel, newrss.version);
            }

        }
    }
}
