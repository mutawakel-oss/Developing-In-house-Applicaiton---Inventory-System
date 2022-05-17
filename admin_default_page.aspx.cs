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
            Hlk.NavigateUrl = "~/admin_default_page.aspx";
            mCheckAuthority();
            
            
            
        }
        catch (Exception exp)
        {
            
        }
    }
    protected void mCheckAuthority()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to check what are the administrative privilges for the user.
        /// Author: mutawakelm
        /// Date :2/28/2009 11:32:43 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (null != Session["spr_creator"])
                if (Session["spr_creatror"].ToString() == "true")
                {
                    pnlMasterTitles.Visible = true;
                    pnlMasterNodes.Visible = true;
                    lnkPurchaseRequest.Visible = true;
                }
            if (null != Session["reciver"])
                if (Session["reciver"].ToString() == "True")
                {
                    lnkDelivery.Visible = true;
                    
                }
            if (null != Session["inspector"])
                if (Session["inspector"].ToString() == "True")
                {
                    lnkDelivery.Visible = true;
                }
            if (null != Session["approval"])
                if (Session["approval"].ToString() == "True")
                {
                    lnkDelivery.Visible = true;
                }
            if (null != Session["assignmenter"])
                if (Session["assignmenter"].ToString() == "True")
                {
                    lnkAssignment.Visible = true;
                    lnkItemDetails.Visible = true;
                }
            if (null != Session["maintainance_manager"])
                if (Session["maintainance_manager"].ToString() == "True")
                {
                    lnkPreventive.Visible = true;
                }
            if (null != Session["reporter"])
                if (Session["reporter"].ToString() == "True")
                {
                    lnkReport.Visible = true;
                }
            if (null != Session["user_management"])
                if (Session["user_management"].ToString() == "True")
                {
                    pnlSystemSetting.Visible = true;
                    pnlSystemSetting2.Visible = true;
                }
            if (null != Session["Technician"])
                if (Session["Technician"].ToString() == "True")
                {
                    lnkPcTechArea.Visible = true;
                }
       

        }
        catch (Exception exp)
        {

        }
    }
    protected void mDepartmentLnkClicked(object sender, EventArgs e)
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
            Response.Redirect("~/department_master_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mRoomLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the room master page.
        /// Author: mutawakelm
        /// Date :2/14/2009 9:06:44 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/room_master_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mCategoryLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the category master page
        /// Author: mutawakelm
        /// Date :2/14/2009 9:08:40 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/category_master.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mVendorLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the vendor master page.
        /// Author: mutawakelm
        /// Date :2/14/2009 9:21:21 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/vendor_master_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mPurchaseRequestLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the new request page.
        /// Author: mutawakelm
        /// Date :2/14/2009 9:23:00 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            Response.Redirect("~/product_request_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mDeliveryLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the delivery page
        /// Author: mutawakelm
        /// Date :2/14/2009 9:24:32 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            Response.Redirect("~/delivery_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mAssignementLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the assignement page.
        /// Author: mutawakelm
        /// Date :2/14/2009 9:27:10 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/assignment_page.aspx");
        }
        catch (Exception exp)
        {

        }
    }
    protected void mPreventiveMain(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the preventive maintainance page.
        /// Author: mutawakelm
        /// Date :2/14/2009 9:28:42 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            Response.Redirect("~/maintainance_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mReportsLnkClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the reports page
        /// Author: mutawakelm
        /// Date :2/14/2009 9:31:13 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/reporting_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mUsersLnkClicked(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/user_management.aspx");
        }
        catch (Exception exp)
        {
        }
    }

    protected void mItemDetailsLnkClicked(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/product_modification_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mRequestorLnkClicked(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/requestor_master.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mpcTechLnkClicked(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/pctech_default_page.aspx");
        }
        catch (Exception exp)
        {
        }
    }
   
}
