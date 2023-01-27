<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewActivities.aspx.cs" Inherits="ExerciseTrackerWebAppBackend.ViewActivities" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Exercise Tracker: View Activities</title>
    <link rel="stylesheet" href="css/Main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="site-wrapper">
            <header class="site-header">
                <div class="header-logo">
                    <img src="images/logo.jpg" alt="company logo" />

                </div>
                <h1>View all activities</h1>
            </header>

            <div class="output-area">
                <asp:Xml ID="xmlOutput" runat="server" DocumentSource="~/data/Activities.xml" TransformSource="~/data/ViewActivities.xslt"></asp:Xml>
            </div>

            <footer class="site-footer">
                <p class="copyright"><small>&copy;<asp:Label ID="lblCopyrightYear" runat="server"></asp:Label> Exercise Tracker</small></p>
            </footer>
        </div>
    </form>
</body>
</html>
