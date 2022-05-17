<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reporting_page.aspx.cs" Inherits="Default2" Title="Reporting Page" %>
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
			<asp:Label ID="lblAssignmentPageTitle" runat=server ForeColor=red Font-Size=Larger Text="Reporting Page" BackColor=lightgray></asp:Label>
			</div>
			<br />
			<br />
			<br />
			<asp:Table ID="tblSearchCriteria" runat=server>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSelectCriteria" runat=server Text="Search By"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSearchCriteria" runat=server AutoPostBack=true OnSelectedIndexChanged="mDdlSearchSelected">
			<asp:ListItem Text="--Select a creteria--" Value="0"></asp:ListItem>
			<asp:ListItem Text="By Categoy" Value="1"></asp:ListItem>
			<asp:ListItem Text="BY User" Value="2"></asp:ListItem>
			<asp:ListItem Text="BY PC Tech" Value="3"></asp:ListItem>
			<asp:ListItem Text="BY ROOM" Value="4"></asp:ListItem>
			</asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			<asp:Panel ID="pnlCategorySearch" runat=server Visible=false>
			<asp:Table ID="assignmentOptionsTable" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblCategory" runat=server Text="Category"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlCategory" runat=server AutoPostBack=true OnSelectedIndexChanged="mSubCategoryDDLselected"></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSubCategory" runat=server Text="Sub-category"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSubCategory" runat=server AutoPostBack=true OnSelectedIndexChanged="mSubSubCategoryDDLselected"></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSubSubCategory" runat=server Text="Sub-sub-category"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSubSubCategory" runat=server AutoPostBack=true OnSelectedIndexChanged="mFillItemsGrid"></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			</asp:Panel>
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
			<asp:Panel ID="pnlPcTechSearch" runat=server Visible=false>
			<asp:Table ID="tblPcTechSearch" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblPCtechSearch" runat=server Text="Select A PC Tech"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlPcTechSearch" runat=server AutoPostBack=true OnSelectedIndexChanged="mPcTechDDLSelected"></asp:DropDownList>
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
		    <asp:Table ID="tblStatistics" runat=server Visible=false>
		    <asp:TableRow>
		    <asp:TableCell>
		    <asp:Label ID="lblSprItemsTitle" runat=server  ForeColor=blue Font-Size=small></asp:Label>
		    </asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblSprItemsCount" runat=server  Font-Size=small></asp:Label>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblAssignedItemsNoText" runat=server ForeColor=blue Font-Size=small></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblAssignedItemsNo" runat=server Font-Size=small ></asp:Label>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblUnassignedItemsNoText" runat=server  ForeColor=blue Font-Size=small></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblUnassignedItemsNo" runat=server Font-Size=small></asp:Label>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			<br />
			<br />
			<%-- The following label for error messages --%>
       <asp:Label ID="lblitemCounterValidity" runat=server ForeColor=red BackColor=white></asp:Label>
          <asp:Table ID="tblUserItemsNavigation" runat=server Visible=false>
       <asp:TableRow>
       <asp:TableCell>
       <asp:Button ID="btnGoUserList" runat=server Text="Return to User List" OnClick="mGoUserListBtnClicked" />
       </asp:TableCell>
       <asp:TableCell>
        <asp:Button ID="btnHideOptions" runat="server" Text="Show Details" OnClick="mShowItemsDetails" />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
          <asp:Table ID="tblSubCategoryNavigation" runat=server Visible=false>
       <asp:TableRow>
       <asp:TableCell>
       <asp:Button ID="btnSubCategoryDetails" runat=server Text="Return to User List" OnClick="mGoSubCategoryListBtnClicked" />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
          <asp:Table ID="tblPcTechListNavigation" runat=server Visible=false>
       <asp:TableRow>
       <asp:TableCell>
       <asp:Button ID="btnGoToPcTechStatisticsList" runat=server Text="Return to PC Tech List" OnClick="mGoPcTechListBtnClicked" />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
       <asp:Table ID="tblRoomStatisticsNavigation" runat=server Visible=false>
       <asp:TableRow>
       <asp:TableCell>
       <asp:Button ID="btnGoToRoomList" runat=server Text="Return to Room Statistics List" OnClick="mGoRoomListBtnClicked" />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
			<%-- Items Grid --%>
		   <asp:DataGrid id="productsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mDisplaySpecification" >
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
               <asp:BoundColumn HeaderText="Tag"  DataField="serial"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SERIAL"  DataField="actSerial"></asp:BoundColumn>
                 
               
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=small  ForeColor="White" />
       </asp:DataGrid>
       <br />
       <%--The following datagrid will be used to display the users assigned items --%>
    
            <asp:DataGrid id="usersItemsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mUsersItemsGridEdit" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode=NumericPages />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=Small />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Details" UpdateText="Update">
                   <ItemStyle ForeColor="Blue" />
               </asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="USER NAME" DataField="user_name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="TOTAL ITEMS" DataField="total"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="LOGIN ID" DataField="userLogin"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=Small  ForeColor="White" />
       </asp:DataGrid>
       <%--The following datagrid will be used to display the users assigned items --%>
       <br />
       <%--The following datagrid will be used to display the sub category data grid report --%>
         <asp:DataGrid id="subCategoryGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mSubCategoryGridEdit" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode=NumericPages />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=Small />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Details" UpdateText="Update">
                   <ItemStyle ForeColor="Blue" />
               </asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ITEM TYPE" DataField="item_type"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="TOTAL NO" DataField="total"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="UNASSIGNED NO" DataField="unassigned"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="ASSIGNED NO" DataField="assigned"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="MINIMUM NO" DataField="minimum"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SYS ID" DataField="sys_id"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=Small  ForeColor="White" />
       </asp:DataGrid>
       <%--End of datagrid that will be used to display the sub category data grid report --%>
        <%--The following datagrid will be used to display the PC Tech statistics report --%>
         <asp:DataGrid id="pcTechListGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mPcTechStatisticsGridEdit" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode=NumericPages />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=Small />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Details" UpdateText="Update">
                   <ItemStyle ForeColor="Blue" />
               </asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="PC TECH NAME" DataField="pc_tech_name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="NO OF ITEMS" DataField="total"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="LOGIN NAME" DataField="login"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=Small  ForeColor="White" />
       </asp:DataGrid>
       <%--End of datagrid that will be used to display the PC Tech statistics report --%>
        <%--The following datagrid will be used to display the room statistics report --%>
         <asp:DataGrid id="roomStatisticsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mRoomStatisticsGridEdit"  >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode=NumericPages />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=Small />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Details" UpdateText="Update">
                   <ItemStyle ForeColor="Blue" />
               </asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ROOM" DataField="room_name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="NO OF ITEMS" DataField="total"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SYS ID" DataField="sys_id"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=Small  ForeColor="White" />
       </asp:DataGrid>
       <%--End of datagrid that will be used to display the roomstatistics report --%>
       
       <br />
       <asp:Table ID="tblAssigning" runat=server Visible=false >
       <asp:TableRow>
       <asp:TableCell>
       <input type=button value="Print" onclick="window.print();" />
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnExporToExl" runat=server Text="Export to PDF" OnClick="mExportToExl"  />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
       
			
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
			 <asp:DataGrid id="specificationDataGrid" runat="server" Width="302px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mSpecificationEditClicked" OnCancelCommand="mSpecificationCancelClicked" OnUpdateCommand="mSpecificationUpdateClicked" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
           <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
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

