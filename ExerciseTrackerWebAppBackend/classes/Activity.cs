using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Linq;

namespace ExerciseTrackerWebAppBackend
{
    public class Activity
    {
        private static string _xmlDataPath = HttpContext.Current.Server.MapPath("~/data/Activities.xml");
        //TODO add validation to setters
        public int ActivityId { get; set; }
        public ActivityType ActivityType { get; set; }
        public string ActivityDate { get; set; }
        public string Duration { get; set; }
        public decimal Distance { get; set; }

        public Activity(int activityId, ActivityType activityType, string activityDate, string duration, decimal distance)
        {
            this.ActivityId = activityId;
            this.ActivityType = activityType;
            this.ActivityDate = activityDate;
            this.Duration = duration;
            this.Distance = distance;
        }

        /// <summary>
        /// Convert the current object to XML format
        /// </summary>
        /// <param name="ns">The default namespace to use</param>
        /// <returns>An XElement representing the object</returns>
        public XElement toXML(XNamespace ns)
        {
            return new XElement(ns + "Activity",
                new XAttribute("Id", ActivityId),
                new XElement(ns + "TypeId", ActivityType.Id),
                new XElement(ns + "Date", ActivityDate),
                new XElement(ns + "Distance", Distance),
                new XElement(ns + "Duration", Duration)
                );
        }

        //static methods
        public static List<Activity> GetAllActivities()
        {
            List<Activity> activityList = new List<Activity>();

            //load the xml file using linq
            XDocument doc = XDocument.Load(_xmlDataPath);
            var xmlActivityList = doc.Root.Elements();
            XNamespace ns = doc.Root.GetDefaultNamespace();

            //get each of the activity elements from the xml
            foreach (var xmlActivity in xmlActivityList)
            {
                //get activity type
                int typeId = int.Parse(xmlActivity.Element(ns + "TypeId").Value);
                ActivityType type = ActivityType.GetType(typeId);

                //build an activity object
                Activity activity = new Activity(
                    int.Parse(xmlActivity.Attribute("Id").Value),
                    type,
                    xmlActivity.Element(ns + "Date").Value,
                    xmlActivity.Element(ns + "Duration").Value,
                    Convert.ToDecimal(xmlActivity.Element(ns + "Distance").Value)
                    ) ;

                //add to the list
                activityList.Add(activity);
            }

            //return the list
            return activityList;
        }

        public static Activity GetActivity(int activityId)
        {
            //find matching activity and return
            return GetAllActivities().FirstOrDefault(a => a.ActivityId == activityId);
        }

        public static void DeleteActivity (int activityId)
        {
            try
            {
                //load the xml file
                XDocument doc = XDocument.Load(_xmlDataPath);
                XNamespace ns = doc.Root.GetDefaultNamespace();

                //find activity in the document
                XElement activity = doc.Root.Elements(ns+"Activity").FirstOrDefault(a => a.Attribute("Id").Value == activityId.ToString());

                //check if activity is not found - throw exception
                if (activity == null)
                {
                    throw new Exception("activity not found");
                }

                //remove the activity from xml and save
                activity.Remove();
                doc.Save(_xmlDataPath);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Generates the next activity id in the sequence, starts at 1 if no existing
        /// </summary>
        /// <returns>The next activity's id</returns>
        public static int GetNextActivityId()
        {
            //default id starts from 1
            int activityId = 1;

            //load the xml file
            XDocument doc = XDocument.Load(_xmlDataPath);
            XNamespace ns = doc.Root.GetDefaultNamespace();

            //check  for existing activities
            if (doc.Descendants(ns+ "Activity").Any())
            {
                //find the max activity id and +1
                activityId = doc.Descendants(ns + "Activity").Max(p => int.Parse(p.Attribute("Id").Value)) + 1;
            }

            //return new id
            return activityId;
        }

        /// <summary>
        /// Add a activity to the xml file
        /// </summary>
        /// <param name="name">activity's name</param>
        /// <param name="type">activity's type</param>
        /// <param name="date">activity's date</param>
        /// <param name="duration">activity's duration</param>
        /// <param name="distance">activity's distance</param>
        /// <returns>The Id of the added activity</returns>
        public static int AddActivity(ActivityType activityType, string activityDate, string duration, decimal distance)
        {
            int activityId;

            try
            {
                //get the next activity id auto-increment
                activityId = GetNextActivityId();

                //build activity object
                Activity activity = new Activity(activityId, activityType, activityDate, duration, distance);

                //load the xml file
                XDocument doc = XDocument.Load(_xmlDataPath);
                XNamespace ns = doc.Root.GetDefaultNamespace();

                //convert the activity to xml
                XElement xmlActivity = activity.toXML(ns);

                //add the activity from xml and save
                doc.Root.Add(xmlActivity);
                doc.Save(_xmlDataPath);

            }
            catch (Exception)
            {
                throw;
            }

            return activityId;
        }

        /// <summary>
        /// Update a activity in the xml file and return their id
        /// </summary>
        /// <param name="activityId">the activity to update</param>
        /// <returns>the updated activity's id</returns>
        public static int UpdateActivity(Activity activity)
        {
            try
            {
                //load the xml file
                XDocument doc = XDocument.Load(_xmlDataPath);
                XNamespace ns = doc.Root.GetDefaultNamespace();

                //find activity in the document
                XElement xmlactivity = doc.Root.Elements(ns + "Activity").FirstOrDefault(a => a.Attribute("Id").Value == activity.ActivityId.ToString());

                //check if activity is not found - throw exception
                if (activity == null)
                {
                    throw new Exception("activity not found");
                }

                //replace the current activity element with the new activity's data
                xmlactivity.ReplaceWith(activity.toXML(ns));
                
                //save xml
                doc.Save(_xmlDataPath);

            }
            catch (Exception)
            {
                throw;
            }

            return activity.ActivityId;
        }
    }
}