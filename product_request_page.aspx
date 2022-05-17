<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="product_request_page.aspx.cs" Inherits="Default2" Title="Products Request Page" %>
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
			<div  id="col_main_right" style="backgrfound-color:LightBlue; background-repeat: no-repeat;" >
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
        <ajaxToolkit:AccordionPane
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Search About SPR </Header>
            <Content>
			<div align=center>
			<asp:Label ID="pageTitile" runat=server ForeColor=red Font-Size=Larger Text="Search About A Request" BackColor=lightgray></asp:Label>
			<br />
			<br />
			</div>
			<asp:Table ID="searchTable" runat=server >
			<asp:TableRow>
		    <asp:TableCell>
			<asp:Label id="lblStartDate" runat=server Text="Starting Date:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtStartingDate"   Width="140px" runat="server"  ></asp:TextBox>
			<asp:RequiredFieldValidator id="startingDateFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtStartingDate" ValidationGroup="searchingGroup"></asp:RequiredFieldValidator>
			 <asp:Image ID="imgStartingDate" runat="server"  ImageUrl="~/Images/date.gif"/>
			 <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="startingDateExtender"  runat="server" PopupButtonID="imgStartingDate" TargetControlID="txtStartingDate" BehaviorID="startingDateExtender1" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			 </asp:TableCell>
			 <asp:TableCell>
			<asp:Label id="lblEndDate" runat=server Text="Ending Date:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtEndingDate"   Width="140px" runat="server"  ></asp:TextBox>
			<asp:RequiredFieldValidator id="endingDateFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtEndingDate" ValidationGroup="searchingGroup"></asp:RequiredFieldValidator>
			 <asp:Image ID="imgEndingDate" runat="server"  ImageUrl="~/Images/date.gif"/>
			 <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="endingDateExtender"  runat="server" PopupButtonID="imgEndingDate" TargetControlID="txtEndingDate" BehaviorID="endingDateExtender1" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			 </asp:TableCell>
			 </asp:TableRow>
			 <asp:TableRow>
			 <asp:TableCell >
			 <asp:Label ID="lblJustificationSearch" runat=server Text="Justification Starts With"></asp:Label>
			 </asp:TableCell>
			 <asp:TableCell >
			 <asp:TextBox ID="txtJustificationPattern" runat=server Width="200px"></asp:TextBox>
			 </asp:TableCell>
			  <asp:TableCell>
			 <asp:Button ID="btnSearchForSpr" runat=server Text="Search" OnClick="mSearchBtnClicked1" />
			 </asp:TableCell>
			 </asp:TableRow>
			<asp:TableRow runat=server>
			<asp:TableCell runat=server>
			<asp:Label ID="lblSearchWithSpr" runat=server Text="Search By SPR NO:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell runat=server ColumnSpan=7>
			<asp:DropDownList ID="txtSearchSpr" runat=server AutoPostBack=true OnSelectedIndexChanged="mSearchBtnClicked" ></asp:DropDownList>
			</asp:TableCell>		
			<asp:TableCell runat=server>
			<asp:Label ID="lblSearchError" runat=server ></asp:Label>
			</asp:TableCell> 
			</asp:TableRow>
			</asp:Table>
			</Content>
			</ajaxToolkit:AccordionPane>
			<ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Add/Update Request Details</Header>
            <Content>
			<div align=center>
			<asp:Label ID="lblAddNewRequestPanel" runat=server ForeColor=red Font-Size=Larger Text="Add/Update Request Details" BackColor=lightgray></asp:Label>
			</div>
			<br />
			<asp:Button ID="Button1" runat=server Text="Clear All Fields" OnClick="mBtnClearClick" />
			<br />
			<br />
			<br />
			<asp:Table ID="productRequestTable" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblPoNo" runat=server Text="PO No:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtPoNo" runat=server  Width="120px"></asp:TextBox>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblSprActualNo" runat=server Text="SPR No:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtActualSpr" runat=server  Width="120px"></asp:TextBox>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblRequester" runat=server Text="Requesting Department:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlDepartments" runat=server ></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblDeliveryDate" runat=server Text="Expected Delivery Date:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtDeliveryDate"   Width="140px" runat="server"  ></asp:TextBox>
			<asp:RequiredFieldValidator id="deliveryRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtDeliveryDate" ValidationGroup="saveGroup"></asp:RequiredFieldValidator>
			 <asp:Image ID="imgDeliveryDt2" runat="server"  ImageUrl="~/Images/date.gif"/>
     <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="DeliveryExtender"  runat="server" PopupButtonID="imgDeliveryDt2" TargetControlID="txtDeliveryDate" BehaviorID="CalendarExtender1" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="Label1" runat=server Text="Cost Code:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtCostCode"   Width="100px" runat="server"  ></asp:TextBox>
			<asp:RequiredFieldValidator id="RequiredCostCodeFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCostCode" ValidationGroup="saveGroup"></asp:RequiredFieldValidator>
			</asp:TableCell>
			</asp:TableRow>
			 <asp:TableRow ID="TableRow1" runat=server>
        <asp:TableCell ID="TableCell1" runat=server>
        
        <asp:Label ID="RequesetrName"  runat=server Text="Requested By"></asp:Label>
        </asp:TableCell>
        <asp:TableCell ID="TableCell2" runat=server>
        <asp:CheckBox ID="requesterChkBox" runat=server AutoPostBack=True OnCheckedChanged="mCheckBoxChanged" />
        <asp:DropDownList ID="ddlRequesterName" runat=server ></asp:DropDownList>
        <asp:RequiredFieldValidator id="requesterRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="ddlRequesterName" ValidationGroup="saveGroup"></asp:RequiredFieldValidator>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell>
        <asp:Label ID="lblJustification" runat=server Text="Justification"></asp:Label>
        </asp:TableCell>
        <asp:TableCell>
        <asp:TextBox ID="txtJustification" runat=server Width="500px" TextMode=MultiLine></asp:TextBox>
        <asp:RequiredFieldValidator id="justificationValidator" runat="server" ErrorMessage="*" ControlToValidate="txtJustification" ValidationGroup="saveGroup"></asp:RequiredFieldValidator>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell>
			<asp:Label id="lblRequestDate" runat=server Text="Date of request:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtRequestDate"   Width="140px" runat="server"  ></asp:TextBox>
			<asp:RequiredFieldValidator id="requestDateFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtRequestDate" ValidationGroup="saveGroup"></asp:RequiredFieldValidator>
			 <asp:Image ID="imgRequestDate" runat="server"  ImageUrl="~/Images/date.gif"/>
			 <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="requestDateExtenderID"  runat="server" PopupButtonID="imgRequestDate" TargetControlID="txtRequestDate" BehaviorID="requestDateExtender" Enabled="True" ></ajaxToolkit:CalendarExtender>  
			 </asp:TableCell>
        </asp:TableRow>
        	<asp:TableRow>
        	<asp:TableCell>
        	<asp:Label ID="lblSprNo" runat=server Text="Sys ID"></asp:Label>
        	</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtSprNo" runat=server  Width="120px" Enabled=false ></asp:TextBox>
			<asp:RequiredFieldValidator id="sprSaveRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtSprNo" ValidationGroup="saveGroup"></asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="sprRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtSprNo" ValidationGroup="productInfoGroup"></asp:RequiredFieldValidator>
			</asp:TableCell>
			</asp:TableRow>
        <asp:TableRow>
        <asp:TableCell>
        <asp:LinkButton ID="btnDisplaySprStatus" runat=server Text="Spr Status" OnClick="mSprStatusBtnClicked"></asp:LinkButton>
        </asp:TableCell>
        </asp:TableRow>
        
			</asp:Table>
			 <%-- spr status Extender Control --%>
			<asp:Button ID="btnStatusExtendrHidden" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="sprStatusPanelExtender" runat="server" 
            TargetControlID="btnStatusExtendrHidden"  PopupControlID="sprStatusPanelEx" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnSprStatusClosed">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="sprStatusPanelEx" runat="server" CssClass="modalPopup" Style="display:none" Width="800" Height="400" ScrollBars=Both>
                 <asp:Panel ID="Panel3" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="lblSprStatusTitle" runat=server Text="Inventory System - Spr Status Form"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="lblSprStatus" runat=server></asp:Label>
                </span></b>
                <div align=center>
			
			<br />
			<br />
				<%-- The following data grid will hold the spr status information --%>
			
        <asp:DataGrid id="sprStatusGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:BoundColumn HeaderText="Date" DataField="date"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Progress" DataField="progress"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Reported By" DataField="reported_by"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
       <asp:Table ID="tblAddNewStatus" runat=server>
       <asp:TableRow>
       <asp:TableCell>
       <asp:Label ID="lblSprReport" runat=server Text="Enter new SPR status:"></asp:Label>
       </asp:TableCell>
       <asp:TableCell>
       <asp:TextBox ID="txtSprStatus" runat=server TextMode=multiLine Height="100px"></asp:TextBox>
       <asp:RequiredFieldValidator id="sprStatusValidator" runat="server" ErrorMessage="*" ControlToValidate="txtSprStatus" ValidationGroup="statusGroup"></asp:RequiredFieldValidator>
       </asp:TableCell>
       <asp:TableCell>
       <asp:Button ID="btnAddNewStatus" runat=server Text="Add Status" OnClick="mAddStatusClicked" CausesValidation=true ValidationGroup="statusGroup" />
       </asp:TableCell>
       </asp:TableRow>
       </asp:Table>
			</div>
                <div style="text-align:center;">
                <asp:Button ID="btnSprStatusClosed"  runat="server" Text="Close"  /></div>                
            </asp:Panel>
            <%--The end of specification extender --%>
			<asp:UpdatePanel ID="UpdatePanel2" 
                             UpdateMode="Conditional"
                             runat="server">
                            <ContentTemplate>
                            <FIELDSET>
          
           <hr />
           <div align=center>
           <asp:Label ID="lblCurrentItemPanel" runat=server ForeColor=red Font-Size=Larger Text="SPR Current Items" BackColor=lightgray></asp:Label>
           </div>
           <asp:DataGrid id="productsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="productsGrid_EditCommand" OnUpdateCommand="productsGrid_UpdateCommand" OnDeleteCommand="DataGrid1_DeleteCommand" OnCancelCommand="productGrid_CancelCommand" >
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
               <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
               
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
        <div align=center>
        <hr />
           
           <br />
       <%-- specification Extender Control --%>
			<asp:Button ID="btnHidden" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="specificationExtender" runat="server" 
            TargetControlID="btnHidden"  PopupControlID="specificationPnl" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnSpecificationCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="specificationPnl" runat="server" CssClass="modalPopup" Style="display:none" Width="400" Height="400" ScrollBars=Both>
                 <asp:Panel ID="Panel2" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="Label2" runat=server Text="Inventory System"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="Label3" runat=server></asp:Label>
                </span></b>
                <div align=center>
			<asp:Label ID="lblItemDisplay" runat=server ForeColor=red Font-Size=Larger Text="Item Specification List" BackColor=lightgray></asp:Label>
			<br />
			<br />
				<%-- The following tags for the data grid --%>
			 <asp:DataGrid id="specificationDataGrid" runat="server" Width="302px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False"  >
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
            <%--The end of specification extender --%>
             <%-- new item addition Extender Control --%>
			<asp:Button ID="btnHidden2" Style="display: none;" runat="server" Text="Fake" />
			       <ajaxToolkit:ModalPopupExtender ID="newItemAdingExtender" runat="server" 
            TargetControlID="btnHidden2"  PopupControlID="addingNewItem" 
            BackgroundCssClass="modalBackground" 
            DropShadow="false"  CancelControlID="btnAddingNewItemCancel">
            </ajaxToolkit:ModalPopupExtender>
                <asp:Panel Direction=leftToRight  ID="addingNewItem" runat="server" CssClass="modalPopup" Style="display:none" Width="1100" Height="400" ScrollBars=Both>
                 <asp:Panel ID="Panel22" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <asp:Label ID="lblPanelTitle" runat=server Text="Inventory System - Adding New Item Form"></asp:Label>
                </div>
            </asp:Panel>
                <b>
                <span >
                <asp:Label  ID="lblHidding" runat=server></asp:Label>
                </span></b>
                <div align=center>
			
			<br />
			<br />
				<%-- The following tags for the data grid --%>
			
           
           </div>
           <asp:Table id="productAddTable" runat="server">
           <asp:TableRow>
           <asp:TableCell ID="tblCellTitle" runat="server">
           Category
           </asp:TableCell>
             <asp:TableCell ID="TableCell6" runat="server">
			<asp:DropDownList ID="ddlCategory" runat=server AutoPostBack=True OnSelectedIndexChanged="mSubCategoryDDLselected" ></asp:DropDownList>
			<asp:RequiredFieldValidator id="categorySaveRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory" ValidationGroup="productInfoGroup"></asp:RequiredFieldValidator>
			</asp:TableCell>
			<asp:TableCell ID="TableCell7" runat="server">
			<asp:DropDownList ID="ddlSubCategory" runat=server OnSelectedIndexChanged="mSubSubCategoryDDLselected" AutoPostBack=True ></asp:DropDownList>
			<asp:RequiredFieldValidator id="subCategoryRequiredValidator" runat="server" ErrorMessage="*" ControlToValidate="ddlSubCategory" ValidationGroup="productInfoGroup"></asp:RequiredFieldValidator>
			</asp:TableCell>
			 <asp:TableCell ID="TableCell8" runat="server">
			<asp:DropDownList ID="ddlSub    SubCategory" runat=server ></asp:DropDownList>
		    <asp:RequiredFieldValidator id="subSubCategoryRequiredValidator" runat="server" ErrorMessage="*" ControlToValidate="ddlSubSubCategory" ValidationGroup="productInfoGroup"></asp:RequiredFieldValidator>
			</asp:TableCell>
          
           <asp:TableCell>
           <asp:Label ID="lblCatNo" Font-Size=Small runat=server Text="QTY:"></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txt_cat_no"  runat=server Width="70px"></asp:TextBox>
           <asp:RequiredFieldValidator id="catNoRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txt_cat_no" ValidationGroup="productInfoGroup"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="txtCatNoValidator" runat="server" ValidationGroup="productInfoGroup" ControlToValidate="txt_cat_no" ErrorMessage="*" ValidationExpression="\d+"></asp:RegularExpressionValidator>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Label ID="orderUnits" Font-Size=Small runat=server Text="U/PRICE:"></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txt_unit_no"  runat=server Width="70px"></asp:TextBox>
           <asp:RequiredFieldValidator id="unitNoRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txt_unit_no" ValidationGroup="productInfoGroup"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="txtUnitNoValidator" runat="server" ValidationGroup="productInfoGroup" ControlToValidate="txt_unit_no" ErrorMessage="*" ValidationExpression="\d+\.\d{2}"></asp:RegularExpressionValidator>
           </asp:TableCell>
         
           
           </asp:TableRow>
           <asp:TableRow>
           <asp:TableCell columnspan=6>
           &nbsp;&nbsp;
           <asp:Label id="lblCatalogNo" runat=server text="Cat No"></asp:Label>
      
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox id="txtCatalogNo" runat=server></asp:TextBox>
       &nbsp;&nbsp;
        
           <asp:Label id="lblUnit" runat=server text="Unit of Order"></asp:Label>
      &nbsp;&nbsp;
       
           <asp:TextBox id="txtUnitOfOrder" runat=server ></asp:TextBox>
        &nbsp;&nbsp;
         
           <asp:Button ID="btnSumitProduct" runat=server Text="Add" OnClick="mBtnSpecificationClicked" CausesValidation=true ValidationGroup="productInfoGroup"  />
           </asp:TableCell>
           </asp:TableRow>
           </asp:Table> <BR />
             <asp:DataGrid ID="deliveryDetialsGrid" Font-Size="Medium" Font-Names="Simplified Arabic" 
                        AutoGenerateColumns="false"  DataSourceID="subSubCategoryDetails"  runat="server"  AllowPaging=false  
              PagerStyle-Mode="NumericPages"
              PagerStyle-Position="Top"
              PagerStyle-HorizontalAlign="Center"
              PagerStyle-CssClass="pageLinks">
        <Columns>
            <asp:TemplateColumn HeaderStyle-VerticalAlign=Top HeaderStyle-Width="120px" headertext="Property">
                <ItemTemplate>
                    <table dir=ltr align="right" cellpadding="0" cellspacing="0" style="cursor:default;">
                        <tr>
                            <td style="border: 0px;">                                           
                            <span>
                                <asp:Label ID="lblPropertyTitle" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "detials_field_name") %>' ></asp:Label>
                            </span>
                            </td>                                       
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateColumn>         
                <asp:TemplateColumn HeaderStyle-VerticalAlign=Top HeaderStyle-Width="150px" headertext="Description">
                <ItemTemplate>
                    <table dir=ltr align="right" cellpadding="0" cellspacing="0" style="cursor:default;">
                        <tr>
                            <td style="border: 0px;">                                           
                            <span>
                                <asp:TextBox ID="txtPropertyDescription" runat=server Width="150px" ></asp:TextBox>
                            </span>
                            </td>                                       
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateColumn>         
            </Columns>
            </asp:DataGrid>
            <asp:Button ID="btnSubmitItems" runat=server Text="Submit" OnClick="btnSubmitItems_Click" Visible=false />
        <div style="text-align:center;">
                <asp:Button ID="btnAddingNewItemCancel"  runat="server" Text="Close"  /></div>  
			</div>
                             
            </asp:Panel>
            <%--The end of new item addition extender --%>
       <DIV></DIV></FIELDSET> 
       
