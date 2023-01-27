using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExerciseTrackerWebAppBackend
{
    public partial class ViewActivities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //generate copyright year
            lblCopyrightYear.Text = DateTime.Now.Year.ToString();
        }
    }
}