<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="delivery_page.aspx.cs" Inherits="Default2" Title="Delivery Entry Page" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SecondBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
  <asp:SqlDataSource runat="server" ID="subSubCategoryDetails"
    ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString1 %>"></asp:SqlDataSource>   
	<div  id="body" style="width:1000px" >
			<div id="col_main_left">
			<a id="content_start"></a>
			Links
			</div>
				<div  id="col_main_right" style="background-color:LightBlue; background-repeat: no-repeat;" >
			 <ajaxToolkit:Accordion
    ID="MyAccordion"
    runat="Server"
    SelectedIndex="0"
    HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    ContentCssClass="accordionContent"
    AutoSize="None"
    FadeTransitions="true"
    TransitionDuration="250"
    FramesPerSecond="40"
    RequireOpenedPane="false"
    SuppressHeaderPostbacks="true">
    <Panes>
             
			<ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header>Search About SPR</Header>
            <Content>
            
 	<div align=center>
			<asp:Label ID="pageTitile" runat=server ForeColor=red Font-Size=Larger Text="Search About A Request" BackColor=lightgray></asp:Label>
			<br />
			<br />
			</div>
			<asp:Table ID="searchTable" runat=server >
			<asp:TableRow ID="TableRow2" runat=server>
			<asp:TableCell ID="TableCell11" runat=server>
			<asp:Label ID="lblSearchWithSpr" runat=server Text="Search By SPR NO:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell ID="TableCell12" runat=server>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:TextBox ID="txtSearchSpr" runat=server ></asp:TextBox>
			<asp:RegularExpressionValidator ID="txtSprNoValidator" runat="server" ValidationGroup="SearchSprGroup" ControlToValidate="txtSearchSpr" ErrorMessage="*" ValidationExpression="\d+"></asp:RegularExpressionValidator>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Button ID="btnSearchSpr" runat=server Text="Search" OnClick="mSearchButtonClicked" CausesValidation=true ValidationGroup="SearchSprGroup" />
			</asp:TableCell>
			<asp:TableCell ID="TableCell14" runat=server>
			<asp:Label ID="lblSearchError" runat=server ></asp:Label>
			</asp:TableCell> 
			</asp:TableRow>
			</asp:Table>
                             
		 <asp:DataGrid id="sprProductsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="SprproductsGrid_EditCommand" OnDeleteCommand="sprProductsGrid_deleteCommand" Visible=false >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Detials" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ITEM NO" DataField="item_no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="DESCRIPTION" DataField="description"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="QTY" DataField="cat_no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="UNIT OF ORDER" DataField="unit_order"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="TOTAL" DataField="total"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="sys ID"  DataField="hidden"></asp:BoundColumn>
               <asp:ButtonColumn CommandName="Delete" Text="Add delivery"></asp:ButtonColumn>
               
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
                <br />
			  </Content>
        </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Recived Items </Header>
            <Content>
			<asp:Label ID="lblSprItemsTitle" runat=server Text="Total SPR Items Number:" ForeColor=blue></asp:Label>
			<asp:Label ID="lblSprItemsCount" runat=server Text="0"></asp:Label>
			<br />
             <asp:DataGrid id="productsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="productsGrid_EditCommand" OnUpdateCommand="productsGrid_UpdateCommand" OnDeleteCommand="DataGrid1_DeleteCommand" OnCancelCommand="productGrid_CancelCommand" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=small />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Profile" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="CATEGORY" DataField="item"></asp:BoundColumn>
               
               <asp:BoundColumn HeaderText="ROOM" DataField="room"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="VENDOR" DataField="vendor"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SPR#" DataField="spr_no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC DATE" DataField="rec_date"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC BY" DataField="rec_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="INS BY" DataField="insp_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="APPR BY" DataField="appro_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="ASS TO" DataField="assigned_to"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="sys ID"  DataField="hidden"></asp:BoundColumn>
               <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn> 
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=small  ForeColor="White" />
       </asp:DataGrid>
            </Content>
            </ajaxToolkit:AccordionPane>
           <ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header>Inspecting Delivered Items   </Header>
            <Content>
            <asp:Panel ID="inspectionPanel" runat=server Visible=false>
            <div align=center>
            <br />
            
			<asp:Label ID="lblInspectionTitle" runat=server ForeColor=Red Font-Size=Larger Text="Inspecting Delivered Items" BackColor=LightGray></asp:Label>
			</div>
			<br />
			<asp:Label ID="lblInsepctedIemsCountTitle" runat=server Text="Total Uninspected Items Number:" ForeColor=blue></asp:Label>
			<asp:Label ID="lblInsepctedIemsCount" runat=server Text="0"></asp:Label>
			<br />
             <asp:DataGrid id="inspectionDataGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mInspectGridDisplay" OnDeleteCommand="inpectGrid_EditCommand"  >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:EditCommandColumn EditText="Profile" ></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ITEM" DataField="item"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="ROOM" DataField="room"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="VENDOR" DataField="vendor"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SPR#" DataField="spr_no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC DATE" DataField="rec_date"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC BY" DataField="rec_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="sys ID"  DataField="hidden"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Status"  DataField="status"></asp:BoundColumn>
                 <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Action" >
                            <ItemTemplate>
                              <asp:DropDownList Width="80px" Font-Size=smaller ID="ddlItemStatus" runat=server   >
                              <asp:ListItem Text=""  Value="0"></asp:ListItem>
                              <asp:ListItem Text="Inspected" Value="2"></asp:ListItem>
                              <asp:ListItem Text="Matched" Value="6"></asp:ListItem>
                              <asp:ListItem Text="Unmatched" Value="7"></asp:ListItem>
                              </asp:DropDownList>
                            </ItemTemplate>
                 </asp:TemplateColumn>
               <asp:ButtonColumn CommandName="Delete" Text=">"></asp:ButtonColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       <asp:Table ID="tblInspectedItemsStatus" runat=server >
       <asp:TableRow>
       <asp:TableCell>
       <asp:Label ID="lblInspctedItems" runat=server Text="Select status"></asp:Label>
       </asp:TableCell>
       <asp:TableCell>
       <asp:DropDownList ID="ddlInspectedItemsStatus" runat=server >
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                              <asp:ListItem Text="Inspected" Value="2"></asp:ListItem>
                              <asp:ListItem Text="Matched" Value="6"></asp:ListItem>
                              <asp:ListItem Text="Unmatched" Value="7"></asp:ListItem>
                              </asp:DropDownList>
                              <asp:CompareValidator ID="inspectAllDllValidator"   ControlToValidate="ddlInspectedItemsStatus" Operator="notEqual" ValueToCompare="0" ErrorMessage="*" runat="server" ValidationGroup="inspectAllGroup"  /> 
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnInspectAllItems" runat=server Text="Inspect All" OnClick="mInspectAll" CausesValidation=true ValidationGroup="inspectAllGroup" />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
             </asp:Panel>
            </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header>Approving Delivered Items   </Header>
            <Content>
            <asp:Panel ID="pnlApproveItems" runat=server Visible=false>
            <div align=center>
            <br />
            
			<asp:Label ID="lblApprovingItems" runat=server ForeColor=Red Font-Size=Larger Text="Approving Delivered Items" BackColor=LightGray></asp:Label>
			</div>
			<br />
		    <asp:Label ID="lblUnapprovedItemsCountTitle" runat=server Text="Total Unapproved Items Number:" ForeColor=blue></asp:Label>
			<asp:Label ID="lblUnapprovedItemsCount" runat=server Text="0"></asp:Label>
			<br />
            <asp:DataGrid id="ApprovingGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mApproveGridDisplay" OnDeleteCommand="approvalGrid_EditCommand"  >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=smaller  />
           <Columns>
               <asp:EditCommandColumn EditText="Profile" ></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ITEM" DataField="item"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="ROOM" DataField="room"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="VENDOR" DataField="vendor"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SPR#" DataField="spr_no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC DATE" DataField="rec_date"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="REC BY" DataField="rec_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="INSP BY" DataField="insp_by"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="sys ID"  DataField="hidden"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Status"  DataField="status"></asp:BoundColumn>
                  <asp:TemplateColumn HeaderStyle-VerticalAlign="Top"  headertext="Action"     >
                            <ItemTemplate>
                              <asp:DropDownList ID="ddlApprovedItemStatus" runat=server >
                              <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                              <asp:ListItem Text="Rejected" Value="4"></asp:ListItem>
                              </asp:DropDownList>
                            </ItemTemplate>
                 </asp:TemplateColumn>
               <asp:ButtonColumn CommandName="Delete" Text=">"></asp:ButtonColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
        <asp:Table ID="tblApproveAllStatus" runat=server >
       <asp:TableRow>
       <asp:TableCell>
       <asp:Label ID="lblApproveAllStatus" runat=server Text="Select status"></asp:Label>
       </asp:TableCell>
       <asp:TableCell>
       <asp:DropDownList ID="ddlApproveAllStatus" runat=server >
                              <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                              <asp:ListItem Text="Rejected" Value="4"></asp:ListItem>
                              </asp:DropDownList>
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnApproveAll" runat=server Text="Approve All" OnClick="mApprovAll" />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
       </asp:Panel>
            </Content>
            </ajaxToolkit:AccordionPane>
           
    </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>
            <%-- specification Extender Control --%>
			<asp:Button ID="btnHidden" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="specificationExtender" runat="server" 
            TargetControlID="btnHidden"  PopupControlID="specificationPnl" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnSpecificationCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="specificationPnl" runat="server" CssClass="modalPopup" Style="display:none" Width="400" Height="800" ScrollBars=Both>
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
       
			</div>
                <div style="text-align:center;">
                <asp:Button ID="btnSpecificationCancel"  runat="server" Text="Close"  /></div>                
            </asp:Panel>
            <%-- End of Assignment Extender --%>
                <%-- adding new delivery Extender Control --%>
			<asp:Button ID="newDeliveryBtn" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="newDeliveryExtender" runat="server" 
            TargetControlID="newDeliveryBtn"  PopupControlID="newDeliveryPanel" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnNewDeliveryClose">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="newDeliveryPanel" runat="server" CssClass="modalPopup" Style="display:none" Width="800" Height="300" ScrollBars=Both>
                 <asp:Panel ID="Panel3" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="newDeliveryExtenderTtile" runat=server Text="Inventory System - New Delivery"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="neewDeliveryHiddenLabel" runat=server></asp:Label>
                </span></b>
                <div align=center>
			
			<br />
			<br />
				<%-- The following tags for the data grid --%>
			<asp:Table ID="tblNewDeliveryAdd" runat=server>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSprQuantity" runat=server Text="Quantity"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSprQuantity" runat=server ></asp:DropDownList>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblSprRoomName" runat=server Text="Room" ></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtRoomName" runat=server ></asp:TextBox>
			<asp:RequiredFieldValidator id="roomValidator" runat="server" ErrorMessage="*" ControlToValidate="txtRoomName" ValidationGroup="AddNewDelivery"></asp:RequiredFieldValidator>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblVendorName" runat=server Text="Vendor"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlVendorName" runat=server ></asp:DropDownList>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblProductsDeliveryDate" runat=server Text="Delivery Date"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtDeliveryDate"   Width="100px" runat="server"></asp:TextBox>
			 <asp:Image ID="imgDeliveryDt2" runat="server"  ImageUrl="~/Images/date.gif"/>
			 <asp:RequiredFieldValidator id="deliveryDateRequiredValidator" runat="server" ErrorMessage="*" ControlToValidate="txtDeliveryDate" ValidationGroup="AddNewDelivery"></asp:RequiredFieldValidator>
     <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="CalendarExtender1"  runat="server" PopupButtonID="imgDeliveryDt2" TargetControlID="txtDeliveryDate" BehaviorID="CalendarExtender1" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			</asp:TableCell>
			<asp:TableCell>
			<asp:Button ID="btnAddNewDelivery" runat=server Text="Add Delivery" OnClick="mAddNewDeliveryClicked" CausesValidation=true ValidationGroup="AddNewDelivery" />
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell ColumnSpan=4>
			<asp:Label ID="lblNewItemError" runat=server ForeColor=red Visible=false></asp:Label>
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			</div>
                <div style="text-align:center;">
                <asp:Button ID="btnNewDeliveryClose"  runat="server" Text="Close"  /></div>                
            </asp:Panel>
            <%-- End of adding new delivery Extender --%>
            <asp:Label ID="lblItemNumber" runat=server Visible=false></asp:Label>
            <asp:Label ID="lblQuantityAmount" runat=server Visible=false></asp:Label>
            <asp:Label ID="lblSprNo1" runat=server Visible=false></asp:Label>
	</div>
	</div>
</asp:Content>

