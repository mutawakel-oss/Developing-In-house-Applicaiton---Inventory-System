<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="maintainance_page.aspx.cs" Inherits="Default2" Title="Preventive Maintainance Page" %>
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
			<asp:Label ID="lblAssignmentPageTitle" runat=server ForeColor=red Font-Size=Larger Text="Preventive Maintainance Page" BackColor=lightgray></asp:Label>
			</div>
			<br />
			<br />
			<br />
			<asp:Table ID="tblSearchCriteria" runat=server>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblPcTechName" runat=server Text="Select A PC Tech Name" ></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlPcTechName" runat=server AutoPostBack=true OnSelectedIndexChanged="mPcTechDDLSelectd">
			</asp:DropDownList>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblStartEndDate" runat=server Text="Starting and Ending Date"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlStartingEndingDates" runat=server AutoPostBack=true OnSelectedIndexChanged="mDatesDDLSelected"></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
		
			<%-- The following tags will be used to format the grid of items --%>
			<hr />
			<div align=center>
			<asp:Label ID="lblItemsGrid" runat=server Text="Items List" ForeColor=red Font-Size=Larger></asp:Label>
		    </div>
			<br />
			<asp:Label ID="lblShowStartDateTitle" runat=server Text="Start Date:"  ForeColor=blue Font-Size=small></asp:Label>
			<asp:Label ID="lblShowStartDateText" runat=server  Font-Size=small></asp:Label>
			&nbsp;&nbsp;&nbsp;
			<asp:Label ID="lblShowEndDateTitle" runat=server Text="End Date:" ForeColor=blue Font-Size=small></asp:Label>
			<asp:Label ID="lblShowEndDateText" runat=server Font-Size=small ></asp:Label>
			<br />
		    <asp:Label ID="lblUnassignedItemsNoText" runat=server  ForeColor=blue Font-Size=small></asp:Label>
			<asp:Label ID="lblUnassignedItemsNo" runat=server Font-Size=small></asp:Label>
			<br />
			<br />
			<%-- The following label for error messages --%>
       <asp:Label ID="lblitemCounterValidity" runat=server ForeColor=red BackColor=white></asp:Label>
			<%-- Items Grid --%>
			  <asp:DataGrid id="productsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False"  OnEditCommand="mDisplaySpecification" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=small />
           <Columns>
               
               <asp:BoundColumn HeaderText="ITEM" DataField="item"></asp:BoundColumn>
               
               <asp:BoundColumn HeaderText="Assigned_to" DataField="assigned_to"></asp:BoundColumn>
                <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Windows Update">
                            <ItemTemplate>
                              <asp:CheckBox ID="w_update" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "w_update")) %>' Enabled=false />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Trend AV Update" >
                            <ItemTemplate>
                              <asp:CheckBox ID="trend_update" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "trend_update")) %>' Enabled=false />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Verify Virus Definition"   >
                            <ItemTemplate>
                              <asp:CheckBox ID="virus_def" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "virus_def")) %>' Enabled=false/>
                            </ItemTemplate>
                 </asp:TemplateColumn>
                   <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Enabling Windows Firewall"    >
                            <ItemTemplate> 
                              <asp:CheckBox ID="windows_firewall" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "windows_firewall")) %>' Enabled=false />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Uninstalling Sygate PF"     >
                            <ItemTemplate>
                              <asp:CheckBox ID="sygate_pf" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sygate_pf")) %>' Enabled=false/>
                            </ItemTemplate>
                 </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Clear Internet Cache"     >
                            <ItemTemplate>
                              <asp:CheckBox ID="internet_cache" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "internet_cache")) %>' Enabled=false/>
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Delete Temp Files"     >
                            <ItemTemplate>
                              <asp:CheckBox ID="temp_files" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "temp_files")) %>' Enabled=false />
                            </ItemTemplate>
                 </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Run Scan Disks"     >
                            <ItemTemplate>
                              <asp:CheckBox ID="scan_disk" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "scan_disk")) %>' Enabled=false/>
                            </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Review Event Log"      >
                            <ItemTemplate>
                              <asp:CheckBox ID="event_log" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "event_log")) %>' Enabled=false/>
                            </ItemTemplate>
                 </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Available HD-GB"       >
                            <ItemTemplate>
                              <asp:CheckBox ID="hd_gb" runat=server Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "hd_gb")) %>' Enabled=false/>
                            </ItemTemplate>
                 </asp:TemplateColumn> 
               <asp:BoundColumn HeaderText="sys ID"  DataField="hidden"></asp:BoundColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Check">
                            <ItemTemplate>
                              <asp:CheckBox ID="chkSelect" runat=server />
                            </ItemTemplate>
                        </asp:TemplateColumn>
               
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=8   ForeColor="White" Width="20" Wrap=true  />
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
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       </asp:TableCell>
       <asp:TableCell>
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnAssign" runat=server Text="Edit" OnClick="mAssignProduct" />
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnDelete" runat=server Text="Delete"  OnClick="mBtnDeleteClick" />
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
                    <asp:Label ID="lblCollegeChoicePanel" runat=server Text="Inventory System - Changing Date"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="lblRegStatment" runat=server></asp:Label>
                </span></b>
              
              <%-- The following panel will be used to assign a proudct to a pc tech --%>
              <asp:Panel ID="pnlAssignToPctech" runat=server  >
              <asp:Table ID="tblAssignToPctech" runat=server>
              	<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblStartDate" runat=server Text="Starting Date:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtStartingDate"   Width="150px" runat="server"   ></asp:TextBox>
			 <asp:Image ID="imgDeliveryDt2" runat="server"  ImageUrl="~/Images/date.gif"/>
            <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="CalendarExtender1"  runat="server" PopupButtonID="imgDeliveryDt2" TargetControlID="txtStartingDate" BehaviorID="CalendarExtender1" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			</asp:TableCell>
			</asp:TableRow>
	        	<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblEndingDate" runat=server Text="Ending Date:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtEndingDate"   Width="150px" runat="server"   ></asp:TextBox>
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
             
                <div style="text-align:center;">
                <asp:Button ID="btnAssigng" runat="server" Text="Assign" OnClick="mBtnAssignClicked"     />
                
                &nbsp;<asp:Button ID="btnFileCancel"  runat="server" Text="Close"  /></div>                
            </asp:Panel>
            <%-- End of Assignment Extender --%>
            <%-- Deleting Extender --%>
            <asp:Button ID="btnDeletExtender" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="DeleteExtender" runat="server" 
            TargetControlID="btnDeletExtender"  PopupControlID="pnlDelete" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnFileCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="pnlDelete" runat="server" CssClass="modalPopup" Style="display:none" Width="500">
                 <asp:Panel ID="Panel3" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="Label3" runat=server Text="Inventory System - Deleting Confirmation"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="Label4" runat=server></asp:Label>
                </span></b>
              
              <%-- The following panel will be used to assign a proudct to a pc tech --%>
              <asp:Panel ID="Panel4" runat=server  >
        Do you want to delete the selected items?
              </asp:Panel>
             
                <div style="text-align:center;">
                <asp:Button ID="btnDeleteAcceptance" runat="server" Text="Yes" OnClick="mBtnAcceptDeleteClick"      />
                
                &nbsp;<asp:Button ID="btnDeleteClose"  runat="server" Text="No"  /></div>                
            </asp:Panel>
            <%-- End of deleting Extender --%>
            <%-- specification Extender Control --%>
			<asp:Button ID="btnHidden" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="specificationExtender" runat="server" 
            TargetControlID="btnHidden"  PopupControlID="specificationPnl" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnSpecificationCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="specificationPnl" runat="server" CssClass="modalPopup" Style="display:none" Width="400" Height="500" ScrollBars=Both>
                 <asp:Panel ID="Panel2" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="Label1" runat=server Text="Inventory System"></asp:Label>
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
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       <br />
       <br />
       <div align=center>
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
			</div>
                <div style="text-align:center;">
                <asp:Button ID="btnSpecificationCancel"  runat="server" Text="Close"  /></div>                
            </asp:Panel>
            <%-- End of Assignment Extender --%>
            <%-- The following label will containthe id number of item --%>
            <asp:Label ID="lblItemNumber" runat=server Visible=false></asp:Label>
            <asp:Label ID="lblStartHiddenDate" runat=server Visible=false></asp:Label>
            <asp:Label ID="lblEndHiddenDate" runat=server Visible=false></asp:Label>
            
		<br />                     
			</div>
	</div>
  
</asp:Content>

