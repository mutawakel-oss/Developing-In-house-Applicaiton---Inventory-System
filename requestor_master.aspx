<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="requestor_master.aspx.cs" Inherits="Default2" Title="Requestors Master  Page" %>
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
			<div align=center>
			<asp:Label ID="lblUserControlPanel" runat=server Text="Requestor Master Page" ForeColor=red Font-Size=medium></asp:Label>
			<br />
			</div>
			<br />
            	  <asp:DataGrid id="RequestorsGrid" runat="server" Width="780px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mEditRoles" OnUpdateCommand="userGrid_UpdateCommand" OnCancelCommand="userGrid_cancelCoomand">
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ID" DataField="user_id"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Requestor Name" DataField="name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Requestor Position" DataField="position"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" Font-Size="7" />
       </asp:DataGrid> 
       <asp:Label ID="lblSelectedId" runat="server" Visible="False"></asp:Label>
                            
			</div>
	</div>
  
</asp:Content>

