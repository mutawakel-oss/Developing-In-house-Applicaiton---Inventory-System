<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vendor_master_page.aspx.cs" Inherits="Default2" Title="Vendor Page" %>
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
            <Header> Search About A Vendor </Header>
            <Content>
			<div align=center>
			<asp:Label ID="pageTitile" runat=server ForeColor=red Font-Size=Larger Text="Search About A Vendor" BackColor=lightgray></asp:Label>
			<br />
			<br />
			</div>
			<asp:Table ID="searchTable" runat=server >
			<asp:TableRow runat=server>
			<asp:TableCell runat=server>
			<asp:Label ID="lblSearchWithName" runat=server Text="Search By Vendor Name:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell runat=server>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:DropDownList ID="txtSearchVendor" runat=server AutoPostBack=true OnSelectedIndexChanged="mSearchSelected"></asp:DropDownList>
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
            <Header> Add/Update Vendor Details </Header>
            <Content>

			<div align=center>
			<asp:Label ID="lblAddNewRequestPanel" runat=server ForeColor=red Font-Size=Larger Text="Add/Update Vendor Details" BackColor=lightgray></asp:Label>
			</div>
			<br />
			<asp:Button ID="Button1" runat=server Text="Clear All Fields" OnClick="mBtnClearClick" />
			<br />
			<br />
			<br />
			<asp:Table ID="vendorDetailsTable" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblVendorName" runat=server Text="Vendor Name"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtVendorName" runat=server  Width="200px"></asp:TextBox>
			<asp:RequiredFieldValidator id="sprSaveRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtVendorName" ValidationGroup="vendorDetailsGroup"></asp:RequiredFieldValidator>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblContactName" runat=server Text="Contact Name"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtContactName" runat=server ></asp:TextBox>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblContactTitle" runat=server Text="Contact Title"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtContactTitle" runat=server></asp:TextBox>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblVendorAddress" runat=server Text="Address"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtVendorAddress" runat=server Width="300px" Height="100px"  TextMode=multiline ></asp:TextBox>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblVendorCity" runat=server Text="City"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtVendorCity" runat=server ></asp:TextBox>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblVendorTelephone1" runat=server Text="Telephone 1"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtTelephone1" runat=server MaxLength=8 ></asp:TextBox>
			<asp:RegularExpressionValidator ID="telephone1Validator" runat="server" ValidationGroup="vendorDetailsGroup" ControlToValidate="txtTelephone1" ErrorMessage="*" ValidationExpression="\d{8}"></asp:RegularExpressionValidator>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblVendorTelephone2" runat=server Text="Telephone2"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox id="txtTelephone2" runat=server MaxLength=8 ></asp:TextBox>
			<asp:RegularExpressionValidator ID="telephone2Validator" runat="server" ValidationGroup="vendorDetailsGroup" ControlToValidate="txtTelephone2" ErrorMessage="*" ValidationExpression="\d{8}"></asp:RegularExpressionValidator>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblMobile" runat=server Text="Mobile"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtMobile" runat=server MaxLength=9></asp:TextBox>
			<asp:RegularExpressionValidator ID="mobileValidator" runat="server" ValidationGroup="vendorDetailsGroup" ControlToValidate="txtMobile" ErrorMessage="*" ValidationExpression="\d{9}"></asp:RegularExpressionValidator>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblFax" runat=server Text="Fax"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtFax" runat=server MaxLength=8></asp:TextBox>
			<asp:RegularExpressionValidator ID="faxValidator" runat="server" ValidationGroup="vendorDetailsGroup" ControlToValidate="txtFax" ErrorMessage="*" ValidationExpression="\d{8}"></asp:RegularExpressionValidator>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Button ID="btnSubmit" runat=server Text="Add Vendor" OnClick="mBtnSubmitClick" CausesValidation=true ValidationGroup="vendorDetailsGroup" />
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
			
		<br />
       
        </Content>
        </ajaxToolkit:AccordionPane>
        </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>
                <asp:Label ID="lblVendorId" runat="server" Visible="False"></asp:Label><br />
                            
			</div>
	</div>
  
</asp:Content>

