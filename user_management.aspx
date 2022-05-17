<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="user_management.aspx.cs" Inherits="Default2" Title="Users  Page" %>
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
			<asp:Label ID="lblUserControlPanel" runat=server Text="User Roles Control Panel" ForeColor=red Font-Size=medium></asp:Label>
			<br />
			</div>
			<br />
            	  <asp:DataGrid id="UsersGrid" runat="server" Width="780px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mEditRoles" OnUpdateCommand="userGrid_UpdateCommand" OnCancelCommand="userGrid_cancelCoomand">
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ID" DataField="user_id"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="User Name" DataField="name"></asp:BoundColumn>
                
                <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="SPR Creator"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="spr_creator" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "spr_creator")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="PC Tech"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="pc_tech" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "pc_tech")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Item Reciever"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="item_reciever" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "item_reciever")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
               <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Item Inspector"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="inspector" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "inspector")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Approval Supervisor"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="approval_supervisor" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "approval_supervisor")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Assignment Supervisor"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="assignment_supervisor" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "assignment_supervisor")) %>'/>
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Maintainance Supervisor"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="maintainance_supervisor" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "maintainance_supervisor")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Reporting Supervisor"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="reporting_supervisor" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "reporting_supervisor")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Roles Supervisor"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="users_supervisor" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "users_supervisor")) %>' />
                            </ItemTemplate>
                 </asp:TemplateColumn>
      
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" Font-Size="7" />
       </asp:DataGrid> 
       <asp:Label ID="lblSelectedId" runat="server" Visible="False"></asp:Label>
                            
			</div>
	</div>
  
</asp:Content>

