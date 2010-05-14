<%@ Page Title="DataMaster" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DataMiningApp._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-family: "Segoe UI Semibold";
            font-size: xx-large;
            color: #666666;
        }
        .style2
        {
            font-family: "Segoe UI Semibold";
            font-size: xx-large;
            color: #CC0000;
        }
    </style>
</head>
<body>
<form id="main_form" runat="server" method=post>
<div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    <span class="style1">A</span><span class="style2">NET</span><span 
                        class="style1">LYTICS</span></h1>
            </div>
            
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="true" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home"/>
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="About"/>
                    </Items>
                </asp:Menu>
            </div>
        </div>
    
    <table align="center">
    <tr>
    <td>
    <asp:Panel ID="mainpanel" runat="server" Height="535px" 
        Width="920px"
        Style="vertical-align:middle;border-color:White;" ScrollBars="None" 
            BorderStyle="None" BorderWidth="0px" >
        <!-- Seed for layout table -->   
        <table runat="server" id="layouttable" border="1" bordercolor="black" borderstyle="solid" cellpadding="10" style="font-family:arial;font-size:12px;border-collapse:collapse;">
        </table>                    
    </asp:Panel>
    </td>
    </tr>
    <tr>
    <td align="right" class="style4">
        <asp:Button ID="next_button" runat="server" Text="Next >>" 
            onclick="next_button_Click" Font-Bold="True" Font-Size="Large" 
            Height="35px" Width="100px" PostBackUrl="~/Default.aspx"/>
        </td>
    </tr>
    </table>
</div>
    </form>
</body>
</html>

