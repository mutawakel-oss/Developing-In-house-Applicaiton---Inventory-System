<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pctech_default_page.aspx.cs" Inherits="Default2" Title="PC Tech Default Page" %>
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
                <asp:Panel ID="pnlMasterTitles" runat="server" CssClass="collapsePanelHeader" >
                    <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; vertical-align: middle;
                        cursor: pointer; padding-top: 5px">
                        <div style="float: left">
                            Help and Other Links<asp:ImageButton ID="Image2" runat="server" AlternateText="(Show Details...)" ImageUrl="~/images/expand_blue.jpg" /></div>
                        
                        <div style="float: left; margin-left: 20px; width: 1px; height: 14px;">
                            &nbsp;</div>
                        <br />
                        <div style="float: right; vertical-align: middle">
                            &nbsp;&nbsp;&nbsp;</div>
                            </div>
                </asp:Panel>
                <asp:Panel ID="pnlMasterNodes" runat="server" CssClass="collapsePanel" Height="1px" >
                
                <asp:LinkButton ID="lnkAddNewServicReport" runat=server Text="Items Online Profile" OnClick="mOnlineProfileClicked" Font-Size=small ToolTip="To display item online profile and add a service."></asp:LinkButton>
                <br />
                
                <asp:LinkButton ID="lnkPreventiveMaintainace" runat=server Text="Preventive Maintainance" OnClick="mPreventiveLnkClicked" Font-Size=small ToolTip="To process the preventive maintainance list."></asp:LinkButton>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
               </asp:Panel>  
			
	
	
         
			<%-- End of menues populating --%>
			</div>
			<div  id="col_main_right" style="background-color:LightBlue; background-repeat: no-repeat;" >
       <div id="PcTechWelcomeDiv" align=center>
       <asp:Label ID="pcTechWelcome" runat=server Text="Welcome in PC Technician Area." ForeColor=red Font-Size=larger></asp:Label>
       
    
       </div>
                            
			</div>
	</div>
  
</asp:Content>

