<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="spr_report_page.aspx.cs" Inherits="Default2" Title="Room Master Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SecondBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
	<div  id="body" style="width:1000px" >
			<div id="col_main_left">
			<a id="content_start"></a>
			Links
			</div>
			<div  id="col_main_right"  >
			<div align=center>
			<br />
			<asp:Label ID="lblReport" runat=server Text="SPR Report Page" ForeColor=red Font-Size=Medium></asp:Label>
			<br />
			<br />
			</div>
            <CR:CrystalReportViewer ID="sprRequestFormViwer" runat="server" AutoDataBind="true" DisplayGroupTree="False" />      
			</div>
	</div>
    
  
</asp:Content>

