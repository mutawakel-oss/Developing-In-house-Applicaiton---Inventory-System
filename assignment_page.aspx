<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="assignment_page.aspx.cs" Inherits="Default2" Title="Assignment Page" %>
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
			<asp:Label ID="lblAssignmentPageTitle" runat=server ForeColor=red Font-Size=Larger Text="Assign/Reassign Items Page" BackColor=lightgray></asp:Label>
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
			<asp:ListItem Text="By Categoy" Value="1"></asp:ListItem>
			<asp:ListItem Text="BY User" Value="2"></asp:ListItem>
			<asp:ListItem Text="BY PC Tech" Value="3"></asp:ListItem>
			<asp:ListItem Text="BY ROOM" Value="4"></asp:ListItem>
			<asp:ListItem Text="BY SPR No" Value="5"></asp:ListItem>
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
			<asp:Panel ID="pnlSprSearch" runat=server Visible=false>
			<asp:Table ID="tblSprSearch" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSprNo" runat=server Text="Enter SPR No"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtSprNo" runat=server ></asp:TextBox>
			<asp:RegularExpressionValidator ID="txtSPRValidator" runat="server" ValidationGroup="sprGroup" ControlToValidate="txtSprNo" ErrorMessage="*" ValidationExpression="\d+"></asp:RegularExpressionValidator>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Button ID="btnSprSearch" runat=server Text="Search" CausesValidation=true ValidationGroup="sprGroup" OnClick="mSprSearchBtnClicked" />
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
			  <asp:DataGrid id="productsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnDeleteCommand="mAssignLinkClicked" OnEditCommand="mDisplaySpecification" >
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
               <asp:BoundColumn HeaderText="TAG NO"  DataField="serial"></asp:BoundColumn>
               
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Check">
                            <ItemTemplate>
                              <asp:CheckBox ID="chkSelect" runat=server />
                            </ItemTemplate>
                        </asp:TemplateColumn>
               
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=small  ForeColor="White" />
       </asp:DataGrid>
       <br />
       <asp:Table ID="tblAssigning" runat=server Visible=false >
       <asp:TableRow>
       
       <asp:TableCell>
       <asp:Button ID="btnSelectAll" runat=server Text="Select All" OnClick="mSelectAll" />
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnClear" runat=server Text="Deselect All" OnClick="mClearBtnClicked" />
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnAssign" runat=server Text="Assign" OnClick="mAssignProduct" />
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnExporToExl" runat=server Text="Export" OnClick="mExportToExl" Visible=false />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
       
			<%-- Assignment Extender Control --%>
			<asp:Button ID="Button1_1" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="AssignementExtender" runat="server" 
            TargetControlID="Button1_1"  PopupControlID="pnl_file" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnFileCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="pnl_file" runat="server" CssClass="modalPopup" Style="display:none" Width="500">
                 <asp:Panel ID="pnl_file_head" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="lblCollegeChoicePanel" runat=server Text="Inventory System"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="lblRegStatment" runat=server></asp:Label>
                </span></b>
                <asp:Table ID="tblAssignmentSelection" runat=server>
                <asp:TableRow>
                <asp:TableCell>
                <asp:Label ID="lblSelectionType" runat=server Text="Choose Assignment Mechanism"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                <asp:DropDownList ID="ddlSelectionType" runat=server AutoPostBack=true OnSelectedIndexChanged="mSelectionTypeDDLSelected">
                <asp:ListItem Text="--Select A Cretirea--" Value="0"></asp:ListItem>
                <asp:ListItem Text="Assign An Item to A user" Value="1"></asp:ListItem>
                <asp:ListItem Text="Assign An Item to A PC Tech" Value="2"></asp:ListItem>
                <asp:ListItem Text="Assign An Item to A Room" Value="3"></asp:ListItem>
                </asp:DropDownList>
                </asp:TableCell>
                </asp:TableRow>
                </asp:Table>
              <asp:Panel ID="pnlAssignUser" runat=server Visible=false >
              <asp:Table ID="usersListTable" runat=server >
              <asp:TableRow>
              <asp:TableCell>
              <asp:Label ID="lblUserName" runat=server Text="User ID"></asp:Label>
              </asp:TableCell>
              <asp:TableCell>
              <asp:TextBox ID="txtUserId" runat=server></asp:TextBox>
              </asp:TableCell>
              <asp:TableCell>
              <asp:Button ID="btnUserSearch" runat=server Text="Search" OnClick="mSearchUser" />
              </asp:TableCell>
              <asp:TableCell>
              <asp:LinkButton ID="linkRefreshUsers" runat=server Text="Refrest" ForeColor=blue OnClick="mRefreshUses"></asp:LinkButton>
              </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow>
              <asp:TableCell>
              <asp:Label ID="lblDepartmentName" runat=server Text="Department Name"></asp:Label>
              </asp:TableCell>
              <asp:TableCell>
              <asp:DropDownList ID="ddlDepartment" runat=server AutoPostBack=true OnSelectedIndexChanged="mDpartmentDDlSelected"></asp:DropDownList>
              </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow>
              <asp:TableCell>
              <asp:Label ID="lblUserNames" runat=server Text="Choose A User"></asp:Label>
              </asp:TableCell>
              <asp:TableCell>
              <asp:DropDownList ID="ddlUsers" runat=server ></asp:DropDownList>
              </asp:TableCell>
              </asp:TableRow>
              </asp:Table>
              </asp:Panel>
              <%-- The following panel will be used to assign a proudct to a pc tech --%>
              <asp:Panel ID="pnlAssignToPctech" runat=server Visible=false >
              <asp:Table ID="tblAssignToPctech" runat=server>
              <asp:TableRow>
              <asp:TableCell>
              <asp:Label ID="lblPcTech" runat=server Text="Select PC Tech:"></asp:Label>
              </asp:TableCell>
              <asp:TableCell>
              <asp:DropDownList ID="ddlPcTech" runat=server ></asp:DropDownList>
              </asp:TableCell>
              </asp:TableRow>
              	<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblStartDate" runat=server Text="Starting Date:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtStartingDate"   Width="100px" runat="server"   ></asp:TextBox>
			 <asp:Image ID="imgDeliveryDt2" runat="server"  ImageUrl="~/Images/date.gif"/>
            <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="CalendarExtender1"  runat="server" PopupButtonID="imgDeliveryDt2" TargetControlID="txtStartingDate" BehaviorID="CalendarExtender1" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			</asp:TableCell>
			</asp:TableRow>
	        	<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblEndingDate" runat=server Text="Ending Date:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtEndingDate"   Width="100px" runat="server"   ></asp:TextBox>
			 <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/date.gif"/>
            <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="CalendarExtender2"  runat="server" PopupButtonID="Image1" TargetControlID="txtEndingDate" BehaviorID="CalendarExtender2" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			</asp:TableCell>
			</asp:TableRow>		
			<asp:TableRow>
			<asp:TableCell ColumnSpan=2>
			<asp:Label ID="lblPcTechError" runat=server ForeColor=red ></asp:Label>
			</asp:TableCell>
			</asp:TableRow>
              </asp:Table>
              </asp:Panel>
              <%-- The following panel will be used to assign a proudct to a room --%>
              <asp:Panel ID="pnlAssignToRoom" runat=server Visible=false >
              <asp:Table ID="tblAssignToRoom" runat=server >
              <asp:TableRow>
              <asp:TableCell>
              <asp:Label ID="lblRoom" runat=server Text="Select Room"></asp:Label>
              </asp:TableCell>
              <asp:TableCell>
              <asp:DropDownList ID="ddlRoom" runat=server ></asp:DropDownList>
              </asp:TableCell>
              </asp:TableRow>
              </asp:Table>
              </asp:Panel>
                <div style="text-align:center;">
                <asp:Button ID="btnAssigng" runat="server" Text="Assign"  OnClick="mBtnAssignClicked"    />
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
            <asp:Label ID="lblSprHiddenNo" runat=server Visible=false></asp:Label>
		<br />                     
			</div>
	</div>
  
</asp:Content>

