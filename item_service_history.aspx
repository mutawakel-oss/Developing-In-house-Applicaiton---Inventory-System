<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="item_service_history.aspx.cs" Inherits="Default2" Title="Item Service History Page" %>
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
			<br />
			<div align=center>
			<asp:Label ID="lblAssignmentPageTitle" runat=server ForeColor=red Font-Size=Larger Text="Item Online Profile Page" BackColor=lightgray></asp:Label>
			</div>
			<br />
			<br />
			<br />
			<asp:Table ID="tblSearchCriteria" runat=server>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSelectCriteria" runat=server Text="Select A Criteria"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSearchCriteria" runat=server AutoPostBack=true OnSelectedIndexChanged="mDdlSearchSelected">
			<asp:ListItem Text="--Select a creteria--" Value="0"></asp:ListItem>
			<asp:ListItem Text="BY User" Value="2"></asp:ListItem>
			<asp:ListItem Text="BY ROOM" Value="4"></asp:ListItem>
			</asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			<asp:Panel ID="pnlUserSearch" runat=server Visible=false >
			<asp:Table ID="tblUserSearch" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblDepSearch" runat=server Text="Select A Department"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlDepSearch" runat=server OnSelectedIndexChanged="mDepartmentSearchDDLSelected" AutoPostBack=true ></asp:DropDownList>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblUserSearch" runat=server Text="Select A User"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlUserSearch" runat=server AutoPostBack=true OnSelectedIndexChanged="mUserDDLSelected"></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			</asp:Panel>
			<asp:Panel ID="pnlRoomSearch" runat=server Visible=false>
			<asp:Table ID="tblRoomSearch" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblRoomSearch" runat=server Text="Select A Room"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlRoomSearch" runat=server AutoPostBack=true OnSelectedIndexChanged="mRoomDDLSelected"></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			</asp:Panel>
			<%-- The following tags will be used to format the grid of items --%>
			<hr />
			<div align=center>
			<asp:Label ID="lblItemsGrid" runat=server Text="Items List" ForeColor=red Font-Size=Larger></asp:Label>
		    </div>
			<br />
			<asp:Label ID="lblSprItemsTitle" runat=server  ForeColor=blue Font-Size=small></asp:Label>
			<asp:Label ID="lblSprItemsCount" runat=server  Font-Size=small></asp:Label>
			<br />
			<asp:Label ID="lblAssignedItemsNoText" runat=server ForeColor=blue Font-Size=small></asp:Label>
			<asp:Label ID="lblAssignedItemsNo" runat=server Font-Size=small ></asp:Label>
			<br />
		    <asp:Label ID="lblUnassignedItemsNoText" runat=server  ForeColor=blue Font-Size=small></asp:Label>
			<asp:Label ID="lblUnassignedItemsNo" runat=server Font-Size=small></asp:Label>
			<br />
			<br />
			<%-- The following label for error messages --%>
       <asp:Label ID="lblitemCounterValidity" runat=server ForeColor=red BackColor=white></asp:Label>
			<%-- Items Grid --%>
			  <asp:DataGrid id="productsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False"  OnEditCommand="mDisplaySpecification" OnDeleteCommand="mAddServiceHisotoryClicked" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=small />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Profile" UpdateText="Update"  ItemStyle-ForeColor=blue></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ITEM" DataField="item"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="ROOM" DataField="room"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="VENDOR" DataField="vendor"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SPR#" DataField="spr_no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC DATE" DataField="rec_date"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC BY" DataField="rec_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="INS BY" DataField="insp_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="APPR BY" DataField="appro_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="ASS TO" DataField="assigned_to"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="sys ID"  DataField="hidden"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="TAG"  DataField="serial"></asp:BoundColumn>
               <asp:ButtonColumn CommandName="Delete" Text="Add Service"></asp:ButtonColumn> 
               
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=small  ForeColor="White" />
       </asp:DataGrid>
       <br />

       	<%-- Assignment Extender Control --%>
			<asp:Button ID="Button1_1" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="addServiceHistoryExtender" runat="server" 
            TargetControlID="Button1_1"  PopupControlID="pnl_file" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnFileCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="pnl_file" runat="server" CssClass="modalPopup" Style="display:none" Width="500">
                 <asp:Panel ID="pnl_file_head" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="lblCollegeChoicePanel" runat=server Text="Inventory System - Adding an Item Service Report"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="lblRegStatment" runat=server></asp:Label>
                </span></b>
         <asp:Table ID="tblAddNewServiceHistory" runat=server >
         <asp:TableRow>
         <asp:TableCell>
         <asp:Label ID="lblAddNewServiceHistory" runat=server Text="Add a report"></asp:Label>
         </asp:TableCell>
         <asp:TableCell>
         <asp:TextBox ID="txtServiceReport" runat=server TextMode=multiline Height="200px" Width="330px"></asp:TextBox>
         </asp:TableCell>
         </asp:TableRow>
         
         </asp:Table>
                <div style="text-align:center;">
                <asp:Button ID="btnAssigng" runat="server" Text="Add"  OnClick="mBtnAddClicked"    />
                
                &nbsp;<asp:Button ID="btnFileCancel"  runat="server" Text="Close"  /></div>                
            </asp:Panel>
            <%-- End of Assignment Extender --%>
		
            <%-- specification Extender Control --%>
			<asp:Button ID="btnHidden" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="specificationExtender" runat="server" 
            TargetControlID="btnHidden"  PopupControlID="specificationPnl" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnSpecificationCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="specificationPnl" runat="server" CssClass="modalPopup" Style="display:none" Width="700" Height="500" ScrollBars=Both>
                 <asp:Panel ID="Panel2" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="Label1" runat=server Text="Inventory System - Item Profile"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="Label2" runat=server></asp:Label>
                </span></b>
                <div align=center>
			<asp:Label ID="lblItemDisplay" runat=server ForeColor=red Font-Size=Larger Text="Item Specification List" BackColor=lightgray></asp:Label>
			<br />
			<br />
				<%-- The following tags for the data grid --%>
			 <asp:DataGrid id="specificationDataGrid" runat="server" Width="302px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
           
               <asp:BoundColumn HeaderText="Property" DataField="property" ItemStyle-ForeColor=blue></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Description" DataField="description"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SysID" DataField="sys_id"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       <br />
        <div align=center>
        <hr />
      		<asp:Label ID="lblUpgradationHistory" runat=server ForeColor=red Font-Size=Larger Text="Upgradation History List" BackColor=lightgray></asp:Label> 
      		<br />
			<br />
			 <asp:DataGrid id="upgradationHistoryGrid" runat="server" Width="302px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:BoundColumn HeaderText="Property" DataField="property"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Description" DataField="description"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="upgradation Date" DataField="upgrade_date"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       </div>
       <br />
       <br />
       <br />
       <div align=center>
       <hr />
      		<asp:Label ID="lblAssignmentHistory" runat=server ForeColor=red Font-Size=Larger Text="Assignment History List" BackColor=lightgray></asp:Label> 
      		<br />
			<br />
			 <asp:DataGrid id="assignmentHistoryGrid" runat="server" Width="302px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:BoundColumn HeaderText="Assigned to" DataField="assigned_to"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Assginment Date" DataField="assignment_date"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Assigned by" DataField="assigned_by"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       </div>
             <div align=center>
        <hr />
      		<asp:Label ID="lblItemServiceHistory" runat=server ForeColor=red Font-Size=Larger Text="Item Service History" BackColor=lightgray></asp:Label> 
      		<br />
			<br />
			 <asp:DataGrid id="serviceHistoryGrid" runat="server" Width="302px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
             <asp:BoundColumn HeaderText="Date" DataField="date"></asp:BoundColumn>  
             <asp:BoundColumn HeaderText="Service Report" DataField="service_report"></asp:BoundColumn>  
             <asp:BoundColumn HeaderText="Done By" DataField="done_by"></asp:BoundColumn>  
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       </div>
        <div align=center>
        <hr />
      		<asp:Label ID="lblPreventiveHistory" runat=server ForeColor=red Font-Size=Larger Text="Preventive Maintainance History List" BackColor=lightgray></asp:Label> 
      		<br />
			<br />
			 <asp:DataGrid id="preventiveGrid" runat="server" Width="302px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
             <asp:BoundColumn HeaderText="Action" DataField="action"></asp:BoundColumn>  
                  <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Windows Update">
                            <ItemTemplate>
                              <asp:CheckBox ID="w_update" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "status")) %>' Enabled=false />
                            </ItemTemplate>
                 </asp:TemplateColumn>
             <asp:BoundColumn HeaderText="Starting Date" DataField="start_date"></asp:BoundColumn>  
             <asp:BoundColumn HeaderText="Ending Date" DataField="end_date"></asp:BoundColumn>  
             <asp:BoundColumn HeaderText="Maintaineed By" DataField="maintained_by"></asp:BoundColumn>  
             
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       </div>
    
			</div>
                <div style="text-align:center;">
                <asp:Button ID="btnSpecificationCancel"  runat="server" Text="Close"  /></div>                
            </asp:Panel>
            <%-- End of Assignment Extender --%>
            <%-- The following label will containthe id number of item --%>
            <asp:Label ID="lblItemNumber" runat=server Visible=false></asp:Label>
            <asp:Label ID="lblAssigned_to" runat=server Visible=false></asp:Label>
            <asp:Label ID="lblSpecifiedFromMaster" runat=server Visible=false></asp:Label>
		<br />                     
			</div>
	</div>
  
</asp:Content>

