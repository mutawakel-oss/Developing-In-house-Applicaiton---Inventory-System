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
            if (Session.Count == 0)
            {
                Response.Redirect("error_page.aspx?error=Session Expired");
            }
            if (!IsPostBack)
            {
                mFillVendorDDL();
            }
            MyAccordion.Width = 800;
            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkHome");
            HyperLink LB = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            LB.Text = "Log Out";
            Hlk.NavigateUrl = "~/admin_default_page.aspx";
        }
        catch (Exception exp)
        {
            
        }
    }
    protected void mFillVendorDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the drop down list of the vendors
        /// Author: mutawakelm
        /// Date :2/2/2009 3:55:52 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strVendorsQuery = "SELECT id,supplier_name,dept_id FROM t_vendor WHERE dept_id=" + Session["departmentId"].ToString();
            int counter = 1;
            reader = GeneralClass.Program.gRetrieveRecord(strVendorsQuery);
            if (reader.HasRows)
            {
                txtSearchVendor.Items.Clear();
                txtSearchVendor.Items.Add("--Select A Vendor--");
                while (reader.Read())
                {
                    txtSearchVendor.Items.Add(reader["supplier_name"].ToString());
                    txtSearchVendor.Items[counter].Value = reader["id"].ToString();
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSearchSelected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to search about a spr information
        /// and display the data on the fields
        /// Author: mutawakelm
        /// Date :10/08/2008 03:13:50 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strVendorName = "";//This variable will hold the name of the vendor
            //The following code will be used to search about the information 
            if (!string.IsNullOrEmpty(txtSearchVendor.SelectedItem.Text))
            {
                string strSearchQuery = "SELECT * from t_vendor WHERE supplier_name='" + txtSearchVendor.SelectedItem.Text + "'";
                reader = GeneralClass.Program.gRetrieveRecord(strSearchQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    txtVendorName.Text = reader["supplier_name"].ToString();
                    txtContactName.Text = reader["contact_name"].ToString();
                    txtContactTitle.Text = reader["contact_title"].ToString();
                    txtVendorAddress.Text = reader["address"].ToString();
                    txtVendorCity.Text = reader["city"].ToString();
                    txtTelephone1.Text = reader["telephone1"].ToString();
                    txtTelephone2.Text = reader["telephone2"].ToString();
                    txtMobile.Text = reader["mobile"].ToString();
                    txtFax.Text = reader["fax"].ToString();
                    reader.Close();
                    MyAccordion.SelectedIndex = 1;
                }
                else
                {
                    reader.Close();
                    mClear();
                }

            }
            
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mBtnClearClick(object sneder, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the function "mClear" which is responsible to clear the fields of the page
        /// Author: mutawakelm
        /// Date :12/08/2008 03:51:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mClear();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mClear()
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to clear all the fields of the 
        /// page to start a new request
        /// Author: mutawakelm
        /// Date :10/08/2008 04:38:11 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
       
        try
        {
            txtVendorName.Text = "";
            txtContactName.Text = "";
            txtContactTitle.Text = "";
            txtVendorAddress.Text = "";
            txtVendorCity.Text = "";
            txtTelephone1.Text = "";
            txtTelephone2.Text = "";
            txtMobile.Text = "";
            txtFax.Text = "";
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mBtnSubmitClick(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to add a new vnedor to the system
        /// Author: mutawakelm
        /// Date :13/09/2008 11:27:22 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("supplier_name", txtVendorName.Text, "S");
            GeneralClass.Program.Add("contact_name", txtContactName.Text, "S");
            GeneralClass.Program.Add("contact_title", txtContactTitle.Text, "S");
            GeneralClass.Program.Add("address", txtVendorAddress.Text, "S");
            GeneralClass.Program.Add("city", txtVendorCity.Text, "S");
            GeneralClass.Program.Add("telephone1", txtTelephone1.Text, "S");
            GeneralClass.Program.Add("telephone2",txtTelephone2.Text, "S");
            GeneralClass.Program.Add("mobile", txtMobile.Text, "S");
            GeneralClass.Program.Add("fax", txtFax.Text, "S");
            GeneralClass.Program.Add("dept_id", Session["departmentId"].ToString(), "I");
            GeneralClass.Program.InsertRecordStatement("t_vendor");
            mClear();
            mFillVendorDDL();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
}
