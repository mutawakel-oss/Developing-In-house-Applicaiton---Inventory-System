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
            string strVendorsQuery = "SELECT id,department_name FROM t_department_master";
            int counter = 1;
            reader = GeneralClass.Program.gRetrieveRecord(strVendorsQuery);
            if (reader.HasRows)
            {
                txtSearchDepartment.Items.Clear();
                txtSearchDepartment.Items.Add("--Select A Department--");
                while (reader.Read())
                {
                    txtSearchDepartment.Items.Add(reader["department_name"].ToString());
                    txtSearchDepartment.Items[counter].Value = reader["id"].ToString();
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
            
            //The following code will be used to search about the information 
            if (!string.IsNullOrEmpty(txtSearchDepartment.SelectedItem.Text))
            {
                string strSearchQuery = "SELECT * from t_department_master WHERE department_name='" + txtSearchDepartment.SelectedItem.Text + "'";
                reader = GeneralClass.Program.gRetrieveRecord(strSearchQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    txtDepartmentName.Text = reader["department_name"].ToString();
                    txtDescription.Text = reader["description"].ToString();
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
            txtDepartmentName.Text = "";
            txtDescription.Text = "";
           
            
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
            bool existency_flag = false;
            string departmentID="";
            GeneralClass.Program.Add("department_name", txtDepartmentName.Text, "S");
            GeneralClass.Program.Add("description", txtDescription.Text, "S");
            for (int i = 1; i < txtSearchDepartment.Items.Count; i++)
            {
                if (txtDepartmentName.Text == txtSearchDepartment.Items[i].Text)
                {
                    existency_flag = true;
                    departmentID = txtSearchDepartment.Items[i].Value;
                }
            }
            if(existency_flag==true)
                GeneralClass.Program.UpdateRecordStatement("t_department_master","id",departmentID);
            else
                GeneralClass.Program.InsertRecordStatement("t_department_master");
            mClear();
            mFillVendorDDL();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
}
