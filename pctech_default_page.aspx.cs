using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.DirectoryServices;

public partial class Default2 : System.Web.UI.Page
{
    System.Data.Odbc.OdbcDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkHome");
            HyperLink LB = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            if (Session.Count == 0)
            {
                Response.Redirect("error_page.aspx?error=Session Expired");
            }
       
            
            LB.Text = "Log Out";
            Hlk.NavigateUrl = "~/pctech_default_page.aspx";
            
            
        }
        catch (Exception exp)
        {
            
        }
    }
    protected void mPreventiveLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This fucntion will be used to open the deparment master page.
        /// Author: mutawakelm
        /// Date :2/14/2009 8:57:25 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            Response.Redirect("~/pc_tec_ maintainance_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mOnlineProfileClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to redirect the user to the page "item_service_history.aspx"
        /// Author: mutawakelm
        /// Date :2/15/2009 1:52:59 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/item_service_history.aspx");
        }
        catch (Exception exp)
        {
        }
    }
  
   
}
