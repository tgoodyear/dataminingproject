<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Start.aspx.cs" Inherits="DataMiningApp.Start" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        &nbsp;</p>
    <p>
        Please select an algorithm from the list
    </p>
    <br />   
    <asp:DropDownList ID="AlgDropDown" runat="server" Width="340px" 
        onselectedindexchanged="AlgDropDown_SelectedIndexChanged" AutoPostBack="True"> 
        </asp:DropDownList>         
    <asp:HyperLink ID="AlgDocLink" runat="server" oninit="AlgDocLink_Init">HyperLink</asp:HyperLink>
    <br />
    <br />   
    <asp:Button ID="Button1" runat="server" Text="Next" onclick="Button1_Click" />
    <br />   
</asp:Content>
