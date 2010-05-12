<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PCA_Results.aspx.cs" Inherits="DataMiningApp.Analysis.PCA.Steps.PCA_Results" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="AlgorithmTime" runat="server" Text="Label"></asp:Label>
    </p>
    <hr />
    <p>
        Principal Component Coefficients and Loadings</p>
    <p>
        <asp:GridView ID="PCView" runat="server" AutoGenerateSelectButton="True" 
            oninit="PCView_Init">
            <SelectedRowStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </p>
    <p>
        <asp:Button ID="Project" runat="server" onclick="Project_Click" 
            Text="Project on 2D Graph" />
&nbsp;
        <asp:Button ID="Finish" runat="server" Text="Finish" />
    </p>
    <hr />
    <p>
        Variance plot</p>
    <p>
        <asp:Chart ID="VariancePlot" runat="server" Palette="EarthTones" Width="886px">
            <series>
                <asp:Series BorderWidth="3" ChartType="Line" Color="Black" 
                    MarkerBorderColor="Black" MarkerBorderWidth="3" MarkerColor="Black" 
                    MarkerStyle="Circle" Name="Series1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY Title="Variance Explained" TitleFont="Microsoft Sans Serif, 16.2pt">
                    </AxisY>
                    <AxisX Title="Principal Component" TitleFont="Microsoft Sans Serif, 16.2pt">
                    </AxisX>
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