</ContentTemplate>
        </asp:UpdatePanel><br />
        <asp:Table ID="requesterTable" runat=server Width="375px" >
       
        <asp:TableRow  runat=server>
        <asp:TableCell>
        <asp:Button ID="btnAddNewItemExtender" runat=server Text="Add New Item" OnClick="mAddNewItemClicked" />
        </asp:TableCell>
        <asp:TableCell  runat=server>
        <asp:Button ID="submitRequest" runat=server Text="Save Request" ValidationGroup="saveGroup" OnClick="mSaveRequest"   />
        </asp:TableCell>
        <asp:TableCell>
        <asp:Button ID="btnPrintSpr" runat=server Text="Print Spr Form" OnClick="mPrintBtnClicked"  Visible=false/>
        </asp:TableCell>
        <asp:TableCell>
        <asp:Button ID="btnAddDeliveryForSpr" runat=server Text="Add deliverd Items" OnClick="mAddDeliveredItemClicked"  Visible=false/>
        </asp:TableCell>
        </asp:TableRow>
        
        </asp:Table>
        </Content>
        </ajaxToolkit:AccordionPane>
        </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>

        <asp:Label ID="lblItemNumber" runat=server Visible=false></asp:Label>
        
        <br />
        
                             
			</div>
	</div>
    <asp:DataGrid ID="DataGrid1" runat="server"    >
        <Columns>
            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
            
            
        </Columns>
    </asp:DataGrid>
     
</asp:Content>

