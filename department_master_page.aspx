<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="department_master_page.aspx.cs" Inherits="Default2" Title="Department Master Page" %>
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
            <Header> Search About A Department </Header>
            <Content>
			<div align=center>
			<asp:Label ID="pageTitile" runat=server ForeColor=red Font-Size=Larger Text="Search About A Department" BackColor=lightgray></asp:Label>
			<br />
			<br />
			</div>
			<asp:Table ID="searchTable" runat=server >
			<asp:TableRow runat=server>
			<asp:TableCell runat=server>
			<asp:Label ID="lblSearchWithName" runat=server Text="Search By Department Name:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell runat=server>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:DropDownList ID="txtSearchDepartment" runat=server AutoPostBack=true OnSelectedIndexChanged="mSearchSelected"></asp:DropDownList>
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
            <Header> Add/Update Department Details </Header>
            <Content>

			<div align=center>
			<asp:Label ID="lblAddNewRequestPanel" runat=server ForeColor=red Font-Size=Larger Text="Add/Update Department Details" BackColor=lightgray></asp:Label>
			</div>
			<br />
			<asp:Button ID="Button1" runat=server Text="Clear All Fields" OnClick="mBtnClearClick" />
			<br />
			<br />
			<br />
			<asp:Table ID="vendorDetailsTable" runat=server >
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label id="lblDepartmentName" runat=server Text="Department Name"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtDepartmentName" runat=server  Width="200px"></asp:TextBox>
			<asp:RequiredFieldValidator id="sprSaveRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtDepartmentName" ValidationGroup="DepartmentDetailsGroup"></asp:RequiredFieldValidator>
			</asp:TableCell>
			</asp:TableRow>	
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblDescription" runat=server Text="Description"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:TextBox ID="txtDescription" runat=server TextMode=multiLine Height=100 ></asp:TextBox>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Button ID="btnSubmit" runat=server Text="Add Department" OnClick="mBtnSubmitClick" CausesValidation=true ValidationGroup="vendorDetailsGroup" />
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

