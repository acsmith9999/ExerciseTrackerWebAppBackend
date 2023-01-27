<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExerciseTrackerWebAppBackend.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Exercise Tracker: Home</title>
    <link rel="stylesheet" href="css/Main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="site-wrapper">
            <header class="site-header">
                <div class="header-logo">
                    <img src="images/logo.jpg" alt="company logo" />

                </div>
                <h1>Activity Management System</h1>
            </header>

            <nav class="main-nav">
                <ul>
                    <li><asp:HyperLink NavigateUrl="~/ViewTypes.aspx" runat="server">View Activity Types</asp:HyperLink></li>
                    <li><asp:HyperLink NavigateUrl="~/ViewActivities.aspx" runat="server">View Activities</asp:HyperLink></li>
                    <li><asp:HyperLink NavigateUrl="~/WebServiceTester.html" runat="server">Web Service Tester</asp:HyperLink></li>
                </ul>
            </nav>

            <footer class="site-footer">
                <p class="copyright"><small>&copy;<asp:Label ID="lblCopyrightYear" runat="server"></asp:Label> Exercise Tracker</small></p>
            </footer>
        </div>
    </form>
</body>
</html>
