<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PCA_Config.aspx.cs" Inherits="DataMiningApp.Analysis.PCA.Steps.PCA_Config" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        Select Data Colums</p>
    <p>
        <asp:ListBox ID="FeatureList" runat="server" Height="251px" Width="370px" 
            oninit="FeatureList_Init" SelectionMode="Multiple">
        </asp:ListBox>
    </p>
    <p>
        Number of principal components:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="PCs" runat="server" Width="116px" AutoPostBack="True" 
            oninit="PCs_Init" ontextchanged="PCs_TextChanged"></asp:TextBox>
&nbsp;&nbsp;
        <asp:Label ID="Warning" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Button ID="Next" runat="server" onclick="Next_Click" Text="Next" />
    </p>
</asp:Content>
