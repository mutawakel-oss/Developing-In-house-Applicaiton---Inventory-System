<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="error_page.aspx.cs" Inherits="Default2" Title="Room Master Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SecondBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
	<div  id="body" style="width:1000px" >
			<div id="col_main_left">
			<a id="content_start"></a>
			Links
			</div>
			<div  id="col_main_right" style="background-color:LightBlue; background-repeat: no-repeat;" >
                <asp:Label ID="lblError" runat="server" ForeColor=red></asp:Label><br />
                <br />
                <br />
                <br />
                <br />
                <br />
			</div>
	</div>
  
</asp:Content>

