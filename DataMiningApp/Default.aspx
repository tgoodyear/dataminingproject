<%@ Page Title="DataMaster" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DataMiningApp._Default" %>

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
            font-weight: bold;
            font-family: "Courier New", Courier, monospace;
        }
        .style4
        {
            height: 13px;
        }
    </style>
</head>
<body>
    <div class="style2">
        <span class="style3">DataMaster</span>
        <br /><br />
    </div>

    <asp:Panel ID="mainpanel" runat="server" BackColor="#F0F0F0" Height="500px" 
        Width="800px" BorderWidth="2px" BorderColor="Silver" BorderStyle="Solid" Style="margin: 0 auto;">
        <form id="main_form" runat="server">
            
        <!-- Seed for layout table -->   
        <table height=100% width=100% runat=server border="1" id="layouttable">
        </table>
        <table width=100%>    
            <tr>
                <td align=right class="style4">
                <asp:Button ID="back_button" runat="server" Text="Back" />
                <asp:Button ID="next_button" runat="server" Text="Next" 
                        onclick="next_button_Click" />
                </td>
            </tr>
        </table>                    
        </form>
    </asp:Panel>

</body>
</html>

