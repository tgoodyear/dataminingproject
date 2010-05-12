<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PCA_2D_Projection.aspx.cs" Inherits="DataMiningApp.Analysis.PCA.Steps.PCA_2D_Projection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Column Name for Sample Label<br />
        <asp:DropDownList ID="LabelColumn" runat="server" AutoPostBack="True" 
            DataTextField="Select a label column" oninit="LabelColumn_Init" 
            onselectedindexchanged="LabelColumn_SelectedIndexChanged" Width="379px">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Chart ID="Projection" runat="server" EnableViewState="True" Height="434px" 
            Width="832px">
            <Series>
                <asp:Series ChartType="Point" Name="Series1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY Title="Second Principal Component" 
                        TitleFont="Microsoft Sans Serif, 16.2pt">
                    </AxisY>
                    <AxisX Title="First Principal Component" 
                        TitleFont="Microsoft Sans Serif, 16.2pt">
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </p>
</asp:Content>
