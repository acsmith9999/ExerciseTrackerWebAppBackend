using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
//added
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace ExerciseTrackerWebAppBackend
{
    /// <summary>
    /// Summary description for ExerciseTrackerWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/ExerciseTracker")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ExerciseTrackerWebService : System.Web.Services.WebService
    {

        [WebMethod(Description = "Get all activity types"), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetTypes()
        {
            //try to fetch and return all types
            try
            {
                //response code 200=ok
                Context.Response.StatusCode = 200;
                 //serialise the data, convert to JSON format
                return new JavaScriptSerializer().Serialize(ActivityType.GetAllTypes());
            }
            catch(Exception)
            {
                //response code500 = server-side error
                Context.Response.StatusCode = 500;

                //return empty list
                return new JavaScriptSerializer().Serialize(new List<ActivityType>());
            }

        }

        [WebMethod(Description = "Get all activities"), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetActivities()
        {
            //try to fetch and return all Activites
            try
            {
                //response code 200=ok
                Context.Response.StatusCode = 200;
                //serialise the data, convert to JSON format
                return new JavaScriptSerializer().Serialize(Activity.GetAllActivities());
            }
            catch (Exception)
            {
                //response code500 = server-side error
                Context.Response.StatusCode = 500;

                //return empty list
                return new JavaScriptSerializer().Serialize(new List<Activity>());
            }

        }

        [WebMethod(Description = "Get one activity by Id"), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetActivity(int activityID)
        {
            try
            {
                //get activity by id
                Activity activity = Activity.GetActivity(activityID);

                //check if activity doesn't exist (null)
                if (activity == null)
                {
                    //error status 404 not found
                    Context.Response.StatusCode = 404;
                    return new JavaScriptSerializer().Serialize(new { Message = "activity does not exist" });
                }

                //response code 200=ok
                Context.Response.StatusCode = 200;
                //serialise the data, convert to JSON format
                return new JavaScriptSerializer().Serialize(activity);
            }
            catch (Exception ex)
            {
                //response code500 = server-side error
                Context.Response.StatusCode = 500;

                //return error message
                return new JavaScriptSerializer().Serialize(new {Message = "Problem getting activity from xml file" + ex.Message });
            }

        }

        [WebMethod(Description = "Delete one activity by Id"), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string DeleteActivity(int activityId)
        {
            try
            {
                //get activity by id
                Activity activity = Activity.GetActivity(activityId);

                //check if activity doesn't exist (null)
                if (activity == null)
                {
                    //error status 404 not found
                    Context.Response.StatusCode = 404;
                    return new JavaScriptSerializer().Serialize(new { Message = "activity does not exist" });
                }


                //remove the activity from xml file
                Activity.DeleteActivity(activityId);

                //response code 200=ok
                Context.Response.StatusCode = 200;
                //return success message
                return new JavaScriptSerializer().Serialize(new { activityId = activityId });
            }
            catch (Exception ex)
            {
                //response code500 = server-side error
                Context.Response.StatusCode = 500;

                //return error message
                return new JavaScriptSerializer().Serialize(new { Message = "Problem deleting activity from xml file" + ex.Message });
            }

        }

        [WebMethod(Description = "Add one activity"), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string AddActivity(int typeId, string date, string duration, decimal distance)
        {
            try
            {
                //optional: simple validation (should be done in classes)
                if(typeId < 0)
                {
                    Context.Response.StatusCode = 400;
                    return new JavaScriptSerializer().Serialize(new { Message = "activity ID must be  0+" });
                }

                //get activity by id
                ActivityType type = ActivityType.GetType(typeId);

                //check if activity type doesn't exist (null)
                if (type == null)
                {
                    //error status 404 not found
                    Context.Response.StatusCode = 404;
                    return new JavaScriptSerializer().Serialize(new { Message = "Type does not exist" });
                }


                //create activity object and save in xml file
                int activityId = Activity.AddActivity(type, date, duration, distance);

                //response code 200=ok
                Context.Response.StatusCode = 200;
                //return success message
                return new JavaScriptSerializer().Serialize(new { activityId = activityId });
            }
            catch (Exception ex)
            {
                //response code500 = server-side error
                Context.Response.StatusCode = 500;

                //return error message
                return new JavaScriptSerializer().Serialize(new { Message = "Problem adding activity to xml file" + ex.Message });
            }

        }

        [WebMethod(Description = "Update an activity"), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string UpdateActivity(int activityId, int typeId, string date, string duration, decimal distance)
        {
            try
            {
                //get activity by id
                Activity activity = Activity.GetActivity(activityId);

                //check if activity doesn't exist (null)
                if (activity == null)
                {
                    //error status 404 not found
                    Context.Response.StatusCode = 404;
                    return new JavaScriptSerializer().Serialize(new { Message = "activity does not exist" });
                }

                //get type by id
                ActivityType type = ActivityType.GetType(typeId);

                //check if department doesn't exist (null)
                if (type == null)
                {
                    //error status 404 not found
                    Context.Response.StatusCode = 404;
                    return new JavaScriptSerializer().Serialize(new { Message = "Type does not exist" });
                }


                //update the activity object's data
                activity.ActivityType = type;
                activity.ActivityDate = date;
                activity.Duration = duration;
                activity.Distance = distance;

                //update the activity in xml file
                activityId = Activity.UpdateActivity(activity);

                //response code 200=ok
                Context.Response.StatusCode = 200;
                //return success message
                return new JavaScriptSerializer().Serialize(new { activityId = activityId });
            }
            catch (Exception ex)
            {
                //response code500 = server-side error
                Context.Response.StatusCode = 500;

                //return error message
                return new JavaScriptSerializer().Serialize(new { Message = "Problem updating activity / saving to xml file" + ex.Message });
            }

        }
    }
}
