<%@ Page Title="DataMaster" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DataMiningApp._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Datamaster</title>
    <style type="text/css">
        .style2
        {
            text-align: center;
        }
        .style3
        {
            font-size: xx-large;
            font-family: Arial;
        }
        .style4
        {
            height: 13px;
        }
    </style>
</head>
<body>
    <div class="style2">
    <p>
        <span class="style3"><strong>DataMaster<br />
        </strong></span>
    </p>
    </div>
    <form id="main_form" runat="server" method=post>
    <table align="center">
    <tr>
    <td>
    <asp:Panel ID="mainpanel" runat="server" BackColor="#F0F0F0" Height="535px" 
        Width="920px" BorderWidth="2px" BorderColor="Silver" BorderStyle="Solid" 
        Style="vertical-align:middle;" ScrollBars="Auto">
            
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
    </form>
</body>
</html>

