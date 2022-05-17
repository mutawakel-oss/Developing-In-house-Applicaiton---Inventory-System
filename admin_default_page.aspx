<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="admin_default_page.aspx.cs" Inherits="Default2" Title="Admin Default Page" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SecondBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
	<div  id="body" style="width:1000px" >
			<div id="col_main_left"  >
			<a id="content_start"></a>
			
			<%-- The following will populate the menues --%>
	  <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="pnlMasterNodes"
        ExpandControlID="pnlMasterTitles"
        CollapseControlID="pnlMasterTitles" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        SkinID="CollapsiblePanelDemo" />
                <asp:Panel ID="pnlMasterTitles" runat="server" CssClass="collapsePanelHeader" Visible=false >
                    <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; vertical-align: middle;
                        cursor: pointer; padding-top: 5px">
                        <div style="float: left"><font size="2">
                            System Master Pages</font></div>
                        
                        <div style="float: left; margin-left: 20px; width: 1px; height: 14px;">
                           </div>
                        <br />
                        <div style="float: right; vertical-align: middle">
                           </div>
                            </div>
                </asp:Panel>
                <asp:Panel ID="pnlMasterNodes" runat="server" CssClass="collapsePanel" Height="1px" Visible=false>
                <asp:ImageButton ID="lnkDepartmentMaster" runat=server ImageUrl="~/images/depar.png" OnClick="mDepartmentLnkClicked" Width="195px" ToolTip="To add a new department." />
                <asp:ImageButton ID="lnkRoomMaster" runat=server ImageUrl="~/images/room.png" OnClick="mRoomLnkClicked" ToolTip="To add a new room." Width="195px" />
                <asp:ImageButton ID="lnkCategoryMaster" runat=server ImageUrl="~/images/category.png" OnClick="mCategoryLnkClicked" ToolTip="To categorize products." Width="195px" />
                <asp:ImageButton ID="lnkVendorMaster" runat=server ImageUrl="~/images/vendor_master.png" OnClick="mVendorLnkClicked" ToolTip="To add a new vendor." Width="195px"/>
                
               </asp:Panel>  
               <%--The following collapsed menue for other events --%>
		  <ajaxToolkit:CollapsiblePanelExtender ID="inventoryProcessCollapsedMenues" runat="Server"
        TargetControlID="pnlInventoryOperationsNodes"
        ExpandControlID="pnlInvenotryOperations"
        CollapseControlID="pnlInvenotryOperations" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        SkinID="CollapsiblePanelDemo" />
                <asp:Panel ID="pnlInvenotryOperations" runat="server" CssClass="collapsePanelHeader" >
                    <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; vertical-align: middle;
                        cursor: pointer; padding-top: 5px">
                        <div style="float: left">
                           <font size="2"> Inventory Operations</font></div>
                       
                        <div style="float: left; margin-left: 20px; width: 1px; height: 14px;">
                            </div>
                        <br />
                        <div style="float: right; vertical-align: middle">
                            </div>
                            </div>
                </asp:Panel>
                <asp:Panel ID="pnlInventoryOperationsNodes" runat="server" CssClass="collapsePanel" Height="1px" >
                <asp:ImageButton ID="lnkPurchaseRequest" runat=server ImageUrl="~/images/purchase_request.png" OnClick="mPurchaseRequestLnkClicked" ToolTip="To add a new purchase request." Visible=false Width="195px"/>
                <asp:ImageButton ID="lnkDelivery" runat=server ImageUrl="~/images/delivery_page.png" OnClick="mDeliveryLnkClicked" ToolTip="To add a new delivered item." Visible=false Width="195px"/>
                <asp:ImageButton ID="lnkItemDetails" runat=server ImageUrl="~/images/item_details.png" OnClick="mItemDetailsLnkClicked" ToolTip="To update item details." Visible=false Width="195px"/>
                <asp:ImageButton ID="lnkAssignment" runat=server ImageUrl="~/images/assignement_page.png" OnClick="mAssignementLnkClicked" ToolTip="To assign an item to (End user,PC Tech,Room)." Visible=false Width="195px"/>
                <asp:ImageButton ID="lnkPreventive" runat=server ImageUrl="~/images/maintaince.png" OnClick="mPreventiveMain" ToolTip="To chck the progress of preventive maintainance process." Visible=false Width="195px"/>
                <asp:ImageButton ID="lnkReport" runat=server ImageUrl="~/images/reports.png" OnClick="mReportsLnkClicked" ToolTip="To display reports." Visible=false Width="195px"/>
                <asp:ImageButton ID="lnkPcTechArea" runat=server ImageUrl="~/images/pc_tech_area.png" OnClick="mpcTechLnkClicked" ToolTip="To display PC-Tech System Area." Visible=false Width="195px"/>
               </asp:Panel>  
               <ajaxToolkit:CollapsiblePanelExtender ID="systemSettingCollapse" runat="Server"
        TargetControlID="pnlSystemSetting2"
        ExpandControlID="pnlSystemSetting"
        CollapseControlID="pnlSystemSetting" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        SkinID="CollapsiblePanelDemo" />
                <asp:Panel ID="pnlSystemSetting" runat="server" CssClass="collapsePanelHeader" Visible=false  >
                    <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; vertical-align: middle;
                        cursor: pointer; padding-top: 5px">
                        <div style="float: left">
                            <font size="2">System Setting</font></div>
                        
                        <div style="float: left; margin-left: 20px; width: 1px; height: 14px;">
                            </div>
                        <br />
                        <div style="float: right; vertical-align: middle">
                            </div>
                            </div>
                </asp:Panel>
                <asp:Panel ID="pnlSystemSetting2" runat="server" CssClass="collapsePanel" Height="1px" Visible=false >
                <asp:ImageButton ID="lnkUsers" runat=server ImageUrl="~/images/users.png" OnClick="mUsersLnkClicked" ToolTip="To manage the users roles." Width="195px"/>
                <asp:ImageButton ID="lnkRequestorMaster" runat=server ImageUrl="~/images/requestor.png" OnClick="mRequestorLnkClicked" ToolTip="To add/edit a requestor." Width="195px"/>
                
               </asp:Panel>  
			<%-- End of menues populating --%>
			</div>
			<div  id="col_main_right" style="background-color:LightBlue; background-repeat: no-repeat;" >
       <div id="AdminWelcomeDiv" align=center>
       <asp:Label ID="welcome" runat=server Text="Welcome in Administration Area."  ForeColor=red Font-Size=larger></asp:Label>
	<br />
       <asp:Image ID="imgInventoryLifeCycle" runat="server" ImageUrl="~/images/inv_diagram.gif" />
       
       </div>
                            
			</div>
	</div>
  
</asp:Content>

