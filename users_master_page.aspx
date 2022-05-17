<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="users_master_page.aspx.cs" Inherits="Default2" Title="Users Rule Master Page" %>
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
			<div  id="col_main_right" style="background-color:LightBlue; background-repeat: no-repeat;" >
             
                <asp:Label ID="lblVendorId" runat="server" Visible="False"></asp:Label><br />
                            
			</div>
	</div>
  
</asp:Content>

