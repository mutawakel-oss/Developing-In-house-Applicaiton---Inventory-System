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
                mFillRoomDDL();
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
    protected void mFillRoomDDL()
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
            string strVendorsQuery = "SELECT id,storage_place FROM t_storage_place WHERE dept_id=" + Session["departmentId"].ToString();
            int counter = 1;
            reader = GeneralClass.Program.gRetrieveRecord(strVendorsQuery);
            if (reader.HasRows)
            {
                txtSearchRoom.Items.Clear();
                txtSearchRoom.Items.Add("--Select A Room--");
                while (reader.Read())
                {
                    txtSearchRoom.Items.Add(reader["storage_place"].ToString());
                    txtSearchRoom.Items[counter].Value = reader["id"].ToString();
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
            if (!string.IsNullOrEmpty(txtSearchRoom.SelectedItem.Text))
            {
                string strSearchQuery = "SELECT * from t_storage_place WHERE storage_place='" + txtSearchRoom.SelectedItem.Text + "'";
                reader = GeneralClass.Program.gRetrieveRecord(strSearchQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    txtRoomName.Text = reader["storage_place"].ToString();
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
            txtRoomName.Text = "";
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
            GeneralClass.Program.Add("storage_place", txtRoomName.Text, "S");
            GeneralClass.Program.Add("description", txtDescription.Text, "S");
            GeneralClass.Program.Add("dept_id", Session["departmentId"].ToString(), "I");
            for (int i = 1; i < txtSearchRoom.Items.Count; i++)
            {
                if (txtRoomName.Text == txtSearchRoom.Items[i].Text)
                {
                    existency_flag = true;
                    departmentID = txtSearchRoom.Items[i].Value;
                }
            }
            if(existency_flag==true)
                GeneralClass.Program.UpdateRecordStatement("t_storage_place", "id", departmentID);
            else
                GeneralClass.Program.InsertRecordStatement("t_storage_place");
            mClear();
            mFillRoomDDL();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
}
