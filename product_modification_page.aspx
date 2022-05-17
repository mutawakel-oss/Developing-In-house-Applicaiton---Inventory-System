<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="product_modification_page.aspx.cs" Inherits="Default2" Title="SPR Items Details Page" %>
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
			<asp:Label ID="lblAssignmentPageTitle" runat=server ForeColor=red Font-Size=Larger Text="SPR Items Details" BackColor=lightgray></asp:Label>
			</div>
			<br />
			<br />
			<br />
			<asp:Table ID="tblSearchCriteria" runat=server>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSelectCriteria" runat=server Text="Enter SPR No"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtSprNoSearch" runat=server ></asp:TextBox>
			<asp:RegularExpressionValidator ID="txtSPRValidator" runat="server" ValidationGroup="sprGroup" ControlToValidate="txtSprNoSearch" ErrorMessage="*" ValidationExpression="\d+"></asp:RegularExpressionValidator>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Label ID="lblStatus" runat=server Text="Status"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSprStatus" runat=server >
			<asp:ListItem Text="Unupdated" Value="1"></asp:ListItem>
			<asp:ListItem Text="Updated" Value="8"></asp:ListItem>
			</asp:DropDownList>
			</asp:TableCell>
			<asp:TableCell>
			<asp:Button ID="btnSearch" runat=server Text="Search" OnClick="mFillItemsGrid" CausesValidation=true ValidationGroup="sprGroup" />
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
		
		
			<%-- The following label for error messages --%>
       <asp:Label ID="lblitemCounterValidity" runat=server ForeColor=red BackColor=white></asp:Label>
			<%-- Items Grid --%>
			  <asp:DataGrid id="productsGrid" runat="server" Width="775px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="mPorductEditCommandClicked" OnCancelCommand="mProductCancelCommandClicked" OnUpdateCommand="mProductUpdateCommandClicked">
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" Font-Size=small />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"  ItemStyle-ForeColor=blue></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="ITEM" DataField="item"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Tag NO"  DataField="tag"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Serial NO"  DataField="serial"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="ROOM" DataField="room"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="SPR#" DataField="spr_no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="sys ID"  DataField="hidden"></asp:BoundColumn>
               
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Size=small  ForeColor="White" />
       </asp:DataGrid>
       <br />
     
       
			
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
	
       <br />
       
       
       <br />
       <br />
       <br />
 
  
     
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

