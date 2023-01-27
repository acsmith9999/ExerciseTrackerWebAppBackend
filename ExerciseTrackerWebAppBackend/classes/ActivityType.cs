using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ExerciseTrackerWebAppBackend
{
    public class ActivityType
    {
        //TODO add validation for setters
        private static string _xmlDataPath = HttpContext.Current.Server.MapPath("~/data/Types.xml");
        public int Id { get; set; }
        public string Name { get; set; }

        public ActivityType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Convert the current object to XML format
        /// </summary>
        /// <param name="ns">The default namespace to use</param>
        /// <returns>An XElement representing the object</returns>
        public XElement toXML(XNamespace ns)
        {
            return new XElement(ns + "Type",
                new XAttribute("Id", Id),
                new XElement(ns + "Name", Name)
                );
        }

        //static methods
        public static List<ActivityType> GetAllTypes()
        {
            List<ActivityType> typeList = new List<ActivityType>();

            //load the xml file using linq
            XDocument doc = XDocument.Load(_xmlDataPath);
            var xmlTypeList = doc.Root.Elements();
            XNamespace ns = doc.Root.GetDefaultNamespace();

            //get each of the type elements from the xml
            foreach (var xmlType in xmlTypeList)
            {
                //build a type object
                ActivityType type = new ActivityType(
                    int.Parse(xmlType.Attribute("Id").Value),
                    xmlType.Element(ns+"Name").Value);

                //add to the list
                typeList.Add(type);
            }

            //return the list
            return typeList;
        }

        public static ActivityType GetType(int typeId)
        {
            //find matching type and return
            return GetAllTypes().FirstOrDefault(t => t.Id == typeId);
        }
    }
}