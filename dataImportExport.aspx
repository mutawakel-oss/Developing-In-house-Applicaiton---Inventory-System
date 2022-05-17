<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dataImportExport.aspx.cs" Inherits="dataImportExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
    <asp:Button ID="btnAppend" runat=server Text="Appen" OnClick="mAppend"  Visible=false/>
    <br />
    <br />
    <asp:Button ID="btnAddSprs" runat=server Text="Add sprs" OnClick="mAddSprs" />
    </div>
    </form>
</body>
</html>
