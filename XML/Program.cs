using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XML
{
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
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C: \Users\СантыбаевХ\Documents\visual studio 2015\Projects";
            string[] files = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            List<Order> oreders = new List<Order>();
            oreders.Add(new Order(1, 1, "brand1", "Article1", "Name1", 1, 1));
            oreders.Add(new Order(2, 2, "brand2", "Article2", "Name2", 2, 2));
            oreders.Add(new Order(3, 3, "brand3", "Article3", "Name3", 3, 3));
            xmlWithPrI(oreders);
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
    }
}
