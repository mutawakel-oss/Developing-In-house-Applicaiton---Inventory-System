<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="category_master.aspx.cs" Inherits="Default2" Title="Categories Master Page" %>
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
        
			<ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Add/Update Category Details </Header>
            <Content>

			<div align=center>
			<asp:Label ID="lblAddNewRequestPanel" runat=server ForeColor=red Font-Size=Larger Text="Add/Update Category Details" BackColor=lightgray></asp:Label>
			</div>
			<br />
			  <asp:Table id="productAddTable" runat="server">
           <asp:TableRow>
           <asp:TableCell>
           <asp:Label ID="lblCategoryName" Font-Size=Small runat=server Text="Category Name:" ></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtCategoryName" BackColor=lightyellow runat=server  Width="100px"></asp:TextBox>
                      <asp:RequiredFieldValidator id="DescriptionNoRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtCategoryName" ValidationGroup="categoryInfoGroup"></asp:RequiredFieldValidator>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Label ID="lblDescription" Font-Size=Small runat=server Text="Description:"></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtDescription"  BackColor=lightyellow runat=server Width="200px"></asp:TextBox>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Button ID="btnAddCategory" runat=server Text="Add New Category" ValidationGroup="categoryInfoGroup" CausesValidation=true OnClick="mAddNewCategory" />
           </asp:TableCell>
           </asp:TableRow>
           </asp:Table>
			<br />
			<br />
			<br />
			  <asp:DataGrid id="CategoryGrid" runat="server" Width="780px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="categoryGrid_EditCommand" OnCancelCommand="productGrid_CancelCommand" OnUpdateCommand="categoryGrid_UpdateCommand" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="Category ID" DataField="cat_id"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Category Name" DataField="cat_name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Category Description" DataField="description"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid> 
			
			
		<br />
       
        </Content>
        </ajaxToolkit:AccordionPane>
        	<ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Add/Update Sub-Category Details </Header>
            <Content>
            <div align=center>
            <asp:Label ID="lblSubSubSelectedId" runat=server ForeColor=red Font-Size=Larger Text="Add/Update Sub-category Details" BackColor=lightgray></asp:Label>
            </div>
			<br />
			  <asp:Table id="sub_category_table" runat="server">
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblParentCategory" runat=server Text="Select Category:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlParentCategory" runat=server AutoPostBack=true OnSelectedIndexChanged="mParentCategoryDDLselected" ></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
           <asp:TableRow>
           <asp:TableCell>
           <asp:Label ID="lblSubCategoryName" Font-Size=Small runat=server Text="Sub-category Name:" ></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtSubCategoryName" BackColor=lightyellow runat=server  Width="100px"></asp:TextBox>
                      <asp:RequiredFieldValidator id="DescriptionNoRequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtSubCategoryName" ValidationGroup="subcategoryInfoGroup"></asp:RequiredFieldValidator>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Label ID="lblSubCatDescription" Font-Size=Small runat=server Text="Description:"></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtSubCatDescription"  BackColor=lightyellow runat=server Width="200px"></asp:TextBox>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Button ID="btnAddSubCategory" runat=server Text="Add New Sub-category" ValidationGroup="subcategoryInfoGroup" CausesValidation=true  OnClick="mAddNewSubCategory" />
           </asp:TableCell>
           </asp:TableRow>
           </asp:Table>
           <br />
            <asp:DataGrid id="sub_category_grid" runat="server" Width="780px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="subCategoryGrid_EditCommand" OnCancelCommand="subCategory_CancelCommand" OnUpdateCommand="subCategoryGrid_UpdateCommand" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="Sub-category ID" DataField="id"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Sub-category Name" DataField="sub_cateogry_name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Sub-category Description" DataField="description"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
               
            </Content>
            </ajaxToolkit:AccordionPane>
        	<ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Add/Update Sub-sub-Category</Header>
            <Content>
            <div align=center>
            <asp:Label ID="Label2" runat=server ForeColor=red Font-Size=Larger Text="Add/Update Sub-sub-category " BackColor=lightgray></asp:Label>
            </div>
			<br />
			  <asp:Table id="sub_sub_category_table" runat="server">
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSelectCategory1" runat=server Text="Select Category:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlParentParentCategory" runat=server AutoPostBack=true OnSelectedIndexChanged="mSubCategoryDDLselect" ></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSelectSubCategory" runat=server Text="Select Sub-category:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSubCategory" runat=server AutoPostBack=true  OnSelectedIndexChanged="mSubSubCategoryDDLSelect" ></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
           <asp:TableRow>
           <asp:TableCell>
           <asp:Label ID="lblSubSub" Font-Size=Small runat=server Text="Sub-sub-category Name:" ></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtSubSubCategoryName" BackColor=lightyellow runat=server  Width="100px"></asp:TextBox>
                      <asp:RequiredFieldValidator id="SubSubRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="txtSubSubCategoryName" ValidationGroup="SubSubcategoryInfoGroup"></asp:RequiredFieldValidator>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Label ID="lblSubSubDescription" Font-Size=Small runat=server Text="Minimum percentage Amount(%):"></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtSubSubDescription"  BackColor=lightyellow runat=server Width="200px"></asp:TextBox>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Button ID="btnSubSubCategoryAdd" runat=server Text="Add New Sub-sub-category" ValidationGroup="SubSubcategoryInfoGroup" CausesValidation=true  OnClick="mAddNewSubSubCategory" />
           </asp:TableCell>
           </asp:TableRow>
           </asp:Table>
           <br />
            <asp:DataGrid id="subSubCategoryGrid" runat="server" Width="780px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="subSubCategoryGrid_EditCommand" OnCancelCommand="SubSubCategory_CancelCommand" OnUpdateCommand="SubSubCategoryGrid_UpdateCommand" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="Sub-sub-category ID" DataField="id"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Sub-sub-category Name" DataField="sub_sub_category_name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Minmum Amount" DataField="description"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
            </Content>
            </ajaxToolkit:AccordionPane>
           <ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Add/Update Sub-sub-Category Details </Header>
            <Content>
            <div align=center>
            <asp:Label ID="lblSubSubDetails" runat=server ForeColor=red Font-Size=Larger Text="Add/Update Sub-sub-category Details" BackColor=lightgray></asp:Label>
            </div>
			<br />
			  <asp:Table id="Table1" runat="server">
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="Label3" runat=server Text="Select Category:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlParentOfSub" runat=server AutoPostBack=true OnSelectedIndexChanged="mSubSubParentDDLselected"   ></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="Label4" runat=server Text="Select Sub-category:"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSubOfSub" runat=server AutoPostBack=true OnSelectedIndexChanged="mSubSubDetailsDdlSelected"   ></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
			<asp:TableRow>
			<asp:TableCell>
			<asp:Label ID="lblSubSub1" runat=server Text="Select Sub-sub-category"></asp:Label>
			</asp:TableCell>
			<asp:TableCell>
			<asp:DropDownList ID="ddlSubSubDropDown" runat=server AutoPostBack=true  OnSelectedIndexChanged="mSubSubCtegoryDetailsSelect"></asp:DropDownList>
			</asp:TableCell>
			</asp:TableRow>
           <asp:TableRow>
           <asp:TableCell>
           <asp:Label ID="Label5" Font-Size=Small runat=server Text="DetialsField:" ></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtSubSubDetails" BackColor=lightyellow runat=server  Width="100px"></asp:TextBox>
                      <asp:RequiredFieldValidator id="subsubDetailsValidator" runat="server" ErrorMessage="*" ControlToValidate="txtSubSubDetails" ValidationGroup="SubSubcategoryDetailsGroup"></asp:RequiredFieldValidator>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Label ID="Label6" Font-Size=Small runat=server Text="Description:"></asp:Label>
           </asp:TableCell>
           <asp:TableCell>
           <asp:TextBox ID="txtSubSubDescription1"  BackColor=lightyellow runat=server Width="200px"></asp:TextBox>
           </asp:TableCell>
           <asp:TableCell>
           <asp:Button ID="btnSubSubDetails" runat=server Text="Add Detils" ValidationGroup="SubSubcategoryDetailsGroup" CausesValidation=true  OnClick="mAddSubSubCategoryDetials"  />
           </asp:TableCell>
           </asp:TableRow>
           </asp:Table>
           <br />
            <asp:DataGrid id="subSubGridDetails" runat="server" Width="780px" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" OnEditCommand="subSubCategoryDetailsGrid_EditCommand" OnCancelCommand="SubSubCategoryDetails_CancelCommand" OnUpdateCommand="SubSubCategoryDetailsGrid_UpdateCommand" >
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
               <asp:BoundColumn HeaderText="Sub-sub-category ID" DataField="id"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Sub-sub-category Name" DataField="detials_field_name"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Sub-sub-category Description" DataField="details_field_description"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid>
            </Content>
            </ajaxToolkit:AccordionPane>
        </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>
           
                <asp:Label ID="lblSelectedId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSubSelectedId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSubSubSelected" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSubSubDetailsSelected" runat="server" Visible="False"></asp:Label><br />
                            
			</div>
	</div>
  
</asp:Content>

