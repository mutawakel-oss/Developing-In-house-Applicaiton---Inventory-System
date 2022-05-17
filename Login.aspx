<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" CodeFile="Login.aspx.cs"
	Inherits="Login_aspx" Title="Inventory System Login" Culture="auto" meta:resourcekey="PageResource1" UICulture="en-US" %>
	<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="Main" runat="server" >
	<div  id="body" style="width:1000px" >
		<div id="col_main_left">
			<div id="user_assistance" style="background-image:url(./Images/back1.jpg)">
				<a id="content_start"></a>
				<h3 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' style="background-image:url(./Images/back1.jpg)">
					<asp:Label runat="server" ID="lblhelplink" Text="Links" meta:resourcekey="lblhelplinkResource1"></asp:Label>					
					
					
					</h3>
					
					</div>	
					
		</div>
		<div  id="col_main_right" style="background-image:url(./Images/body_ground.JPG); background-repeat: no-repeat;" >
			<h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section" style="background-image:url(./Images/back1.jpg)">
			<asp:Label runat="server" ID="Label9" Text="Login" meta:resourcekey="Label9Resource1"></asp:Label>	
			<asp:LinkButton  runat="server" ID="arabicLang" Text="ÚÑÈí" OnClick="arabicLogin" Font-Bold="True" Font-Size="Medium" ForeColor="Black" Visible="False" ></asp:LinkButton><br />
                &nbsp;</h2>
			<div  runat="server" dir='<%$ Resources:MulResource, TextDirection %>'   class="content_right" style="width:788px"  >
                &nbsp;<asp:Table   ID="Table1" runat="server" BorderColor="MidnightBlue" BorderWidth="0px"  Height="134px" BorderStyle="Ridge">
					<asp:TableRow Width="100%" runat="server">
					<asp:TableCell Width="100%" runat="server"><asp:Table runat="server" BorderColor="Blue" BorderWidth="0px"><asp:TableRow runat="server"><asp:TableCell runat="server">
					
					
			
					<hr />
					</asp:TableCell>
</asp:TableRow>
</asp:Table>
</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow>
					<asp:TableCell>
					<asp:Label ID="lblDeptName" runat=server Text="Department"></asp:Label>
					&nbsp;&nbsp;&nbsp;
					<asp:DropDownList ID="ddlDepartmentName" runat=server ></asp:DropDownList>
					</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow  runat="server">
					<asp:TableCell runat="server">
					<asp:Login ID="LoginConrol" runat="server" TitleText="" UserNameLabelText="User Name" PasswordLabelText="Password" CssClass="login_box" OnAuthenticate="LoginConrol_Authenticate" RememberMeText="Login as Local Admin" meta:resourcekey="LoginConrolResource1">
					<TextBoxStyle CssClass="text"></TextBoxStyle>
				</asp:Login>
					</asp:TableCell>
					</asp:TableRow>				
					</asp:Table>
					
			
                <asp:Label ID="languageLabel" runat="server" Text="en-US" Visible="False"></asp:Label>
                <asp:Label ID="startFlag" runat="server" Text="first" Visible="False"></asp:Label><%--<asp:DropDownList ID ="ddlLogin" runat="server">
				<asp:ListItem> Med</asp:ListItem>
				<asp:ListItem>Local Administrator</asp:ListItem>
				</asp:DropDownList>--%><%--<p>
					<asp:HyperLink ID="RegisterLink" runat="server" NavigateUrl="~/Register.aspx">Create an Account</asp:HyperLink>
				</p>
				<p>
					<asp:LinkButton ID="ForgotPasswordButton" runat="server" OnClick="ForgotPasswordButton_Click">Forgot Password?</asp:LinkButton>
				</p>
				<asp:PasswordRecovery ID="PasswordRecovery" runat="server" Visible="False" UserNameTitleText=""
					QuestionTitleText="Step 2: Identity Confirmation." UserNameInstructionText="Step 1: Enter your User Name."
					Width="280px" OnInit="PasswordRecovery_Init" OnSendMailError="PasswordRecovery_SendMailError">
					<TitleTextStyle Font-Bold="True"></TitleTextStyle>
					<InstructionTextStyle Font-Bold="True"></InstructionTextStyle>
					<LabelStyle Wrap="False" />
				</asp:PasswordRecovery>--%><br /><br /><br />
				<asp:Panel  ID="AccessNoticePanel" runat="server" EnableViewState="False" Visible="False">				
				</asp:Panel>
				
				<br /><br /><br /><br /><br /><br /><br />
			</div>
		</div>
	</div>
</asp:Content>

